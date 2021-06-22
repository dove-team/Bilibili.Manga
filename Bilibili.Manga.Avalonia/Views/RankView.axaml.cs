using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Bilibili.Manga.Avalonia.Windows;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.WebClient.Api;

namespace Bilibili.Manga.Avalonia.Views
{
    public partial class RankView : UserControl
    {
        private ManagaClient Client { get; }
        public RankView()
        {
            AvaloniaXamlLoader.Load(this);
            Client = new ManagaClient();
            InitializeComponent();
        }
        private async void InitializeComponent()
        {
            var listBox = this.FindControl<CellListView>("listBox");
            listBox.ItemClick += ListBox_ItemClick;
            var result = await Client.Rank();
            if (result.IsSuccess())
                listBox.Items = result.Data;
        }
        private void ListBox_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            if (e.ClickMouse == MouseButton.Left && e.ClickItem is ListBoxItem item)
            {
                if (item.Content is Mange manga)
                {
                    MangaWindow window = new MangaWindow();
                    window.Show();
                    window.InitData(manga.Comic_Id);
                }
            }
        }
    }
}