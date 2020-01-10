using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;

namespace ComProvis.Actions.Actions.Notification.Helper
{
    public class MailHelper
    {
        #region Fields
        string _Sender;
        string _Subject;
        string _Body;
        dynamic _Object;
        bool _IsHtml;
        readonly string _SubjectPrefix;
        List<Attachment> _attachments;
        string _cc;
        private readonly IConfiguration _configuration;

        #endregion

        #region Properties
        /// <summary>
        /// Sender
        /// </summary>
        public string Sender
        {
            get { return _Sender; }
            set { _Sender = value; }
        }
        /// <summary>
        /// Recipients
        /// </summary>
        public string Recipients { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        /// <summary>
        /// Body
        /// </summary>
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        /// <summary>
        /// Bcc
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// SmtpServer
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Object
        /// </summary>
        public dynamic Object
        {
            get { return _Object; }
            set { _Object = value; }
        }
        /// <summary>
        /// IsHtml
        /// </summary>
        public bool IsHtml
        {
            get { return _IsHtml; }
            set { _IsHtml = value; }
        }

        /// <summary>
        /// Attachment
        /// </summary>
        public List<Attachment> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }

        public string Cc
        {
            get { return _cc; }
            set { _cc = value; }
        }


        #endregion

        #region Constructor

        public MailHelper(string smtpServer)
        {
            SmtpServer = smtpServer;
            _Sender = null;
            Recipients = null;
            _Subject = null;
            _Body = null;
        }

        public MailHelper(string template, string smtpServer, string subjectPrefix)
            : this(template, smtpServer, true, subjectPrefix)
        {
        }

        public MailHelper(string template, string smtpServer, bool htmlTemplate, string subjectPrefix)
        {
            _SubjectPrefix = subjectPrefix;
            SmtpServer = smtpServer;

            var index = template.IndexOf("<!DOCTYPE");

            XDocument doc;
            if (htmlTemplate && index > -1)
            {
                _IsHtml = true;

                doc = XDocument.Parse(template.Substring(0, index));
                _Body = template.Substring(index);
            }
            else
            {
                doc = XDocument.Parse(template);
                if (doc.Root != null) _Body = doc.Root.Element("body").Value;
            }

            _Sender = doc.Root.Element("from").Value;
            Recipients = doc.Root.Element("to")?.Value;
            _Subject = doc.Root.Element("subject")?.Value;

            if (doc.Root.Element("cc") != null) _cc = doc.Root.Element("cc")?.Value;
        }

        public MailHelper(string sender, string recipients, string subject, string body, string smtpServer)
        {
            SmtpServer = smtpServer;
            _Sender = sender;
            Recipients = recipients;
            _Subject = subject;
            _Body = body;
        }
        #endregion

        #region Methods
        public void ParseBody(JObject param)
        {
            _Object = param;

            foreach (var key in param)
            {
                var propertyName = $"${key.Key}";
                if (_Body.Contains(propertyName))
                {
                    var propValue = key.Value;
                    var value = propValue?.ToString() ?? "";
                    _Body = _Body.Replace(propertyName, value);
                }
            }
            _Body = _Body.Replace("\\n", Environment.NewLine);
        }

        public bool Send() => Send(null);

        public bool Send(string bcc)
        {
            Bcc = bcc;

            if (String.IsNullOrEmpty(Recipients)) return false;

            if (SmtpServer != string.Empty)
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_Sender),
                    Subject = $"{_SubjectPrefix}{_Subject}",
                    Body = _Body,
                    IsBodyHtml = _IsHtml
                };

                if (Recipients.StartsWith("$"))
                {
                    Recipients = Object["Email"];
                }

                //Send message to all recipients
                var recipientsArr = Recipients.Split(';');
                foreach (var recipient in recipientsArr)
                {
                    message.To.Add(new MailAddress(recipient.Trim()));
                }

                if (!string.IsNullOrEmpty(Bcc))
                {
                    var bccRecipientsArr = Bcc.Split(';');
                    foreach (var bccRecipient in bccRecipientsArr)
                    {
                        message.Bcc.Add(new MailAddress(bccRecipient.Trim()));
                    }
                }

                if (!string.IsNullOrEmpty(_cc))
                {
                    if (_cc.StartsWith("$")) _cc = Object["CcEmail"];

                    if (_cc != null)
                    {
                        var ccRecipientsArr = _cc.Split(';');
                        foreach (var ccRecipient in ccRecipientsArr)
                            message.CC.Add(new MailAddress(ccRecipient.Trim()));
                    }
                }

                if (_attachments != null && _attachments.Count > 0)
                {
                    foreach (var a in _attachments)
                    {
                        var data = Convert.FromBase64String(a.File);
                        var ms = new MemoryStream(data);
                        using (var attachment = new System.Net.Mail.Attachment(ms, a.FileName))
                        {
                            message.Attachments.Add(attachment);
                        }
                    }
                }

                using (var smtp = new SmtpClient(SmtpServer))
                {
                    smtp.Send(message);
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
