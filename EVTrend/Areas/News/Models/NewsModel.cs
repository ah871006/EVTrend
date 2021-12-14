using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EVTrend.Areas.News.Models
{
    public class NewsModel
    {
        [Display(Name = "�����s��")]
        public int NewsNo { get; set; }

        [Display(Name = "��������")]
        public int NewsTypeNo { get; set; }

        [Display(Name = "���D")]
        public string NewsTitle { get; set; }

        [Display(Name = "���e")]
        public string NewsContent { get; set; }

        [Display(Name = "Ĳ�Φ���")]
        public int NewsHits { get; set; }

        [Display(Name = "�Х߮ɶ�")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "�ק�ɶ�")]
        public DateTime ModifyTime { get; set; }

        [Display(Name = "��������")]
        public DateTime NewsEnd { get; set; }

        [Display(Name = "�����Хߪ�")]
        public string NewsCreateUser { get; set; }

        [Display(Name = "�����ק��")]
        public string NewsModifyUser { get; set; }
    }

    public class NewsPageModel
    {
        public List<NewsModel> News { get; } = new List<NewsModel>();
    }
}
