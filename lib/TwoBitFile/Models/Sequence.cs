namespace System.Collections.Sequence
{
    using Generic;
    using IO;
    using JetBrains.Annotations;
    using Linq;
    using Runtime.InteropServices;
    using Security;
    using Text;

    public partial class F2Bit
    {
        [CustomMarshalAspect, StructLayout(LayoutKind.Sequential)]
        public struct Sequence
        {
            public int DNASize;

            public int BlockCount;
            public int[] BlockStarts;
            public int[] BlockSizes;

            public int MaskCount;
            public int[] MaskStarts;
            public int[] MaskSizes;

            public int _;

            public byte[] DNA;

            public string Raw => Encoding.ASCII.GetString(DNA);

            [SecurityCritical, UsedImplicitly]
            internal static Sequence MarshalFrom(UnmanagedMemoryStream stream)
            {
                static int[] readInt32Array(UnmanagedMemoryStream ptr, int len)
                {
                    return Enumerable.Range(0, len).Select(ofs => ptr.ReadInt32()).ToArray();
                }

                var seq    = new Sequence();

                seq.DNASize = stream.ReadInt32();
                seq.BlockCount = stream.ReadInt32();
                seq.BlockStarts = readInt32Array(stream, seq.BlockCount);
                seq.BlockSizes = readInt32Array(stream, seq.BlockCount);

                seq.MaskCount = stream.ReadInt32();
                seq.MaskStarts = readInt32Array(stream, seq.MaskCount);
                seq.MaskSizes = readInt32Array(stream, seq.MaskCount);

                seq._ = stream.ReadInt32();
                seq.DNA = stream.ReadBytes(4 * seq.DNASize);
               
                return seq;
            }
        }
    }
}