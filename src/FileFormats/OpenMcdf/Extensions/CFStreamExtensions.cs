﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Workshell.OpenMcdf.Extensions
{
    internal static class CFStreamExtension
    {


        /// <summary>
        /// Return the current <see cref="T:Workshell.OpenMcdf.CFStream">CFStream</see> object 
        /// as a <see cref="T:System.IO.Stream">Stream</see> object.
        /// </summary>
        /// <param name="cfStream">Current <see cref="T:Workshell.OpenMcdf.CFStream">CFStream</see> object</param>
        /// <returns>A <see cref="T:System.IO.Stream">Stream</see> object representing structured stream data</returns>
        public static Stream AsIOStream(this CFStream cfStream)
        {
            return new StreamDecorator(cfStream);
        }

        ///// <summary>
        ///// Return the current <see cref="T:OpenMcdf.CFStream">CFStream</see> object 
        ///// as a OLE properties Stream.
        ///// </summary>
        ///// <param name="cfStream"></param>
        ///// <returns>A <see cref="T:OpenMcdf.OLEProperties.PropertySetStream">OLE Propertie stream</see></returns>
        //public static OLEProperties.PropertySetStream AsOLEProperties(this CFStream cfStream)
        //{
        //    var result = new OLEProperties.PropertySetStream();
        //    result.Read(new BinaryReader(new StreamDecorator(cfStream)));
        //    return result;
        //}
    }
}
