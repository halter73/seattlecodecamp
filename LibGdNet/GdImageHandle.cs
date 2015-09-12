using System;
using System.Runtime.InteropServices;

namespace LibGdNet
{
    public class GdImageHandle : SafeHandle
    {
        public GdImageHandle() : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid
        {
            get
            {
                return handle == IntPtr.Zero;
            }
        }

        public GdImage Image
        {
            get
            {
                return Marshal.PtrToStructure<GdImage>(handle);
            }
        }

        protected override bool ReleaseHandle()
        {
            if (handle != IntPtr.Zero)
            {
                LibGd.gdFree(handle);
                handle = IntPtr.Zero;
            }

            return true;
        }
    }
}
