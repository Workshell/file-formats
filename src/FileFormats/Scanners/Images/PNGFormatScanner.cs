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
using System.IO;
using System.Runtime.InteropServices;
using Workshell.FileFormats.Formats.Images;

namespace Workshell.FileFormats.Scanners.Images
{
    public class PNGFormatScanner : ImageFormatScanner
    {
        private const uint IHDRType = 1380206665;
        private const uint IENDType = 1145980233;

        private static readonly byte?[] Signature = new byte?[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        private static readonly int ChunkSize = FileFormatUtils.SizeOf<Chunk>();
        private static readonly int HeaderChunkSize = FileFormatUtils.SizeOf<IHDRChunk>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Chunk
        {
            public uint Length;
            public uint Type;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct IHDRChunk
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Width;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Height;
            public byte BitDepth;
            public byte ColorType;
            public byte Compression;
            public byte Filter;
            public byte Interlace;
        }

        public PNGFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (!ValidateStart(job.StartBytes))
                return null;

            if (FileFormatUtils.IsNullOrEmpty(job.EndBytes))
                return null;

            if (!ValidateEnd(job.EndBytes))
                return null;

            var fingerprint = new PNGImageFormat();

            return fingerprint;
        }

        private bool ValidateStart(byte[] startBytes)
        {
            if ((Signature.Length + ChunkSize + HeaderChunkSize) > startBytes.Length)
                return false;

            if (!FileFormatUtils.MatchBytes(startBytes, Signature))
                return false;

            using (var mem = new MemoryStream(startBytes))
            {
                mem.Seek(Signature.Length, SeekOrigin.Begin);

                var chunk = FileFormatUtils.Read<Chunk>(mem);

                if (chunk.Type != IHDRType)
                    return false;
            }

            return true;
        }

        private bool ValidateEnd(byte[] bytes)
        {
            if (bytes.Length < ChunkSize)
                return false;

            var chunk = FileFormatUtils.Read<Chunk>(bytes, bytes.Length - 12, ChunkSize);

            if (chunk.Length != 0 && chunk.Type != IENDType)
                return false;

            return true;
        }

        #endregion
    }
}
