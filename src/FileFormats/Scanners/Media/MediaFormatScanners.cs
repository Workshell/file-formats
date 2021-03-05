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

namespace Workshell.FileFormats.Scanners.Media
{
    internal static class MediaFormatScanners
    {
        private static readonly FileFormatScanner[] _scanners = new FileFormatScanner[]
        {
            new MatroskaFormatScanner(), 
            new WebMFormatScanner(),
            new MP4FormatScanner(),
            new M4AFormatScanner(),
            new M4VFormatScanner(),
            new ThreeGPFormatScanner(),
            new QuickTimeFormatScanner(),
            new FlashVideoFormatScanner(),
            new AVIFingerprintScanner(), 
            new WaveFormatScanner(),
            new OggFormatScanner(), 
            new AdvancedSystemsFormatScanner(),
            new AC3FormatScanner(),
            new AudioInterchangeFileFormatScanner(), 
            new FLACFormatScanner(), 
            new RealAudioFormatScanner(), 
            new BasicAudioFormatScanner(),
            new AMRFormatScanner(),
        };

        #region Methods

        public static void Register()
        {
            foreach (var scanner in _scanners)
            {
                FileFormat.Scanners.Register(scanner);
            }
        }

        #endregion
    }
}
