using System;
using System.Runtime.InteropServices;

namespace PcscDotNet
{
    /// <summary>
    /// The PC/SC provider of WinSCard in Linux.
    /// </summary>
    public class PCSCliteAPI : IPcscProvider
    {
        private const string PCSC_LIB_UNIX = "libpcsclite.so.1";
        public const string DllName = PCSC_LIB_UNIX;

        #region Imports

        // https://pcsclite.alioth.debian.org/api/group__API.html#gaa1b8970169fd4883a6dc4a8f43f19b67
        /// <summary>
        /// Establishes the resource manager context within which database operations are performed.
        /// </summary>
        /// <param name="dwScope">Scope of the resource manager context (user domain, system domain).</param>
        /// <param name="pvReserved1"></param>
        /// <param name="pvReserved2"></param>
        /// <param name="phContext">Handle to the established resource manager context.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        public unsafe static extern SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        // https://pcsclite.alioth.debian.org/api/group__API.html#ga93b07815789b3cf2629d439ecf20f0d9
        /// <summary>
        /// Provides the list of readers within a set of named reader groups, eliminating duplicated.
        /// </summary>
        /// <param name="hContext">Handle that identifies the resource manager context for the query.</param>
        /// <param name="mszGroups">Names of the reader groups defined to the system (all readers, default reader, local reader, system reader).</param>
        /// <param name="mszReaders">Multi-string that list the card readers within the supplied reader groups.</param>
        /// <param name="pcchReaders">Length of the mszReaders buffer in characters.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public unsafe static extern SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        // https://pcsclite.alioth.debian.org/api/group__API.html#ga4e515829752e0a8dbc4d630696a8d6a5
        /// <summary>
        /// Establishes a connection between the calling application and a smart card.
        /// </summary>
        /// <param name="hContext">Connection context to the PC/SC Resource manager.</param>
        /// <param name="szReader">Reader name to connect to.</param>
        /// <param name="dwShareMode">Mode of connection type : exclusive or shared.</param>
        /// <param name="dwPreferredProtocols">Desired protocol use : T0 or T1.</param>
        /// <param name="phCard">Handle to this connection.</param>
        /// <param name="pdwActiveProtocol">Established protocol to this connection.</param>
        /// <returns></returns>
        [DllImport(DllName)]
        public unsafe static extern SCardError SCardConnect(SCardContext hContext, string szReader, SCardShare dwShareMode, SCardProtocols dwPreferredProtocols, SCardHandle* phCard, SCardProtocols* pdwActiveProtocol);

        // https://pcsclite.alioth.debian.org/api/group__API.html#gaddb835dce01a0da1d6ca02d33ee7d861
        /// <summary>
        /// Established a temporary exclusive access mode for doing a serie of commands in a transaction.
        /// </summary>
        /// <param name="hCard">Connection made from SCardConnect()</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        public static extern SCardError SCardBeginTransaction(SCardHandle hCard);

        // https://pcsclite.alioth.debian.org/api/group__API.html#ga9a2d77242a271310269065e64633ab99
        /// <summary>
        /// Sends a service request to the smart card and expects to receive data back from the card.
        /// </summary>
        /// <param name="hCard">A reference value returned from the SCardConnect function.</param>
        /// <param name="pioSendPci">A pointer to the protocol header structure for the instruction.</param>
        /// <param name="pbSendBuffer">A pointer to the actual data to written to the card.</param>
        /// <param name="cbSendLength">The length in bytes of the pbSendBuffer parameter.</param>
        /// <param name="pioRecvPci">Pointer to the protocol header structure for the instruction.</param>
        /// <param name="pbRecvBuffer">Pointer to any data returned from the card.</param>
        /// <param name="pcbRecvLength">Supplies the length in bytes of the pbRecvBuffer parameter and receives the actual number of bytes received from the smart card.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        public unsafe static extern SCardError SCardTransmit(SCardHandle hCard, SCardIORequest* pioSendPci, byte* pbSendBuffer, int cbSendLength, SCardIORequest* pioRecvPci, byte* pbRecvBuffer, int* pcbRecvLength);

        // https://pcsclite.alioth.debian.org/api/group__API.html#gae8742473b404363e5c587f570d7e2f3b
        /// <summary>
        /// Completes a previously declared transaction, allowing other applications to resume interactions with the card.
        /// </summary>
        /// <param name="hCard">Reference value obtained from a previous call to SCardConnect.</param>
        /// <param name="dwDisposition">Action to take on the card in the connected reader on close. (eject, leave, reset, unpower)</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        public static extern SCardError SCardEndTransaction(SCardHandle hCard, SCardDisposition dwDisposition);

        // https://pcsclite.alioth.debian.org/api/group__API.html#ga4be198045c73ec0deb79e66c0ca1738a
        /// <summary>
        /// Terminates a connection previously opened between the calling application and a smart card in the target reader.
        /// </summary>
        /// <param name="hCard">Reference value obtained from a previous call to SCardConnect.</param>
        /// <param name="dwDisposition">Action to take on the card in the connected reader on close (leave, reset, unpower, eject).</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        public static extern SCardError SCardDisconnect(SCardHandle hCard, SCardDisposition dwDisposition);

        // https://pcsclite.alioth.debian.org/api/group__API.html#ga0522241e3180cb05dfd166e28930e961
        /// <summary>
        /// Releases memory that has been returned from the resource manager using the SCARD_AUTOALLOCATE  length designator.
        /// </summary>
        /// <param name="hContext">Handle that identifies the resource manager context returned from SCardEstablishContext.</param>
        /// <param name="pvMem">Memory block to be released.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        public unsafe static extern SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        // https://pcsclite.alioth.debian.org/api/group__API.html#ga6aabcba7744c5c9419fdd6404f73a934
        /// <summary>
        /// Closes an established resource manager context.
        /// </summary>
        /// <param name="hContext">Handle that identifies the resource manager context.</param>
        /// <returns>Error context.</returns>
        [DllImport(DllName)]
        public static extern SCardError SCardReleaseContext(SCardContext hContext);

        // https://pcsclite.alioth.debian.org/api/group__API.html#gaacbbc0c6d6c0cbbeb4f4debf6fbeeee6
        /// <summary>
        /// Terminates all oustanding actions within  a specific resource manager context.
        /// </summary>
        /// <param name="hContext">Handle that identifies the resource manager context.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        public static extern SCardError SCardCancel(SCardContext hContext);

        #endregion

        #region Implemented methods

        unsafe SCardError IPcscProvider.SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext)
        {
            return SCardEstablishContext(dwScope, pvReserved1, pvReserved2, phContext);
        }
                
        unsafe SCardError IPcscProvider.SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders)
        {
            return SCardListReaders(hContext, mszGroups, mszReaders, pcchReaders);
        }

        unsafe SCardError IPcscProvider.SCardConnect(SCardContext hContext, string szReader, SCardShare dwShareMode, SCardProtocols dwPreferredProtocols, SCardHandle* phCard, SCardProtocols* pdwActiveProtocol)
        {
            return SCardConnect(hContext, szReader, dwShareMode, dwPreferredProtocols, phCard, pdwActiveProtocol);
        }
        
        SCardError IPcscProvider.SCardBeginTransaction(SCardHandle hCard)
        {
            return SCardBeginTransaction(hCard);
        }

        unsafe byte[] IPcscProvider.AllocateIORequest(int informationLength)
        {
            if (informationLength <= 0) return new byte[sizeof(SCardIORequest)];
            var remain = (informationLength += sizeof(SCardIORequest)) % sizeof(void*);
            return new byte[remain == 0 ? informationLength : informationLength + sizeof(SCardIORequest) - remain];
        }

        unsafe void IPcscProvider.WriteIORequest(void* pIORequest, SCardProtocols protocol, int totalLength, byte[] information)
        {
            var p = (SCardIORequest*)pIORequest;
            p->Protocol = protocol;
            p->PciLength = totalLength;
            if (information?.Length > 0)
            {
                Marshal.Copy(information, 0, (IntPtr)(p + 1), information.Length);
            }
        }
        
        unsafe void IPcscProvider.ReadIORequest(void* pIORequest, out SCardProtocols protocol, out byte[] information)
        {
            var p = (SCardIORequest*)pIORequest;
            protocol = p->Protocol;
            var length = p->PciLength - sizeof(SCardIORequest);
            if (length <= 0)
            {
                information = null;
            }
            else
            {
                information = new byte[length];
                Marshal.Copy((IntPtr)(p + 1), information, 0, length);
            }
        }

        unsafe SCardError IPcscProvider.SCardTransmit(SCardHandle hCard, void* pioSendPci, byte* pbSendBuffer, int cbSendLength, void* pioRecvPci, byte* pbRecvBuffer, int* pcbRecvLength)
        {
            return SCardTransmit(hCard, (SCardIORequest*)pioSendPci, pbSendBuffer, cbSendLength, (SCardIORequest*)pioRecvPci, pbRecvBuffer, pcbRecvLength);
        }

        SCardError IPcscProvider.SCardEndTransaction(SCardHandle hCard, SCardDisposition dwDisposition)
        {
            return SCardEndTransaction(hCard, dwDisposition);
        }
        
        SCardError IPcscProvider.SCardDisconnect(SCardHandle hCard, SCardDisposition dwDisposition)
        {
            return SCardDisconnect(hCard, dwDisposition);
        }
        
        unsafe SCardError IPcscProvider.SCardFreeMemory(SCardContext hContext, void* pvMem)
        {
            return SCardError.Successs;
        }

        SCardError IPcscProvider.SCardReleaseContext(SCardContext hContext)
        {
            return SCardReleaseContext(hContext);
        }

        SCardError IPcscProvider.SCardCancel(SCardContext hContext)
        {
            return SCardCancel(hContext);
        }

        #endregion

        #region TO CHANGE

        public byte[] AllocateIORequest(int informationLength)
        {
            throw new System.NotImplementedException();
        }

        public byte[] AllocateReaderStates(int count)
        {
            throw new System.NotImplementedException();
        }

        public unsafe void* AllocateString(string value)
        {
            throw new System.NotImplementedException();
        }

        public unsafe string AllocateString(void* ptr, int length)
        {
            throw new System.NotImplementedException();
        }

        public unsafe void FreeString(void* ptr)
        {
            throw new System.NotImplementedException();
        }

        public unsafe void ReadIORequest(void* pIORequest, out SCardProtocols protocol, out byte[] information)
        {
            throw new System.NotImplementedException();
        }

        public unsafe void ReadReaderState(void* pReaderStates, int index, out void* pReaderName, out SCardReaderStates currentState, out SCardReaderStates eventState, out byte[] atr)
        {
            throw new System.NotImplementedException();
        }
        
        public unsafe SCardError SCardControl(SCardHandle hCard, int dwControlCode, void* lpInBuffer, int nInBufferSize, void* lpOutBuffer, int nOutBufferSize, int* lpBytesReturned)
        {
            throw new System.NotImplementedException();
        }

        public int SCardCtlCode(SCardControlFunction function)
        {
            throw new System.NotImplementedException();
        }

        public unsafe SCardError SCardGetStatusChange(SCardContext hContext, int dwTimeout, void* rgReaderStates, int cReaders)
        {
            throw new System.NotImplementedException();
        }

        public SCardError SCardIsValidContext(SCardContext hContext)
        {
            throw new System.NotImplementedException();
        }

        public unsafe SCardError SCardListReaderGroups(SCardContext hContext, void* mszGroups, int* pcchGroups)
        {
            throw new System.NotImplementedException();
        }

        public unsafe SCardError SCardReconnect(SCardHandle hCard, SCardShare dwShareMode, SCardProtocols dwPreferredProtocols, SCardDisposition dwInitialization, SCardProtocols* pdwActiveProtocol)
        {
            throw new System.NotImplementedException();
        }

        public unsafe void WriteReaderState(void* pReaderStates, int index, SCardReaderStates currentState)
        {
            throw new System.NotImplementedException();
        }

        public unsafe void WriteReaderState(void* pReaderStates, int index, void* pReaderName)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
