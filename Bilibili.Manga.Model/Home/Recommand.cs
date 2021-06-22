using Bilibili.Manga.Model.Common;
using System.Collections.Generic;

namespace Bilibili.Manga.Model.Home
{
    public sealed class Recommand
    {
        public string Seed { get; set; }
        public List<Mange> List { get; set; }
    }
}