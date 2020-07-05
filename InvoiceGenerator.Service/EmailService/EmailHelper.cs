using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using invoiceGenerator.PersistenceSql;
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
            var serialNum = 1;
            foreach (var invoiceLine in invoiceToGenerate.InvoiceItemses)
            {
                var item = Item.GetItem(invoiceLine.ItemId);
                invoiceHtml.Append("<tr><td>").Append(invoiceLine.Quantity.ToString()).Append("</td><td>").Append(item.Name).Append("</td></tr>");
            }
            invoiceHtml.Append("</table>");
            return invoiceHtml.ToString();
        }

        public void SendInvoiceMail(InvoiceModel invoiceToGenerate)
        {
            invoiceToGenerate.Customer = Customer.GetCustomer(invoiceToGenerate.CustomerId);
            invoiceToGenerate.Employee = Employee.GetEmployee(invoiceToGenerate.EmployeeId);

            var mailBody = GenerateInvoice(invoiceToGenerate);

            var emailSettingGroup = Settings.GetSettingByGroup(Constants.EmailSettings);
            var fromEmail = emailSettingGroup.FirstOrDefault(s => s.Key == Constants.FromEmailSettings).Value;
            var fromUserName = emailSettingGroup.FirstOrDefault(s => s.Key == Constants.FromUserNameSettings).Value;
            var fromPassword = emailSettingGroup.FirstOrDefault(s => s.Key == Constants.FromPasswordSettings).Value;

            var invoiceSettings = Settings.GetSettingByGroup(Constants.InvoiceSettings);
            var subject = invoiceSettings.FirstOrDefault(s => s.Key == Constants.InvoiceSubjectSettings).Value;

            using (MailMessage mailMessage = new MailMessage(fromEmail, invoiceToGenerate.Customer.EmailId, subject, mailBody))
            {
                try
                {
                    mailMessage.IsBodyHtml = true;
                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.EnableSsl = true;
                        smtpClient.Port = 587;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential(fromUserName, fromPassword);
                        smtpClient.Send(mailMessage);
                    }
                }
                catch (Exception ex)
                {
                    //Log error
                }
            }
        }
    }
}
