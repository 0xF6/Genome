namespace System.Molecular
{
    using Acid;
    using Collections.Generic;
    using Extra;
    using JetBrains.Annotations;
    using Linq;
    using MoreLinq.Extensions;

    [PublicAPI]
    public class RNA : NucleicAcid
    {
        public RNA(IEnumerable<Codon> codons) : base(codons) { }

        public static RNA Transcribe(string code) => new RNA(DNA_TO_RNA(code).Batch(3).Select(x => new Codon(x)));
    }
}
