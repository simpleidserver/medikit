// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EID.Exceptions;
using Medikit.EID.Resources;
using PcscDotNet;
using System;
using System.Collections.Generic;

namespace Medikit.EID
{
    public class BeIDCardDiscovery : IDisposable
    {
        private PcscContext _context;

        public BeIDCardDiscovery()
        {
            EstablishContext();
        }

        public IEnumerable<string> GetReaders()
        {
            CheckContextIsEstablished();
            return _context.GetReaderNames();
        }

        public BeIDCardConnector Connect(string readerName)
        {
            CheckContextIsEstablished();
            var connection = _context.Connect(readerName, SCardShare.Shared, SCardProtocols.T0);
            return new BeIDCardConnector(connection, _context);
        }

        private void CheckContextIsEstablished()
        {
            if (_context == null || !_context.IsEstablished)
            {
                throw new BeIDCardException(Global.ContextNotEstablished);
            }
        }

        private void EstablishContext()
        {
            var platform = Environment.OSVersion.Platform;
            if (platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE)
            {
                _context = Pcsc<WinSCard>.EstablishContext(SCardScope.User);
            }
            else
            {
                _context = Pcsc<PCSCliteAPI>.EstablishContext(SCardScope.User);
            }
        }

        public void Dispose()
        {
            if (_context != null && !_context.IsDisposed)
            {
                _context.Dispose();
            }
        }
    }
}