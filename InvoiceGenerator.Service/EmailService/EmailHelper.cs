using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using InvoiceGenerator.Models;

namespace InvoiceGenerator.Service.EmailService
{
    public class EmailHelper
    {
        public string GenerateInvoice(InvoiceModel invoiceToGenerate)
        {
            StringBuilder invoiceHtml = new StringBuilder();
            invoiceHtml.Append("<b>INVOICE : ").Append(invoiceToGenerate.Id.ToString()).Append("</b><br />");
            invoiceHtml.Append("<b>DATE : </b>").Append(DateTime.Now.ToShortDateString()).Append("<br />");
            invoiceHtml.Append("<b>Invoice Amt :</b> $").Append(invoiceToGenerate.TotalAmount.ToString()).Append("<br />");
            invoiceHtml.Append("<br /><b>CUSTOMER CONTACT INFO:</b><br />");
            invoiceHtml.Append("<b>Name : </b>").Append(invoiceToGenerate.Customer.Name).Append("<br />");
            invoiceHtml.Append("<b>Phone : </b>").Append(invoiceToGenerate.Customer.PhoneNumber).Append("<br />");
            invoiceHtml.Append("<b>Email : </b>").Append(invoiceToGenerate.Customer.EmailId).Append("<br />");
            invoiceHtml.Append("<br /><b>PRODUCTS:</b><br /><table><tr><th>Qty</th><th>Product</th></tr>");
            // InvoiceItem should be a collection property which contains list of invoice lines
            foreach (var invoiceLine in invoiceToGenerate.InvoiceItemses)
            {
                invoiceHtml.Append("<tr><td>").Append(invoiceLine.Quantity.ToString()).Append("</td><td>").Append(invoiceLine.Item.Name).Append("</td></tr>");
            }
            invoiceHtml.Append("</table>");
            return invoiceHtml.ToString();
        }

        public void SendInvoiceMail(InvoiceModel invoiceToGenerate)
        {
            var mailBody = GenerateInvoice(invoiceToGenerate);

            using (MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, mailBody))
            {
                mailMessage.IsBodyHtml = true;
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "127.0.0.1";
                    smtp.Credentials = new System.Net.NetworkCredential("username", "password");
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
