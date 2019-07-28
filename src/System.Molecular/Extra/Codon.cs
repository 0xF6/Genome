namespace System.Molecular.Extra
{
    using Acid;
    using JetBrains.Annotations;
    using Linq;

    [PublicAPI]
    public sealed class Codon
    {
        public bool IsProteinInitiator() 
            => Value is "AUG";
        public bool IsStop() 
            => Value is "UAG" || Value is "UGA" || Value is "UAA";
        /// <summary>
        /// Thrid symbols of codon
        /// </summary>
        public char[] Code { get; }
        /// <summary>
        /// Acid of this codon
        /// </summary>
        public AminoAcid Acid { get; }

        public Codon(string code)
        {
            static char _validate(char s)
            {
                if (s is 'A' || s is 'C' || s is 'G' || s is 'U')
                    return s;
                throw new ArgumentException($"Invalid symbol '{s}' in codon.");
            }

            Code = code.ToUpperInvariant().Select(_validate).ToArray();
            Acid = AminoAcid.By(code);
        }


        #region Extra

        public string Value => string.Join("", Code);

        #endregion
    }
}