#region License
//  Copyright(c) 2018, Workshell Ltd
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Workshell.FileFormats.Formats.Archives;

namespace Workshell.FileFormats.Scanners.Archives
{
    public class CabinetFormatScanner : FileFormatScanner
    {
        private static readonly byte?[] Signature = new byte?[] { 0x4D, 0x53, 0x43, 0x46 };
        private static readonly int HeaderSize = FileFormatUtils.SizeOf<Header>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Header
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Signature;
            public uint Reserved1;
            public uint CabinetSize;
            public uint Reserved2;
            public uint FirstOffset;
            public uint Reserved3;
            public byte VersionMinor;
            public byte VersionMajor;
            public ushort FolderCount;
            public ushort FileCount;
            public ushort Flags;
            public ushort SetId;
            public ushort CabId;
        }

        public CabinetFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (job.StartBytes.Length < HeaderSize)
                return null;

            var header = FileFormatUtils.Read<Header>(job.StartBytes, 0, HeaderSize);

            if (!FileFormatUtils.MatchBytes(header.Signature, Signature))
                return null;

            if (!(header.VersionMajor == 1 && header.VersionMinor == 3))
                return null;

            var fingerprint = new CabinetFormat();

            return fingerprint;
        }

        #endregion
    }
}
