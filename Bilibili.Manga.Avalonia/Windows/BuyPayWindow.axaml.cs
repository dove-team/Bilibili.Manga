using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bilibili.Manga.Model.Info;
using Bilibili.Manga.Common;
using Bilibili.Manga.WebClient.Api;
using Avalonia;
using Avalonia.Extensions.Controls;
using Newtonsoft.Json.Linq;
using HA = Avalonia.Layout.HorizontalAlignment;

namespace Bilibili.Manga.Windows
{
    public partial class BuyPayWindow : WindowBase
    {
        private int EpId { get; set; }
        private EpClient Client { get; }
        private BuyInfo Info { get; set; }
        private TextBlock TbTitle { get; set; }
        private StackPanel Panel { get; set; }
        public event CallbackJObjectObject Callback;
        public BuyPayWindow()
        {
            AvaloniaXamlLoader.Load(this);
            Client = new EpClient();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            ShowInTaskbar = false;
            TbTitle = this.FindControl<TextBlock>("title");
            Panel = this.FindControl<StackPanel>("panel");
            SetSize(new Size(300, 240));
        }
        public void InitData(int id, BuyInfo data)
        {
            EpId = id;
            Info = data;
            TbTitle.Text = $" £”‡¬˛±“: { data.Remain_Gold }£ª\r\n £”‡¬˛∂¡»Ø£∫{data.Remain_Coupon }£ª\r\n £”‡Õ®”√»Ø£∫{data.Remain_Silver }£ª\r\n £”‡œﬁ√‚ø®£∫{data.Remain_Item}";
            AddButton("¬˛±“π∫¬Ú", 1);
            AddButton("¬˛∂¡»Øπ∫¬Ú", 2);
            AddButton("Õ®”√»Øπ∫¬Ú", 3);
            AddButton("œﬁ√‚ø®π∫¬Ú", 4);
        }
        private void AddButton(string content, int type)
        {
            Button button = new Button
            {
                Width = 180,
                Content = content,
                CommandParameter = type,
                HorizontalAlignment = HA.Center,
                Padding = new Thickness(0, 8, 0, 0),
                HorizontalContentAlignment = HA.Center
            };
            button.Click += Button_Click;
            this.Panel.Children.Add(button);
        }
        private async void Button_Click(object? sender, RoutedEventArgs e)
        {
            if (e.Source is Button button)
            {
                var pay = Info.Pay_Gold;
                var cid = Info.Recommend_Coupon_Id;
                var type = button.CommandParameter.ToInt32();
                var result = await Client.Buy(EpId, type, cid, pay);
                if (result.IsSuccess())
                {
                    await MessageBox.Show("Ã· æ", "π∫¬Ú≥…π¶£°");
                    JObject obj = JObject.FromObject(new { cid = Info.Comic_Id, id = EpId });
                    Callback?.Invoke(this, obj);
                    this.Close();
                }
                else if (result.IsNotEmpty())
                    await MessageBox.Show("Ã· æ", result.Msg);
                else
                    await MessageBox.Show("Ã· æ", "ªÒ»° ˝æ› ß∞‹£°");
            }
        }
    }
}