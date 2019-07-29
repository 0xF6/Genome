namespace System.Molecular.Acid
{
    using Collections.Generic;
    using Diagnostics;
    using Extra;
    using JetBrains.Annotations;
    using Linq;

    [PublicAPI, DebuggerDisplay("{ToString()}")]
    public class NucleicAcid
    {
        public Codon[] Codons { get; set; }

        public NucleicAcid(IEnumerable<Codon> codons) => Codons = codons.ToArray();

        #region Overrides of Object

        public override string ToString() 
            => string.Join(" ", Codons.Select(x => x.ToString()));

        #endregion

        internal static string RNA_TO_DNA(string code) => code.ToUpperInvariant()
            .Replace("U", "T").Replace("\r", "").Replace("\n", "");
        internal static string DNA_TO_RNA(string code) => code.ToUpperInvariant()
            .Replace("T", "U").Replace("\r", "").Replace("\n", "");
    }
}