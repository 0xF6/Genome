namespace System.Molecular.Extra
{
    using Acid;
    using Collections.Generic;
    using Diagnostics;
    using JetBrains.Annotations;
    using Linq;

    [PublicAPI, DebuggerDisplay("{ToFullString()}")]
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

        public Codon(IEnumerable<char> code) : this(string.Join("", code.ToArray())) { }

        public Codon(string code)
        {
            static char _validate(char s)
            {
                if (s is 'A' || s is 'C' || s is 'G' || s is 'U')
                    return s;
                throw new ArgumentException($"Invalid symbol '{s}' in codon.");
            }

            Code = code.ToUpperInvariant().Select(_validate).ToArray();
            Acid = AminoAcid.Translate(code);
        }


        #region Extra

        public string Value => string.Join("", Code);

        #endregion

        #region Overrides of Object

        public override string ToString() => $"{Value}";
        public string ToFullString() => $"{Value} {Acid}";

        #endregion
    }
}