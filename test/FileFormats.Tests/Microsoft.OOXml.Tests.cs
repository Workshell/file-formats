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
using System.Reflection.Metadata;
using System.Text;

using NUnit.Framework;

using Workshell.FileFormats;
using Workshell.FileFormats.Formats.Microsoft.OOXml;

namespace FileFormats.Tests
{
    partial class FileFormatTests
    {
        [Test]
        public void OOXml_Samples_Exist()
        {
            var files = new[]
            {
                @"microsoft\ooxml\test.docx",
                @"microsoft\ooxml\test.docm",
                @"microsoft\ooxml\test.dotx",
                @"microsoft\ooxml\test.dotm",
                @"microsoft\ooxml\test.xlsx",
                @"microsoft\ooxml\test.xlsm",
                @"microsoft\ooxml\test.xltx",
                @"microsoft\ooxml\test.xltm",
                @"microsoft\ooxml\test.xlsb",
                @"microsoft\ooxml\test.xlam",
                @"microsoft\ooxml\test.pptx",
                @"microsoft\ooxml\test.pptm",
                @"microsoft\ooxml\test.potx",
                @"microsoft\ooxml\test.potm",
                @"microsoft\ooxml\test.ppsx",
                @"microsoft\ooxml\test.ppsm",
                @"microsoft\ooxml\test.vsdx",
                @"microsoft\ooxml\test.vsdm",
                @"microsoft\ooxml\test.vstx",
                @"microsoft\ooxml\test.vstm",
                @"microsoft\ooxml\test.vssx",
                @"microsoft\ooxml\test.vssm"
            };

            foreach(var file in files)
                Assert.True(File.Exists(Path.Combine(_samplesDirectory, file)));
        }

        #region DOCX/DOCM

        [Test]
        public void OOXml_Document_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.docx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_Document_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.docx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is WordDocumentFormat);
        }

        [Test]
        public void OOXml_Document_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.docx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (WordDocumentFormat) fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_DocumentWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.docm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_DocumentWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.docm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is WordDocumentFormat);
        }

        [Test]
        public void OOXml_DocumentWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.docm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (WordDocumentFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion
        
        #region DOTX/DOTM

        [Test]
        public void OOXml_DocumentTemplate_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.dotx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_DocumentTemplate_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.dotx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is WordDocumentTemplateFormat);
        }

        [Test]
        public void OOXml_DocumentTemplate_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.dotx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (WordDocumentTemplateFormat) fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_DocumentTemplateWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.dotm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_DocumentTemplateWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.dotm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is WordDocumentTemplateFormat);
        }

        [Test]
        public void OOXml_DocumentTemplateWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.dotm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (WordDocumentTemplateFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region XLSX/XLSM

        [Test]
        public void OOXml_Spreadsheet_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_Spreadsheet_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ExcelWorkbookFormat);
        }

        [Test]
        public void OOXml_Spreadsheet_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (ExcelWorkbookFormat)fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_SpreadsheetWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_SpreadsheetWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ExcelWorkbookFormat);
        }

        [Test]
        public void OOXml_SpreadsheetWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (ExcelWorkbookFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region XLTX/XLTM

        [Test]
        public void OOXml_SpreadsheetTemplate_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xltx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_SpreadsheetTemplate_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xltx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ExcelWorkbookTemplateFormat);
        }

        [Test]
        public void OOXml_SpreadsheetTemplate_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xltx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (ExcelWorkbookTemplateFormat) fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_SpreadsheetTemplateWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xltm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_SpreadsheetTemplateWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xltm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ExcelWorkbookTemplateFormat);
        }

        [Test]
        public void OOXml_SpreadsheetTemplateWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xltm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (ExcelWorkbookTemplateFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region XLSB/XLAM

        [Test]
        public void OOXml_BinarySpreadsheet_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsb");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_BinarySpreadsheet_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsb");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ExcelBinaryWorkbookFormat);
        }

        [Test]
        public void OOXml_BinarySpreadsheet_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlsb");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (ExcelBinaryWorkbookFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_ExcelAddin_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlam");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_ExcelAddin_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlam");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ExcelAddinFormat);
        }

        [Test]
        public void OOXml_ExcelAddin_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.xlam");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (ExcelAddinFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region PPTX/PPTM

        [Test]
        public void OOXml_Presentation_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.pptx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_Presentation_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.pptx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is PowerPointPresentationFormat);
        }

        [Test]
        public void OOXml_Presentation_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.pptx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (PowerPointPresentationFormat) fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_PresentationWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.pptm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_PresentationWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.pptm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is PowerPointPresentationFormat);
        }

        [Test]
        public void OOXml_PresentationWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.pptm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (PowerPointPresentationFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region POTX/POTM

        [Test]
        public void OOXml_PresentationTemplate_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.potx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_PresentationTemplate_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.potx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is PowerPointPresentationTemplateFormat);
        }

        [Test]
        public void OOXml_PresentationTemplate_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.potx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (PowerPointPresentationTemplateFormat)fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_PresentationTemplateWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.potm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_PresentationTemplateWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.potm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is PowerPointPresentationTemplateFormat);
        }

        [Test]
        public void OOXml_PresentationTemplateWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.potm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (PowerPointPresentationTemplateFormat)fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region PPSX/PPSM

        [Test]
        public void OOXml_Slideshow_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.ppsx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_Slideshow_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.ppsx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is PowerPointSlideshowFormat);
        }

        [Test]
        public void OOXml_Slideshow_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.ppsx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (PowerPointSlideshowFormat)fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_SlideshowWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.ppsm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_SlideshowWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.ppsm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is PowerPointSlideshowFormat);
        }

        [Test]
        public void OOXml_SlideshowWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.ppsm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (PowerPointSlideshowFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region VSDX/VSDM

        [Test]
        public void OOXml_Drawing_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vsdx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_Drawing_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vsdx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is VisioDrawingFormat);
        }

        [Test]
        public void OOXml_Drawing_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vsdx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (VisioDrawingFormat) fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_DrawingWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vsdm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_DrawingWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vsdm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is VisioDrawingFormat);
        }

        [Test]
        public void OOXml_DrawingWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vsdm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (VisioDrawingFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region VSTX/VSTM

        [Test]
        public void OOXml_DrawingTemplate_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vstx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_DrawingTemplate_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vstx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is VisioDrawingTemplateFormat);
        }

        [Test]
        public void OOXml_DrawingTemplate_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vstx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (VisioDrawingTemplateFormat) fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_DrawingTemplateWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vstm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_DrawingTemplateWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vstm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is VisioDrawingTemplateFormat);
        }

        [Test]
        public void OOXml_DrawingTemplateWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vstm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (VisioDrawingTemplateFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion

        #region VSSX/VSSM

        [Test]
        public void OOXml_Stencil_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vssx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_Stencil_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vssx");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is VisioStencilFormat);
        }

        [Test]
        public void OOXml_Stencil_Does_Not_Have_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vssx");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (VisioStencilFormat) fingerprint;

            Assert.False(documentFingerprint.Macros);
        }

        [Test]
        public void OOXml_StencilWithMacros_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vssm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void OOXml_StenvilWithMacros_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vssm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is VisioStencilFormat);
        }

        [Test]
        public void OOXml_StencilWithMacros_Has_Macros()
        {
            var fileName = Path.Combine(_samplesDirectory, @"microsoft\ooxml\test.vssm");
            var fingerprint = FileFormat.Get(fileName);
            var documentFingerprint = (VisioStencilFormat) fingerprint;

            Assert.True(documentFingerprint.Macros);
        }

        #endregion
    }
}
