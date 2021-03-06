﻿namespace invoiceGenerator.PersistenceSql
{
    public class Constants
    {
        public const string EmailSettings = "EmailSettings";
        public const string FromEmailSettings = "FromEmail";
        public const string FromUserNameSettings = "FromUserName";
        public const string FromPasswordSettings = "FromPassword";


        //Invoice
        public const string InvoiceSettings = "InvoiceSettings";
        public const string InvoiceSubjectSettings = "InvoiceSubject";
        public const string IsInvoiceSendInEmailSetting = "IsInvoiceSendInEmail";

        //SmsSettings
        public const string SmsSettings = "SmsSettings";
        public const string IsInvoiceSendInSms = "IsInvoiceSendInSms";
        public const string TwilioAccountSid = "TwilioAccountSid";
        public const string TwilioAuthToken = "TwilioAuthToken";
        public const string TwilioPhoneNumber = "TwilioPhoneNumber";
    }
}
