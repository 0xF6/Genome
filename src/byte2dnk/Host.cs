namespace byte2dnk
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using Algorithm;
    using Pastel;
    using static Log;
    internal static class Host
    {
        public static void Main(string[] args)
        {
            //if(!args.Any())
            //    Error($"No file specified.", 1);
            StringToCodons();
        }


        public static void StringToCodons()
        {
            var str = "Влад Гей";

            Console.OutputEncoding = Encoding.Unicode;

            Info($"String: '{str.Pastel(Color.Gray)}', Length: {str.Length}");
            var asw = HexCodon.Encode(Encoding.UTF8.GetBytes(str)).Select(x =>
                $"{x.Acid.Symbol.ToString().Pastel(getColorByAminoAcidsState(x.Acid.State))}");
            Info($"acid map: {string.Join(" ", asw)}");
            Info($"");
            Info($"Detail table:");

            foreach (var codon in HexCodon.Encode(Encoding.UTF8.GetBytes(str)))
                Info($"\t{codon.Value.Pastel(getColorByAminoAcidsState(codon.Acid.State))} {codon.Acid.FullName}/{codon.Acid.Symbol}");
            Console.WriteLine();
        }
    }
}
