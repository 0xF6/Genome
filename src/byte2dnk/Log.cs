namespace byte2dnk
{
    using System;
    using System.Drawing;
    using System.Molecular.Acid;
    using Pastel;
    using static System.Console;

    internal static class Log
    {
        public static void Error(string msg, int? exitCode)
        {
            WriteLine($"[{"b2d".Pastel(Color.Coral)}][{"ERR".Pastel(Color.Red)}]: {msg}");
            if(exitCode is null)
                return;
            Environment.Exit(exitCode.Value);
        }
        public static void Warn(string msg)
        {
            WriteLine($"[{"b2d".Pastel(Color.Coral)}][{"WRN".Pastel(Color.Orange)}]: {msg}");
        }
        public static void Info(string msg)
        {
            WriteLine($"[{"b2d".Pastel(Color.Coral)}]: {msg}");
        }

        public static Color getColorByAminoAcidsState(AAS state) =>
            state switch
            {
                AAS.Acidic      => Color.MediumPurple,
                AAS.Basic       => Color.CornflowerBlue,
                AAS.NonPolar    => Color.Yellow,
                AAS.Polar       => Color.MediumSeaGreen,
                AAS.Terminator  => Color.DarkRed,
                _               => Color.White,
            };
    }
}