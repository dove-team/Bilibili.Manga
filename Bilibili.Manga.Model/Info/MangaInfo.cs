using Bilibili.Manga.Model.Common;
using System.Collections.Generic;
using System.Linq;

namespace Bilibili.Manga.Model.Info
{
    public sealed class MangaInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Comic_Type { get; set; }
        public decimal Page_Default { get; set; }
        public decimal Page_Allow { get; set; }
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
        public decimal Last_Ord { get; set; }
        public decimal Is_Finish { get; set; }
        public decimal Status { get; set; }
        public decimal Fav { get; set; }
        public decimal Read_Order { get; set; }
        public string Evaluate { get; set; }
        public decimal Total { get; set; }
        public List<EpListItem> Ep_List { get; set; }
        public string Release_Time { get; set; }
        public decimal Is_Limit { get; set; }
        public decimal Read_Epid { get; set; }
        public string Last_Read_Time { get; set; }
        public decimal Is_Download { get; set; }
        public string Read_Short_Title { get; set; }
        public List<TagItem> Styles2 { get; set; }
        public string Renewal_Time { get; set; }
        public string Last_Short_Title { get; set; }
        public decimal Discount_Type { get; set; }
        public decimal Discount { get; set; }
        public string Discount_End { get; set; }
        public string No_Reward { get; set; }
        public decimal Batch_Discount_Type { get; set; }
        public decimal Ep_Discount_Type { get; set; }
        public string Has_Fav_Activity { get; set; }
        public decimal Fav_Free_Amount { get; set; }
        public string Allow_Wait_Free { get; set; }
        public decimal Wait_Hour { get; set; }
        public string Wait_Free_At { get; set; }
        public decimal No_Danmaku { get; set; }
        public decimal Auto_Pay_Status { get; set; }
        public string No_Month_Ticket { get; set; }
        public string Immersive { get; set; }
        public string No_Discount { get; set; }
        public decimal Show_Type { get; set; }
        public decimal Pay_Mode { get; set; }
        public List<string> Chapters { get; set; }
        public string Classic_Lines { get; set; }
        public decimal Pay_For_New { get; set; }
        public FavComicInfo Fav_Comic_Info { get; set; }
        public decimal Serial_Status { get; set; }
        public SeriesInfo Series_Info { get; set; }
        public decimal Album_Count { get; set; }
        public decimal Wiki_Id { get; set; }
        public decimal Disable_Coupon_Amount { get; set; }
        public string Japan_Comic { get; set; }
        public string Decimaleract_Value { get; set; }
        public string Temporary_Finish_Time { get; set; }
        public string Video { get; set; }
        public string Decimalroduction { get; set; }
        public decimal Comment_Status { get; set; }
        public string No_Screenshot { get; set; }
        public decimal Type { get; set; }
        public List<string> Vomic_Cvs { get; set; }
        public string Tags
        {
            get => string.Join("，", Styles2.Select(x => x.Name));
        }
        public string Authors
        {
            get => string.Join("，", Author_Name);
        }
        public string Newest
        {
            get => string.Format("更新到{0}话", Last_Short_Title);
        }
    }
}