using System;
using System.Runtime.InteropServices;

namespace LibGdNet
{
    public static class LibGd
    {
        [DllImport("libgd2.dll", EntryPoint = "gdImageCreateFromPngPtr@8")]
        public static extern GdImageHandle gdImageCreateFromPngPtr(int bufSize, IntPtr buffer);

        [DllImport("libgd2.dll", EntryPoint = "gdImagePngPtr@8")]
        public static extern IntPtr gdImagePngPtr(GdImageHandle im, out int size);

        [DllImport("libgd2.dll", EntryPoint = "gdImageCreate@8")]
        public static extern GdImageHandle gdImageCreate(int sx, int sy);

        [DllImport("libgd2.dll", EntryPoint = "gdFree@4")]
        public static extern void gdFree(IntPtr im);

        [DllImport("libgd2.dll", EntryPoint = "gdImageCopyResized@40")]
        public static extern void gdImageCopyResized(
               GdImageHandle dst, GdImageHandle src, int dstX, int dstY,
               int srcX, int srcY, int dstW, int dstH, int srcW,
               int srcH);
    }
}
