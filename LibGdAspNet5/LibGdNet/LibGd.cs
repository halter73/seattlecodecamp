using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LibGdNet
{
    public static class LibGd
    {
        private static string[] _dllNames = new[] { "zlib1.dll", "libpng13.dll", "jpeg62.dll", "libiconv2.dll", "freetype6.dll", "libgd2.dll" };

        public static void LoadWindows(string libDirectory)
        {
            foreach (var dll in _dllNames)
            {
                var dllPath = Path.Combine(libDirectory, dll);
                if (LoadLibrary(dllPath) == IntPtr.Zero)
                {
                    throw new Exception($"Failed to load {dllPath}!");
                }
            }
        }

        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("libgd2", EntryPoint = "gdImageCreateFromPngPtr@8")]
        public static extern GdImageHandle gdImageCreateFromPngPtr(int bufSize, IntPtr buffer);

        [DllImport("libgd2", EntryPoint = "gdImagePngPtr@8")]
        public static extern IntPtr gdImagePngPtr(GdImageHandle im, out int size);

        [DllImport("libgd2", EntryPoint = "gdImageCreate@8")]
        public static extern GdImageHandle gdImageCreate(int sx, int sy);

        [DllImport("libgd2", EntryPoint = "gdFree@4")]
        public static extern void gdFree(IntPtr im);

        [DllImport("libgd2", EntryPoint = "gdImageCopyResized@40")]
        public static extern void gdImageCopyResized(
               GdImageHandle dst, GdImageHandle src, int dstX, int dstY,
               int srcX, int srcY, int dstW, int dstH, int srcW,
               int srcH);
    }
}
