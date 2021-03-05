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
using System.IO.Compression;
using System.Runtime.InteropServices;

using Workshell.FileFormats.Formats.Archives;

namespace Workshell.FileFormats.Scanners.Archives
{
    public class ZipFormatScanner : FileFormatScanner
    {
        private const uint ZipFileSignature = 67324752; // PK

        private static readonly int ZipFileHeaderSize = FileFormatUtils.SizeOf<ZipFileHeader>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct ZipFileHeader
        {
            public uint Signature;
            public ushort Version;
            public ushort Flags;
            public ushort CompressionMethod;
            public ushort LastModifiedTime;
            public ushort LastModifiedDate;
            public uint CRC;
            public uint CompressedSize;
            public uint UncompressedSize;
            public ushort FilenameLength;
            public ushort ExtraFieldLength;
        }

        public ZipFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
            {
                return null;
            }

            if (!Validate(job))
            {
                return null;
            }

            var fingerprint = new ZipFormat();

            return fingerprint;
        }

        protected bool ValidateStartBytes(FileFormatScanJob job)
        {
            const string cacheKey = "IsZIP";

            if (job.Cache.Exists(cacheKey))
            {
                var cachedResult = job.Cache.Get<bool>(cacheKey);

                return cachedResult;
            }

            var valid = false;

            if (!FileFormatUtils.IsNullOrEmpty(job.StartBytes))
            {
                if (job.StartBytes.Length > ZipFileHeaderSize)
                {
                    var header = FileFormatUtils.Read<ZipFileHeader>(job.StartBytes, 0, ZipFileHeaderSize);

                    if (header.Signature == ZipFileSignature)
                    {
                        valid = true;
                    }
                }
            }

            job.Cache.Set(cacheKey, valid);

            return valid;
        }

        protected bool Validate(FileFormatScanJob job)
        {
            try
            {
                using (new ZipArchive(job.Stream, ZipArchiveMode.Read, true))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
