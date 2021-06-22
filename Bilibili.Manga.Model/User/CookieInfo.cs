using System.Collections.Generic;

namespace Bilibili.Manga.Model.User
{
    public sealed class CookieInfo
    {
        public List<Cookies> Cookies { get; set; }
        public List<string> Domains { get; set; }
    }
    public sealed class Cookies
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Http_only { get; set; }
        public int Expires { get; set; }
    }
}