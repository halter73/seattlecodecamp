using System;
using System.Runtime.InteropServices;

namespace LibGdAspNet5
{
    public class PlatformApis
    {

        public static bool IsWindows()
        {
#if DNXCORE50
            // Until Environment.OSVersion.Platform is exposed on .NET Core, we
            // try to call uname and if that fails we assume we are on Windows.
            return GetUname() == string.Empty;
#else
            var p = (int)Environment.OSVersion.Platform;
            return (p != 4) && (p != 6) && (p != 128);
#endif
        }

        static string GetUname()
        {
            var buffer = new byte[8192];
            var gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                IntPtr buf = gcHandle.AddrOfPinnedObject();

                if (uname(buf) == 0)
                {
                    return Marshal.PtrToStringAnsi(buf);
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                gcHandle.Free();
            }
        }

        [DllImport("libc")]
        static extern int uname(IntPtr buf);
    }
}
