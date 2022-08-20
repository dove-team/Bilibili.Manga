using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Extensions.Event;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Bilibili.Manga.Avalonia.Windows;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.WebClient.Api;
using DynamicData;
using System.Collections.ObjectModel;

namespace Bilibili.Manga.Avalonia.Views
{
    public partial class SearchView : UserControl
    {
        private int Page { get; set; } = 1;
        private ManagaClient Client { get; }
        private GridView ListBox { get; set; }
        private EditTextView Keywords { get; set; }
        private ObservableCollection<MangeItem> SearchList { get; }
        public SearchView()
        {
            AvaloniaXamlLoader.Load(this);
            SearchList = new ObservableCollection<MangeItem>();
            InitializeComponent();
            Client = new ManagaClient();
        }
        private void InitializeComponent()
        {
            var btnSearch = this.FindControl<Button>("btnSearch");
            ListBox = this.FindControl<GridView>("listBox");
            Keywords = this.FindControl<EditTextView>("txtKeywords");
            btnSearch.Click += BtnSearch_Click;
            ListBox.Items = SearchList;
            ListBox.ItemClick += ListBox_ItemClick;
        }
        private void ListBox_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            if (e.ClickMouse == MouseButton.Left && e.ClickItem is ListBoxItem item)
            {
                if (item.Content is MangeItem manga)
                {
                    MangaWindow window = new MangaWindow();
                    window.Show();
                    window.InitData(manga.Id);
                }
            }
        }
        private void BtnSearch_Click(object? sender, RoutedEventArgs e)
        {
            Page = 1;
            LoadData();
        }
        private void LoadData()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    SearchList.Clear();
                    var keywords = Keywords.Text;
                    if (keywords.IsNotEmpty())
                    {
                        var result = await Client.Search(keywords, Page);
                        if (result.IsSuccess())
                            SearchList.AddRange(result.Data.List);
                    }
                }
                catch { }
            });
        }
    }
}