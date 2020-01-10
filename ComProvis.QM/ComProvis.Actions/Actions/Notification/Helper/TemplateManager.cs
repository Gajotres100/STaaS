using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace ComProvis.Actions.Actions.Notification.Helper
{
    public class TemplateManager
    {
        static ResourceManager _resourceManager;
        public static bool? UseFileSystemResourceManager;
        public static ResourceManager EmailResources(string fileSystemResourceManagerResourcesPath) => _resourceManager ?? (_resourceManager = new FileSystemResourceManager(fileSystemResourceManagerResourcesPath ??
                                                            "MailTemplates", ".html"));
    }
}
