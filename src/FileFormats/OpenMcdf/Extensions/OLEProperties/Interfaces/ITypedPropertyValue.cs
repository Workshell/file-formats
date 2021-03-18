using System;
using System.Collections.Generic;
using System.Text;

namespace Workshell.OpenMcdf.Extensions.OLEProperties.Interfaces
{
    internal interface ITypedPropertyValue : IProperty
    {
        VTPropertyType VTType
        {
            get;
            //set;
        }

        PropertyDimensions PropertyDimensions
        {
            get;
        }

        bool IsVariant
        {
            get; 
        }
    }
}
