using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibGdNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(args.Length == 2);

            byte[] srcPngBytes = File.ReadAllBytes(args[0]);
            GCHandle pngBytesPtr = GCHandle.Alloc(srcPngBytes, GCHandleType.Pinned);

            IntPtr gdSrcImage = LibGd.gdImageCreateFromPngPtr(srcPngBytes.Length, pngBytesPtr.AddrOfPinnedObject());
            var srcImage = Marshal.PtrToStructure<GdImage>(gdSrcImage);

            IntPtr gdDstImage = LibGd.gdImageCreate(256, 256);

            LibGd.gdImageCopyResized(gdDstImage, gdSrcImage, 0, 0, 0, 0,
                256, 128,
                srcImage.sx, srcImage.sy);

            int numDstBytes = 0;
            IntPtr dstPngPtr = LibGd.gdImagePngPtr(gdDstImage, ref numDstBytes);
            byte[] dstPngBytes = new byte[numDstBytes];
            Marshal.Copy(dstPngPtr, dstPngBytes, 0, numDstBytes);

            File.WriteAllBytes(args[1], dstPngBytes);

            Console.WriteLine(gdSrcImage);
        }
    }
}
