using System;
using System.Runtime.InteropServices;

namespace LibGdNet
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GdImage
    {
        private const int gdMaxColors = 256;

        /* Palette-based image pixels */
        IntPtr pixels;
        public int sx;
        public int sy;
        /* These are valid in palette images only. See also
           'alpha', which appears later in the structure to
           preserve binary backwards compatibility */
        int colorsTotal;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = gdMaxColors)]
        int[] red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = gdMaxColors)]
        int[] green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = gdMaxColors)]
        int[] blue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = gdMaxColors)]
        int[] open;
        /* For backwards compatibility, this is set to the
           first palette entry with 100% transparency,
           and is also set and reset by the 
           gdImageColorTransparent function. Newer
           applications can allocate palette entries
           with any desired level of transparency; however,
           bear in mind that many viewers, notably
           many web browsers, fail to implement
           full alpha channel for PNG and provide
           support for full opacity or transparency only. */
        int transparent;
        IntPtr polyInts;
        int polyAllocated;
        IntPtr brush;
        IntPtr tile;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = gdMaxColors)]
        int[] brushColorMap;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = gdMaxColors)]
        int[] tileColorMap;
        int styleLength;
        int stylePos;
        IntPtr style;
        int interlace;
        /* New in 2.0: thickness of line. Initialized to 1. */
        int thick;
        /* New in 2.0: alpha channel for palettes. Note that only
           Macintosh Internet Explorer and (possibly) Netscape 6
           really support multiple levels of transparency in
           palettes, to my knowledge, as of 2/15/01. Most
           common browsers will display 100% opaque and
           100% transparent correctly, and do something 
           unpredictable and/or undesirable for levels
           in between. TBB */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = gdMaxColors)]
        int[] alpha;
        /* Truecolor flag and pixels. New 2.0 fields appear here at the
           end to minimize breakage of existing object code. */
        int trueColor;
        IntPtr tpixels;
        /* Should alpha channel be copied, or applied, each time a
           pixel is drawn? This applies to truecolor images only.
           No attempt is made to alpha-blend in palette images,
           even if semitransparent palette entries exist. 
           To do that, build your image as a truecolor image,
           then quantize down to 8 bits. */
        int alphaBlendingFlag;
        /* Should the alpha channel of the image be saved? This affects
           PNG at the moment; other future formats may also
           have that capability. JPEG doesn't. */
        int saveAlphaFlag;

        /* There should NEVER BE ACCESSOR MACROS FOR ITEMS BELOW HERE, so this
           part of the structure can be safely changed in new releases. */

        /* 2.0.12: anti-aliased globals. 2.0.26: just a few vestiges after
          switching to the fast, memory-cheap implementation from PHP-gd. */
        int AA;
        int AA_color;
        int AA_dont_blend;

        /* 2.0.12: simple clipping rectangle. These values
          must be checked for safety when set; please use
          gdImageSetClip */
        int cx1;
        int cy1;
        int cx2;
        int cy2;
    }
}
