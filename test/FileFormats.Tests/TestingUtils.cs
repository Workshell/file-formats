using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Workshell.FileFormats.Tests
{
    internal static class TestingUtils
    {
        #region Methods

        public static Stream GetFileFromResources(string fileName)
        {
            var path = $"Workshell.FileFormats.Tests.{fileName}";
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(path);

            return stream;
        }

        #endregion
    }
}
