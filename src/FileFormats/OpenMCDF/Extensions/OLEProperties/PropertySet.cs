using System.Collections.Generic;
using Workshell.FileFormats.OpenMCDF.Extensions.OLEProperties.Interfaces;

namespace Workshell.FileFormats.OpenMCDF.Extensions.OLEProperties
{
    internal class PropertySet
    {
        public uint Size { get; set; }

        public uint NumProperties { get; set; }

        public List<PropertyIdentifierAndOffset> PropertyIdentifierAndOffsets { get; set; } = new List<PropertyIdentifierAndOffset>();

        public List<ITypedPropertyValue> Properties { get; set; } = new List<ITypedPropertyValue>();
    }
}