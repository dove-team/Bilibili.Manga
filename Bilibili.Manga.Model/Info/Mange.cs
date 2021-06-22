using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Bilibili.Manga.Model.Common
{
    public sealed class Mange
    {
        public int Id { get; set; }
        public int Comic_Id { get; set; }
        public string Title { get; set; }
        private string subTitle;
        public string Sub_Title
        {
            get => subTitle;
            set => subTitle = "第" + value + "话";
        }
        private string _img;
        public string Img
        {
            get => _img;
            set => _img = value + "@600w.jpg";
        }
        public string Jump_Value { get; set; }
        public List<TagItem> Styles { get; set; }
        public string StylesString
        {
            get
            {
                if (Styles != null && Styles.Count > 0)
                    return string.Join(",", Styles.Select(x => x.Name));
                else
                    return string.Empty;
            }
        }
        public decimal Comment_Total { get; set; }
        private string verticalcover;
        public string Vertical_Cover
        {
            get => verticalcover;
            set => verticalcover = value + "@600w.jpg";
        }
        public string Ext { get; set; }
        public decimal Recommend_Type { get; set; }
        public decimal Type { get; set; }
        public decimal Jump_Type { get; set; }
        public decimal Icon { get; set; }
        public string Introduction { get; set; }
        public List<TagItem> Labels { get; set; }
        public string Description { get; set; }
        public string Allow_Wait_Free { get; set; }
        public string Column { get; set; }
        public object Combine { get; set; }
        public JObject Anime { get; set; }
        public List<string> Comments { get; set; }
        public List<string> Columns { get; set; }
        public decimal Discount_Type { get; set; }
        public string Last_Ep_Title { get; set; }
        public string Last_Short_Title { get; set; }
        public decimal Page_Num { get; set; }
        public double Review_Score { get; set; }
        public PvInfo Pv_Info { get; set; }
        public TagItem Rank { get; set; }
        public decimal Discount { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Author { get; set; }
        public List<string> Authors { get; set; }
        public string AuthorString
        {
            get => string.Join("，", Author);
        }
        public decimal Favorite_State { get; set; }
        public string Newest
        {
            get => string.Format("更新到 {0} 话", Last_Short_Title);
        }
    }
}