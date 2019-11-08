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
using Workshell.FileFormats.Formats.Executables;

namespace Workshell.FileFormats.Tests
{
    partial class FileFormatTests
    {
        #region Portable Executables

        [Test]
        public void Executables_PortableExecutable_x86_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("executable/test-x86.exe"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Executables_PortableExecutable_x86_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("executable/test-x86.exe"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is PortableExecutableFormat);
            }
        }

        [Test]
        public void Executables_PortableExecutable_x86_Is_Correct_Image_Format()
        {
            using (var stream = GetFileFromZip("executable/test-x86.exe"))
            {
                var fingerprint = FileFormat.Get(stream);
                var peFingerprint = (PortableExecutableFormat) fingerprint;

                Assert.AreEqual(PEImageFormat._32Bit, peFingerprint.Format);
            }
        }

        [Test]
        public void Executables_PortableExecutable_x86_Is_Correct_CLR()
        {
            using (var stream = GetFileFromZip("executable/test-x86.exe"))
            {
                var fingerprint = FileFormat.Get(stream);
                var peFingerprint = (PortableExecutableFormat) fingerprint;

                Assert.AreEqual(true, peFingerprint.IsCLR);
            }
        }

        [Test]
        public void Executables_PortableExecutable_x64_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("executable/test-x64.exe"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Executables_PortableExecutable_x64_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("executable/test-x64.exe"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is PortableExecutableFormat);
            }
        }

        [Test]
        public void Executables_PortableExecutable_x64_Is_Correct_Image_Format()
        {
            using (var stream = GetFileFromZip("executable/test-x64.exe"))
            {
                var fingerprint = FileFormat.Get(stream);
                var peFingerprint = (PortableExecutableFormat) fingerprint;

                Assert.AreEqual(PEImageFormat._64Bit, peFingerprint.Format);
            }
        }

        [Test]
        public void Executables_PortableExecutable_x64_Is_Correct_CLR()
        {
            using (var stream = GetFileFromZip("executable/test-x64.exe"))
            {
                var fingerprint = FileFormat.Get(stream);
                var peFingerprint = (PortableExecutableFormat) fingerprint;

                Assert.AreEqual(true, peFingerprint.IsCLR);
            }
        }

        #endregion

        #region ELF Executables

        [Test]
        public void Executables_ELF_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("executable/test_elf_x64"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Executables_ELF_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("executable/test_elf_x64"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is ELFExecutableFormat);
            }
        }

        [Test]
        public void Executables_ELF_Is_Correct_Image_Format()
        {
            using (var stream = GetFileFromZip("executable/test_elf_x64"))
            {
                var fingerprint = FileFormat.Get(stream);
                var elfFingerprint = (ELFExecutableFormat) fingerprint;

                Assert.AreEqual(ELFImageFormat._64Bit, elfFingerprint.Format);
            }
        }

        #endregion

        #region Mach-O Executables

        [Test]
        public void Executables_MachO_Returns_Not_Null()
        {
            using (var stream = GetFileFromZip("executable/test_macho_x64"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.IsNotNull(fingerprint);
            }
        }

        [Test]
        public void Executables_MachO_Is_Correct_Format()
        {
            using (var stream = GetFileFromZip("executable/test_macho_x64"))
            {
                var fingerprint = FileFormat.Get(stream);

                Assert.True(fingerprint is MachOExecutableFormat);
            }
        }

        [Test]
        public void Executables_MachO_Is_Correct_Image_Format()
        {
            using (var stream = GetFileFromZip("executable/test_macho_x64"))
            {
                var fingerprint = FileFormat.Get(stream);
                var machFingerprint = (MachOExecutableFormat) fingerprint;

                Assert.AreEqual(MachOImageFormat._64Bit, machFingerprint.Format);
            }
        }

        #endregion
    }
}
