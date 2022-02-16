namespace Bilibili.Manga.Model.Common
{
    public sealed class TagItem
    {
        public TagItem() { }
        public TagItem(string tag)
        {
            Id = "-1";
            Tag = tag;
            Name = "全部";
            Checked = true;
        }
        public string Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}