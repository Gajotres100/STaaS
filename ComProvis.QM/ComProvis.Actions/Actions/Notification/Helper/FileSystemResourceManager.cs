using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;

namespace ComProvis.Actions.Actions.Notification.Helper
{
    class FileSystemResourceManager : ResourceManager
    {
        #region Constants

        const string HeaderTemplate = "header-template";
        const string FooterTemplate = "footer-template";

        #endregion

        #region Variables

        readonly string _path;
        readonly string _fileExtension;

        #endregion

        #region Constructor

        public FileSystemResourceManager(string path, string fileExtension)
        {
            _path = path;
            _fileExtension = fileExtension;
        }

        #endregion

        #region Methods


        public override string GetString(string name)
        {
            var fileName = Path.Combine(_path, name + _fileExtension);

            if (File.Exists(fileName))
            {
                var fileContent = File.ReadAllText(fileName);
                fileContent = LoadTemplate(fileContent, HeaderTemplate);
                fileContent = LoadTemplate(fileContent, FooterTemplate);

                return fileContent;
            }
            return null;
        }

        public string LoadTemplate(string text, string templateName)
        {
            var pattern = string.Format("<{0}>(.*)</{0}>", templateName);
            var regex = new Regex(pattern);
            var v = regex.Match(text);
            if (v.Groups.Count > 0)
            {
                var templateFileName = v.Groups[1].ToString();
                if (!string.IsNullOrEmpty(templateName))
                {
                    templateFileName = Path.Combine(_path, templateFileName);
                    if (File.Exists(templateFileName))
                    {
                        var content = File.ReadAllText(templateFileName);
                        text = Regex.Replace(text, pattern, content);
                    }
                }
            }

            return text;
        }

        #endregion
    }
}
