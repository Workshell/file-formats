﻿#region License
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
    public class ThreeGPFormatScanner : BaseMediaFileFormatScanner
    {
        private static readonly string[] _signatureStrings = new string[]
        {
            "3ge6", "3ge7", "3gg6", "3gp1", "3gp2", "3gp3", "3gp4", "3gp5",
            "3gp6", "3gp7", "3gr6", "3gr7", "3gs6", "3gs7", "kddi"
        };
        private static readonly string[] _secondSignatureStrings = new string[] { "3g2a" };
        private static readonly byte?[][] _signatures;
        private static readonly byte?[][] _secondSignatures;

        static ThreeGPFormatScanner()
        {
            var signatures = new List<byte?[]>();

            foreach (var _signatureString in _signatureStrings)
            {
                var bytes = Encoding.ASCII.GetBytes(_signatureString).Cast<byte?>().ToList();

                while (bytes.Count != 4)
                {
                    bytes.Add(null);
                }

                signatures.Add(bytes.ToArray());
            }

            _signatures = signatures.ToArray();

            var secondSignatures = new List<byte?[]>();

            foreach (var _signatureString in _secondSignatureStrings)
            {
                var bytes = Encoding.ASCII.GetBytes(_signatureString).Cast<byte?>().ToList();

                while (bytes.Count != 4)
                {
                    bytes.Add(null);
                }

                secondSignatures.Add(bytes.ToArray());
            }

            _secondSignatures = secondSignatures.ToArray();
        }

        public ThreeGPFormatScanner()
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
                    var fingerprint = new ThreeGPFormat();

                    return fingerprint;
                }
            }

            foreach (var signature in _secondSignatures)
            {
                if (FileFormatUtils.MatchBytes(job.StartBytes, 8, signature))
                {
                    var fingerprint = new ThreeGP2Format();

                    return fingerprint;
                }
            }

            return null;
        }

        #endregion
    }
}
