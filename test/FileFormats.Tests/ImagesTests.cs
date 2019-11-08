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
using Workshell.FileFormats.Formats.Images;

namespace Workshell.FileFormats.Tests
{
    partial class FileFormatTests
    {
        #region Bitmap (BMP)

        [Test]
        public void Images_Bitmap_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("image/test.bmp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Images_Bitmap_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("image/test.bmp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is BitmapImageFormat);
            }
        }

        #endregion

        #region Portable Network Graphic (PNG)

        [Test]
        public void Images_PNG_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("image/test.png"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Images_PNG_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("image/test.png"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is PNGImageFormat);
            }
        }

        #endregion

        #region JPEG

        [Test]
        public void Images_JPEG_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("image/test.jpg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Images_JPEG_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("image/test.jpg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is JPEGImageFormat);
            }
        }

        #endregion

        #region Graphics Interchange Format (GIF)

        [Test]
        public void Images_GIF_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("image/test.gif"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Images_GIF_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("image/test.gif"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is GIFImageFormat);
            }
        }

        #endregion

        #region TIFF

        [Test]
        public void Images_TIFF_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("image/test.tif"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Images_TIFF_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("image/test.tif"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is TIFFImageFormat);
            }
        }

        #endregion
    }
}
