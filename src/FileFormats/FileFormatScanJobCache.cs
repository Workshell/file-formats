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

namespace Workshell.FileFormats
{
    public sealed class FileFormatScanJobCache
    {
        private readonly Dictionary<string, object> _cache;

        internal FileFormatScanJobCache()
        { 
            _cache = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        #region Methods

        public void Clear()
        {
            _cache.Clear();
        }

        public bool Set(string key, object value)
        {
            return Set(key, value, true);
        }

        public bool Set(string key, object value, bool replace)
        {
            if (_cache.ContainsKey(key))
            {
                if (!replace)
                {
                    return false;
                }
            }

            _cache.Add(key, value);

            return true;
        }

        public bool Remove(string key)
        {
            return _cache.Remove(key);
        }

        public bool Exists(string key)
        {
            return _cache.ContainsKey(key);
        }

        public object Get(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                return null;
            }

            return _cache[key];
        }

        public T Get<T>(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                return default(T);
            }

            return (T)_cache[key];
        }

        #endregion

        #region Properties

        public int Count => _cache.Count;

        #endregion
    }
}
