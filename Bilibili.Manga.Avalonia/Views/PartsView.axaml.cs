using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.Avalonia.ViewModels;
using Bilibili.Manga.Model.Home;
using Bilibili.Manga.Avalonia.Windows;
using Avalonia.Input;

namespace Bilibili.Manga.Avalonia.Views
{
    public partial class PartsView : UserControl
    {
        private PartsViewModel ViewModel { get; }
        private HorizontalItemsRepeater StyleList { get; set; }
        private HorizontalItemsRepeater AreaList { get; set; }
        private HorizontalItemsRepeater StatusList { get; set; }
        private HorizontalItemsRepeater OrderList { get; set; }
        private HorizontalItemsRepeater PriceList { get; set; }
        public PartsView()
        {
            AvaloniaXamlLoader.Load(this);
            ViewModel = new PartsViewModel();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            DataContext = ViewModel;
            StyleList = this.FindControl<HorizontalItemsRepeater>("styleList");
            AreaList = this.FindControl<HorizontalItemsRepeater>("areaList");
            StatusList = this.FindControl<HorizontalItemsRepeater>("statusList");
            OrderList = this.FindControl<HorizontalItemsRepeater>("orderList");
            PriceList = this.FindControl<HorizontalItemsRepeater>("priceList");
            var listBox = this.FindControl<CellListView>("listBox");
            listBox.ItemClick += ListBox_ItemClick;
            GetTypeTags();
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
        private void GetTypeTags()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    var result = await ViewModel.Client.GetType();
                    if (result.IsSuccess())
                    {
                        var arr = result.Data;
                        StyleList.Items = arr.Styles.SetAllTag("styles");
                        AreaList.Items = arr.Areas.SetAllTag("areas");
                        StatusList.Items = arr.Status.SetAllTag("status");
                        PriceList.Items = arr.Prices.SetAllTag("prices");
                        OrderList.Items = arr.Orders;
                    }
                }
                catch { }
            });
        }
    }
}