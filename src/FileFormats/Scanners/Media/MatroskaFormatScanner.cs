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
using System.Text;
using Workshell.FileFormats.Formats.Media;
using Workshell.FileFormats.Scanners.Containers;

namespace Workshell.FileFormats.Scanners.Media
{
    public class MatroskaFormatScanner : EBMLFormatScanner
    {
        private static readonly byte?[] Matroska = new byte?[] { 0x6D, 0x61, 0x74, 0x72, 0x6F, 0x73, 0x6B, 0x61 };

        public MatroskaFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
                return null;

            var fingerprint = new MatroskaFormat();

            return fingerprint;
        }

        protected override bool ValidateStartBytes(FileFormatScanJob job)
        {
            if (!base.ValidateStartBytes(job))
                return false;

            const string cacheKey = "IsMatroska";

            if (job.Cache.Exists(cacheKey))
            {
                var cachedResult = job.Cache.Get<bool>(cacheKey);

                return cachedResult;
            }

            var result = false;

            for (var i = 0; i < job.StartBytes.Length; i++)
            {
                if (job.StartBytes[i] == 0x6D && Utils.MatchBytes(job.StartBytes, i, Matroska))
                {
                    result = true;
                    break;
                }
            }

            job.Cache.Set(cacheKey, result);

            return result;
        }

        #endregion
    }
}
