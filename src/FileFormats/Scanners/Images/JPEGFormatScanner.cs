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
    public class JPEGFormatScanner : ImageFormatScanner
    {
        private static readonly byte?[] Signature = new byte?[] { 0xFF, 0xD8 };
        private static readonly byte?[] JFIF = new byte?[] { 0xFF, 0xE0 };
        private static readonly byte?[] EXIF = new byte?[] { 0xFF, 0xE1 };
        private static readonly int HeaderSize = FileFormatUtils.SizeOf<JFIFHeader>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct JFIFHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SOI;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] App;
            public ushort Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Identifier;
            public ushort Version;
            public byte Units;
            public ushort XDensity;
            public ushort YDensity;
            public byte XThumbnail;
            public byte YThumbnail;
        }

        public JPEGFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (job.StartBytes.Length <= HeaderSize)
                return null;

            var header = FileFormatUtils.Read<JFIFHeader>(job.StartBytes, 0, HeaderSize);

            if (!FileFormatUtils.MatchBytes(header.SOI, Signature))
                return null;

            var exif = FileFormatUtils.MatchBytes(header.App, EXIF);

            if (!FileFormatUtils.MatchBytes(header.App, JFIF) && exif)
                return null;

            var fingerprint = new JPEGImageFormat(exif);

            return fingerprint;
        }

        #endregion
    }
}
