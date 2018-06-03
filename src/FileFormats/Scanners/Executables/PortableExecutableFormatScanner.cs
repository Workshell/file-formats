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
using System.Runtime.InteropServices;
using Workshell.FileFormats.Formats.Executables;

namespace Workshell.FileFormats.Scanners.Executables
{
    public class PortableExecutableFormatScanner : ExecutableFormatScanner
    {
        private const ushort DOSMagicNumber = 23117;
        private const uint PEMagicNumber = 17744;

        private static readonly int DOSHeaderSize = Utils.SizeOf<DOSHeader>();
        private static readonly int FileHeaderSize = Utils.SizeOf<FileHeader>();
        private static readonly int OptionalHeader32Size = Utils.SizeOf<OptionalHeader32>();
        private static readonly int OptionalHeader64Size = Utils.SizeOf<OptionalHeader64>();

        [StructLayout(LayoutKind.Sequential)]
        private struct DOSHeader 
        {
            public ushort e_magic;              // Magic number
            public ushort e_cblp;               // Bytes on last page of file
            public ushort e_cp;                 // Pages in file
            public ushort e_crlc;               // Relocations
            public ushort e_cparhdr;            // Size of header in paragraphs
            public ushort e_minalloc;           // Minimum extra paragraphs needed
            public ushort e_maxalloc;           // Maximum extra paragraphs needed
            public ushort e_ss;                 // Initial (relative) SS value
            public ushort e_sp;                 // Initial SP value
            public ushort e_csum;               // Checksum
            public ushort e_ip;                 // Initial IP value
            public ushort e_cs;                 // Initial (relative) CS value
            public ushort e_lfarlc;             // File address of relocation table
            public ushort e_ovno;               // Overlay number
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] e_res_1;            // Reserved words
            public ushort e_oemid;              // OEM identifier (for e_oeminfo)
            public ushort e_oeminfo;            // OEM information; e_oemid specific
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public ushort[] e_res_2;            // Reserved words
            public int e_lfanew;                // File address of new exe header
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FileHeader
        {
            public uint Signature;
            public ushort Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public ushort Characteristics;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DataDirectory
        {
            public uint VirtualAddress;
            public uint Size;
        }

        private enum MagicType
        {
            PE32 = 0x10b,
            PE32plus = 0x20b
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OptionalHeader32
        {
            public ushort Magic;
            public byte MajorLinkerVersion;
            public byte MinorLinkerVersion;
            public uint SizeOfCode;
            public uint SizeOfInitializedData;
            public uint SizeOfUninitializedData;
            public uint AddressOfEntryPoint;
            public uint BaseOfCode;
            public uint BaseOfData;
            public uint ImageBase;
            public uint SectionAlignment;
            public uint FileAlignment;
            public ushort MajorOperatingSystemVersion;
            public ushort MinorOperatingSystemVersion;
            public ushort MajorImageVersion;
            public ushort MinorImageVersion;
            public ushort MajorSubsystemVersion;
            public ushort MinorSubsystemVersion;
            public uint Win32VersionValue;
            public uint SizeOfImage;
            public uint SizeOfHeaders;
            public uint CheckSum;
            public ushort Subsystem;
            public ushort DllCharacteristics;
            public uint SizeOfStackReserve;
            public uint SizeOfStackCommit;
            public uint SizeOfHeapReserve;
            public uint SizeOfHeapCommit;
            public uint LoaderFlags;
            public uint NumberOfRvaAndSizes;

            public DataDirectory ExportTable;
            public DataDirectory ImportTable;
            public DataDirectory ResourceTable;
            public DataDirectory ExceptionTable;
            public DataDirectory CertificateTable;
            public DataDirectory BaseRelocationTable;
            public DataDirectory Debug;
            public DataDirectory Architecture;
            public DataDirectory GlobalPtr;
            public DataDirectory TLSTable;
            public DataDirectory LoadConfigTable;
            public DataDirectory BoundImport;
            public DataDirectory IAT;
            public DataDirectory DelayImportDescriptor;
            public DataDirectory CLRRuntimeHeader;
            public DataDirectory Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OptionalHeader64
        {
            public ushort Magic;
            public byte MajorLinkerVersion;
            public byte MinorLinkerVersion;
            public uint SizeOfCode;
            public uint SizeOfInitializedData;
            public uint SizeOfUninitializedData;
            public uint AddressOfEntryPoint;
            public uint BaseOfCode;
            public ulong ImageBase;
            public uint SectionAlignment;
            public uint FileAlignment;
            public ushort MajorOperatingSystemVersion;
            public ushort MinorOperatingSystemVersion;
            public ushort MajorImageVersion;
            public ushort MinorImageVersion;
            public ushort MajorSubsystemVersion;
            public ushort MinorSubsystemVersion;
            public uint Win32VersionValue;
            public uint SizeOfImage;
            public uint SizeOfHeaders;
            public uint CheckSum;
            public ushort Subsystem;
            public ushort DllCharacteristics;
            public ulong SizeOfStackReserve;
            public ulong SizeOfStackCommit;
            public ulong SizeOfHeapReserve;
            public ulong SizeOfHeapCommit;
            public uint LoaderFlags;
            public uint NumberOfRvaAndSizes;

            public DataDirectory ExportTable;
            public DataDirectory ImportTable;
            public DataDirectory ResourceTable;
            public DataDirectory ExceptionTable;
            public DataDirectory CertificateTable;
            public DataDirectory BaseRelocationTable;
            public DataDirectory Debug;
            public DataDirectory Architecture;
            public DataDirectory GlobalPtr;
            public DataDirectory TLSTable;
            public DataDirectory LoadConfigTable;
            public DataDirectory BoundImport;
            public DataDirectory IAT;
            public DataDirectory DelayImportDescriptor;
            public DataDirectory CLRRuntimeHeader;
            public DataDirectory Reserved;
        }

        public PortableExecutableFormatScanner()
        {
        }

        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (Utils.IsNullOrEmpty(job.StartBytes))
                return null;

            if (job.StartBytes.Length < DOSHeaderSize + 64 + FileHeaderSize + OptionalHeader64Size)
                return null;

            var dosHeader = Utils.Read<DOSHeader>(job.StartBytes, 0, DOSHeaderSize);

            if (dosHeader.e_magic != DOSMagicNumber)
                return null;

            if (dosHeader.e_lfanew == 0 || 
                dosHeader.e_lfanew >= (256 * (1024 * 1024)) || 
                dosHeader.e_lfanew % 4 != 0 || 
                dosHeader.e_lfanew < DOSHeaderSize || 
                dosHeader.e_lfanew >= job.StartBytes.Length)
                return null;

            if (dosHeader.e_lfanew < DOSHeaderSize)
                return null;

            var stubOffset = DOSHeaderSize;
            var stubSize = dosHeader.e_lfanew - DOSHeaderSize;

            if ((stubOffset + stubSize) >= job.StartBytes.Length)
                return null;

            var fileHeaderOffset = stubOffset + stubSize;
            var fileHeader = Utils.Read<FileHeader>(job.StartBytes, fileHeaderOffset, FileHeaderSize);

            if (fileHeader.Signature != PEMagicNumber)
                return null;

            var optHeaderOffset = fileHeaderOffset + FileHeaderSize;
            var magic = Utils.ReadUInt16(job.StartBytes, optHeaderOffset);
            var is32bit = (magic == (ushort)MagicType.PE32);
            var is64bit = (magic == (ushort)MagicType.PE32plus);

            if (!is32bit && !is64bit)
                return null;

            DataDirectory clrDataDirectory;

            if (is32bit)
            {
                if (optHeaderOffset + OptionalHeader32Size >= job.StartBytes.Length)
                    return null;

                var optHeader = Utils.Read<OptionalHeader32>(job.StartBytes, optHeaderOffset, OptionalHeader32Size);

                clrDataDirectory = optHeader.CLRRuntimeHeader;
            }
            else
            {
                if (optHeaderOffset + OptionalHeader64Size >= job.StartBytes.Length)
                    return null;

                var optHeader = Utils.Read<OptionalHeader64>(job.StartBytes, optHeaderOffset, OptionalHeader64Size);

                clrDataDirectory = optHeader.CLRRuntimeHeader;
            }

            var isCLR = (clrDataDirectory.Size > 0 && clrDataDirectory.VirtualAddress > 0);
            var fingerprint = new PortableExecutableFormat(is32bit ? PEImageFormat._32Bit : PEImageFormat._64Bit, isCLR);

            return fingerprint;
        }

        #endregion
    }
}
