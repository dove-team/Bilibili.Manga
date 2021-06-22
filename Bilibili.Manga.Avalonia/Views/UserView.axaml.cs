using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Bilibili.Manga.Avalonia.ViewModels;
using Bilibili.Manga.Avalonia.Windows;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.Model.Info;
using Bilibili.Manga.WebClient;
using System;

namespace Bilibili.Manga.Avalonia.Views
{
    public partial class UserView : UserControl
    {
        private Image UserCover { get; set; }
        private CellListView BuyList { get; set; }
        private TextBlock UserName { get; set; }
        private CellListView FollowList { get; set; }
        private CellListView HistoryList { get; set; }
        private UserViewModel ViewModel { get; }
        public UserView()
        {
            AvaloniaXamlLoader.Load(this);
            ViewModel = new UserViewModel();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            DataContext = ViewModel;
            UserCover = this.FindControl<Image>("userCover");
            UserName = this.FindControl<TextBlock>("userName");
            UserCover.Tapped += UserCover_Tapped;
            var orderFollow = this.FindControl<RadioButton>("orderFollow");
            orderFollow.Checked += Order_Checked;
            var orderUpdate = this.FindControl<RadioButton>("orderUpdate");
            orderUpdate.Checked += Order_Checked;
            var orderRecent = this.FindControl<RadioButton>("orderRecent");
            orderRecent.Checked += Order_Checked;
            BuyList = this.FindControl<CellListView>("buyList");
            BuyList.VirtualizationMode = ItemVirtualizationMode.None;
            FollowList = this.FindControl<CellListView>("followList");
            FollowList.VirtualizationMode = ItemVirtualizationMode.None;
            HistoryList = this.FindControl<CellListView>("historyList");
            HistoryList.VirtualizationMode = ItemVirtualizationMode.None;
            BuyList.ItemClick += BuyList_ItemClick;
            FollowList.ItemClick += FollowList_ItemClick;
            HistoryList.ItemClick += HistoryList_ItemClick;
            ViewModel.Init();
            ShowInfo();
        }
        private void BuyList_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            switch (e.ClickMouse)
            {
                case MouseButton.Left:
                    {
                        if (e.ClickItem is ListBoxItem item && item.Content is HistoryManga manga)
                        {
                            MangaWindow window = new MangaWindow();
                            window.Show();
                            window.InitData(manga.Comic_Id);
                        }
                        break;
                    }
            }
        }
        private void HistoryList_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            switch (e.ClickMouse)
            {
                case MouseButton.Left:
                    {
                        if (e.ClickItem is ListBoxItem item && item.Content is HistoryManga manga)
                        {
                            MangaWindow window = new MangaWindow();
                            window.Show();
                            window.InitData(manga.Comic_Id);
                        }
                        break;
                    }
                case MouseButton.Right:
                    {
                        if (e.ClickItem is ListBoxItem item && item.Content is HistoryManga manga)
                        {
                            PopupMenu popupMenu = new PopupMenu
                            {
                                Tag = manga,
                                Items = new[] { "删除记录", "查看详细" }
                            };
                            popupMenu.ItemClick += PopupMenu_ItemClick;
                            popupMenu.Show(item);
                        }                        
                        break;
                    }
            }
        }
        private void FollowList_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            switch (e.ClickMouse)
            {
                case MouseButton.Left:
                    {
                        if (e.ClickItem is ListBoxItem item && item.Content is FollowMange manga)
                        {
                            MangaWindow window = new MangaWindow();
                            window.Show();
                            window.InitData(manga.Comic_Id);
                        }
                        break;
                    }
                case MouseButton.Right:
                    {
                        if (e.ClickItem is ListBoxItem item && item.Content is FollowMange manga)
                        {
                            PopupMenu popupMenu = new PopupMenu
                            {
                                Tag = manga,
                                Items = new[] { "取消关注" }
                            };
                            popupMenu.ItemClick += PopupMenu_ItemClick;
                            popupMenu.Show(item);
                        }
                        break;
                    }
            }
        }
        private async void PopupMenu_ItemClick(object? sender, ItemClickEventArgs e)
        {
            if (sender is PopupMenu popupMenu)
            {
                if (popupMenu.Tag is HistoryManga historyManga)
                {
                    switch (e.ItemIndex)
                    {
                        case 0:
                            {
                                var result = await ViewModel.Client.Delete(historyManga.Comic_Id);
                                if (result.IsSuccess())
                                    await MessageBox.Show("提示", "删除记录成功！", MessageBoxButtons.Ok);
                                ViewModel.GetHistoryData();
                                break;
                            }
                        case 1:
                            {
                                MangaWindow window = new MangaWindow();
                                window.Show();
                                window.InitData(historyManga.Comic_Id);
                                break;
                            }
                    }
                }
                else if (popupMenu.Tag is FollowMange followMange)
                {
                    var result = await ViewModel.Client.UnFollow(followMange.Comic_Id);
                    if (result.IsSuccess())
                        await MessageBox.Show("提示", "取消追漫成功！", MessageBoxButtons.Ok);
                    ViewModel.GetFollowData(1);
                }
            }
        }
        private async void UserCover_Tapped(object? sender, RoutedEventArgs e)
        {
            if (!ApiProvider.IsLogin)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Callback += (_) => { ShowInfo(); };
                loginWindow.Show();
            }
            else
            {
                if (await MessageBox.Show("提示", "是否退出账号？") == true)
                {
                    SettingHelper.UserID = 0;
                    SettingHelper.UserInfo = null;
                    SettingHelper.AccessKey = string.Empty;
                    SettingHelper.UserHead = string.Empty;
                    SettingHelper.RefreshToken = string.Empty;
                    var bitmap = new Bitmap(StaticValue.AssetLoader.Open(new Uri("avares://Bilibili.Manga.Avalonia/Assets/Login/noavatar.png")));
                    UserCover.Source = bitmap;
                    UserName.Text = "未登录";
                }
            }
        }
        public async void ShowInfo()
        {
            var userInfo = SettingHelper.UserInfo;
            if (userInfo.IsNotEmpty())
            {
                UserName.Text = userInfo.Name;
                using var stream = await Current.HttpClient.GetImageStream(new Uri(userInfo.Face));
                UserCover.Source = new Bitmap(stream);
            }
            ViewModel.Init();
        }
        private void Order_Checked(object? sender, RoutedEventArgs e)
        {
            if (e.Source is RadioButton button)
            {
                var order = button.CommandParameter.ToInt32();
                ViewModel.GetFollowData(order);
            }
        }
    }
}