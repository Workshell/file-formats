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
using Workshell.FileFormats.Formats.Archives;

namespace Workshell.FileFormats.Scanners.Archives
{
    public class RARFormatScanner : FileFormatScanner
    {
        private static readonly byte?[] Signature = new byte?[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07 };

        public RARFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (job.StartBytes.Length < Signature.Length)
                return null;

            if (!FileFormatUtils.MatchBytes(job.StartBytes, Signature))
                return null;

            var fingerprint = new RarFormat();

            return fingerprint;
        }

        #endregion
    }
}
