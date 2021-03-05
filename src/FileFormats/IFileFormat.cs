using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshell.FileFormats
{
    public interface IFileFormat
    {
        #region Properties

        string[] ContentTypes { get; }
        string[] Extensions { get; }
        string Description { get; }
        int SortIndex { get; }

        #endregion        
    }
}
