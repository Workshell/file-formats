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

using Workshell.OpenMcdf;

using Workshell.FileFormats.Formats.Microsoft.Legacy;
using Workshell.FileFormats.Scanners.Containers;

namespace Workshell.FileFormats.Scanners.Microsoft
{
    public class PublisherFormatScanner : CompoundFileStorageFormatScanner
    {
        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
            {
                return null;
            }

            var hasQuill = false;
            var hasContents = false;
            var hasEnvelope = false;

            using (var file = new CompoundFile(job.Stream, CFSUpdateMode.ReadOnly, CFSConfiguration.LeaveOpen))
            {
                var items = new List<CFItem>();

                file.RootStorage.VisitEntries((item) => items.Add(item), true);

                foreach (var item in items)
                {
                    if (string.Compare(item.Name, "Quill", StringComparison.Ordinal) == 0)
                    {
                        hasQuill = true;
                    }
                    else if (item.Name.IndexOf("Contents", StringComparison.Ordinal) > -1)
                    {
                        hasContents = true;
                    }
                    else if (item.Name.IndexOf("Envelope", StringComparison.Ordinal) > -1)
                    {
                        hasEnvelope = true;
                    }
                }
            }

            if (hasQuill && hasContents && hasEnvelope)
            {
                var fingerprint = new PublisherFormat();

                return fingerprint;
            }

            return null;
        }

        #endregion
    }
}
