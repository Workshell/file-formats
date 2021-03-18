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
using System.Threading.Tasks;

namespace Workshell.FileFormats
{
    public sealed class FileFormatScanJob
    {
        private readonly FileFormatScanner[] _scanners;

        internal FileFormatScanJob(IEnumerable<FileFormatScanner> scanners, byte[] startBytes, byte[] endBytes, Stream stream)
        {
            _scanners = scanners.ToArray();

            Cache = new FileFormatScanJobCache();
            StartBytes = startBytes;
            EndBytes = endBytes;
            Stream = stream;
        }

        #region Methods

        public FileFormat Scan()
        {
            var fingerprints = new List<FileFormat>();

            // Run each scanner
            foreach (var scanner in _scanners)
            {
                Stream.Seek(0, SeekOrigin.Begin);

                var fingerprint = scanner.Match(this);

                if (fingerprint == null)
                {
                    continue;
                }

                if (!fingerprints.Contains(fingerprint))
                {
                    fingerprints.Add(fingerprint);
                }
            }

            // Return
            return GetRelevantFileFormat(fingerprints);
        }

        public async Task<FileFormat> ScanAsync()
        {
            var fingerprints = new List<FileFormat>();

            // Run each scanner
            foreach (var scanner in _scanners)
            {
                Stream.Seek(0, SeekOrigin.Begin);

                var fingerprint = await scanner.MatchAsync(this);

                if (fingerprint == null)
                {
                    continue;
                }

                if (!fingerprints.Contains(fingerprint))
                {
                    fingerprints.Add(fingerprint);
                }
            }

            // Return
            return GetRelevantFileFormat(fingerprints);
        }

        private static FileFormat GetRelevantFileFormat(IList<FileFormat> fingerprints)
        {
            if (fingerprints.Count == 1)
            {
                return fingerprints.Single();
            }

            if (fingerprints.Count > 1)
            {
                var orderedFingerprints = fingerprints.OrderByDescending(fingerprint => fingerprint.SortIndex);

                return orderedFingerprints.First();
            }

            return null;
        }

        #endregion

        #region Properties

        public FileFormatScanJobCache Cache { get; }
        public byte[] StartBytes { get; }
        public byte[] EndBytes { get; }
        public Stream Stream { get; }

        #endregion
    }
}
