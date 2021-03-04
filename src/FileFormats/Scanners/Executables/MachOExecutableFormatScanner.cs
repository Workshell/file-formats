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
using System.Runtime.InteropServices;
using System.Threading;

using Workshell.FileFormats.Formats.Executables;

namespace Workshell.FileFormats.Scanners.Executables
{
    public class MachOExecutableFormatScanner : ExecutableFormatScanner
    {
        private uint MAGIC = 0xfeedface;    // 32-bit, big-endian
        private uint CIGAM = 0xcefaedfe;    // 32-bit, little-endian
        private uint MAGIC_64 = 0xfeedfacf; // 64-bit, big-endian
        private uint CIGAM_64 = 0xcffaedfe; // 64-bit, little-endian

        private static readonly int HeaderSize = FileFormatUtils.SizeOf<Header>();

        [StructLayout(LayoutKind.Sequential)]
        private struct Header 
        {
            public uint magic;      // Mach magic number identifier
            public uint cputype;    // CPU specifier
            public uint cpusubtype; // Machine specifier
            public uint filetype;   // Type of file
            public uint ncmds;      // Number of load commands
            public uint sizeofcmds; // Size of all the load commands
            public uint flags;      // Flags
        }

        public MachOExecutableFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
            {
                return null;
            }

            if (job.StartBytes.Length < 1024)
            {
                return null;
            }

            var header = FileFormatUtils.Read<Header>(job.StartBytes, 0, HeaderSize);
            var is32Bit = (header.magic == 0xfeedface || header.magic == 0xcefaedfe);
            var is64Bit = (header.magic == 0xfeedfacf || header.magic == 0xcffaedfe);

            if (!is32Bit && !is64Bit)
            {
                return null;
            }

            MachOImageFormat format;

            if (is32Bit)
            {
                format = MachOImageFormat._32Bit;
            }
            else
            {
                format = MachOImageFormat._64Bit;
            }

            if (is32Bit)
            {
                format = MachOImageFormat._32Bit;
            }

            if (is64Bit)
            {
                format = MachOImageFormat._64Bit;
            }

            var littleEnd = (header.magic == 0xcefaedfe || header.magic == 0xcffaedfe);
            var bigEnd = (header.magic == 0xfeedface || header.magic == 0xfeedfacf);

            if (!littleEnd && !bigEnd)
            {
                return null;
            }

            MachOImageEndianness endianness;

            if (littleEnd)
            {
                endianness = MachOImageEndianness.Little;
            }
            else
            {
                endianness = MachOImageEndianness.Big;
            }

            var fingerprint = new MachOExecutableFormat(format, endianness);

            return fingerprint;
        }

        #endregion
    }
}
