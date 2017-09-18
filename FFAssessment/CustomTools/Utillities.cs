using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Win32;
using System.Configuration;
using System.Web.Configuration;

namespace FFAssessment
{
    public class Utillities
    {
        public class ConfigFiles
        {
            public string GetValue(string Key)
            {
                return System.Configuration.ConfigurationManager.AppSettings[Key].ToString();
            }
            public void SaveValue(string Key,string Value)
            {

                Configuration AppConfigSettings = WebConfigurationManager.OpenWebConfiguration("~");
                AppConfigSettings.AppSettings.Settings[Key].Value = Value;
                AppConfigSettings.Save();
                
            }
            
        }
        public class WindowsRegistry
        {
            public string Key { get; set; }
            public string SubKey { get; set; }
            public void SaveValue(string strValue)
            {
                Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Key);
                registryKey.SetValue(SubKey, strValue);
                registryKey.Close();
            }
            public string GetValue()
            {
                return "";
            }
        }
    }
}