using System;

namespace Bilibili.Manga.Model.User
{
    public sealed class TokenInfo
    {
        public long Mid { get; set; }
        public int Expires_in { get; set; }
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public DateTime Expires_Datetime { get; set; }
    }
}