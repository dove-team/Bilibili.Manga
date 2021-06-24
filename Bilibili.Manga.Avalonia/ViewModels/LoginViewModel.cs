using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Media.Imaging;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using Bilibili.Manga.WebClient;
using Bilibili.Manga.WebClient.Api;
using ReactiveUI;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Reactive;
using System.Timers;
using ZXing;
using ZXing.Common;

namespace Bilibili.Manga.Avalonia.ViewModels
{
    public sealed class LoginViewModel : ReactiveObject, IDisposable
    {
        private bool isQrLogin=false;
        public string Passwd { get; set; }
        public string Account { get; set; }
        private Bitmap _qrSource;
        public Bitmap QrSource
        {
            get => _qrSource;
            set => this.RaiseAndSetIfChanged(ref _qrSource, value);
        }
        private string _Message;
        public string Message
        {
            get => _Message;
            set => this.RaiseAndSetIfChanged(ref _Message, value);
        }
        private Timer Timer { get; }
        public Window Window { get; set; }
        private string AuthCode { get; set; }
        private LoginClient UserManager { get; set; }
        public event CallbackObject Callback;
        public event CallbackBooleanObject QrLoginCallback;
        public ReactiveCommand<string, Unit> OnButtonClick { get; }
        public LoginViewModel(Window window)
        {
            Window = window;
            Timer = new Timer(6000);
            Timer.Elapsed += Timer_Elapsed;
            UserManager = new LoginClient { };
            OnButtonClick = ReactiveCommand.Create<string>(ButtonClick);
        }
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var result = await UserManager.PollQRAuthInfo(AuthCode);
                if (result.IsSuccess())
                {
                    Timer.Stop();
                    var userId = result.Data.Mid;
                    var data = await UserManager.GetUserInfo(userId);
                    if (data.IsNotEmpty())
                    {
                        var userInfo = data.Data.Card;
                        SettingHelper.UserInfo = userInfo;
                        StaticValue.CurrentUserId = userInfo.Mid.ToInt64();
                        StaticValue.CurrentUserName = userInfo.Name;
                        var userHead = await Current.HttpClient.GetImageStream(userInfo.Face, userInfo.Mid);
                        SettingHelper.UserHead = userHead;
                        StaticValue.CurrentUserIcon = new Bitmap(userHead);
                        var expiresDatetime = DateTime.Now.AddSeconds(result.Data.Expires_in);
                        SettingHelper.AccessKey = result.Data.Access_token;
                        SettingHelper.UserID = userId;
                        SettingHelper.LoginExpires = expiresDatetime;
                        SettingHelper.RefreshToken = result.Data.Refresh_token;
                        if (StaticValue.LoginEvent != null)
                            StaticValue.LoginEvent.Execute();
                        Callback?.Invoke(this);
                    }
                }
            }
            catch(Exception ex)
            {
                LogManager.Instance.LogError("PollQRAuthInfo", ex);
            }
        }
        private async void ButtonClick(string obj)
        {
            switch (obj)
            {
                case "qr":
                    {
                        isQrLogin = true;
                        QrLoginCallback?.Invoke(this, true);
                        var result = await UserManager.GetQRAuthInfo();
                        if (result.Code == 0)
                        {
                            try
                            {
                                BarcodeWriter barcodeWriter = new BarcodeWriter
                                {
                                    Format = BarcodeFormat.QR_CODE,
                                    Options = new EncodingOptions
                                    {
                                        Margin = 1,
                                        Height = 200,
                                        Width = 200
                                    }
                                };
                                using var img = barcodeWriter.Write(result.Data.Url);
                                using var ms = new MemoryStream();
                                img.Save(ms, ImageFormat.Png);
                                ms.Position = 0;
                                ms.Flush();
                                AuthCode = result.Data.Auth_Code;
                                var bitmap = new Bitmap(ms);
                                QrSource = bitmap;
                                Timer.Start();
                            }
                            catch (Exception ex)
                            {
                                LogManager.Instance.LogError("GetQRAuthInfo", ex);
                                Timer.Stop();
                                await MessageBox.Show("提示", "获取二维码失败！");
                            }
                        }
                        break;
                    }
                case "login":
                    {
                        QrLoginCallback?.Invoke(this, false);
                        if (!isQrLogin)
                        {
                            if (Account.IsNotEmpty() && Passwd.IsNotEmpty())
                            {
                                var callbackModel = await UserManager.LoginV3(Account, Passwd);
                                if (callbackModel.IsNotEmpty())
                                {
                                    switch (callbackModel.Status)
                                    {
                                        case LoginStatus.Success:
                                            {
                                                var result = await UserManager.GetUserInfo(SettingHelper.UserID);
                                                var userInfo = result.Data.Card;
                                                SettingHelper.UserInfo = userInfo;
                                                StaticValue.CurrentUserId = userInfo.Mid.ToInt64();
                                                StaticValue.CurrentUserName = userInfo.Name;
                                                var userHead = await Current.HttpClient.GetImageStream(userInfo.Face, userInfo.Mid);
                                                SettingHelper.UserHead = userHead;
                                                StaticValue.CurrentUserIcon = new Bitmap(userHead);
                                                if (StaticValue.LoginEvent != null)
                                                    StaticValue.LoginEvent.Execute();
                                                Message = "登录成功！";
                                                Callback?.Invoke(this);
                                                break;
                                            }
                                        case LoginStatus.Fail:
                                        case LoginStatus.Error:
                                            {
                                                Message = "登录失败，失败信息：" + callbackModel.Message;
                                                break;
                                            }
                                        case LoginStatus.NeedCaptcha:
                                            {
                                                Message = "登录需要验证码，请使用网页登录";
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    await MessageBox.Show(Window, "提示", "请求网络失败！！");
                                }
                            }
                            else
                            {
                                await MessageBox.Show(Window, "提示", "请输入账号密码！");
                            }
                        }
                        isQrLogin = false;
                        break;
                    }
            }
        }
        public void Dispose()
        {
            try
            {
                Timer.Stop();
                Timer.Dispose();
            }
            catch { }
        }
    }
}