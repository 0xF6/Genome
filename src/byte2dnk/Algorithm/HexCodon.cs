namespace byte2dnk.Algorithm
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Molecular.Extra;
    using MoreLinq;

    public static class HexCodon
    {
        public static readonly List<(string codon, string hex)> map;

        static HexCodon()
        {
            map = GetPermutations(new[] {"A", "C", "G", "U"}, 3)
                .Select(x => new Codon(x))
                .Where(x => !x.IsStop())
                .Where(x => !x.IsProteinInitiator())
                .Take(16)
                .Select(x => x.Value)
                .Select((codon, i) => (codon, $"{i:X}"))
                .ToList();
        }

        public static Codon[] Encode(byte[] arr) =>
            arr.SelectMany(x => new[] {(byte) ((x & 0xF0) >> 4), (byte) ((x & 0x0F) >> 0)})
                .Select(x => map.First(z => z.hex.Equals($"{x:X}")).codon)
                .Select(x => new Codon(x))
                .ToArray();


        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}