using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Bilibili.Manga.Avalonia.ViewModels;
using Bilibili.Manga.Common;

namespace Bilibili.Manga.Avalonia.Windows
{
    public partial class LoginWindow : WindowBase
    {
        private Image Ic33 { get; set; }
        private Image Ic22 { get; set; }
        private Grid QrLogin { get; set; }
        private Grid PwdLogin { get; set; }
        private Image Ic22Hide { get; set; }
        private Image Ic33Hide { get; set; }
        public event CallbackObject Callback;
        public LoginWindow()
        {
            AvaloniaXamlLoader.Load(this);
            Width = 480;
            Height = 320;
            var model = new LoginViewModel(this);
            model.Callback += Model_Callback;
            model.QrLoginCallback += Model_QrLoginCallback;
            DataContext = model;
            InitializeComponent();
        }
        private void Model_Callback(object sender)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
           {
               Callback?.Invoke(this);
               this.Close();
           });
        }
        private void PwdBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            Ic33Hide.IsVisible = false;
            Ic22Hide.IsVisible = false;
            Ic33.IsVisible = true;
            Ic22.IsVisible = true;
        }
        private void PwdBox_Focus(object? sender, GotFocusEventArgs e)
        {
            Ic33.IsVisible = false;
            Ic22.IsVisible = false;
            Ic33Hide.IsVisible = true;
            Ic22Hide.IsVisible = true;
        }
        public void InitializeComponent()
        {
            var pwdBox = this.FindControl<TextBox>("pwdBox");
            pwdBox.GotFocus += PwdBox_Focus;
            pwdBox.LostFocus += PwdBox_LostFocus;
            Ic22 = this.FindControl<Image>("Ic22");
            Ic22Hide = this.FindControl<Image>("Ic22Hide");
            Ic33 = this.FindControl<Image>("Ic33");
            Ic33Hide = this.FindControl<Image>("Ic33Hide");
            QrLogin = this.FindControl<Grid>("QrLogin");
            PwdLogin = this.FindControl<Grid>("PwdLogin");
        }
        private void Model_QrLoginCallback(object sender, bool result)
        {
            if (result)
            {
                QrLogin.IsVisible = true;
                PwdLogin.IsVisible = false;
            }
            else
            {
                QrLogin.IsVisible = false;
                PwdLogin.IsVisible = true;
            }
        }
    }
}