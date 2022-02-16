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
        private bool AutoUpdate { get; set; }
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
                    var result = await MessageBox.Show(this, "��ʾ", "�����°汾�ͻ��ˣ��Ƿ������ؽ��и��£�");
                    AutoUpdate = result == true;
                    DownloadManager.Instance.ProgressComplete += Instance_ProgressComplete;
                    await DownloadManager.Instance.Create(App.UpdatePackage).ConfigureAwait(false);
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
                        if (await MessageBox.Show(this, "��ʾ", "�����°汾��ϣ��Ƿ�װ��") == true)
                        {
                            Client.ApplyNow(filePath);
                            await Task.Delay(800);
                            Process.GetCurrentProcess().Kill();
                        }
                    });
                }
            }
        }
    }
}