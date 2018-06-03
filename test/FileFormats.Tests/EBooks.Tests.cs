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
using Workshell.FileFormats.Formats;
using Workshell.FileFormats.Formats.EBooks;

namespace FileFormats.Tests
{
    partial class FileFormatTests
    {
        [Test]
        public void eBooks_Samples_Exist()
        {
            var files = new[]
            {
                @"ebook\test.epub",
                @"ebook\test.mobi",
            };

            foreach(var file in files)
                Assert.True(File.Exists(Path.Combine(_samplesDirectory, file)));
        }

        #region ePub

        [Test]
        public void eBooks_ePub_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"ebook\test.epub");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void eBooks_ePub_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"ebook\test.epub");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is EPubFormat);
        }

        #endregion

        #region Amazon/MobiPocket (MOBI)

        [Test]
        public void eBooks_Mobi_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"ebook\test.mobi");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void eBooks_Mobi_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"ebook\test.mobi");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is MobiFormat);
        }

        #endregion
    }
}
