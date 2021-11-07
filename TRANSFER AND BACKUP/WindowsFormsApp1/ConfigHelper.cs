using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class ConfigHelper
    {
        public static ConfigOptions configOptions;
        public static void LoadConfigurations(string configPath, ComboBox combo)
        {
            configOptions = new ConfigOptions();
            configOptions.Options = new List<ConfigOption>();
            if (File.Exists(configPath))

            {
                ConfigOption configOption = null;
                foreach (string line in File.ReadLines(configPath))
                {

                    if (line.Contains("BackupName:"))
                    {
                        configOption = new ConfigOption();
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        configOption.BackupName = lineset2;
                    }
                    if (line.Contains("SourceName:"))
                    {
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        configOption.Source = lineset2;
                    }

                    if (line.Contains("DestinationName:"))
                    {
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        configOption.Destination = lineset2;
                        configOptions.Options.Add(configOption);
                    }

                }

            }
            foreach (var option in ConfigHelper.configOptions.Options.OrderBy(x=>x.BackupName))
            {
                combo.Items.Add(option.BackupName);
            }
        }
    }
}
