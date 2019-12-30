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
            Atoms = new DefaultAtomStorage();
        }
    }

    public static class Atom
    {
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
    }

    public interface IAtomStorage
    {
        int Add(float x, float y, float z);

        int Add(float x, float y);

        int Add(Vector3 vector);

        int Add(Vector2 vector);
    }

    internal class DefaultAtomStorage : IAtomStorage
    {
    }
}