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
using Workshell.FileFormats.Formats.Containers;
using Workshell.FileFormats.OpenMCDF;

namespace Workshell.FileFormats.Scanners.Containers
{
    public class CompoundFileStorageFormatScanner : FileFormatScanner
    {
        private static readonly byte?[] Signature = new byte?[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

        public CompoundFileStorageFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
                return null;

            var fingerprint = new CompoundFileStorageFormat();

            return fingerprint;
        }

        protected bool ValidateStartBytes(FileFormatScanJob job)
        {
            const string cacheKey = "IsOleCFS";

            if (job.Cache.Exists(cacheKey))
            {
                var cachedResult = job.Cache.Get<bool>(cacheKey);

                return cachedResult;
            }

            var result = false;

            if (!Utils.IsNullOrEmpty(job.StartBytes))
            {
                if (job.StartBytes.Length > 8)
                {
                    if (Utils.MatchBytes(job.StartBytes, Signature))
                    {
                        result = true;
                    }
                }
            }

            job.Cache.Set(cacheKey, result);

            return result;
        }

        protected bool Validate(Stream stream)
        {
            try
            {
                using (new CompoundFile(stream))
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
