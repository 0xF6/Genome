namespace System.Molecular
{
    using Acid;
    using Collections.Generic;
    using Extra;

    public class DNA : NucleicAcid
    {
        public DNA(IEnumerable<Codon> codons) : base(codons) { }
    }
}