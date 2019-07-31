namespace System.Collections.Sequence
{
    using Runtime.InteropServices;

    public partial class F2Bit
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Head
        {
            public int Signature;
            public int Version;
            public int SeqCount;
            public int Reserved;
        }
    }
}