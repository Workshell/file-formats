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

namespace FileFormats.Tests
{
    partial class FileFormatTests
    {
        [Test]
        public void Other_Samples_Exist()
        {
            var files = new[]
            {
                "test.pdf",
                "test.ani",
                "test.swf",
                "test.xml"
            };

            foreach(var file in files)
                Assert.True(File.Exists(Path.Combine(_samplesDirectory, file)));
        }

        #region PDF

        [Test]
        public void Others_PDF_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.pdf");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Others_PDF_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.pdf");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is PDFFormat);
        }

        #endregion

        #region Animated Cursor (ANI)

        [Test]
        public void Others_AnimatedCursor_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.ani");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Others_AnimatedCursor_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.ani");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is AnimatedCursorFormat);
        }

        #endregion

        #region Adobe Flash (SWF)

        [Test]
        public void Others_AdobeFlash_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.swf");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Others_AdobeFlash_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.swf");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is FlashFormat);
        }

        #endregion

        #region XML

        [Test]
        public void Others_XML_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.xml");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Others_XML_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, "test.xml");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is XmlFormat);
        }

        #endregion
    }
}
