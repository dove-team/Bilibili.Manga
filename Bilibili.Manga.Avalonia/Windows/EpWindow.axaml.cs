using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.WebClient;
using Bilibili.Manga.WebClient.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilibili.Manga.Avalonia.Windows
{
    public partial class EpWindow : WindowBase
    {
        private int LoadCount = 0;
        private bool IsLoading = false;
        private EpClient Client { get; }
        private StackPanel Panel { get; set; }
        private List<EpImage> Images { get; }
        public EpWindow()
        {
            AvaloniaXamlLoader.Load(this);
            Client = new EpClient();
            Images = new List<EpImage>();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            Panel = this.FindControl<StackPanel>("panel");
            if (Panel.Parent is ScrollViewer scrollViewer)
                scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
        }
        private void ScrollViewer_ScrollChanged(object? sender, ScrollChangedEventArgs e)
        {
            if (e.Source is ScrollViewer scrollViewer && scrollViewer.Offset.Y > 0)
            {
                if ((scrollViewer.Offset.Y + Bounds.Height) >= (Panel.Bounds.Height * 0.8))
                    GetNext();
            }
        }
        public void Init(int epId)
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var result = await Client.GetImages(epId);
                if (result.IsSuccess())
                {
                    Images.Clear();
                    Images.AddRange(result.Data.Images);
                    GetNext();
                }
                else if (result.IsSuccess(1))
                {
                    if (await MessageBox.Show("提示", "需要购买此章节") == true)
                    {
                        var info = await Client.BuyInfo(epId);
                        if (info.Code == 0)
                        {
                            BuyPayWindow buyPayWindow = new BuyPayWindow();
                            buyPayWindow.Callback += BuyPayWindow_Callback;
                            buyPayWindow.InitData(epId, info.Data);
                            await buyPayWindow.ShowDialog(this);
                        }
                        else
                        {
                            await MessageBox.Show("错误", "获取购买信息失败!");
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    await MessageBox.Show("错误", "获取信息失败!");
                }
            });
        }
        private void BuyPayWindow_Callback(object sender, int value)
        {
            Init(value);
        }
        private async void GetNext()
        {
            if (!IsLoading)
            {
                IsLoading = true;
                if (LoadCount < Images.Count)
                {
                    var imgs = Images.Skip(LoadCount).Take(3).Select(x => x.Path);
                    var result = await Client.GetUrl(imgs);
                    if (result.IsSuccess())
                    {
                        foreach (var image in result.Data)
                        {
                            var url = $"{image.Url}?token={image.Token}";
                            var imageCtl = await CreateImageAsync(url);
                            this.Panel.Children.Add(imageCtl);
                        }
                        LoadCount += 3;
                    }
                }
                IsLoading = false;
            }
        }
        private async Task<Image> CreateImageAsync(string url)
        {
            Image imageCtl = new Image();
            try
            {
                using var result = await Current.HttpClient.GetImageStream(new Uri(url));
                if (result != null && result.Length > 0)
                    imageCtl.Source = new Bitmap(result);
            }
            catch { }
            return imageCtl;
        }
    }
}