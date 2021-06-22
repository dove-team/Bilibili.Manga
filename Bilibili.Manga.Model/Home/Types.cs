using Bilibili.Manga.Model.Common;
using System.Collections.Generic;

namespace Bilibili.Manga.Model.Home
{
    public sealed class Types
    {
        private List<TagItem> styles;
        public List<TagItem> Styles
        {
            get => styles;
            set
            {
                if (value != null && value.Count > 0)
                    value.ForEach(i => i.Tag = "styles");
                styles = value;
            }
        }
        private List<TagItem> areas;
        public List<TagItem> Areas
        {
            get => areas;
            set
            {
                if (value != null && value.Count > 0)
                    value.ForEach(i => i.Tag = "areas");
                areas = value;
            }
        }
        private List<TagItem> status;
        public List<TagItem> Status
        {
            get => status;
            set
            {
                if (value != null && value.Count > 0)
                    value.ForEach(i => i.Tag = "status");
                status = value;
            }
        }
        private List<TagItem> orders;
        public List<TagItem> Orders
        {
            get => orders;
            set
            {
                if (value != null && value.Count > 0)
                    value.ForEach(i => i.Tag = "orders");
                orders = value;
            }
        }
        private List<TagItem> prices;
        public List<TagItem> Prices
        {
            get => prices;
            set
            {
                if (value != null && value.Count > 0)
                    value.ForEach(i => i.Tag = "prices");
                prices = value;
            }
        }
    }
}