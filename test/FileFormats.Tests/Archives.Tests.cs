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
using System.Text;

using NUnit.Framework;

using Workshell.FileFormats;
using Workshell.FileFormats.Formats.Archives;

namespace FileFormats.Tests
{
    partial class FileFormatTests
    {
        [Test]
        public void Archives_Samples_Exist()
        {
            var files = new[]
            {
                @"archive\test.zip",
                @"archive\test.gz",
                @"archive\test.7z",
                @"archive\test.bz2",
                @"archive\test.rar",
                @"archive\test.cab"
            };

            foreach(var file in files)
                Assert.True(File.Exists(Path.Combine(_samplesDirectory, file)));
        }

        #region ZIP

        [Test]
        public void Archives_Zip_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.zip");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Archives_Zip_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.zip");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ZipFormat);
        }

        #endregion

        #region GZ

        [Test]
        public void Archives_GZip_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.gz");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Archives_GZip_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.gz");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is GZipFormat);
        }

        #endregion

        #region 7Z

        [Test]
        public void Archives_SevenZip_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.7z");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Archives_SevenZip_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.7z");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is SevenZipFormat);
        }

        #endregion

        #region BZ2

        [Test]
        public void Archives_BZip_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.bz2");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Archives_BZip_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.bz2");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is BZipFormat);
        }

        #endregion

        #region RAR

        [Test]
        public void Archives_Rar_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.rar");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Archives_Rar_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.rar");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is RarFormat);
        }

        #endregion

        #region CAB

        [Test]
        public void Archives_Cabinets_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.cab");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Archives_Cabinets_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"archive\test.cab");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is CabinetFormat);
        }

        #endregion
    }
}
