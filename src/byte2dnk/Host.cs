namespace byte2dnk
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Metadata;
    using System.Text;
    using Algorithm;
    using MoreLinq;
    using Newtonsoft.Json;
    using Pastel;
    using static Log;
    internal static class Host
    {
        public static void Main(string[] args)
        {
            //if(!args.Any())
            //    Error($"No file specified.", 1);
            Console.OutputEncoding = Encoding.Unicode;
            StringToCodons();
        }

        public static void ImageToCodons()
        {
            var bytes = File.ReadAllBytes(@"C:\Users\io\Pictures\4nDEjdHS2-c.jpg");

            //var asw = HexCodon.Encode(bytes).Select(x =>
            //    $"{x.Acid.Symbol.ToString().Pastel(getColorByAminoAcidsState(x.Acid.State))}");
           // Info($"acid map: {string.Join(" ", asw)}");
            Info(HexCodon.Encode(bytes).Length.ToString());
            //var res = string.Join("\n", string.Join("", HexCodon.Encode(bytes).SelectMany(x => x.Value)).Batch(3 * 25).Select(x => string.Join("", x)));
            //File.WriteAllText("./image.codons4", res);
        }


        public static void StringToCodons()
        {
            var str = "Ты пошел нахуй";
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
