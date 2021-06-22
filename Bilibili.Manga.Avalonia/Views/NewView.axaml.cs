using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
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
        private async void InitializeComponent()
        {
            var listBox = this.FindControl<CellListView>("listBox");
            listBox.ItemClick += ListBox_ItemClick;
            var result = await Client.New(1);
            if (result.IsSuccess())
                listBox.Items = result.Data;
        }
        private void ListBox_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            if (e.ClickMouse == MouseButton.Left && e.ClickItem is ListBoxItem item)
            {
                if (item.Content is PartManga manga)
                {
                    MangaWindow window = new MangaWindow();
                    window.Show();
                    window.InitData(manga.Season_Id);
                }
            }
        }
    }
}