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
using Workshell.FileFormats.Formats;

namespace Workshell.FileFormats.Scanners
{
    public class PDFFormatScanner : FileFormatScanner
    {
        private static readonly byte?[] StartSignature = new byte?[] { 0x25, 0x50, 0x44, 0x46, 0x2D };
        private static readonly byte?[] EndSignature = new byte?[] { 0x45, 0x4F, 0x46, 0x0A };

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job.StartBytes))
                return null;

            if (!ValidateEndBytes(job.EndBytes))
                return null;

            var fingerprint = new PDFFormat();

            return fingerprint;
        }

        private bool ValidateStartBytes(byte[] bytes)
        {
            if (FileFormatUtils.IsNullOrEmpty(bytes))
                return false;

            if (bytes.Length < 20)
                return false;

            if (!FileFormatUtils.MatchBytes(bytes, StartSignature))
                return false;

            return true;
        }

        private bool ValidateEndBytes(byte[] bytes)
        {
            if (FileFormatUtils.IsNullOrEmpty(bytes))
                return false;

            if (bytes.Length < EndSignature.Length)
                return false;

            if (!FileFormatUtils.MatchBytes(bytes, bytes.Length - EndSignature.Length, EndSignature))
                return false;

            return true;
        }

        #endregion
    }
}
