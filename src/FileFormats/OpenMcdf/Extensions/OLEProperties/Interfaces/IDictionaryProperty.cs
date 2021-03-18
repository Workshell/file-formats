using System.IO;

namespace Workshell.OpenMcdf.Extensions.OLEProperties.Interfaces
{
    internal interface IDictionaryProperty : IProperty
    {
        void Read(BinaryReader br);
        void Write(BinaryWriter bw);
    }
}