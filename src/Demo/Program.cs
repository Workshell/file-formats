﻿#region License
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
using System.IO;

using Workshell.FileFormats.Formats;

namespace Workshell.FileFormats.Demonstration
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();

                return;
            }

            var fileName = args[0];

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Error: Cannot find file - " + fileName);
                Console.WriteLine();

                return;
            }

            var fingerprint = FileFormat.Get(fileName);

            if (fingerprint == null)
            {
                Console.WriteLine("Unknown format.");
                Console.WriteLine();
            }
            else
            {
                WriteFingerprint(fingerprint);
            }
        }

        static void Usage()
        {
            Console.WriteLine("Usage: demo.exe file");
            Console.WriteLine();
        }

        static void WriteFingerprint(FileFormat fingerprint)
        {
            var contentTypes = string.Join(", ", fingerprint.ContentTypes);
            var extensions = string.Join(", ", fingerprint.Extensions);

            Console.WriteLine($"Class         : {fingerprint.GetType().Name}");
            Console.WriteLine($"Content Types : {contentTypes}");
            Console.WriteLine($"Extensions    : {extensions}");
            Console.WriteLine();
        }
    }
}
