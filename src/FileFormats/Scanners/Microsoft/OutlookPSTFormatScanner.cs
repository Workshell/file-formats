#region License
//  Copyright(c) 2021, Workshell Ltd
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

using Workshell.FileFormats.Formats.Microsoft;

namespace Workshell.FileFormats.Scanners.Microsoft
{
    public class OutlookPSTFormatScanner : FileFormatScanner
    {
        private const uint Magic = 1313096225;
        private const ushort MagicClient = 19795;

        private static readonly int HeaderSize = FileFormatUtils.SizeOf<Header>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Header
        {
            public uint Magic;
            public uint CRCPartial;
            public ushort MagicClient;
            public ushort Ver;
            public ushort VerClient;
            public byte PlatformCreate;
            public byte PlatformAccess;
            public ulong Reserved;
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
            {
                return null;
            }

            if (job.StartBytes.Length <= HeaderSize)
            {
                return null;
            }

            var header = FileFormatUtils.Read<Header>(job.StartBytes, 0, HeaderSize);

            if (header.Magic != Magic)
            {
                return null;
            }

            if (header.MagicClient != MagicClient)
            {
                return null;
            }

            var format = PSTFormat.Unknown;

            if (header.Ver == 14 || header.Ver == 15)
            {
                format = PSTFormat.ANSI;
            }
            else if (header.Ver >= 23)
            {
                format = PSTFormat.Unicode;
            }

            if (header.VerClient != 19)
            {
                return null;
            }

            if (header.PlatformCreate != 1 && header.PlatformAccess != 1)
            {
                return null;
            }

            var fingerprint = new OutlookPSTFormat(format);

            return fingerprint;
        }

        #endregion
    }
}
