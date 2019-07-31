namespace System.Collections.Sequence
{
    using Generic;
    using JetBrains.Annotations;
    using Linq;
    using Runtime.InteropServices;
    using Security;

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

            public static explicit operator int(Sequence e) => 1;

            [SecurityCritical, UsedImplicitly]
            internal static Sequence MarshalFrom(byte[] arr)
            {
                if(arr.Length < 6)
                    throw new ArgumentException($"byte array less 6", nameof(arr));

                static int[] readInt32Array(IntPtr ptr, int len, ref int offset)
                {
                    var _offset = offset; // fuck cannot use ref inside lambda expression
                    var result = Enumerable.Range(0, len)
                        .Select(_ => (_offset += sizeof(int))).Select(ofs => Marshal.ReadInt32(ptr + ofs)).ToArray();
                    offset = _offset;
                    return result;
                }

                var seq    = new Sequence();
                var offset = 0;
                var ptr    = Marshal.AllocHGlobal(arr.Length);
                Marshal.Copy(arr, 0, ptr, arr.Length);

                seq.DNASize = Marshal.ReadInt32(ptr + offset);
                offset += sizeof(int);
                seq.BlockCount = Marshal.ReadInt32(ptr + offset);
                offset += sizeof(int);
                seq.BlockStarts = readInt32Array(ptr, seq.BlockCount, ref offset);
                seq.BlockSizes = readInt32Array(ptr, seq.BlockCount, ref offset);
                Marshal.FreeHGlobal(ptr);
                return seq;
            }
        }
    }
}