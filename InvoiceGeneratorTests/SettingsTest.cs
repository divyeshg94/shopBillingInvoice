using System;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceGeneratorTests
{
    [TestClass]
    public class SettingsTests
    {
        [TestMethod]
        public void DefaultSeed()
        {
            AddSetting("FromEmail", "divyeshgovardhanan@gmail.com", "EmailSettings");
            AddSetting("FromUserName", "divyeshgovardhanan@gmail.com", "EmailSettings");
            AddSetting("FromPassword", "******", "EmailSettings");
        }

        private void AddSetting(string key, string value, string group)
        {
            var setting = new SettingModel()
            {
                Key = key,
                Value = value,
                Group = group,
                CreatedOn = DateTime.UtcNow
            };
            Settings.AddSetting(setting);
        }
    }
}