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
using Workshell.FileFormats.Formats;

namespace Workshell.FileFormats.Scanners
{
    public class FlashFormatScanner : FileFormatScanner
    {
        private static readonly byte?[] FwsSignature = new byte?[] { 0x46, 0x57, 0x53 };
        private static readonly byte?[] CwsSignature = new byte?[] { 0x43, 0x57, 0x53 };
        private static readonly byte?[] ZwsSignature = new byte?[] { 0x5A, 0x57, 0x53 };

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (job.StartBytes.Length < 1024)
                return null;

            FlashCompressionType? compressionType = null;

            if (FileFormatUtils.MatchBytes(job.StartBytes, FwsSignature))
            {
                compressionType = FlashCompressionType.Uncompressed;
            }
            else if (FileFormatUtils.MatchBytes(job.StartBytes, CwsSignature))
            {
                compressionType = FlashCompressionType.CompressedZlib;
            }
            else if (FileFormatUtils.MatchBytes(job.StartBytes, ZwsSignature))
            {
                compressionType = FlashCompressionType.CompressedLZMA;
            }

            if (compressionType == null)
                return null;

            var fingerprint = new FlashFormat(compressionType.Value);

            return fingerprint;
        }

        #endregion
    }
}
