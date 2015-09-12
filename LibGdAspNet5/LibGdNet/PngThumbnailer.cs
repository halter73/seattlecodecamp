using System;
using System.Runtime.InteropServices;

namespace LibGdNet
{
    public static class PngThumbnailer
    {
        public static byte[] CreateThumbnail(byte[] fullImg)
        {
            GCHandle pngBytesPtr = GCHandle.Alloc(fullImg, GCHandleType.Pinned);

            try
            {
                using (GdImageHandle gdSrcImage = LibGd.gdImageCreateFromPngPtr(fullImg.Length, pngBytesPtr.AddrOfPinnedObject()))
                using (GdImageHandle gdDstImage = LibGd.gdImageCreate(128, 128))
                {
                    GdImage srcImage = gdSrcImage.Image;
                    LibGd.gdImageCopyResized(gdDstImage, gdSrcImage, 0, 0, 0, 0,
                        128, 128,
                        srcImage.sx, srcImage.sy);

                    int numDstBytes;
                    IntPtr dstPngPtr = LibGd.gdImagePngPtr(gdDstImage, out numDstBytes);
                    byte[] dstPngBytes = new byte[numDstBytes];
                    Marshal.Copy(dstPngPtr, dstPngBytes, 0, numDstBytes);

                    return dstPngBytes;
                }
            }
            finally
            {
                pngBytesPtr.Free();
            }
        }
    }
}
