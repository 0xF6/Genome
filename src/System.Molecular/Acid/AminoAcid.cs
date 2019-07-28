namespace System.Molecular.Acid
{
    using Amino;
    using Linq;

    public abstract class AminoAcid
    {
        public string FullName => this.GetType().Name;

        public string Formula   { get; protected set; }
        public string ShortName { get; protected set; }
        public char   Symbol    { get; protected set; }
        public AAS    State     { get; protected set; }

        public virtual AminoAcidIdentifiers Identifiers { get; } = default;

        

        public static AminoAcid By(string code) => By(code.ToArray());

        public static AminoAcid By(params char[] code)
        {
            if(code.Length != 3) 
                throw new ArgumentException($"amino-acid code must be 3 characters.");

            switch (code[0], code[1], code[2])
            {
                case ('U', 'U', 'U'): case ('U', 'U', 'C'):
                    return new Phenylalanine();
                case ('U', 'U', 'A'): case ('U', 'U', 'G'):
                case ('C', 'U', 'U'): case ('C', 'U', 'C'):
                case ('C', 'U', 'A'): case ('C', 'U', 'G'):
                    return new Leucine();
                case ('A', 'U', 'U'): case ('A', 'U', 'C'):
                case ('A', 'U', 'A'):
                    return new Isoleucine();
            }
            return new UnknownAcid();
        }


        protected void Set(string formula, string @short, char sym, AAS state)
        {
            this.Formula = formula;
            this.ShortName = @short;
            this.Symbol = sym;
            this.State = state;
        }
    }
    /// <summary>
    /// Amino acids biochemical properties
    /// </summary>
    public enum AAS
    {
        NonPolar,
        Polar,
        Basic,
        Acidic,
        Terminator
    }
    public struct AminoAcidIdentifiers
    {
        /// <summary>
        /// Chemical Abstracts Service Registry Number
        /// </summary>
        public string CASNumber { get; set; }
        /// <summary>
        /// Chemical Entities of Biological Interest
        /// </summary>
        public string ChEBI { get; set; }

        /// <summary>
        /// Unique Ingredient Identifier
        /// </summary>
        public string UNII { get; set; }
        /// <summary>
        /// Simplified molecular-input line-entry system
        /// </summary>
        public string SMILES { get; set; } // TODO create library for working and parsing SMILES
    }
}