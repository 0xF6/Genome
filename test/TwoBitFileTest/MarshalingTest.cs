namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Sequence;
    using System.Text;
    using NUnit.Framework;

    public class MarshalingTest
    {

        [Test]
        public void IndexTest()
        {
            var arr = new List<byte>();

            arr.Add((byte) Encoding.ASCII.GetByteCount("test_name"));
            arr.AddRange(Encoding.ASCII.GetBytes("test_name"));
            arr.AddRange(BitConverter.GetBytes(493));

            var result = F2Bit.Index.MarshalFrom(arr.ToArray());
            Assert.AreEqual("test_name", Encoding.ASCII.GetString(result.Name));
            Assert.AreEqual(493, result.Offset);
        }
    }
}