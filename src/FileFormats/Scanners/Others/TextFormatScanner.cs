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
using System.Linq;
using System.Threading.Tasks;

using Workshell.FileFormats.Formats;
using Workshell.Ude;

namespace Workshell.FileFormats.Scanners
{
    public class TextFormatScanner : FileFormatScanner
    {
        private static readonly IList<byte?[]> StartSignatures = new List<byte?[]>()
        {
            new byte?[] { 0xEF, 0xBB, 0xBF },
            new byte?[] { 0xFE, 0xFF },
            new byte?[] { 0xFF, 0xFE },
            new byte?[] { 0x00, 0x00, 0xFE, 0xFF },
            new byte?[] { 0xFF, 0xFE, 0x00, 0x00 },
            new byte?[] { 0x2B, 0x2F, 0x76 },
            new byte?[] { 0xF7, 0x64, 0x4C },
            new byte?[] { 0xDD, 0x73, 0x66, 0x73 },
            new byte?[] { 0x0E, 0xFE, 0xFF },
            new byte?[] { 0xFB, 0xEE, 0x28 },
            new byte?[] { 0x84, 0x31, 0x95, 0x33 },
        };

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job.StartBytes))
            {
                return null;
            }

            var fingerprint = new TextFormat();

            return fingerprint;
        }

        protected bool ValidateStartBytes(byte[] bytes)
        {
            if (FileFormatUtils.IsNullOrEmpty(bytes))
            {
                return false;
            }

            if (bytes.Length < 2)
            {
                return false;
            }

            var confidence = 0.0f;

            if (bytes.Length >= 2)
            {
                var sigs = StartSignatures.Where(_ => _.Length == 2);

                foreach (var sig in sigs)
                {
                    if (FileFormatUtils.MatchBytes(bytes, sig))
                    {
                        confidence = 0.8f;

                        break;
                    }
                }
            }

            if (bytes.Length >= 3 && confidence == 0.0f)
            {
                var sigs = StartSignatures.Where(_ => _.Length == 3);

                foreach (var sig in sigs)
                {
                    if (FileFormatUtils.MatchBytes(bytes, sig))
                    {
                        confidence = 0.8f;

                        break;
                    }
                }
            }

            if (bytes.Length >= 4 && confidence == 0.0f)
            {
                var sigs = StartSignatures.Where(_ => _.Length == 4);

                foreach (var sig in sigs)
                {
                    if (FileFormatUtils.MatchBytes(bytes, sig))
                    {
                        confidence = 0.8f;

                        break;
                    }
                }
            }

            var cdet = new CharsetDetector();

            cdet.Feed(bytes, 0, bytes.Length);
            cdet.DataEnd();

            if (!string.IsNullOrWhiteSpace(cdet.Charset) && cdet.Confidence >= 1.0)
            {
                confidence = 1.0f;
            }

            return (confidence >= 0.8f);
        }

        #endregion
    }
}
