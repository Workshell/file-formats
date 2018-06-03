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
using System.Linq;
using System.Text;
using Workshell.FileFormats.Formats.Media;

namespace Workshell.FileFormats.Scanners.Media
{
    public class FlashVideoFormatScanner : FileFormatScanner
    {
        private static readonly byte?[] Signature = new byte?[] { 0x46, 0x4C, 0x56, 0x01, null };

        public FlashVideoFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (Utils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (!Utils.MatchBytes(job.StartBytes, Signature))
                return null;

            if (job.StartBytes[4] != 0x01 && job.StartBytes[4] != 0x04 && job.StartBytes[4] != 0x05)
                return null;

            var fingerprint = new FlashVideoFormat();

            return fingerprint;
        }

        #endregion
    }
}
