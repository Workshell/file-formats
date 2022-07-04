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
using System.Text;

using Workshell.OpenMcdf;

using Workshell.FileFormats.Formats.Microsoft.Legacy;
using Workshell.FileFormats.Scanners.Containers;

namespace Workshell.FileFormats.Scanners.Microsoft
{
    public class EncryptedDocumentFormatScanner : CompoundFileStorageFormatScanner
    {
        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
            {
                return null;
            }

            var isEncrypted = false;

            using (var file = new CompoundFile(job.Stream, CFSUpdateMode.ReadOnly, CFSConfiguration.LeaveOpen))
            {
                var items = new List<CFItem>();

                file.RootStorage.VisitEntries((item) => items.Add(item), true);

                foreach (var item in items)
                {
                    if (string.Compare(item.Name, "EncryptionInfo", StringComparison.Ordinal) == 0 && item.IsStream)
                    {
                        if (ValidateItem((CFStream)item))
                        {
                            isEncrypted = true;

                            break;
                        }
                    }
                }
            }

            if (isEncrypted)
            {
                var fingerprint = new EncryptedDocumentFormat();

                return fingerprint;
            }

            return null;
        }

        private bool ValidateItem(CFStream stream)
        {
            var content = Encoding.ASCII.GetString(stream.GetData());

            if (content.IndexOf("xmlns=\"http://schemas.microsoft.com/office/2006/encryption\"", StringComparison.Ordinal) == -1)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
