﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComProvis.Actions.Proxy.STaaSPlatformApi
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "STaaSPlatformApi.ISTaaSPlatformApi")]
    public interface ISTaaSPlatformApi
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceCreate", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceCreateResponse")]
        bool DiskSpaceCreate(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceCreate", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceCreateResponse")]
        System.Threading.Tasks.Task<bool> DiskSpaceCreateAsync(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceMarkDeleted", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceMarkDeletedResponse")]
        bool DiskSpaceMarkDeleted(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceMarkDeleted", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceMarkDeletedResponse")]
        System.Threading.Tasks.Task<bool> DiskSpaceMarkDeletedAsync(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceDelete", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceDeleteResponse")]
        bool DiskSpaceDelete(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceDelete", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/DiskSpaceDeleteResponse")]
        System.Threading.Tasks.Task<bool> DiskSpaceDeleteAsync(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/TempChunkDirectoryDelete", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/TempChunkDirectoryDeleteResponse")]
        void TempChunkDirectoryDelete(string filename);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/TempChunkDirectoryDelete", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/TempChunkDirectoryDeleteResponse")]
        System.Threading.Tasks.Task TempChunkDirectoryDeleteAsync(string filename);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileDownload", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileDownloadResponse")]
        System.IO.Stream FileDownload(string path);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileDownload", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileDownloadResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> FileDownloadAsync(string path);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileExists", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileExistsResponse")]
        bool FileExists(string filePath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileExists", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileExistsResponse")]
        System.Threading.Tasks.Task<bool> FileExistsAsync(string filePath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileCopy", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileCopyResponse")]
        bool FileCopy(string sourcePath, string destinationPath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileCopy", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileCopyResponse")]
        System.Threading.Tasks.Task<bool> FileCopyAsync(string sourcePath, string destinationPath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileDelete", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileDeleteResponse")]
        void FileDelete(string filePath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileDelete", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileDeleteResponse")]
        System.Threading.Tasks.Task FileDeleteAsync(string filePath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileUpload", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileUploadResponse")]
        bool FileUpload(byte[] data, string path);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/FileUpload", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/FileUploadResponse")]
        System.Threading.Tasks.Task<bool> FileUploadAsync(byte[] data, string path);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileExists", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileExistsResponse")]
        bool ChunkedFileExists(string filePath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileExists", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileExistsResponse")]
        System.Threading.Tasks.Task<bool> ChunkedFileExistsAsync(string filePath);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileUpload", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileUploadResponse")]
        bool ChunkedFileUpload(byte[] data, string path, string filename, int chunkNumber, int totalChunks, int chunkSize, int currentChunkSize, long totalSize, bool overwriteFile);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileUpload", ReplyAction = "http://tempuri.org/ISTaaSPlatformApi/ChunkedFileUploadResponse")]
        System.Threading.Tasks.Task<bool> ChunkedFileUploadAsync(byte[] data, string path, string filename, int chunkNumber, int totalChunks, int chunkSize, int currentChunkSize, long totalSize, bool overwriteFile);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISTaaSPlatformApiChannel : ComProvis.Actions.Proxy.STaaSPlatformApi.ISTaaSPlatformApi, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class STaaSPlatformApiClient : System.ServiceModel.ClientBase<ComProvis.Actions.Proxy.STaaSPlatformApi.ISTaaSPlatformApi>, ComProvis.Actions.Proxy.STaaSPlatformApi.ISTaaSPlatformApi
    {

        public STaaSPlatformApiClient()
        {
        }

        public STaaSPlatformApiClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public STaaSPlatformApiClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public STaaSPlatformApiClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public STaaSPlatformApiClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public bool DiskSpaceCreate(string name)
        {
            return base.Channel.DiskSpaceCreate(name);
        }

        public System.Threading.Tasks.Task<bool> DiskSpaceCreateAsync(string name)
        {
            return base.Channel.DiskSpaceCreateAsync(name);
        }

        public bool DiskSpaceMarkDeleted(string name)
        {
            return base.Channel.DiskSpaceMarkDeleted(name);
        }

        public System.Threading.Tasks.Task<bool> DiskSpaceMarkDeletedAsync(string name)
        {
            return base.Channel.DiskSpaceMarkDeletedAsync(name);
        }

        public bool DiskSpaceDelete(string name)
        {
            return base.Channel.DiskSpaceDelete(name);
        }

        public System.Threading.Tasks.Task<bool> DiskSpaceDeleteAsync(string name)
        {
            return base.Channel.DiskSpaceDeleteAsync(name);
        }

        public void TempChunkDirectoryDelete(string filename)
        {
            base.Channel.TempChunkDirectoryDelete(filename);
        }

        public System.Threading.Tasks.Task TempChunkDirectoryDeleteAsync(string filename)
        {
            return base.Channel.TempChunkDirectoryDeleteAsync(filename);
        }

        public System.IO.Stream FileDownload(string path)
        {
            return base.Channel.FileDownload(path);
        }

        public System.Threading.Tasks.Task<System.IO.Stream> FileDownloadAsync(string path)
        {
            return base.Channel.FileDownloadAsync(path);
        }

        public bool FileExists(string filePath)
        {
            return base.Channel.FileExists(filePath);
        }

        public System.Threading.Tasks.Task<bool> FileExistsAsync(string filePath)
        {
            return base.Channel.FileExistsAsync(filePath);
        }

        public bool FileCopy(string sourcePath, string destinationPath)
        {
            return base.Channel.FileCopy(sourcePath, destinationPath);
        }

        public System.Threading.Tasks.Task<bool> FileCopyAsync(string sourcePath, string destinationPath)
        {
            return base.Channel.FileCopyAsync(sourcePath, destinationPath);
        }

        public void FileDelete(string filePath)
        {
            base.Channel.FileDelete(filePath);
        }

        public System.Threading.Tasks.Task FileDeleteAsync(string filePath)
        {
            return base.Channel.FileDeleteAsync(filePath);
        }

        public bool FileUpload(byte[] data, string path)
        {
            return base.Channel.FileUpload(data, path);
        }

        public System.Threading.Tasks.Task<bool> FileUploadAsync(byte[] data, string path)
        {
            return base.Channel.FileUploadAsync(data, path);
        }

        public bool ChunkedFileExists(string filePath)
        {
            return base.Channel.ChunkedFileExists(filePath);
        }

        public System.Threading.Tasks.Task<bool> ChunkedFileExistsAsync(string filePath)
        {
            return base.Channel.ChunkedFileExistsAsync(filePath);
        }

        public bool ChunkedFileUpload(byte[] data, string path, string filename, int chunkNumber, int totalChunks, int chunkSize, int currentChunkSize, long totalSize, bool overwriteFile)
        {
            return base.Channel.ChunkedFileUpload(data, path, filename, chunkNumber, totalChunks, chunkSize, currentChunkSize, totalSize, overwriteFile);
        }

        public System.Threading.Tasks.Task<bool> ChunkedFileUploadAsync(byte[] data, string path, string filename, int chunkNumber, int totalChunks, int chunkSize, int currentChunkSize, long totalSize, bool overwriteFile)
        {
            return base.Channel.ChunkedFileUploadAsync(data, path, filename, chunkNumber, totalChunks, chunkSize, currentChunkSize, totalSize, overwriteFile);
        }
    }
}
