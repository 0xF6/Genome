namespace System.Collections.Sequence
{
    using JetBrains.Annotations;
    using Runtime.InteropServices;
    using Security;

    public partial class F2Bit
    {
        [CustomMarshalAspect, StructLayout(LayoutKind.Sequential)]
        public struct Index
        {
            public byte[] Name; /* ^nameSize 1 bytes, ^Name 1*(nameSize) bytes */
            public int Offset;

            [SecurityCritical, UsedImplicitly]
            internal static Index MarshalFrom(byte[] arr)
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
                Marshal.FreeHGlobal(ptr);
                return idx;
            }
        }
    }
}