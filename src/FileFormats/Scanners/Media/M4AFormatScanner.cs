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
using System.IO;
using System.Linq;
using System.Text;

using Workshell.FileFormats.Formats.Media;

namespace Workshell.FileFormats.Scanners.Media
{
    public class M4AFormatScanner : BaseMediaFileFormatScanner
    {
        private static readonly string[] _signatureStrings = new string[] { "M4A", "M4B", "M4P" };
        private static readonly byte?[][] _signatures;

        static M4AFormatScanner()
        {
            var signatures = new List<byte?[]>();

            foreach (var _signatureString in _signatureStrings)
            {
                var bytes = Encoding.ASCII.GetBytes(_signatureString).Cast<byte?>().ToList();

                while (bytes.Count != 4)
                    bytes.Add(null);

                signatures.Add(bytes.ToArray());
            }

            _signatures = signatures.ToArray();
        }

        public M4AFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
            {
                return null;
            }

            foreach (var signature in _signatures)
            {
                if (FileFormatUtils.MatchBytes(job.StartBytes, 8, signature))
                {
                    var fingerprint = new M4AFormat();

                    return fingerprint;
                }
            }

            return null;
        }

        #endregion
    }
}
