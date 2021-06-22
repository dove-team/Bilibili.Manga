namespace Bilibili.Manga.Model.User
{
    public sealed class AccountLogin
    {
        public int Ts { get; set; }
        public int Code { get; set; }
        public LoginData Data { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
    }
}