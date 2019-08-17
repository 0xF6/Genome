namespace System.Collections.Sequence
{
    using Generic;
    using IO;
    using JetBrains.Annotations;
    using Linq;
    using MoreLinq;
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

            public MemoryStream dnaData;

            public string this[Range range] 
                => Slice(range.Start.Value, range.End.Value);

            public string Slice(int at, int to)
            {
                dnaData.Seek(at / 2, SeekOrigin.Begin);
                return FromBytes(dnaData.ReadBytes(to / 2));
            }

            public static string FromBytes(byte[] a) => string.Join("", FromBitArray(new BitArray(a)));


            private static IEnumerable<char> FromBitArray(BitArray array)
            {
                var arrBools = array.Cast<bool>();
                var arrBytes = arrBools.Batch(2)
                    .Select(z => Map(z.First(), z.Last()));
                return arrBytes;
            }
            public static char Map(bool b1, bool b2) => (b1, b2) switch
            {
                (false, false) => 'T',
                (false, true ) => 'C',
                (true , false) => 'A',
                (true , true ) => 'G'
            };

            public static char Map(byte b) => b switch
            {
                0b00 => 'T',
                0b01 => 'C',
                0b10 => 'A',
                0b11 => 'G',
                   _ => 'N',
            };

            char byte_to_base(char b, int offset) {
                var rev_offset = 3 - offset;
                var mask = 3 << (rev_offset * 2);
                var idx = (b & mask) >> (rev_offset * 2);
                var bases = "TCAG".ToArray();
                return bases[idx];
            }

            [SecurityCritical, UsedImplicitly]
            internal static Sequence MarshalFrom(UnmanagedMemoryStream stream)
            {
                static int[] readInt32Array(UnmanagedMemoryStream ptr, int len)
                {
                    return Enumerable.Range(0, len).Select(ofs => ptr.ReadInt32()).ToArray();
                }

                var seq         = new Sequence();
                seq.DNASize     = stream.ReadInt32();
                seq.BlockCount  = stream.ReadInt32();
                seq.BlockStarts = readInt32Array(stream, seq.BlockCount);
                seq.BlockSizes  = readInt32Array(stream, seq.BlockCount);
                seq.MaskCount   = stream.ReadInt32();
                seq.MaskStarts  = readInt32Array(stream, seq.MaskCount);
                seq.MaskSizes   = readInt32Array(stream, seq.MaskCount);
                seq._           = stream.ReadInt32();
                seq.dnaData     = new MemoryStream();
                var pos = stream.Position;
                for (var i = 0; i < seq.BlockCount; i++)
                {
                    var size = seq.BlockSizes[i];
                    var offset = seq.BlockStarts[i];

                    stream.Seek(pos + offset, SeekOrigin.Begin);
                    seq.dnaData.Write(stream.ReadBytes(size % 4));
                }
                return seq;
            }
        }
    }
}