using System.Net;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace InvoiceGenerator.Service.Service
{
    public class SmsService
    {
        public void SendInvoiceSms(InvoiceModel invoiceToGenerate)
        {
            var twilioAccountSid = Settings.GetSetting(Constants.TwilioAccountSid).Value;
            var twilioAuthToken = Settings.GetSetting(Constants.TwilioAuthToken).Value;
            var twilioPhoneNumber = Settings.GetSetting(Constants.TwilioPhoneNumber).Value;

            TwilioClient.Init(twilioAccountSid, twilioAuthToken);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var body = ConstructInvoiceSmsMessage(invoiceToGenerate);
            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                //to: new Twilio.Types.PhoneNumber("+91" + invoiceToGenerate.Customer.PhoneNumber)
                to: new Twilio.Types.PhoneNumber("+919487867505")
            );
        }

        private string ConstructInvoiceSmsMessage(InvoiceModel invoiceToGenerate)
        {
            var message = "Thank you for visiting us!!!\n";
            message += "Please find your invoice\n\n";

            message += $"Total Amount - {invoiceToGenerate.TotalAmount},\n\n";

            message += "With Love,\n";
            message += "Crescent Beauty Lounge";
            return message;
        }
    }
}
