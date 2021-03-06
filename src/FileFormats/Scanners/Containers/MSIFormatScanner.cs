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

using OpenMcdf;
using OpenMcdf.Extensions;

using Workshell.FileFormats.Formats.Containers;

namespace Workshell.FileFormats.Scanners.Containers
{
    public class MSIFormatScanner : CompoundFileStorageFormatScanner
    {
        #region Methods

        public override FileFormat Match(FileFormatScanJob job)
        {
            if (!ValidateStartBytes(job))
            {
                return null;
            }

            FileFormat fingerprint = null;

            try
            {
                var matchTitle = false;
                var matchApp = false;
                
                using (var file = new CompoundFile(job.Stream, CFSUpdateMode.ReadOnly, CFSConfiguration.LeaveOpen))
                {
                    var items = new List<CFItem>();

                    file.RootStorage.VisitEntries((item) => items.Add(item), true);

                    foreach (var item in items)
                    {
                        if (item.Name == "\u0005SummaryInformation")
                        {
                            var container = ((CFStream)item).AsOLEPropertiesContainer();

                            foreach (var prop in container.Properties)
                            {
                                if (prop.PropertyName == "PIDSI_TITLE")
                                {
                                    var value = prop.Value.ToString().Trim('\0');
                                    
                                    matchTitle = (string.Compare(value, "Installation Database", StringComparison.OrdinalIgnoreCase) == 0);
                                }
                                else if (prop.PropertyName == "PIDSI_APPNAME")
                                {
                                    var value = prop.Value.ToString().Trim('\0');
                                    
                                    matchApp = (value.IndexOf("Windows Installer XML Toolset", StringComparison.OrdinalIgnoreCase) > -1);
                                }
                            }

                            if (matchTitle && matchApp)
                            {
                                break;
                            }
                        }
                    }
                }

                if (matchTitle && matchApp)
                {
                    fingerprint = new MSIFormat();
                }
            }
            catch
            {
                fingerprint = null;
            }

            return fingerprint;
        }

        #endregion
    }
}
