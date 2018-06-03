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
using System.Threading;
using Workshell.FileFormats.Formats.Executables;

namespace Workshell.FileFormats.Scanners.Executables
{
    public class ExecutableLinkableFormatScanner : ExecutableFormatScanner
    {
        private static readonly byte?[] Signature = new byte?[] { 0x7F, 0x45, 0x4C, 0x46 };

        public ExecutableLinkableFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (job.StartBytes.Length < 1024)
                return null;

            if (!FileFormatUtils.MatchBytes(job.StartBytes, 0, Signature))
                return null;

            var is32Bit = (job.StartBytes[4] == 0x01);
            var is64Bit = (job.StartBytes[4] == 0x02);

            if (!is32Bit && !is64Bit)
                return null;

            ELFImageFormat imageFormat;

            if (is32Bit)
            {
                imageFormat = ELFImageFormat._32Bit;
            }
            else
            {
                imageFormat = ELFImageFormat._64Bit;
            }

            var littleEnd = (job.StartBytes[5] == 0x01);
            var bigEnd = (job.StartBytes[5] == 0x02);

            if (!littleEnd && !bigEnd)
                return null;

            ELFImageEndianness imageEndianness;

            if (littleEnd)
            {
                imageEndianness = ELFImageEndianness.Little;
            }
            else
            {
                imageEndianness = ELFImageEndianness.Big;
            }

            if (job.StartBytes[6] != 0x01)
                return null;

            var version = FileFormatUtils.ReadUInt32(job.StartBytes, 20, bigEnd);

            if (version != 1)
                return null;

            var fingerprint = new ELFExecutableFormat(imageFormat, imageEndianness);

            return fingerprint;
        }

        #endregion
    }
}
