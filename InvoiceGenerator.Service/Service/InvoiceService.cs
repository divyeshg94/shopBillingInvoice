using System.Threading.Tasks;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.EmailService;

namespace InvoiceGenerator.Service.Service
{
    public class InvoiceService
    {
        public async Task AddInvoice(InvoiceModel invoice)
        {
            await Invoice.AddInvoice(invoice);
            var isSendEmailInvoice = Settings.GetSetting(Constants.IsInvoiceSendInEmailSetting).Value;

            bool.TryParse(isSendEmailInvoice, out var isSendMailForInvoice);

            if (isSendMailForInvoice)
            {
                var emailHelper = new EmailHelper();
                emailHelper.SendInvoiceMail(invoice);
            }
        }

        public async Task SendEmailForInvoice(int invoiceId)
        {
            var invoice = await Invoice.GetInvoice(invoiceId);
            var emailHelper = new EmailHelper();
            emailHelper.SendInvoiceMail(invoice);
        }
    }
}
