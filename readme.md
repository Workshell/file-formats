# Workshell.FileFormats

[![License](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/Workshell/file-formats/blob/master/license.txt)
[![NuGet](https://img.shields.io/nuget/v/Workshell.FileFormats.svg)](https://www.nuget.org/packages/Workshell.FileFormats/)
[![Build Status](https://dev.azure.com/Workshell-DevOps/file-formats/_apis/build/status/Build%20Master?branchName=master)](https://dev.azure.com/Workshell-DevOps/file-formats/_build/latest?definitionId=3&branchName=master)

A class library for discovering the format of files. Useful when receiving data from end-users when you cannot always rely on
the content type specified (or trust it).

The library is extensible, it can support further formats.


## Installation

Stable builds are available as NuGet packages. You can install it via the Package Manager or via the Package Manager Console:

```
Install-Package Workshell.FileFormats
```


## Usage

You can call `FileFormat.Get(string)` with a file name to attempt to disocver the format of a file, such as:

```
var format = FileFormat.Get(@"C:\Windows\explorer.exe");
```

You can also call `FileFormat.Get(Stream)` on a stream, such as:

```
var file = new FileStream(@"C:\Windows\explorer.exe", FileMode.Open, FileAccess.Read);
var format = FileFormat.Get(file);
```

Note that the `Stream` instance should support seeking as the stream will need to be rewound during the scanning process.

If a format could not be found then a simple `null` will be returned. If it is found then you can test the return type directly, such as:

```
if (format is PortableExecutableFormat)
{
    // Do something...
}
```

Formats can also take advantage of inheritance, for example the Office Open XML Word Document format has the following chain:

```
WordDocumentFormat -> OfficeZipFormat -> ZipFormat -> FileFormat
```

This means you can check further up the chain. For example:

```
if (format is ZipFormat)
{
    // Do something...
}
```

The above example would cover _any_ file that's detected as being ZIP based, which includes Office Open XML and the Open Document Format.


## Extending

In order to add support for a new format you first need to define a subclass of `FileFormat` which defines the content types and
file extensions of matched data.

Take for example the PDF format:

```
public class PDFFormat : FileFormat
{
    private static string[] _contentTypes => new[] 
    { 
        "application/pdf",
        "application/x-pdf"
    };
    private static string[] _extensions => new[] { "pdf" };

    public PDFFormat() : base(_contentTypes, _extensions)
    {
    }

    public override int SortIndex => 10;
}
```

You will also note the `SortIndex` property. This is useful for files that count as two different types.

For example, Office Open XML file types are based on ZIP files, so they are both ZIP files _and_ Office files.
When scanning we want the OOXML format to _rank_ higher than the ZIP format, so we give it a higher sort index.

Once you have your `FileFormat` class you then need to subclass `FileFormatScanner` which is used to perform the actual data scanning.

The `FileFormatScanner` class looks like this:

```
public abstract class FileFormatScanner
{
    public abstract FileFormat Match(FileFormatScanJob job);
}
```

It only has one method you need to override, `FileFormatScanner.Match(FileFormatScanJob)`.

The `FileFormatScanJob` instance supplied to the method contains the first and last 4KB of the data being scanned and a stream.
You use these to perform the analysis required to determine the format of the data.

The 7-Zip scanner for example looks like this:

```
public class SevenZipFormatScanner : FileFormatScanner
{
    private static readonly byte?[] Signature = new byte?[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C };

    public SevenZipFormatScanner()
    {
    }

    public override FileFormat Match(FileFormatScanJob job)
    {
        if (FileFormatUtils.IsNullOrEmpty(job.StartBytes))
            return null;

        if (job.StartBytes.Length <= Signature.Length)
            return null;

        if (!FileFormatUtils.MatchBytes(job.StartBytes, Signature))
            return null;

        var fingerprint = new SevenZipFormat();

        return fingerprint;
    }
}
```

Take a look at the existing formats for reference implementations.


## Recognised File Formats

||Name|Example Extension(s)
|-|-|-
|Archive Formats|7-Zip|.7z
||BZip|.bz2
||Cabinet|.cab
||RAR|.rar
||Zip|.zip<br/>.zipx
|Containers|Java Archive|.jar
||NuGet Package|.nupkg
|eBooks|ePub|.epub
||Amazon/MobiPocket eBook|.mobi
|Executables|Executable and Linkable Format (Linux etc)|.axf<br/>.bin<br/>.elf<br/>.o<br/>.prx<br/>.puff<br/>.ko<br/>.mod<br/>.so
||Mach-O (macOS, iOS etc)|.o<br/>.dylib<br/>.bundle
||Portable Executable (Windows, .NET)|.exe<br/>.dll<br/>.ocx<br/>.scr<br/>.cpl<br/>.sys
|Graphics|Bitmap|.bmp
||Graphics Interchange Format|.gif
||JPEG|.jpg<br/>.jpeg
||Portable Network Graphic|.png
||Tagged Image File Format|.tif<br/>.tiff
|Media|3GP|.3gp
||3GP2|.3g2
||Adaptive Multi-Rate Audio|.amr<br/>.3ga
||Advanced Systems Format (Windows Media)|.asf<br/>.wmv<br/>.wma
||Audio Video Interleaved|.avi
||Audio Interchange File Format|.aiff<br/>.aif<br/>.aifc
||Basic Audio|.au
||Dolby AC-3|.ac3
||Flash Video|.flv
||Free Lossless Audio Codec|.flac
||MPEG-4 Part 14 (MP4)|.mp4
||Matroska|.mkv<br/>.mka<br/>.mks<br/>.mk3d
||M4V|.m4v
||M4A|.m4a
||Ogg|.ogg<br/>.ogv<br/>.oga<br/>.ogx<br/>.ogm<br/>.spx<br/>.opus
||QuickTime|.mov<br/>.qt
||WebM|.webm
||Wave Audio|.wav
||RealAudio|.rm<br/>.ram
|Microsoft Office|Access|.mdb<br/>.accdb
||Excel Workbook, Template or Add-In|.xls<br/>.xlt<br/>xla
||Outlook|.pst
||PowerPoint Presentation, Template, Slideshow or Add-In|.ppt<br/>.pot<br/>.pps<br/>.ppa
||Publisher|.pub
||Visio Drawing, Template or Stencil|.vsd<br/>.vst<br/>.vss
||Word Document or Template|.doc<br/>.dot
|Microsoft Office (OpenXML)|Excel Add-In|.xlam
||Excel Binary Workbook|.xlsb
||Excel Workbook|.xlsx<br/>.xlsm
||Excel Workbook Template|.xtlx<br/>.xltm
||PowerPoint Add-In|.ppam
||PowerPoint Presentation|.pptx<br/>.pptm
||PowerPoint Presentation Template|.potx<br/>.potm
||PowerPoint Slideshow|.ppsx<br/>.ppsm
||Visio Drawing|.vsdx<br/>.vsdm
||Visio Drawing Template|.vstx<br/>.vstm
||Visio Stencil|.vssx<br/>.vssm
||Word Document|.docx<br/>.docm
||Word Document Template|.dotx<br/>.dotm
|OpenOffice|Chart|.odc
||Chart Template|.otc
||Database|.odb
||Document|.odt
||Document Template|.ott
||Drawing|.odg
||Drawing Template|.otg
||Formula|.odf
||Formula Template|.otf
||Image|.odi
||Image Template|.oti
||Master Document|.odm
||Presentation|.odp
||Presentation Template|.otp
||Spreadsheet|.ods
||Spreadsheet Template|.odt
|OpenOffice (Flat)|Document|.fodt
||Drawing|.fodg
||Presentation|.fodp
||Spreadsheet|.fods
|Unified Office Format|Document|.uot<br/>.uof
||Presentation|.uop<br/>.uof
||Spreadsheet|.uos<br/>.uof
|Others|Animated Cursor|.ani
||Flash|.swf
||Portable Document Format|.pdf
||eXtensible Markup Language (XML)|.xml

If you think there's a common format we should cover then please do let us know and we'll try and add support for it.


## Attribution

We currently use a modified and internalised variant of OpenMCDF for reading some file formats, especially legacy Microsoft Office files.

The original version of OpenMCDF is available here:

* https://github.com/CodeCavePro/OpenMCDF


## MIT License

Copyright (c) Workshell Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.