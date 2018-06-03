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
using Workshell.FileFormats.Formats.Images;

namespace Workshell.FileFormats.Scanners.Images
{
    public class GIFFingerprinterScanner : ImageFormatScanner
    {
        private static readonly byte?[] EightySevenSignature = new byte?[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 };
        private static readonly byte?[] EightyNineSignature = new byte?[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 };
        private static readonly int HeaderSize = FileFormatUtils.SizeOf<GIFHeader>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct GIFHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Signature;
            public ushort Width;
            public ushort Height;
            public byte Packed;
            public byte BackgroundColour;
            public byte AspectRatio;
        }

        public GIFFingerprinterScanner()
        {

        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (job.StartBytes.Length <= HeaderSize)
                return null;

            if (!FileFormatUtils.MatchBytes(job.StartBytes, EightySevenSignature) && !FileFormatUtils.MatchBytes(job.StartBytes, EightyNineSignature))
                return null;

            var fingerprint = new GIFImageFormat();

            return fingerprint;
        }

        #endregion
    }
}
