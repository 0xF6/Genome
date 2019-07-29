namespace System.Molecular.Acid.Amino
{
    public class Stop : AminoAcid
    {
        public Stop(string Name) => Set("<none>" /* ??? */, Name, 'S', AAS.Terminator);
    }

    public class Opal : Stop
    {
        public Opal() : base(nameof(Opal)) { }
    }
    public class Orche : Stop
    {
        public Orche() : base(nameof(Orche)) { }
    }
    public class Amber : Stop
    {
        public Amber() : base(nameof(Amber)) { }
    }
}