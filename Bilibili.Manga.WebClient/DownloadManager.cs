using Bilibili.Manga.Common;
using Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Bilibili.Manga.WebClient
{
    public sealed class DownloadManager
    {
        private static DownloadManager instance;
        public static DownloadManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DownloadManager();
                return instance;
            }
        }
        private readonly List<string> array;
        private DownloadService Service { get; set; }
        private DirectoryInfo SaveDirectory { get; set; }
        private readonly DownloadConfiguration configuration;
        public event ProgressCompleteHandler ProgressComplete;
        private DownloadManager()
        {
            configuration = new DownloadConfiguration()
            {
                ChunkCount = 8,
                Timeout = int.MaxValue,
                BufferBlockSize = 10240,
                ParallelDownload = true,
                MaxTryAgainOnFailover = int.MaxValue,
                MaximumBytesPerSecond = long.MaxValue,
                RequestConfiguration =
                {
                    Accept = "*/*",
                    KeepAlive = false,
                    UseDefaultCredentials = false,
                    Headers = new WebHeaderCollection(),
                    ProtocolVersion = HttpVersion.Version11,
                    CookieContainer =  new CookieContainer(),
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36 Edg/95.0.1020.44"
                }
            };
            array = new List<string>();
        }
        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var hasError = e.Cancelled || e.Error != null;
            string address = string.Empty, path = string.Empty;
            if (e.UserState is DownloadPackage package)
            {
                address = package.Address;
                path = package.FileName;
                array.Add(path);
            }
            if (e.Error is Exception ex)
                LogManager.Instance.LogError("OnDownloadFileCompleted", ex);
            ProgressComplete?.Invoke(sender, hasError, new Uri(address), path);
        }
        public void Init(string root)
        {
            SaveDirectory = new DirectoryInfo(root);
        }
        public async Task<bool> Create(string url)
        {
            try
            {
                if (Service != null)
                {
                    Service.Clear();
                    Service.DownloadFileCompleted -= OnDownloadFileCompleted;
                }
                Service = new DownloadService(configuration);
                Service.DownloadFileCompleted += OnDownloadFileCompleted;
                await Service.DownloadFileTaskAsync(url, SaveDirectory).ConfigureAwait(false);
                return true;
            }
            catch { }
            return false;
        }
        public void Clear()
        {
            if (array.Count > 0)
            {
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, "temp"));
                Service.Clear();
                foreach (var path in array)
                {
                    if (File.Exists(path))
                        File.Delete(path);
                }
            }
        }
    }
}