using System;
using System.Collections.Generic;
using File = System.IO.File;

namespace InvoiceGenerator.Service.Service
{
    public class CustomFileRetiever : iTextSharp.tool.xml.net.FileRetrieveImpl
    {
        private IList<string> rootdirs;
        private IList<string> urls;

        public CustomFileRetiever(string baseUrl)
        {
            rootdirs = new List<string>();
            urls = new List<string>() {
            baseUrl + "\\CustomerInvoiceTemplate.css"
            };
        }

        private Uri DetectWithRootUrls(string href)
        {
            foreach (string root in urls)
            {
                try
                {
                    return new Uri(root + href);
                }
                catch (UriFormatException)
                {
                }
            }
            throw new UriFormatException();
        }

        public override void ProcessFromHref(string href, iTextSharp.tool.xml.net.IReadingProcessor processor)
        {
            Uri url = null;
            bool isfile = false;
            string f = href;
            try
            {
                url = new Uri(href);
            }
            catch (UriFormatException)
            {
                try
                {
                    url = DetectWithRootUrls(href);
                }
                catch (UriFormatException)
                {
                    // its probably a file, try to detect it.
                    isfile = true;
                    if (!(File.Exists(href)))
                    {
                        isfile = false;
                        foreach (string root in rootdirs)
                        {
                            f = System.IO.Path.Combine(root, href);
                            if (System.IO.File.Exists(f))
                            {
                                isfile = true;
                                break;
                            }

                        }
                    }
                }
            }
            System.IO.Stream inp = null;
            if (null != url)
            {

                //***********************
                //Begin changed part
                //***********************
                System.Net.WebRequest w = System.Net.WebRequest.Create(url);
                w.UseDefaultCredentials = true;
                w.PreAuthenticate = true;
                w.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //***********************
                //End changed part
                //***********************

                try
                {
                    inp = w.GetResponse().GetResponseStream();
                }
                catch (System.Net.WebException)
                {
                    throw new System.IO.IOException(iTextSharp.tool.xml.exceptions.LocaleMessages.GetInstance().GetMessage("retrieve.file.from.nothing"));
                }
            }
            else if (isfile)
            {
                inp = new System.IO.FileStream(f, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            }
            else
            {
                throw new System.IO.IOException(iTextSharp.tool.xml.exceptions.LocaleMessages.GetInstance().GetMessage("retrieve.file.from.nothing"));
            }
            Read(processor, inp);
        }

        private void Read(iTextSharp.tool.xml.net.IReadingProcessor processor, System.IO.Stream inp)
        {
            try
            {
                int inbit = -1;
                while ((inbit = inp.ReadByte()) != -1)
                {
                    processor.Process(inbit);
                }
            }
            catch (System.IO.IOException e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    if (null != inp)
                    {
                        inp.Close();
                    }
                }
                catch (System.IO.IOException e)
                {
                    throw new iTextSharp.tool.xml.exceptions.RuntimeWorkerException(e);
                }
            }
        }
    }
}
