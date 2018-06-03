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
using Workshell.FileFormats.Formats.Media;
using Workshell.FileFormats.Scanners.Containers;

namespace Workshell.FileFormats.Scanners.Media
{
    public class AVIFingerprintScanner : ResourceInterchangeFileFormatScanner
    {
        private const int Type = 541677121;

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
                return null;

            var header = FileFormatUtils.Read<RIFFHeader>(job.StartBytes);

            if (header.Type != Type)
                return null;

            var fingerprint = new AVIFormat();

            return fingerprint;
        }

        #endregion
    }
}
