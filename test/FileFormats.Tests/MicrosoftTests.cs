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
using System.Reflection.Metadata;
using System.Text;

using NUnit.Framework;

using Workshell.FileFormats;
using Workshell.FileFormats.Formats.Microsoft;
using Workshell.FileFormats.Formats.Microsoft.Legacy;

namespace Workshell.FileFormats.Tests
{
    partial class FileFormatTests
    {
        #region Access (ACCDB/MDB)

        [Test]
        public void Office_AccessDatabase_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.accdb"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_AccessDatabase_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.accdb"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is AccessDatabaseFormat);
            }
        }

        [Test]
        public void Office_Legacy_AccessDatabase_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.mdb"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_Legacy_AccessDatabase_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.mdb"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyAccessDatabaseFormat);
            }
        }

        #endregion

        #region Word (DOC/DOT)

        [Test]
        public void Office_WordDocument_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.doc"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_WordDocument_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.doc"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyWordDocumentFormat);
            }
        }

        [Test]
        public void Office_WordDocumentTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.dot"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_WordDocumentTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.dot"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyWordDocumentFormat);
            }
        }

        #endregion

        #region Excel (XLS/XLT/XLA)

        [Test]
        public void Office_ExcelWorkbook_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.xls"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_ExcelWorkbook_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.xls"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyExcelWorkbookFormat);
            }
        }

        [Test]
        public void Office_ExcelWorkbookTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.xlt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_ExcelWorkbookTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.xlt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyExcelWorkbookFormat);
            }
        }

        [Test]
        public void Office_ExcelAddin_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.xla"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_ExcelAddin_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.xla"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyExcelWorkbookFormat);
            }
        }

        #endregion

        #region PowerPoint (PPT/POT/PPS/PPA)

        [Test]
        public void Office_PowerPointPresentation_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.ppt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_PowerPointPresentation_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.ppt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyPowerPointPresentationFormat);
            }
        }

        [Test]
        public void Office_PowerPointPresentationTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.pot"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_PowerPointPresentationTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.pot"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyPowerPointPresentationFormat);
            }
        }

        [Test]
        public void Office_PowerPointSlideshow_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.pps"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_PowerPointSlideshow_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.pps"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyPowerPointPresentationFormat);
            }
        }

        #endregion

        #region Visio (VSD/VST/VSS)

        [Test]
        public void Office_VisioDrawing_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.vsd"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_VisioDrawing_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.vsd"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyVisioDrawingFormat);
            }
        }

        [Test]
        public void Office_VisioDrawingTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.vst"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_VisioDrawingTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.vst"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyVisioDrawingFormat);
            }
        }

        [Test]
        public void Office_VisioStencil_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.vss"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_VisioStencil_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.vss"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is LegacyVisioDrawingFormat);
            }
        }

        #endregion

        #region Publisher (PUB)

        [Test]
        public void Office_Publisher_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_Publisher_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is PublisherFormat);
            }
        }

        [Test]
        public void Office_PublisherTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.template.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_PublisherTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.template.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is PublisherFormat);
            }
        }

        [Test]
        public void Office_Publisher98_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.1998.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_Publisher98_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.1998.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is PublisherFormat);
            }
        }

        [Test]
        public void Office_Publisher2K_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.2000.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_Publisher2K_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.2000.pub"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is PublisherFormat);
            }
        }

        #endregion

        #region Outlook (PST)

        [Test]
        public void Office_PST_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.pst"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_PST_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.pst"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is OutlookPSTFormat);
            }
        }

        #endregion
        
        #region Outlook (Message)
        
        [Test]
        public void Office_MSG_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("microsoft/test.msg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Office_MSG_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("microsoft/test.msg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is OutlookMessageFormat);
            }
        }
        
        #endregion
    }
}
