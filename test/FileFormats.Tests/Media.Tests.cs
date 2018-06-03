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
using Workshell.FileFormats.Formats.Media;

namespace FileFormats.Tests
{
    partial class FileFormatTests
    {
        [Test]
        public void Media_Samples_Exist()
        {
            var files = new[]
            {
                @"video\test.mkv",
                @"audio\test.mka",
                @"video\test.webm",
                @"video\test.mp4",
                @"audio\test.m4a",
                @"video\test.m4v",
                @"video\test.3gp",
                @"video\test.3g2",
                @"video\test.mov",
                @"video\test.flv",
                @"video\test.avi",
                @"audio\test.wav",
                @"video\test.ogv",
                @"audio\test.oga",
                @"video\test.wmv",
                @"audio\test.wma",
                @"audio\test.ac3",
                @"audio\test.aiff",
                @"audio\test.flac",
                @"audio\test.ra",
                @"audio\test.au"
            };

            foreach(var file in files)
                Assert.True(File.Exists(Path.Combine(_samplesDirectory, file)));
        }

        #region Matroska (MKV/MKA)

        [Test]
        public void Media_Matroska_Video_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.mkv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_Matroska_Video_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.mkv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is MatroskaFormat);
        }

        [Test]
        public void Media_Matroska_Audio_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.mka");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_Matroska_Audio_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.mka");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is MatroskaFormat);
        }

        #endregion

        #region WebM

        [Test]
        public void Media_WebM_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.webm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_WebM_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.webm");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is WebMFormat);
        }

        #endregion

        #region MP4

        [Test]
        public void Media_MP4_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.mp4");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_MP4_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.mp4");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is MP4Format);
        }

        #endregion

        #region M4A

        [Test]
        public void Media_M4A_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.m4a");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_M4A_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.m4a");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is M4AFormat);
        }

        #endregion

        #region M4V

        [Test]
        public void Media_M4V_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.m4v");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_M4V_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.m4v");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is M4VFormat);
        }

        #endregion

        #region 3GP/3G2

        [Test]
        public void Media_3GP_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.3gp");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_3GP_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.3gp");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ThreeGPFormat);
        }

        [Test]
        public void Media_3G2_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.3g2");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_3G2_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.3g2");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is ThreeGP2Format);
        }

        #endregion

        #region QuickTime (MOV/QT)

        [Test]
        public void Media_QuickTime_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.mov");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_QuickTime_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.mov");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is QuickTimeFormat);
        }

        #endregion

        #region Flash Video (FLV)

        [Test]
        public void Media_FlashVideo_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.flv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_FlashVideo_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.flv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is FlashVideoFormat);
        }

        #endregion

        #region Audio Vido Interleaved (AVI)

        [Test]
        public void Media_AVI_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.avi");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_AVI_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.avi");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is AVIFormat);
        }

        #endregion

        #region Wave Audio (WAV)

        [Test]
        public void Media_WAV_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.wav");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_WAV_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.wav");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is WaveFormat);
        }

        #endregion

        #region Ogg (OGG/OGV/OGA)

        [Test]
        public void Media_Ogg_Audio_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.oga");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_Ogg_Audio_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.oga");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is OggFormat);
        }

        [Test]
        public void Media_Ogg_Video_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.ogv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_Ogg_Video_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.ogv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is OggFormat);
        }

        #endregion

        #region Advanced Systems Format (ASF/WMV/WMA)

        [Test]
        public void Media_ASF_Audio_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.wma");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_ASF_Audio_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.wma");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is AdvancedSystemsFormat);
        }

        [Test]
        public void Media_ASF_Video_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.wmv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_ASF_Video_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"video\test.wmv");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is AdvancedSystemsFormat);
        }

        #endregion

        #region Dolby Digital Audio (AC3)

        [Test]
        public void Media_Dolby_AC3_Audio_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.ac3");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_Dolby_AC3_Audio_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.ac3");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is AC3Format);
        }

        #endregion

        #region Audio Interchange File Format (AIFF)

        [Test]
        public void Media_AIFF_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.aiff");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_AIFF_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.aiff");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is AIFFFormat);
        }

        #endregion

        #region Free Lossless Audio Codec (FLAC)

        [Test]
        public void Media_FLAC_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.flac");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_FLAC_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.flac");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is FLACFormat);
        }

        #endregion

        #region RealAudio (RA)

        [Test]
        public void Media_RealAudio_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.ra");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_RealAudio_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.ra");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is RealAudioFormat);
        }

        #endregion

        #region Basic Audio (AU)

        [Test]
        public void Media_BasicAudio_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.au");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_BasicAudio_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.au");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is BasicAudioFormat);
        }

        #endregion

        #region Adaptive Multi-Rate (AMR)

        [Test]
        public void Media_AMR_Returns_Not_Null()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.amr");
            var fingerprint = FileFormat.Get(fileName);

            Assert.IsNotNull(fingerprint);
        }

        [Test]
        public void Media_AMR_Is_Correct_Format()
        {
            var fileName = Path.Combine(_samplesDirectory, @"audio\test.amr");
            var fingerprint = FileFormat.Get(fileName);

            Assert.True(fingerprint is AMRFormat);
        }

        #endregion
    }
}
