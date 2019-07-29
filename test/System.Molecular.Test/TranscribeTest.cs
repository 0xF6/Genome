namespace Tests
{
    using System.Molecular;
    using NUnit.Framework;

    public class TranscribeTest
    {
        [Test]
        public void FirstTest()
        {
            var code = "TAACCCTAACCCTAACCCTAACCCTAACCCTAACCCTAACCCTAACCCTAACCCTAACCCTAACCCTAACCC";
            var dna = RNA.Transcribe(code);
            Assert.AreEqual(code.Length / 3, dna.Codons.Length);
        }
    }
}