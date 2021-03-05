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

using Workshell.FileFormats.Formats.Images;

namespace Workshell.FileFormats.Scanners.Images
{
    public class BitmapFormatScanner : ImageFormatScanner
    {
        private static readonly byte?[] BitmapSignature= new byte?[] { 0x42, 0x4D }; // BM
        private static readonly int BitmapFileHeaderSize = FileFormatUtils.SizeOf<BitmapFileHeader>();
        private static readonly int BitmapInfoHeaderSize = FileFormatUtils.SizeOf<BitmapInfoHeader>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct BitmapFileHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Signature;
            public uint Size;
            public ushort Reserved1;
            public ushort Reserved2;
            public uint BitmapOffset;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct BitmapInfoHeader
        {
            public uint Size;
            public int Width;
            public int Height;
            public ushort Planes;
            public ushort BitCount;
            public uint Compression;
            public uint SizeImage;
            public int XPelsPerMeter;
            public int YPelsPerMeter;
            public uint ClrUsed;
            public uint ClrImportant;
        }

        public BitmapFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
            {
                return null;
            }

            if (job.StartBytes.Length <= (BitmapFileHeaderSize + BitmapInfoHeaderSize))
            {
                return null;
            }

            var fileHeader = FileFormatUtils.Read<BitmapFileHeader>(job.StartBytes, 0, BitmapFileHeaderSize);

            if (!FileFormatUtils.MatchBytes(fileHeader.Signature, BitmapSignature))
            {
                return null;
            }

            var infoHeader = FileFormatUtils.Read<BitmapInfoHeader>(job.StartBytes, BitmapFileHeaderSize, BitmapInfoHeaderSize);

            if (infoHeader.Size != BitmapInfoHeaderSize)
            {
                return null;
            }

            var fingerprint = new BitmapImageFormat();

            return fingerprint;
        }

        #endregion
    }
}
