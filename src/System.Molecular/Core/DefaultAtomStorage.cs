namespace System.Molecular.Core
{
    using Collections;
    using Collections.Generic;
    using Linq;
    using Numerics;

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
        public int Add(string label, Vector3 position = default) 
            => Add(Atom.GetAtomicIndex(label));
        public int Add(string label, Vector2 position) 
            => Add(label, new Vector3(position, 0));
        public int Add(Vector3 vector)
            => Add(6, vector);

        
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

            var v1 = Vector3.Subtract(c2, c1);
            var v2 = Vector3.Subtract(c3, c2);
            var v3 = Vector3.Subtract(c4, c3);
            
            var n1 = Vector3.Cross(v1, v2);
            var n2 = Vector3.Cross(v2, v3);

            return -MathF.Atan2(v2.Length() * Vector3.Dot(v1, n2), Vector3.Dot(n1, n2));
        }

        public Vector3 Center 
            => atoms.Where(x => x != null).Aggregate(Vector3.Zero, (c, v) => c + v.Position);

        public IEnumerator<Atom> GetEnumerator() => atoms.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}