using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoSpa.Models
{
    public class ConfigurationDto
    {
        public object AppSettings { get; set; }
        public string EnvironmentName { get; set; }
    }
}
