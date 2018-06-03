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
using Workshell.FileFormats.Formats.Microsoft.OOXml;
using Workshell.FileFormats.Scanners.Archives;

namespace Workshell.FileFormats.Scanners.Microsoft.OOXml
{
    public class PowerPointPresentationFormatScanner : ZipFormatScanner
    {
        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
                return null;

            var contentTypes = OOXmlUtils.GetContentTypesFromZip(job);

            if (string.IsNullOrWhiteSpace(contentTypes))
                return null;

            if (contentTypes.IndexOf("application/vnd.openxmlformats-officedocument.presentationml.presentation.main+xml",StringComparison.Ordinal) == -1 &&
                contentTypes.IndexOf("application/vnd.ms-powerpoint.presentation.macroEnabled.main+xml", StringComparison.Ordinal) == -1)
                return null;

            var macros = (contentTypes.IndexOf("application/vnd.ms-powerpoint.presentation.macroEnabled.main+xml", StringComparison.Ordinal) > -1);
            var fingerprint = new PowerPointPresentationFormat(macros);

            return fingerprint;
        }

        #endregion
    }
}
