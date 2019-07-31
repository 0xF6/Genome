namespace System.Collections.Sequence
{
    using Diagnostics;
    using Generic;
    using Reflection;
    using Runtime.InteropServices;
    using Security;

    internal static class Native
    {
        [SecurityCritical]
        public static T FromBytesUnmanaged<T>(byte[] arr) where T : unmanaged  
        {
            var str  = new T();
            var size = Marshal.SizeOf(str);
            var ptr  = Marshal.AllocHGlobal(size);

            Marshal.Copy(arr, 0, ptr, size);
            str = (T)Marshal.PtrToStructure(ptr, str.GetType());
            Marshal.FreeHGlobal(ptr);
            return str;
        }
        [SecurityCritical] // for calling in reflection 'FromBytesUnmanaged', 'MarshalFrom', 'MarshalTo'
        public static T FromBytes<T>(byte[] arr) where T : struct
        {
            if (typeof(T).GetCustomAttribute(typeof(CustomMarshalAspectAttribute)) == null)
            {
                var method = typeof(Native).GetMethod("FromBytesUnmanaged", BindingFlags.Static);
                Debug.Assert(method != null, $"{nameof(method)} != null");
                method.MakeGenericMethod(typeof(T));
                return (T)method.Invoke(null, new object[]{ arr });
            }
            else
            {
                var method = typeof(T).GetMethod("MarshalFrom", BindingFlags.Static);
                Debug.Assert(method != null, $"{nameof(method)} != null");
                Debug.Assert(method.IsSecurityCritical, $"!{{{typeof(T).Name}.MarshalFrom}}.IsSecurityCritical");
                Debug.Assert(method.ReturnType == typeof(T), "method.ReturnType != typeof(T)");
                return (T)method.Invoke(null, new object[] {arr});
            }
        }


        public static byte[] ReadBytes(IntPtr ptr, int len, ref int offset)
        {
            var bytes = new List<byte>();
            while (len > offset)
                bytes.Add(Marshal.ReadByte(ptr + offset++));
            return bytes.ToArray();
        }
    }
    [AttributeUsage(AttributeTargets.Struct)]
    internal class CustomMarshalAspectAttribute : Attribute { }
}