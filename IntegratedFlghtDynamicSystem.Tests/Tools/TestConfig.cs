using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedFlghtDynamicSystem.Global;

namespace IntegratedFlghtDynamicSystem.Tests.Tools
{
    public class TestConfig : IConfig
    {
        private readonly Configuration _configuration;

        public TestConfig(string configPath)
        {
            var configFileMap = new ExeConfigurationFileMap {ExeConfigFilename = configPath};
            _configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }
        
        public string ConnectionStrings(string connectionString)
        {
            return _configuration.ConnectionStrings.ConnectionStrings[connectionString].ConnectionString;
        }

        public string Lang
        {
            get
            {
                return _configuration.AppSettings.Settings["Lang"].Value;
            }
        }
    }
}
