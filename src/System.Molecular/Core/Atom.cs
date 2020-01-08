namespace System.Molecular.Core
{
    using Linq;
    using @internal;
    using Numerics;

    [Serializable]
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
}