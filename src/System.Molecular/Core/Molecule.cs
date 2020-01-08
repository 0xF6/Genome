using System.Collections.Generic;
using System.Linq;
using System.Molecular.Core.@internal;

namespace System.Molecular.Core
{
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
}