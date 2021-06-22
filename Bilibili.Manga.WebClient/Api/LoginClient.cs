using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using Bilibili.Manga.Model.User;
using PCLCrypto;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bilibili.Manga.WebClient.Api
{
    public sealed class LoginClient
    {
        private HttpClientEx HttpClient => Current.HttpClient;
        public async Task<LoginCallback> LoginV3(string username, string password)
        {
            try
            {
                string url = "https://passport.bilibili.com/api/v3/oauth2/login";
                var pwd = await EncryptedPassword(password);
                string data = $"username={Uri.EscapeDataString(username)}&password={Uri.EscapeDataString(pwd)}&gee_type=10";
                var results = await HttpClient.PostResults(url, data, ApiProvider.AndroidTVKey);
                var loginModel = results.ToObject<AccountLogin>();
                if (loginModel.IsNotEmpty())
                {
                    if (loginModel.Code == 0)
                    {
                        if (loginModel.Data.Status == 0)
                        {
                            SettingHelper.AccessKey = loginModel.Data.Token_info.Access_token;
                            SettingHelper.RefreshToken = loginModel.Data.Token_info.Refresh_token;
                            SettingHelper.LoginExpires = DateTime.Now.AddSeconds(loginModel.Data.Token_info.Expires_in);
                            SettingHelper.UserID = loginModel.Data.Token_info.Mid;
                            return new LoginCallback
                            {
                                Status = LoginStatus.Success,
                                Message = "登录成功"
                            };
                        }
                        else if (loginModel.Data.Status == 1)
                        {
                            return new LoginCallback
                            {
                                Status = LoginStatus.NeedValidate,
                                Message = "本次登录需要安全验证",
                                Url = loginModel.Data.Url
                            };
                        }
                        else
                        {
                            return new LoginCallback
                            {
                                Status = LoginStatus.Fail,
                                Message = loginModel.Message
                            };
                        }
                    }
                    else if (loginModel.Code == -105)
                    {
                        return new LoginCallback
                        {
                            Status = LoginStatus.NeedCaptcha,
                            Message = "登录需要验证码"
                        };
                    }
                    else
                    {
                        return new LoginCallback
                        {
                            Status = LoginStatus.Fail,
                            Message = loginModel.Message
                        };
                    }
                }
                else
                {
                    return new LoginCallback { Status = LoginStatus.Fail };
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("LoginV3", ex);
                return new LoginCallback
                {
                    Status = LoginStatus.Error,
                    Message = "登录出现小问题,请重试"
                };
            }
        }
        public async Task<Respone<TokenInfo>> PollQRAuthInfo(string authCode)
        {
            try
            {
                string url = "https://passport.bilibili.com/x/passport-tv-login/qrcode/poll";
                string data = $"auth_code={authCode}&guid={Guid.NewGuid()}&local_id={Current.LocalID}";
                string result = await HttpClient.PostResults(url, data, ApiProvider.AndroidTVKey);
                var m = result.ToObject<Respone<TokenInfo>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<TokenInfo>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<QRAuthInfo>> GetQRAuthInfo()
        {
            try
            {
                string url = "https://passport.bilibili.com/x/passport-tv-login/qrcode/auth_code";
                string data = $"local_id={Current.LocalID}";
                string result = await HttpClient.PostResults(url, data, ApiProvider.AndroidTVKey);
                var m = result.ToObject<Respone<QRAuthInfo>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<QRAuthInfo>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<User>> GetUserInfo(long userID)
        {
            try
            {
                string url = string.Format("https://app.bilibili.com/x/v2/space?ps=10&vmid={0}", userID);
                string results = await HttpClient.GetResults(url);
                var m = results.ToObject<Respone<User>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetUserInfo", ex);
                return new Respone<User>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        private async Task<string> EncryptedPassword(string passWord)
        {
            string base64String;
            try
            {
                string url = "https://passport.bilibili.com/api/oauth2/getKey";
                string stringAsync = await HttpClient.PostResults(url, string.Empty);
                var jObjects = stringAsync.ToJObject();
                string hash = jObjects["data"]["hash"].ToString();
                string key = jObjects["data"]["key"].ToString();
                string hashPass = string.Concat(hash, passWord);
                var collection = Regex.Match(key, "BEGIN PUBLIC KEY-----(?<key>[\\s\\S]+)-----END PUBLIC KEY");
                string publicKey = collection.Groups["key"].Value.Trim();
                byte[] numArray = Convert.FromBase64String(publicKey);
                var asymmetricKeyAlgorithmProvider = WinRTCrypto.AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithm.RsaPkcs1);
                var cryptographicKey = asymmetricKeyAlgorithmProvider.ImportPublicKey(numArray, 0);
                var buffer = WinRTCrypto.CryptographicEngine.Encrypt(cryptographicKey, Encoding.UTF8.GetBytes(hashPass), null);
                base64String = Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("EncryptedPassword", ex);
                base64String = passWord;
            }
            return base64String;
        }
    }
}