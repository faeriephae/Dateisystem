using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dateisystem.BusinessLayer
{
    public static class ErrorHandling
    {
        /// <summary>
        /// Writes error message to debug output window.
        /// </summary>
        /// <param name="e">Error.</param>
        public static void ErrorMsg(Exception e)
        {
            Debug.WriteLine($"\n Hi, \n {e.Message} \n Caused by {e.InnerException} \n Thrown by {e.TargetSite} \n (ノಠ益ಠ)ノ彡┻━┻ \n");
        }
    }
}
