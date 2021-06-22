using System.Collections.Generic;

namespace Bilibili.Manga.Model.User
{
    public sealed class LoginData
    {
        public int Status { get; set; }
        public TokenInfo Token_info { get; set; }
        public CookieInfo Cookie_info { get; set; }
        public List<string> Sso { get; set; }
        public string Url { get; set; }
        public long Mid { get; set; }
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public int Expires_in { get; set; }
    }
}