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
using Workshell.FileFormats.Formats.Media;

namespace Workshell.FileFormats.Scanners.Media
{
    public class BaseMediaFileFormatScanner : FileFormatScanner
    {
        private static readonly byte?[] Signature = new byte?[] { null, null, null, null, 0x66, 0x74, 0x79, 0x70 };

        public BaseMediaFileFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
                return null;

            var fingerprint = new BaseMediaFileFormat();

            return fingerprint;
        }

        protected virtual bool ValidateStartBytes(FileFormatScanJob job)
        {
            const string cacheKey = "IsQuickTime";

            if (job.Cache.Exists(cacheKey))
            {
                var cachedResult = job.Cache.Get<bool>(cacheKey);

                return cachedResult;
            }

            var result = false;

            if (!Utils.IsNullOrEmpty(job.StartBytes))
            {
                if (Utils.MatchBytes(job.StartBytes, 0, Signature))
                {
                    result = true;
                }
            }

            job.Cache.Set(cacheKey, result);

            return result;
        }

        #endregion
    }
}
