namespace Workshell.FileFormats.OpenMCDF.Extensions.OLEProperties.Interfaces
{
    internal interface ITypedPropertyValue : IBinarySerializable
    {
        bool IsArray { get; set; }

        bool IsVector { get; set; }

        object PropertyValue { get; set; }

        VtPropertyType VtType
        {
            get;
        }
    }
}