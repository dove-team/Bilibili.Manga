namespace Bilibili.Manga.Model.User
{
    public sealed class LoginCallback
    {
        public LoginStatus Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public byte[] Captcha { get; internal set; }
    }
}