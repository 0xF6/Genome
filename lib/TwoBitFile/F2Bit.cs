namespace System.Collections.Sequence
{
    using Generic;
    using IO;
    using JetBrains.Annotations;
    using Linq;
    using Runtime.InteropServices;
    using Threading.Tasks;

    [PublicAPI]
    public partial class F2Bit
    {
        public Head Header;
        public List<Index> Indexes = new List<Index>();
        public List<Sequence> Sequences = new List<Sequence>();



        public static F2Bit LoadFrom(string file) 
            => LoadFrom(File.OpenRead(file));

        public static F2Bit LoadFrom(Stream stream) 
            => LoadFromAsync(stream)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        public static async Task<F2Bit> LoadFromAsync(Stream stream)
        {
            var buffer = new byte[1024 * 4];
            using var ms = new MemoryStream();
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                await ms.WriteAsync(buffer, 0, read);
            return Load(ms.ToArray());
        }


        public static unsafe F2Bit Load(byte[] array)
        {
            var file = new F2Bit();
            file.Header = Native.FromBytes<Head>(array.Take(sizeof(Head)).ToArray());
            UnmanagedMemoryStream a = default;
            fixed (byte* p = &array[16])
                a = new UnmanagedMemoryStream(p, array.Length - 16);

            for (var i = 0; i < file.Header.SeqCount; i++)
                file.Indexes.Add(Index.MarshalFrom(a));
            for (var i = 0; i < file.Header.SeqCount; i++)
                file.Sequences.Add(Sequence.MarshalFrom(a));

            return null;
        }
    }

    internal static class Ex
    {
        public static int ReadInt32(this UnmanagedMemoryStream stream)
        {
            var bytes = new byte[4];
            for (var i = 0; i < 4; i++)
                bytes[i] = (byte)stream.ReadByte();
            return BitConverter.ToInt32(bytes, 0);
        }
        public static byte[] ReadBytes(this UnmanagedMemoryStream stream, int len)
        {
            var bytes = new byte[len];
            for (var i = 0; i < len; i++)
                bytes[i] = (byte)stream.ReadByte();
            return bytes;
        }
    }
}
