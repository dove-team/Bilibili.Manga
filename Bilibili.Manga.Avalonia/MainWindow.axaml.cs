using Avalonia;
using Avalonia.Extensions.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.WebClient;
using Bilibili.Manga.WebClient.Api;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Bilibili.Manga.Avalonia
{
    public partial class MainWindow : WindowBase
    {
        private UpdateClient Client { get; }
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
                App.UpdatePackage = await Client.CheckUpdate();
                if (App.UpdatePackage.IsNotEmpty())
                {
                    if (true == await MessageBox.Show(this, "提示", "存在新版本客户端，是否在下载进行更新？"))
                    {
                        DownloadManager.Instance.ProgressComplete += Instance_ProgressComplete;
                        await DownloadManager.Instance.Create(App.UpdatePackage).ConfigureAwait(false);
                    }
                }
            });
        }
        private void Instance_ProgressComplete(object sender, bool hasError, Uri downloadUrl, string filePath)
        {
            if (!hasError)
            {
                if (File.Exists(filePath))
                {
                    Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        if (await MessageBox.Show(this, "提示", "下载新版本完毕，是否安装（由于系统限制需要手动进行安装）？") == true)
                        {
                            Client.ApplyNow(filePath);
                            App.Exit();
                        }
                    });
                }
            }
        }
    }
}