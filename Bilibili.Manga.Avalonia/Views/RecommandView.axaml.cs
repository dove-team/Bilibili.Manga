using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.Avalonia.Windows;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.WebClient.Api;
using DynamicData;
using System.Collections.ObjectModel;
using Avalonia.Input;
using Avalonia.Extensions.Event;

namespace Bilibili.Manga.Avalonia.Views
{
    public partial class RecommandView : UserControl
    {
        private ManagaClient Client { get; }
        private GridView ListBox { get; set; }
        private ObservableCollection<Mange> Manges { get; }
        public RecommandView()
        {
            AvaloniaXamlLoader.Load(this);
            Client = new ManagaClient();
            Manges = new ObservableCollection<Mange>();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            ListBox = this.FindControl<GridView>("listBox");
            ListBox.Items = Manges;
            ListBox.ItemClick += ListBox_ItemClick;
            LoadData();
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
        private void LoadData()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    Manges.Clear();
                    var result = await Client.Recommand();
                    if (result.IsSuccess())
                        Manges.AddRange(result.Data.List);
                }
                catch { }
            });
        }
    }
}