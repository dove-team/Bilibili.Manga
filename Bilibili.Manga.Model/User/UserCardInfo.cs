using System;
using System.Collections.Generic;

namespace Bilibili.Manga.Model.User
{
    public sealed class User
    {
        public UserCardInfo Card { get; set; }
    }
    public sealed class UserCardInfo
    {
        public List<string> Attentions { get; set; }
        public string Mid { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Face { get; set; }
        public bool Approve { get; set; }
        public int? Rank { get; set; }
        public string RankStr
        {
            get
            {
                switch (Rank)
                {
                    case 0:
                        return "普通用户";
                    case 5000:
                        return "注册会员";
                    case 10000:
                        return "正式会员";
                    case 20000:
                        return "字幕君";
                    case 25000:
                        return "VIP用户";
                    case 30000:
                        return "职人";
                    case 32000:
                        return "站长大人";
                    default:
                        return "蜜汁等级";
                }
            }
        }
        public string Birthday { get; set; }
        public long Regtime { get; set; }
        public string Attention { get; set; }
        public string RegTime
        {
            get
            {
                DateTime dtStart = new DateTime(1970, 1, 1);
                long lTime = long.Parse(Regtime + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow).ToString("d");
            }
        }
        public int Fans { get; set; }
        public UserInfo Level_Info { get; set; }
        public string Sign { get; set; }
        public Vip Vip { get; set; }
        public Official Official_verify { get; set; }
        public RelationList Relation { get; set; }
    }
    public sealed class Vip
    {
        public int VipType { get; set; }
        public int VipStatus { get; set; }
        public long VipDueDate { get; set; }
    }
    public sealed class Official
    {
        public int Type { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }
    public sealed class RelationList
    {
        public int Status { get; set; }
    }
}