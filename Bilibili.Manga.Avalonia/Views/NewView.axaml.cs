using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Extensions.Event;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Bilibili.Manga.Avalonia.Windows;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Home;
using Bilibili.Manga.WebClient.Api;

namespace Bilibili.Manga.Avalonia.Views
{
    public partial class NewView : UserControl
    {
        private ManagaClient Client { get; }
        public NewView()
        {
            AvaloniaXamlLoader.Load(this);
            Client = new ManagaClient();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            var listBox = this.FindControl<GridView>("listBox");
            listBox.ItemClick += ListBox_ItemClick;
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var result = await Client.New(1);
                if (result.IsSuccess())
                    listBox.Items = result.Data;
            });
        }
        private void ListBox_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            if (e.ClickMouse == MouseButton.Left && e.ClickItem is ListBoxItem item)
            {
                if (item.Content is PartManga manga)
                {
                    MangaWindow window = new MangaWindow();
                    window.InitData(manga.Season_Id);
                    window.Show();
                }
            }
        }
    }
}