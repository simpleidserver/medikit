namespace PcscDotNet
{
    /// <summary>
    /// The protocol control information structure.
    /// Any protocol-specific information then immediately follows this structure.
    /// The entire length of the structure must be aligned with the underlying hardware architecture word size.
    /// For example, in Win32 the length of any PCI information must be a multiple of four bytes so that it aligns on a 32-bit boundary.
    /// (For WinSCard in Windows and pcsc-lite in OS X.)
    /// </summary>
    public struct SCardIORequest
    {
        /// <summary>
        /// Protocol in use.
        /// </summary>
        public SCardProtocols Protocol;

        /// <summary>
        /// Length, in bytes, of the current structure plus any following PCI-specific information.
        /// </summary>
        public int PciLength;
    }
}
