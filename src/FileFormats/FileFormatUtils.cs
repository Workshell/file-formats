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
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;

namespace Workshell.FileFormats
{
    public static class FileFormatUtils
    {
        #region Methods

        public static int SizeOf<T>() where T : struct
        {
            #if NET45
            var result = Marshal.SizeOf(typeof(T));
            #else
            var result = Marshal.SizeOf<T>();
            #endif

            return result;
        }

        public static T Read<T>(byte[] bytes) where T : struct
        {
            return Read<T>(bytes, 0, bytes.Length);
        }

        public static T Read<T>(byte[] bytes, int offset, int length) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(length);

            try
            {
                Marshal.Copy(bytes, offset, ptr, length);

                #if NET45
                T result = (T)Marshal.PtrToStructure(ptr, typeof(T));
                #else
                T result = Marshal.PtrToStructure<T>(ptr);
                #endif

                return result;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public static T Read<T>(Stream stream) where T : struct
        {
            var size = SizeOf<T>();

            return Read<T>(stream,size);
        }

        public static T Read<T>(Stream stream, int size) where T : struct
        {
            return Read<T>(stream,size,false);
        }

        public static T Read<T>(Stream stream, int size, bool allowSmaller) where T : struct
        {
            var buffer = new byte[size];           
            var numRead = stream.Read(buffer,0,buffer.Length);

            if (!allowSmaller && numRead < size)
            {
                throw new IOException("Could not read all of structure from stream.");
            }

            if (numRead < size)
            {
                return default(T);
            }

            return Read<T>(buffer);
        }

        public static byte ReadByte(byte[] bytes, int index)
        {
            return bytes[index];
        }

        public static byte ReadByte(Stream stream)
        {
            var buffer = new byte[1];

            stream.Read(buffer, 0, buffer.Length);

            return ReadByte(buffer, 0);
        }

        public static short ReadInt16(byte[] bytes, int startIndex = 0, bool bigEndian = false)
        {
            if (bytes == null || bytes.Length != sizeof(short))
            {
                throw new ArgumentException("Invalid bytes specified.", nameof(bytes));
            }

            var buffer = new byte[sizeof(short)];
            var idx = 0;

            for (var i = startIndex; i < (startIndex + buffer.Length); i++)
            {
                buffer[idx] = bytes[i];
                idx++;
            }

            if (bigEndian)
            {
                buffer = Swap(buffer);
            }

            return BitConverter.ToInt16(buffer, 0);
        }

        public static short ReadInt16(Stream stream, bool bigEndian = false)
        {
            var buffer = ReadBytes(stream, sizeof(short));

            return ReadInt16(buffer, 0, bigEndian);
        }

        public static int ReadInt32(byte[] bytes, int startIndex = 0, bool bigEndian = false)
        {
            if (bytes == null || bytes.Length != sizeof(int))
            {
                throw new ArgumentException("Invalid bytes specified.", nameof(bytes));
            }

            var buffer = new byte[sizeof(int)];
            var idx = 0;

            for (var i = startIndex; i < (startIndex + buffer.Length); i++)
            {
                buffer[idx] = bytes[i];
                idx++;
            }

            if (bigEndian)
            {
                buffer = Swap(buffer);
            }

            return BitConverter.ToInt32(buffer, 0);
        }

        public static int ReadInt32(Stream stream, bool bigEndian = false)
        {
            var buffer = ReadBytes(stream, sizeof(int));

            return ReadInt32(buffer, 0, bigEndian);
        }

        public static long ReadInt64(byte[] bytes, int startIndex = 0, bool bigEndian = false)
        {
            if (bytes == null || bytes.Length != sizeof(long))
            {
                throw new ArgumentException("Invalid bytes specified.", nameof(bytes));
            }

            var buffer = new byte[sizeof(long)];
            var idx = 0;

            for (var i = startIndex; i < (startIndex + buffer.Length); i++)
            {
                buffer[idx] = bytes[i];
                idx++;
            }

            if (bigEndian)
            {
                buffer = Swap(buffer);
            }

            return BitConverter.ToInt64(buffer, 0);
        }

        public static long ReadInt64(Stream stream, bool bigEndian = false)
        {
            var buffer = ReadBytes(stream, sizeof(long));

            return ReadInt64(buffer, 0, bigEndian);
        }

        public static ushort ReadUInt16(byte[] bytes, int startIndex = 0, bool bigEndian = false)
        {
            if (bytes == null || bytes.Length < sizeof(ushort))
            {
                throw new ArgumentException("Invalid bytes specified.", nameof(bytes));
            }

            var buffer = new byte[sizeof(ushort)];
            var idx = 0;

            for (var i = startIndex; i < (startIndex + buffer.Length); i++)
            {
                buffer[idx] = bytes[i];
                idx++;
            }

            if (bigEndian)
            {
                buffer = Swap(buffer);
            }

            return BitConverter.ToUInt16(buffer, 0);
        }

        public static ushort ReadUInt16(Stream stream, bool bigEndian = false)
        {
            var buffer = ReadBytes(stream, sizeof(ushort));

            return ReadUInt16(buffer, 0, bigEndian);
        }

        public static uint ReadUInt32(byte[] bytes, int startIndex = 0, bool bigEndian = false)
        {
            if (bytes == null || bytes.Length < sizeof(uint))
            {
                throw new ArgumentException("Invalid bytes specified.", nameof(bytes));
            }

            var buffer = new byte[sizeof(uint)];
            var idx = 0;

            for (var i = startIndex; i < (startIndex + buffer.Length); i++)
            {
                buffer[idx] = bytes[i];
                idx++;
            }

            if (bigEndian)
            {
                buffer = Swap(buffer);
            }

            return BitConverter.ToUInt32(buffer, 0);
        }

        public static uint ReadUInt32(Stream stream, bool bigEndian = false)
        {
            var buffer = ReadBytes(stream, sizeof(uint));

            return ReadUInt32(buffer, 0, bigEndian);
        }

        public static ulong ReadUInt64(byte[] bytes, int startIndex = 0, bool bigEndian = false)
        {
            if (bytes == null || bytes.Length != sizeof(ulong))
            {
                throw new ArgumentException("Invalid bytes specified.", nameof(bytes));
            }

            var buffer = new byte[sizeof(ulong)];
            var idx = 0;

            for (var i = startIndex; i < (startIndex + buffer.Length); i++)
            {
                buffer[idx] = bytes[i];
                idx++;
            }

            if (bigEndian)
            {
                buffer = Swap(buffer);
            }

            return BitConverter.ToUInt64(buffer,0);
        }

        public static ulong ReadUInt64(Stream stream, bool bigEndian = false)
        {
            var buffer = ReadBytes(stream, sizeof(ulong));

            return ReadUInt64(buffer, 0, bigEndian);
        }

        public static byte[] ReadBytes(byte[] bytes, int size)
        {
            return ReadBytes(bytes, 0, size);
        }

        public static byte[] ReadBytes(byte[] bytes, int offset, int size)
        {
            var realSize = size;

            if ((offset + size) > bytes.Length)
            {
                realSize = bytes.Length - offset;
            }

            var result = new byte[realSize];

            Array.Copy(bytes, offset, result, 0, realSize);

            return result;
        }

        public static byte[] ReadBytes(Stream stream, int size)
        {
            var buffer = new byte[size];
            var numRead = stream.Read(buffer, 0, size);
            var result = new byte[numRead];

            Array.Copy(buffer, 0, result, 0, numRead);

            return result;
        }

        public static byte HiByte(ushort value)
        {
            return Convert.ToByte((value >> 8) & 0xFF);
        }

        public static byte LoByte(ushort value)
        {
            return Convert.ToByte(value & 0xFF);
        }

        public static ushort HiWord(uint value)
        {
            return Convert.ToUInt16((value >> 16) & 0xFFFF);
        }

        public static ushort LoWord(uint value)
        {
            return Convert.ToUInt16(value & 0xFFFF);
        }

        public static uint HiDWord(ulong value)
        {
            return Convert.ToUInt32((value >> 32) & 0xFFFFFFFF);
        }

        public static uint LoDWord(ulong value)
        {
            return Convert.ToUInt32(value & 0xFFFFFFFF);
        }

        public static ulong MakeUInt64(uint ms, uint ls)
        {
            ulong result = (((ulong)ms) << 32) | ls;

            return result;
        }

        public static byte[] Swap(byte[] bytes)
        {
            var result = new byte[bytes.Length];
            var idx = 0;

            for (var i = bytes.Length - 1; i >= 0 ; i--)
            {
                result[idx] = bytes[i];
                idx++;
            }

            return result;
        }

        public static bool IsNullOrEmpty<T>(T[] array)
        {
            if (array == null)
            {
                return true;
            }

            if (array.Length == 0)
            {
                return true;
            }

            return false;
        }

        public static string GetFileFromZip(ZipArchive archive, string name)
        {
            foreach (var entry in archive.Entries)
            {
                if (string.Compare(entry.Name, name, StringComparison.Ordinal) == 0)
                {
                    using (var stream = entry.Open())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8, true);

                        return reader.ReadToEnd();
                    }
                }
            }

            return string.Empty;
        }

        public static string GetFileFromZip(Stream stream, string name)
        {
            try
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Read, true))
                {
                    return GetFileFromZip(archive, name);
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool MatchBytes(byte[] input, int offset, int length, byte?[] pattern)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "No input bytes specified.");
            }

            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern), "No pattern bytes specified.");
            }

            if (length < pattern.Length)
            {
                throw new ArgumentException("Pattern cannot be smaller than specified input length.", nameof(pattern));
            }

            var idx = 0;

            for (var i = offset; i < (offset + length); i++)
            {
                var inputByte = input[i];
                var patternByte = pattern[idx];

                if (patternByte != null && inputByte != patternByte)
                {
                    return false;
                }

                idx++;
            }

            return true;
        }

        public static bool MatchBytes(byte[] input, int offset, byte?[] pattern)
        {
            return MatchBytes(input, offset, pattern.Length, pattern);
        }

        public static bool MatchBytes(byte[] input, byte?[] pattern)
        {
            return MatchBytes(input, 0, pattern.Length, pattern);
        }

        #endregion
    }
}
