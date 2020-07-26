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
            var invoiceId = await Invoice.AddInvoice(invoice);
            invoice.Id = invoiceId;
            var invoicePath = ConstructInvoicePdf(invoice);

            var isSendEmailInvoice = Settings.GetSetting(Constants.IsInvoiceSendInEmailSetting).Value;
            bool.TryParse(isSendEmailInvoice, out var isSendMailForInvoice);
            if (isSendMailForInvoice)
            {
                var emailHelper = new EmailHelper();
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
                Test(invoiceModel);
                return "";

                List<string> cssFiles = new List<string>();

                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Invoices");
                bool exists = System.IO.Directory.Exists(directory);

                if (!exists)
                    System.IO.Directory.CreateDirectory(directory);

                var invoiceFilePath = directory + $"\\{invoiceModel.Id}-{invoiceModel.Customer.Name}.pdf";

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
                fs.Close();
                output.Position = 0;
                return invoiceFilePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //https://stackoverflow.com/questions/25164257/how-to-convert-html-to-pdf-using-itextsharp/25164258
        private void Test(InvoiceModel invoiceModel)
        {
            Byte[] bytes;

            //Boilerplate iTextSharp setup here
            //Create a stream that we can write to, in this case a MemoryStream
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document())
                {

                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {

                        //Open the document for writing
                        doc.Open();

                        //Our sample HTML and CSS
                        Dictionary<string, string> replacements = GetReplacements(invoiceModel);
                        var css = ReadMailTemplate("CustomerInvoiceTemplate.css");
                        var body = GetMailBody("CustomerInvoiceTemplate.html", replacements);

                        var example_html = body;
                        var example_css = css;

                        /**************************************************
                         * Use the XMLWorker to parse HTML and CSS        *
                         * ************************************************/
                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_html)))
                            {
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            }
                        }
                        doc.Close();
                    }
                }

                //After all of the PDF "stuff" above is done and closed but **before** we
                //close the MemoryStream, grab all of the active bytes from the stream
                bytes = ms.ToArray();
            }

            //Now we just need to do something with those bytes.
            //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
            //You could also write the bytes to a database in a varbinary() column (but please don't) or you
            //could pass them to another function for further PDF processing.
            var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            System.IO.File.WriteAllBytes(testFile, bytes);
        }

        #region Private
        private static Dictionary<string, string> GetReplacements(InvoiceModel invoiceModel)
        {
            var logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Images\\Crescent-T.png");
            
            var replacements = new Dictionary<string, string>();
            replacements.Add("InvoiceId", invoiceModel.Id.ToString());
            replacements.Add("Date", DateTime.UtcNow.ToString());
            replacements.Add("CustomerName", invoiceModel.Customer.Name);
            replacements.Add("CustomerEmailId", invoiceModel.Customer.EmailId);
            replacements.Add("CustomerPhone", invoiceModel.Customer.PhoneNumber);
            replacements.Add("LogoPath", logoPath);
            replacements.Add("InvoiceItems", GetEmailInvoiceItemstemplate(invoiceModel));
            return replacements;
        }

        private static string GetEmailInvoiceItemstemplate(InvoiceModel invoiceModel)
        {
            var itemsHtml = "<tr><th>S.No</th><th>Name</th><th>Unit Price</th><th>Quantity</th><th>Total Price</th></tr>";
            var serialNum = 1;
            foreach (var item in invoiceModel.InvoiceItemses)
            {
                itemsHtml += $"<tr><td>{serialNum}</td><td>{item.Item.Name}</td><td>Rs. {item.UnitPrice}</td><td>{item.Quantity}</td><td>Rs. {item.TotalPrice}</td></tr>";
            }
            return itemsHtml;
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
