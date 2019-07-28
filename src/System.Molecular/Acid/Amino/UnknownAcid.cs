namespace System.Molecular.Acid.Amino
{
    public class UnknownAcid : AminoAcid
    {
        public UnknownAcid() => Set("<none>", "Unk", 'U', AAS.Basic);
    }
}