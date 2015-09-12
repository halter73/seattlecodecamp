using System;
using System.Diagnostics;
using System.IO;

namespace LibGdNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(args.Length == 2);

            byte[] srcPngBytes = File.ReadAllBytes(args[0]);

            File.WriteAllBytes(args[1], PngThumbnailer.CreateThumbnail(srcPngBytes));

            Console.WriteLine("Done!");
        }
    }
}
