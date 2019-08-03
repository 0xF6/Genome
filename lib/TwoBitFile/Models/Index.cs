namespace System.Collections.Sequence
{
    using IO;
    using JetBrains.Annotations;
    using Runtime.InteropServices;
    using Security;
    using Text;

    public partial class F2Bit
    {
        [CustomMarshalAspect, StructLayout(LayoutKind.Sequential)]
        public struct Index
        {
            public byte[] Name; /* ^nameSize 1 bytes, ^Name 1*(nameSize) bytes */
            public int Offset;

            public string FinalName => Encoding.ASCII.GetString(Name);

            internal static Index MarshalFrom(UnmanagedMemoryStream stream)
            {
                var idx    = new Index();
                var len = stream.ReadByte();
                idx.Name   = Native.ReadBytes(stream, len);
                idx.Offset = stream.ReadInt32();
                return idx;
            }

            [SecurityCritical, UsedImplicitly]
            internal static Index MarshalFrom(byte[] arr, out int bytesRead)
            {
                if(arr.Length < 6)
                    throw new ArgumentException($"byte array less 6", nameof(arr));
                var idx    = new Index();
                var offset = 0;
                var ptr    = Marshal.AllocHGlobal(arr.Length);
                Marshal.Copy(arr, 0, ptr, arr.Length);
                var len    = Marshal.ReadByte(ptr + (offset++));
                idx.Name   = Native.ReadBytes(ptr, len + offset, ref offset);
                idx.Offset = Marshal.ReadInt32(ptr + offset);
                offset += 4;
                Marshal.FreeHGlobal(ptr);
                bytesRead = offset;
                return idx;
            }
        }
    }
}