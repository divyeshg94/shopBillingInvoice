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
            UpdateSettings("FromEmail", "divyeshgovardhanan@gmail.com", "EmailSettings");
            UpdateSettings("FromUserName", "divyeshgovardhanan@gmail.com", "EmailSettings");
            UpdateSettings("FromPassword", "Dhakshu020415", "EmailSettings");
            UpdateSettings("InvoiceSubject", "Invoice", "InvoiceSettings");
            UpdateSettings("IsInvoiceSendInEmail", true.ToString(), "InvoiceSettings");
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

        private void UpdateSettings(string key, string value, string group)
        {
            var setting = new SettingModel()
            {
                Key = key,
                Value = value,
                Group = group,
                CreatedOn = DateTime.UtcNow
            };
            Settings.UpdateSettingByKey(setting);
        }
    }
}