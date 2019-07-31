namespace System.Collections.Sequence
{
    using IO;
    using JetBrains.Annotations;
    using Threading.Tasks;

    [PublicAPI]
    public partial class F2Bit
    {
        public static F2Bit LoadFrom(string file) 
            => LoadFrom(File.OpenRead(file));

        public static F2Bit LoadFrom(Stream stream) 
            => LoadFromAsync(stream)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        public static async Task<F2Bit> LoadFromAsync(Stream stream)
        {
            var buffer = new byte[1024];
            using var ms = new MemoryStream();
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                await ms.WriteAsync(buffer, 0, read);
            return Load(ms.ToArray());
        }


        public static F2Bit Load(byte[] array)
        {
            return null;
        }
    }
}
