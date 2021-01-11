using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Logger
{
    public class LoggedItemViewModel
    {
        public DateTime Time { get; set; }

        /// <summary>
        /// The "Header", e.g. "File Not Found"
        /// </summary>
        public string LogHeader { get; set; }

        /// <summary>
        /// The "Description", e.g. "The file [C:\Users\Meeee\Something.txt] was not found" 
        /// </summary>
        public string LogDescription { get; set; }

        /// <summary>
        /// The actual exception itself, if one was thrown that is
        /// </summary>
        public Exception RawException { get; set; }

        public LoggedItemViewModel()
        {
            LogHeader = "";
            LogDescription = "";

            // DateTime.ToString("T") returns HH:mm:ss, recommended for this
            //string a = DateTime.Now.ToString("T");
            // in XAML, use {... StringFormat={}T}
        }
    }
}
