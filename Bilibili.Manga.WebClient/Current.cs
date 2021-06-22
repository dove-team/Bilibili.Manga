using System;

namespace Bilibili.Manga.WebClient
{
    public sealed class Current
    {
        private Current() { }
        public static string LocalID { get; private set; }
        private static object HasInit { get; set; } = false;
        public static Random Radom { get; private set; }
        public static HttpClientEx HttpClient { get; private set; }
        public static void Init()
        {
            if (HasInit is bool value && value == false)
            {
                HasInit = true;
                Radom = new Random();
                HttpClient = new HttpClientEx();
                LocalID = Guid.NewGuid().ToString();
            }
        }
    }
}