﻿namespace Bilibili.Manga.Model.Home
{
    public sealed class PartManga
    {
        public decimal Season_Id { get; set; }
        public string Title { get; set; }
        public string Sub_Title
        {
            get
            {
                if (Is_Finish == 1)
                    return "[已完结]共 " + Last_Ord + " 话";
                else
                    return "更新到 " + Last_Short_Title + " 话";
            }
        }
        private string HorizontalCover;
        public string Horizontal_Cover
        {
            get => HorizontalCover;
            set => HorizontalCover = value + "@600w.jpg";
        }
        private string SquareCover;
        public string Square_Cover
        {
            get => SquareCover;
            set => SquareCover = value + "@600w.jpg";
        }
        private string VerticalCover;
        public string Vertical_Cover
        {
            get => VerticalCover;
            set => VerticalCover = value + "@600w.jpg";
        }
        public int Is_Finish { get; set; }
        public decimal Status { get; set; }
        public double Last_Ord { get; set; }
        public decimal Total { get; set; }
        public string Release_Time { get; set; }
        public string Last_Short_Title { get; set; }
        public decimal Discount_Type { get; set; }
        public string Allow_Wait_Free { get; set; }
        public decimal Type { get; set; }
    }
}