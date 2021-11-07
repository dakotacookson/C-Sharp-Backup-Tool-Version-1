using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ConfigOptions
    {
        public List<ConfigOption> Options { get; set; }
    }
    public class ConfigOption
    {
        public string BackupName { get; set; }
        public string Source { get; set; }
        public string Destination{ get; set; }
    }
}
