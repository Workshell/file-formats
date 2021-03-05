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
using System.Linq;
using System.Xml.Linq;

using Workshell.FileFormats.Formats.UOF;

namespace Workshell.FileFormats.Scanners.UOF
{
    public sealed class UnifiedOfficeFormatScanner : XmlFormatScanner
    {
        private static readonly Dictionary<string, Type> _mimeMap = new Dictionary<string, Type>(StringComparer.Ordinal)
        {
            { "vnd.uof.text", typeof(UOFDocumentFormat) },
            { "vnd.uof.spreadsheet", typeof(UOFSpreadsheetFormat) },
            { "vnd.uof.presentation", typeof(UOFPresentationFormat) },
        };

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            try
            {
                var xml = XDocument.Load(job.Stream);

                if (xml.Root == null)
                {
                    return null;
                }

                var ns = xml.Root.GetNamespaceOfPrefix("uof");

                if (ns == null)
                {
                    return null;
                }

                if (string.Compare(ns.NamespaceName, xml.Root.Name.NamespaceName, StringComparison.Ordinal) != 0)
                {
                    return null;
                }

                if (string.Compare("UOF", xml.Root.Name.LocalName, StringComparison.Ordinal) != 0)
                {
                    return null;
                }

                var attr = xml.Root.Attributes()
                    .Where(a => a.Name.NamespaceName == ns.NamespaceName)
                    .FirstOrDefault(a => a.Name.LocalName == "mimetype");

                if (attr == null)
                {
                    return null;
                }

                var mimeType = attr.Value;

                if (!_mimeMap.ContainsKey(mimeType))
                {
                    return null;
                }

                var type = _mimeMap[mimeType];
                var fingerprint = (FileFormat)Activator.CreateInstance(type);

                return fingerprint;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
