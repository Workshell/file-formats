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

using Workshell.FileFormats.Scanners.Archives;
using Workshell.FileFormats.Scanners.Containers;
using Workshell.FileFormats.Scanners.EBooks;
using Workshell.FileFormats.Scanners.Executables;
using Workshell.FileFormats.Scanners.Images;
using Workshell.FileFormats.Scanners.Media;
using Workshell.FileFormats.Scanners.Microsoft;
using Workshell.FileFormats.Scanners.ODF;
using Workshell.FileFormats.Scanners.UOF;

namespace Workshell.FileFormats
{
    public abstract class FileFormat
    {
        private const int FourKilobytes = 1024 * 4; // 4K

        static FileFormat()
        {
            Scanners = new FileFormatScanners();

            // Others
            Scanners.Register(new PortableExecutableFormatScanner());

            ImageFormatScanners.Register();
            ArchiveFormatScanners.Register();
            MicrosoftFormatScanners.Register(); // Microsoft (Office et al)
            OpenDocumentFormatScanners.Register(); // Open Document Formats
            UnifiedOfficeFormatScanners.Register(); // Unified Office Formats
            ContainerFormatScanners.Register();
            MediaFormatScanners.Register();
            ExecutableFormatScanners.Register();
            EBookFormatScanners.Register();
            OtherFormatScanners.Register();
        }

        protected FileFormat(IEnumerable<string> contentTypes, IEnumerable<string> extensions, string description)
        {
            ContentTypes = contentTypes.ToArray();
            Extensions = extensions.ToArray();
            Description = description;
            SortIndex = 0;
        }

        #region Static Methods

        public static FileFormat Get(Stream stream)
        {
            // Get first 4K
            stream.Seek(0, SeekOrigin.Begin);

            var firstBytes = FileFormatUtils.ReadBytes(stream, FourKilobytes);

            // Get last 4K
            var lastBytes = firstBytes;

            if (stream.Length > FourKilobytes)
            {
                var offset = stream.Length - FourKilobytes;

                stream.Seek(offset, SeekOrigin.Begin);

                lastBytes = FileFormatUtils.ReadBytes(stream, FourKilobytes);
            }

            // Perform scan
            var job = new FileFormatScanJob(Scanners, firstBytes, lastBytes, stream);
            var fingerprint = job.Scan();

            return fingerprint;
        }

        public static FileFormat Get(string fileName)
        {
            using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Get(file);
            }
        }

        #endregion

        #region Static Properties

        public static FileFormatScanners Scanners { get; }

        #endregion

        #region Properties

        public string[] ContentTypes { get; }
        public string[] Extensions { get; }
        public string Description { get; }
        public virtual int SortIndex { get; }

        #endregion
    }
}
