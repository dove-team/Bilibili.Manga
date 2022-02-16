using Avalonia.Extensions.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.WebClient;
using Bilibili.Manga.WebClient.Api;
using System;
using System.IO;

namespace Bilibili.Manga.Avalonia
{
    public partial class MainWindow : WindowBase
    {
        private UpdateClient Client { get; }
        private bool AutoUpdate { get; set; }
        private string UpdatePackage { get; set; }
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            Client = new UpdateClient();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                UpdatePackage = await Client.CheckUpdate();
                if (UpdatePackage.IsNotEmpty())
                {
                    var result = await MessageBox.Show(this, "提示", "存在新版本客户端，将自动下载，是否在下载完毕后自动安装？");
                    AutoUpdate = result == true;
                    DownloadManager.Instance.ProgressComplete += Instance_ProgressComplete;
                    await DownloadManager.Instance.Create(UpdatePackage).ConfigureAwait(false);
                }
            });
        }
        private void Instance_ProgressComplete(object sender, bool hasError, Uri downloadUrl, string filePath)
        {
            if (!hasError)
            {
                if (File.Exists(filePath))
                {

                }
            }
        }
    }
}