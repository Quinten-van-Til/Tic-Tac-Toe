using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boter_Kaas_Eieren
{

    /// <summary>
    /// Value of a cell in the current game
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// Blank cells
        /// </summary>
        Free,
        /// <summary>
        /// Cells with an O 
        /// </summary>
        Nought,
        /// <summary>
        /// Cells with a X
        /// </summary>
        Cross 

    }
}
