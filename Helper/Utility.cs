using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

using System.IO;
using System.Security.Cryptography;
using Spire.Pdf;
using System.Drawing;



namespace Helper
{
    public class Utility
    {

     BLL.Log _BLL = new BLL.Log();

        public static string getWebConfig(string key)
        {
            if (key != "" || key != null)
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            else
            {
                return "";
            }
        }

        //public static void SendEmail(string[] mailTo, string subject, string mailBody, bool IsBodyHtml)
        //{
        //    string[] AttachmentFilePath = { "" };
        //    SendEmail(mailTo, subject, mailBody, IsBodyHtml, AttachmentFilePath);
        //}

        //public static void SendEmail(string[] mailTo, string subject, string mailBody, bool IsBodyHtml, string[] AttachmentFilePath)
        //{


        //    string Phase = getWebConfig("Phase");
        //    string TestingPrefix = "";

        //    if (Phase.ToUpper() == "DEV" || Phase.ToUpper() == "DEVELOPMENT")
        //    {
        //        TestingPrefix = "[**ทดสอบระบบ**]";
        //        mailTo = getWebConfig("TestingmailTo").Split(';');

        //    }

        //    string mailSubject = TestingPrefix + subject;
        //    string[] mailBCC = getWebConfig("mailBCC").ToString().Split(';');


        //    string mailHost = getWebConfig("mailHost");
        //    int mailPort = Convert.ToInt16(getWebConfig("mailPort"));
        //    string mailEncoding = getWebConfig("mailEncoding");
        //    string mailFrom = getWebConfig("mailFrom");
        //    string mailFromDisplay = getWebConfig("mailFromDisplay");

        //    MailMessage mail = new MailMessage();
        //    //            SmtpClient SmtpServer = new SmtpClient
        //    //            {
        //    //               // Host = mailHost,
        //    //                Port = 587,
        //    //                Host = "smtp.gmail.com",
        //    //                Credentials = new System.Net.NetworkCredential("", ""),
        //    //UseDefaultCredentials = false ,
        //    //                EnableSsl = false
        //    //            };
        //    mail.BodyEncoding = Encoding.GetEncoding(mailEncoding);
        //    mail.From = new MailAddress(mailFrom, mailFromDisplay);
        //    mail.Subject = mailSubject;


        //    if (AttachmentFilePath != null)
        //    {
        //        foreach (string pathFile in AttachmentFilePath)
        //        {
        //            mail.Attachments.Add(new Attachment(pathFile));
        //        }
        //    }

        //    foreach (var recipient in mailTo)
        //    {
        //        if (recipient != "") { mail.To.Add(recipient); }
        //    }

        //    foreach (var repCC in mailBCC)
        //    {
        //        if (repCC != "") { mail.Bcc.Add(repCC); }
        //    }


        //    //SmtpServer.UseDefaultCredentials = false;
        //    //SmtpServer.EnableSsl = true;
        //    //NetworkCredential nc = new NetworkCredential("prakasit.y@gmail.com", "123456@qA");
        //    // SmtpServer.Credentials = nc;
        //    //  mail.IsBodyHtml = IsBodyHtml;
        //    mail.Body = mailBody;


        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Port = 587;
        //    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        //    smtp.Credentials = new System.Net.NetworkCredential("prakasit.y@gmail.com", "123456@qA");
        //    //Or your Smtp Email ID and Password
        //    smtp.EnableSsl = true;
        //    smtp.Send(mail);

        //}

        public static void SendEmail(string[] mailTo,string[] mailCC, string subject, string mailBody, bool IsBodyHtml, string[] AttachmentFilePath,string mailForm)
        {


            string Phase = getWebConfig("Phase");
            string TestingPrefix = "";

            if (Phase.ToUpper() == "DEV" || Phase.ToUpper() == "DEVELOPMENT")
            {
                TestingPrefix = "[**ทดสอบระบบ**]";
                mailTo = getWebConfig("TestingmailTo").Split(';');

            }



            string mailSubject = TestingPrefix + subject;
            string[] mailBCC = mailCC;
            string mailHost = getWebConfig("mailHost");
            int mailPort = Convert.ToInt16(getWebConfig("mailPort"));
            string mailEncoding = getWebConfig("mailEncoding");
            string mailFrom = getWebConfig("mailFrom");
            string mailFromDisplay = getWebConfig("mailFromDisplay");
            string mailAccount = getWebConfig("mailAccount");
            string mailAccountPass = getWebConfig("mailAccountPass");

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient
            {
                Host = mailHost,
                Port = mailPort,
                Credentials = new System.Net.NetworkCredential(mailAccount,mailAccountPass),
                EnableSsl = false
            };

            mail.BodyEncoding = Encoding.GetEncoding(mailEncoding);
            mail.From = new MailAddress(mailFrom, mailFromDisplay);
            mail.Subject = mailSubject;

            if (AttachmentFilePath != null)
            {
                foreach (string pathFile in AttachmentFilePath)
                {
                    mail.Attachments.Add(new Attachment(pathFile));
                }
            }

            if (mailTo.Length > 0)
            {
                foreach (var recipient in mailTo)
                {
                    if (recipient != "") { mail.To.Add(recipient); }
                }

                mail.IsBodyHtml = IsBodyHtml;
                mail.Body = mailBody;

                try
                {
                   // SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    SmtpServer.Dispose();
                }

            }


            if (mailBCC.Length > 0)
            {
                MailMessage mailBackend = new MailMessage();
                SmtpClient SmtpServerBackend = new SmtpClient
                {
                    Host = mailHost,
                    Port = mailPort,
                    Credentials = new System.Net.NetworkCredential(mailAccount, mailAccountPass),
                    EnableSsl = false
                };
                mailBackend.BodyEncoding = Encoding.GetEncoding(mailEncoding);
                mailBackend.From = new MailAddress(mailFrom, mailFromDisplay);
                mailBackend.Subject = mailSubject;

                if (AttachmentFilePath != null)
                {
                    foreach (string pathFile in AttachmentFilePath)
                    {
                        mailBackend.Attachments.Add(new Attachment(pathFile));
                    }
                }


                mailBackend.To.Clear();
                foreach (var repCC in mailBCC)
                {
                    if (repCC != "")
                    {
                        mailBackend.To.Add(repCC);
                    }
                }

                mailBody = System.Text.RegularExpressions.Regex.Replace(mailBody, "<a.*?a>","");

                mailBackend.IsBodyHtml = IsBodyHtml;
                mailBackend.Body = mailBody;

                if (mailBackend.To.Count > 0)
                {
                    try
                    {
                        SmtpServerBackend.Send(mailBackend);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    finally
                    {
                    //    SmtpServerBackend.Dispose();
                    }

                }

            }

        }

        //public static void SendEmail(string mailSubject, string[] mailTo, string[] mailCC, string[] pathAttachmentFile, string mailBody, bool IsBodyHtml)
        //{
        //    string mailHost = getWebConfig("mailHost");
        //    int mailPort = Convert.ToInt16(getWebConfig("mailPort"));
        //    string mailEncoding = getWebConfig("mailEncoding");
        //    string mailFrom = getWebConfig("mailFrom");
        //    string mailFromDisplay = getWebConfig("mailFromDisplay");

        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient
        //    {
        //        Host = mailHost,
        //        Port = mailPort,
        //        //Host = "smtp.gmail.com",
        //        Credentials = new System.Net.NetworkCredential("prakasit.y@gmail.com", "123456@qA"),
        //        //  UseDefaultCredentials = false,
        //        EnableSsl = true
        //    };

        //    mail.BodyEncoding = Encoding.GetEncoding(mailEncoding);
        //    mail.From = new MailAddress(mailFrom, mailFromDisplay);
        //    mail.Subject = mailSubject;

        //    foreach (var recipient in mailTo)
        //    {
        //        mail.To.Add(recipient);
        //    }



        //    if (pathAttachmentFile != null)
        //    {
        //        foreach (string pathFile in pathAttachmentFile)
        //        {
        //            mail.Attachments.Add(new Attachment(pathFile));
        //        }
        //    }


        //    foreach (var repCC in mailCC)
        //    {
        //        if (repCC != "")
        //        {
        //            mail.Bcc.Add(repCC);
        //        }

        //    }

        //    mail.IsBodyHtml = IsBodyHtml;
        //    mail.Body = mailBody;

        //    SmtpServer.Send(mail);
        //}


        public static Json<T> ToJqGridResult<T>(JqGridParameter gridPara, List<T> result) where T : new()
        {
            int pageIndex = Convert.ToInt32(gridPara.Page) - 1;
            int pageSize = gridPara.Rows ?? result.Count();
            int totalRecords = result.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            if (!string.IsNullOrEmpty(gridPara.Sidx))
            {
                try
                {
                    //--ถ้ามีการ order
                    //  result = result.AsQueryable().OrderBy(gridPara.Sidx + " " + gridPara.Sord).ToList();
                }
                catch
                {
                }
            }

            return new Json<T>
            {
                valid = true,
                total = totalPages,
                page = gridPara.Page,
                records = totalRecords,
                data = result.Skip(pageIndex * pageSize).Take(pageSize).ToList<T>()
            };
        }

        public static Json<T> ToJqGridResult<T>(JqGridParameter gridPara, List<T> result, int? TotalRecord = null) where T : new()
        {
            int pageIndex = Convert.ToInt32(gridPara.Page) - 1;
            int pageSize = gridPara.Rows ?? result.Count();
            int totalRecords = TotalRecord ?? result.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            if (!string.IsNullOrEmpty(gridPara.Sidx))
            {
                try
                {
                    //--ถ้ามีการ order
                    //result = result.AsQueryable().OrderBy(gridPara.Sidx + " " + gridPara.Sord).ToList();
                }
                catch
                {
                }
            }

            return new Json<T>
            {
                valid = true,
                total = totalPages,
                page = gridPara.Page,
                records = totalRecords,
                //data = result.Skip(pageIndex * pageSize).Take(pageSize).ToList<T>()
                data = result
            };
        }

        public static JsonFlexBox<T> ToJsonFlexBoxResult<T>(List<T> result) where T : new()
        {
            var _json = new JsonFlexBox<T>();

            _json.results = result;
            _json.total = result.Count;

            return _json;
        }

        public static string[] PdfToImage(string fileName)
        {
            List<string> strFilename = new List<string>();

            PdfDocument pdf = new PdfDocument();

            string p;
            p = System.Configuration.ConfigurationSettings.AppSettings["PATH_ATTACH_FILE"];
            string path = System.AppDomain.CurrentDomain.BaseDirectory + p;
          //  pdf.LoadFromFile(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Pdf\Sample Inv format.pdf");
            pdf.LoadFromFile( path + "/Sample Inv format.pdf");

            System.Drawing.Image jpg;

            for (int i = 0; i < pdf.Pages.Count; i++)
            {
                //jpg = pdf.SaveAsImage(i);
                //jpg.Save("C://" + i + ".jpg");

                System.Drawing.Image bmp = pdf.SaveAsImage(i);
                System.Drawing.Image emf = pdf.SaveAsImage(i, Spire.Pdf.Graphics.PdfImageType.Metafile);
                System.Drawing.Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
                using (Graphics g = Graphics.FromImage(zoomImg))
                {
                    g.ScaleTransform(2.0f, 2.0f);
                    g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
                }
                bmp.Save(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToBmp" + i + ".jpeg");
                System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToBmp" + i + ".jpeg");
                //  emf.Save("convertToEmf.png");
                // System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Images/convertToEmf.png");
                zoomImg.Save(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToZoom" + i + ".PNG");
                System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToZoom" + i + ".PNG");
            }


            return strFilename.ToArray();

        }

        public void WriteLog(Model.Log Log)
        {
            _BLL.WriteLog(Log);
        }

    }

    public class Json<T>
    {
        public bool valid { get; set; }
        public int? total { get; set; }
        public int? page { get; set; }
        public int? records { get; set; }
        public List<T> data { get; set; }
    }

    public class JqGridParameter
    {
        public int? Rows { get; set; }
        public int? Page { get; set; }
        public string Sidx { get; set; }
        public string Sord { get; set; }
    }

    public class JsonFlexBox<T>
    {
        public List<T> results { get; set; }
        public int total { get; set; }
    }



    
    }

