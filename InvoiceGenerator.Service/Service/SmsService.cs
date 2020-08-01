using InvoiceGenerator.Models;

namespace InvoiceGenerator.Service.Service
{
    public class SmsService
    {
        public void SendInvoiceSms(InvoiceModel invoiceToGenerate)
        {
            //const string accountSid = "AC3b7064289030bbf51fd8733b01e9fbda";
            //const string authToken = "c2bddfd631911af796043bc9d0653fdb";

            //TwilioClient.Init(accountSid, authToken);

            //var message = MessageResource.Create(
            //    body: "This is the ship that made the Kessel Run in fourteen parsecs?",
            //    from: new Twilio.Types.PhoneNumber("+15017122661"),
            //    to: new Twilio.Types.PhoneNumber("+15558675310")
            //);
        }
    }
}
