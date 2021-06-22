using System.Collections.Generic;

namespace Bilibili.Manga.Model.Common
{
    public sealed class MangeItem
    {
        public int Id { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set => _title = value.Replace("<em class=\"keyword\">", "").Replace("</em>", "");
        }
        public string Org_Title { get; set; }
        public List<string> Alia_Title { get; set; }
        private string horizontalcover;
        public string Horizontal_Cover
        {
            get => horizontalcover;
            set => horizontalcover = value + "@600w.jpg";
        }
        private string squarecover;
        public string Square_Cover
        {
            get => squarecover;
            set => squarecover = value + "@600w.jpg";
        }
        private string verticalcover;
        public string Vertical_Cover
        {
            get => verticalcover;
            set => verticalcover = value + "@600w.jpg";
        }
        public List<string> Author_Name { get; set; }
        public List<string> Styles { get; set; }
        public decimal Is_Finish { get; set; }
        public string Allow_Wait_Free { get; set; }
        public decimal Discount_Type { get; set; }
        public decimal Type { get; set; }
        public Wiki Wiki { get; set; }
    }
    public sealed class Wiki
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Origin_Title { get; set; }
        public string Vertical_Cover { get; set; }
        public string Producer { get; set; }
        public List<string> Author_Name { get; set; }
        public string Publish_Time { get; set; }
        public string Frequency { get; set; }
    }
}