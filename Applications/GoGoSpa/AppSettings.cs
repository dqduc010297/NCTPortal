using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoSpa
{
    /// <summary>
    /// App Setting for this application
    /// </summary>
    public class AppSettings
    {
        public string ClientId { get; set; }
        public string AppVersion { get; set; }
        public string ApiUrl { get; set; }

        public Dictionary<string, string> ServiceRegistry { get; set; }
    }
}
