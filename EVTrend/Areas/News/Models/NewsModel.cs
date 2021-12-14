using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EVTrend.Areas.News.Models
{
    public class NewsModel
    {
        [Display(Name = "消息編號")]
        public int NewsNo { get; set; }

        [Display(Name = "消息類型")]
        public int NewsTypeNo { get; set; }

        [Display(Name = "標題")]
        public string NewsTitle { get; set; }

        [Display(Name = "內容")]
        public string NewsContent { get; set; }

        [Display(Name = "觸及次數")]
        public int NewsHits { get; set; }

        [Display(Name = "創立時間")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "修改時間")]
        public DateTime ModifyTime { get; set; }

        [Display(Name = "消息結束")]
        public DateTime NewsEnd { get; set; }

        [Display(Name = "消息創立者")]
        public string NewsCreateUser { get; set; }

        [Display(Name = "消息修改者")]
        public string NewsModifyUser { get; set; }
    }

    public class NewsPageModel
    {
        public List<NewsModel> News { get; } = new List<NewsModel>();
    }
}
