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
using Workshell.FileFormats.Formats.ODF;
using Workshell.FileFormats.Scanners.Archives;

namespace Workshell.FileFormats.Scanners.ODF
{
    public class OpenDocumentFormatScanner : ZipFormatScanner
    {
        private static readonly Dictionary<string, Type> _mimeMap = new Dictionary<string, Type>(StringComparer.Ordinal)
        {
            // Documents
            { "application/vnd.oasis.opendocument.text", typeof(ODFDocumentFormat) },
            { "application/vnd.oasis.opendocument.spreadsheet", typeof(ODFSpreadsheetFormat) },
            { "application/vnd.oasis.opendocument.presentation", typeof(ODFPresentationFormat) },
            { "application/vnd.oasis.opendocument.graphics", typeof(ODFDrawingFormat) },
            { "application/vnd.oasis.opendocument.chart", typeof(ODFChartFormat) },
            { "application/vnd.oasis.opendocument.formula", typeof(ODFFormulaFormat) },
            { "application/vnd.oasis.opendocument.image", typeof(ODFImageFormat) },
            { "application/vnd.oasis.opendocument.text-master", typeof(ODFMasterDocumentFormat) },
            { "application/vnd.oasis.opendocument.base", typeof(ODFDatabaseFormat) },
            { "application/vnd.oasis.opendocument.database", typeof(ODFDatabaseFormat) },
            { "application/vnd.sun.xml.base", typeof(ODFDatabaseFormat) },

            // Templates
            { "application/vnd.oasis.opendocument.text-template", typeof(ODFDocumentTemplateFormat) },
            { "application/vnd.oasis.opendocument.spreadsheet-template", typeof(ODFSpreadsheetTemplateFormat) },
            { "application/vnd.oasis.opendocument.presentation-template", typeof(ODFPresentationTemplateFormat) },
            { "application/vnd.oasis.opendocument.graphics-template", typeof(ODFDrawingTemplateFormat) },
            { "application/vnd.oasis.opendocument.chart-template", typeof(ODFChartTemplateFormat) },
            { "application/vnd.oasis.opendocument.formula-template", typeof(ODFFormulaTemplateFormat) },
            { "application/vnd.oasis.opendocument.image-template", typeof(ODFImageFormat) },
        };

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
                return null;

            var mimeType = ODFUtils.GetMimeTypeFromZip(job);

            if (string.IsNullOrWhiteSpace(mimeType))
                return null;

            if (!_mimeMap.ContainsKey(mimeType))
                return null;

            var type = _mimeMap[mimeType];
            var fingerprint = (FileFormat)Activator.CreateInstance(type);

            return fingerprint;
        }

        #endregion
    }
}
