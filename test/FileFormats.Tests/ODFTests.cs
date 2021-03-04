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
using Workshell.FileFormats.Formats.ODF;

namespace Workshell.FileFormats.Tests
{
    partial class FileFormatTests
    {
        #region Document (ODT/OTT/FODT)

        [Test]
        public void ODF_Document_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.odt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_Document_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.odt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFDocumentFormat);
            }
        }

        [Test]
        public void ODF_DocumentTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.ott"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_DocumentTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.ott"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFDocumentTemplateFormat);
            }
        }

        [Test]
        public void ODF_FlatDocument_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.fodt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_FlatDocument_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.fodt"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFFlatDocumentFormat);
            }
        }

        #endregion

        #region Spreadsheet (ODS/OTS)

        [Test]
        public void ODF_Spreadsheet_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.ods"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_Spreadsheet_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.ods"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFSpreadsheetFormat);
            }
        }

        [Test]
        public void ODF_SpreadsheetTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.ots"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_SpreadsheetTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.ots"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFSpreadsheetTemplateFormat);
            }
        }

        [Test]
        public void ODF_FlatSpreadsheet_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.fods"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_FlatSpreadsheet_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.fods"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFFlatSpreadsheetFormat);
            }
        }

        #endregion

        #region Presentation (ODP/OTP)

        [Test]
        public void ODF_Presentation_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.odp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_Presentation_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.odp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFPresentationFormat);
            }
        }

        [Test]
        public void ODF_PresentationTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.otp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_PresentationTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.otp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFPresentationTemplateFormat);
            }
        }

        [Test]
        public void ODF_FlatPresentation_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.fodp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_FlatPresentation_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.fodp"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFFlatPresentationFormat);
            }
        }

        #endregion

        #region Drawing (ODG/OTG)

        [Test]
        public void ODF_Drawing_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.odg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_Drawing_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.odg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFDrawingFormat);
            }
        }

        [Test]
        public void ODF_DrawingTemplate_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.otg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_DrawingTemplate_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.otg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFDrawingTemplateFormat);
            }
        }

        [Test]
        public void ODF_FlatDrawing_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.fodg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_FlatDrawing_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.fodg"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFFlatDrawingFormat);
            }
        }

        #endregion

        #region Chart (ODC/OTC)

        [Test]
        public void ODF_Chart_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.odc"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_Chart_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.odc"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFChartFormat);
            }
        }

        #endregion

        #region Formula (ODF/OTF)

        [Test]
        public void ODF_Formula_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.odf"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_Formula_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.odf"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFFormulaFormat);
            }
        }

        #endregion

        #region Formula (ODI/OTI)

        /*
        [Test]
        public void ODF_Image_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"odf\test.odi");
            var fingerprint = Fingerprint.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void ODF_Image_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"odf\test.oti");
            var fingerprint = Fingerprint.Get(fileName);

            Assert.True(fingerprint is ODFImageFingerprint);
        }
        */

        #endregion

        #region Master Document (ODM)

        [Test]
        public void ODF_MasterDocument_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.odm"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_MasterDocument_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.odm"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFMasterDocumentFormat);
            }
        }

        #endregion

        #region Database (ODB)

        [Test]
        public void ODF_Database_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("odf/test.odb"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void ODF_Database_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("odf/test.odb"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ODFDatabaseFormat);
            }
        }

        #endregion
    }
}
