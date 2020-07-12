using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.EmailService;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace InvoiceGenerator.Service.Service
{
    public class InvoiceService
    {
        public static readonly string EmailTemplatesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");

        public async Task AddInvoice(InvoiceModel invoice)
        {
            await Invoice.AddInvoice(invoice);
            var isSendEmailInvoice = Settings.GetSetting(Constants.IsInvoiceSendInEmailSetting).Value;

            bool.TryParse(isSendEmailInvoice, out var isSendMailForInvoice);

            if (isSendMailForInvoice)
            {
                var emailHelper = new EmailHelper();
                var invoicePath = ConstructInvoicePdf(invoice);
                emailHelper.SendInvoiceMail(invoice, invoicePath);
            }
        }

        public async Task SendEmailForInvoice(int invoiceId)
        {
            var invoice = await Invoice.GetInvoice(invoiceId);
            var invoicePath = ConstructInvoicePdf(invoice);
            var emailHelper = new EmailHelper();
            emailHelper.SendInvoiceMail(invoice, invoicePath);
        }

        public string ConstructInvoicePdf(InvoiceModel invoiceModel)
        {
            try
            {
                List<string> cssFiles = new List<string>();
                var invoiceFilePath = $"D:\\Repo\\Personal\\shopBillingInvoice\\{invoiceModel.Id}.pdf";

                cssFiles.Add(@"CustomerInvoiceTemplate.css");

                var output = new MemoryStream();

                Dictionary<string, string> replacements = GetReplacements(invoiceModel);
                var body = GetMailBody("CustomerInvoiceTemplate.html", replacements);
                var input = new MemoryStream(Encoding.UTF8.GetBytes(body));

                var document = new Document();
                var fs = new FileStream(invoiceFilePath, FileMode.Create);
                var writer = PdfWriter.GetInstance(document, fs);
                writer.CloseStream = false;

                document.Open();
                var htmlContext = new iTextSharp.tool.xml.pipeline.html.HtmlPipelineContext(null);
                htmlContext.SetTagFactory(iTextSharp.tool.xml.html.Tags.GetHtmlTagProcessorFactory());

                iTextSharp.tool.xml.pipeline.css.ICSSResolver cssResolver = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssResolver.FileRetrieve = new CustomFileRetiever(EmailTemplatesFolderPath);

                var pipeline = new iTextSharp.tool.xml.pipeline.css.CssResolverPipeline(cssResolver, new iTextSharp.tool.xml.pipeline.html.HtmlPipeline(htmlContext, new iTextSharp.tool.xml.pipeline.end.PdfWriterPipeline(document, writer)));
                var worker = new iTextSharp.tool.xml.XMLWorker(pipeline, true);
                var p = new iTextSharp.tool.xml.parser.XMLParser(worker);
                p.Parse(input);
                document.Close();
                output.Position = 0;
                return invoiceFilePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private
        private static Dictionary<string, string> GetReplacements(InvoiceModel invoiceModel)
        {
            var replacements = new Dictionary<string, string>();
            replacements.Add("InvoiceId", invoiceModel.Id.ToString());
            replacements.Add("Date", DateTime.UtcNow.ToString());
            replacements.Add("CustomerName", invoiceModel.Customer.Name);
            replacements.Add("CustomerEmailId", invoiceModel.Customer.EmailId);
            replacements.Add("CustomerPhone", invoiceModel.Customer.PhoneNumber);
            return replacements;
        }

        public static string GetMailBody(string bodyFileName, IDictionary replacements)
        {
            var body = ReadMailTemplate(bodyFileName);
            return DoReplacements(body, replacements);
        }

        public static string ReadMailTemplate(string bodyFileName)
        {
            string body = string.Empty;
            string fullFilePath = Path.Combine(EmailTemplatesFolderPath, bodyFileName);
            TextReader textReader = (TextReader)new StreamReader(fullFilePath);
            try
            {
                body = textReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                textReader.Close();
            }
            return body;
        }

        public static string DoReplacements(string body, IDictionary replacements)
        {
            if (replacements != null && !string.IsNullOrEmpty(body))
            {
                foreach (object index in (IEnumerable)replacements.Keys)
                {
                    string pattern = "%" + index.ToString() + "%";
                    string str = replacements[index] as string;
                    if (pattern == null)
                    { throw new ArgumentException("MailDefinition_InvalidReplacements"); }

                    if (str == null) { str = String.Empty; }
                    string replacement = str.Replace("$", "$$").Replace("\n", "<BR>");
                    body = Regex.Replace(body, pattern, replacement, RegexOptions.IgnoreCase);
                }
            }
            return body;
        }
        #endregion
    }
}
