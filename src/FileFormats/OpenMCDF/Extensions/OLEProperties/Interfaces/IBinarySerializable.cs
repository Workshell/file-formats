using System.IO;

namespace Workshell.FileFormats.OpenMCDF.Extensions.OLEProperties.Interfaces
{
    internal interface IBinarySerializable
    {
        void Write(BinaryWriter bw);
        void Read(BinaryReader br);
    }
}