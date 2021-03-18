using System;
using System.Collections.Generic;
using System.Text;

namespace Workshell.OpenMcdf.Extensions.OLEProperties.Interfaces
{
    internal interface IProperty : IBinarySerializable
    {

        object Value
        {
            get;
            set;
        }

        PropertyType PropertyType
        {
            get;
        }
       
    }
}
