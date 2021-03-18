using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Workshell.OpenMcdf.Extensions.OLEProperties.Interfaces
{
    internal interface IBinarySerializable
    {
        void Write(BinaryWriter bw);
        void Read(BinaryReader br);
    }
}
