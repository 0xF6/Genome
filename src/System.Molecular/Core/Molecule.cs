using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Molecular.Core.@internal;
using static System.Numerics.Vector3;

namespace System.Molecular.Core
{
    using Numerics;

    [Serializable]
    public class Molecule
    {
        protected int MaxAtoms { get; set; }
        protected int MaxBonds { get; set; }

        public int Color { get; protected set; }
        public string Name { get; protected set; }

        protected object Metadata { get; set; }

        protected IAtomStorage Atoms { get; private set; }

        public Molecule() : this(true)
            => MaxAtoms = MaxBonds = byte.MaxValue;

        public Molecule((int atoms, int bonds) max) : this(true)
        {
            var (atoms, bonds) = max;
            this.MaxBonds = bonds;
            this.MaxAtoms = atoms;
        }

        private Molecule(bool init)
        {
            if (!init) return;
            Atoms = new DefaultAtomStorage(MaxAtoms);
        }

        public float RelativeWeight() 
            => Weight(SD.RelativeMass);

        public float AbsoluteWeight() 
            => Weight(SD.AbsoluteMass);

        private float Weight(IReadOnlyList<float> tableOfMass)
            => Atoms.Aggregate(0f, (f, atom) => f + tableOfMass[atom.AtomicIndex]);
    }

    public class Atom
    {
        #region metadata

        public int AtomicIndex { get; internal set; }
        public int Charge { get; internal set; }
        public int Mass { get; internal set; }

        public Vector3 Position { get; internal set; }

        internal ulong Flags { get; set; }

        #endregion

        public Atom(int index) => this.AtomicIndex = index;

        [Flags]
        public enum Color
        {
            None = 0,
            Blue = 1 << 6,
            Red = 1 << 7,
            Green = Blue | Red,
            Magenta = 1 << 8,
            Orange = Blue | Magenta
        }

        [Flags]
        public enum Parity
        {
            None = 0,
            One = 1,
            Two = 2,
            Unknown = 3
        }
        [Flags]
        public enum Chirality
        {
            Unknown,
            NotChiral           = 0x10000,
            Meso                = 0x20000,
            Racemic             = 0x30000,
            KnownEnantiomer     = 0x40000,
            UnknownEnantiomer   = 0x50000,
            Epimers             = 0x60000,
            Diastereomers       = 0x70000
        }

        /// <summary>
        /// Get atom index by label
        /// </summary>
        public static int GetAtomicIndex(string atomLabel) =>
            SD.AtomLabels
                .Select((label, index) => (label, index))
                .FirstOrDefault(x => x.label.Equals(atomLabel, StringComparison.InvariantCultureIgnoreCase))
                .index;
    }

    public interface IAtomStorage : IEnumerable<Atom>
    {
        int Add(float x, float y, float z);

        int Add(float x, float y);

        int Add(Vector3 vector);

        int Add(Vector2 vector);


        int Count();

        Atom this[int index] { get; }
    }

    internal class DefaultAtomStorage : IAtomStorage
    {
        private Atom[] atoms { get; }


        public Atom this[int index] => atoms[index];

        public DefaultAtomStorage(int max) => atoms = new Atom[max];

        public int Count() => this.atoms.Length;

        public int Add(float x, float y, float z) 
            => Add(new Vector3(x, y, z));
        public int Add(float x, float y) 
            => Add(new Vector2(x, y));
        public int Add(Vector2 vector)
            => Add(new Vector3(vector, 0));
        public int Add(Vector3 vector)
            => Add(6, vector);
        public int Add(string label) 
            => Add(Atom.GetAtomicIndex(label));

        
        public int Add(int atomicIndex, Vector3 pos = default)
        {
            var len = atoms.Count(x => x != null);
            if (len == atoms.Length)
                throw new Exception($"too many"); // todo
            atoms[len] = new Atom(atomicIndex)
            {
                Position = pos
            };
            return len + 1;
        }
        /// <summary>
        /// </summary>
        /// <param name="atom">
        /// atom 4 valid atom indices defining a connected atom sequence
        /// </param>
        /// <returns>
        /// torsion in the range: -pi <= torsion <= pi
        /// </returns>
        public float GetTorsionIndex(int[] atom)
        {
            if (atom.Length != 4)
                throw new OverflowException("Need 4 valid atom.");

            var c1 = atoms[atom[0]].Position;
            var c2 = atoms[atom[1]].Position;
            var c3 = atoms[atom[2]].Position;
            var c4 = atoms[atom[3]].Position;

            var v1 = Subtract(c2, c1);
            var v2 = Subtract(c3, c2);
            var v3 = Subtract(c4, c3);
            
            var n1 = Cross(v1, v2);
            var n2 = Cross(v2, v3);

            return -MathF.Atan2(v2.Length() * Dot(v1, n2), Dot(n1, n2));
        }

        public Vector3 Center 
            => atoms.Where(x => x != null).Aggregate(Zero, (c, v) => c + v.Position);

        public IEnumerator<Atom> GetEnumerator() => atoms.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}