using EVTrend.Areas.News.Models;
using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Globalization;

namespace EVTrend.Areas.News.Controllers
{
    [Area(areaName: "News")]

    public class NewsController : _BaseController
    {
        public IActionResult Index()
        {
            return View("ShowNews", GetNewsPageModel());
        }

        private DataTable GetAllNews()
        {
            var sqlStr = string.Format("SELECT NewsNo, NewsTypeNo, NewsTitle, NewsContent, NewsHits, CreateTime, ModifyTime, NewsEnd, NewsCreateUser, NewsModifyUser from evtrend.`news`");
            var data = _DB_GetData(sqlStr);
            return data;
        }

        private NewsModel ConvertRowToNewsModel(DataRow row)
        {
            var news = new NewsModel();

            news.NewsNo = (int)row.ItemArray.GetValue(0);
            news.NewsTypeNo = (int)row.ItemArray.GetValue(1);
            news.NewsTitle = row.ItemArray.GetValue(2).ToString();
            news.NewsContent = row.ItemArray.GetValue(3).ToString();
            news.NewsHits = (int)row.ItemArray.GetValue(4);
            news.CreateTime = DateTime.Parse(row.ItemArray.GetValue(5).ToString());
            if (row.ItemArray.GetValue(6).ToString() != "")
            {
                news.ModifyTime = DateTime.Parse(row.ItemArray.GetValue(6).ToString());
            }
            // news.NewsEnd = DateTime.Parse(row.ItemArray.GetValue(7).ToString());
            news.NewsCreateUser = row.ItemArray.GetValue(8).ToString();
            // news.NewsModifyUser = row.ItemArray.GetValue(9).ToString();
            //login-forget-password <span class="offset-1" id="forget-password"><a href="/Account/ForgetPassword">忘記密碼?</a></span>
            return news;
        }

        private NewsPageModel GetNewsPageModel()
        {
            var newsPageModel = new NewsPageModel();

            var news = GetAllNews();
            foreach (DataRow row in news.Rows)
            {
                newsPageModel.News.Add(ConvertRowToNewsModel(row));
            }

            return newsPageModel;
        }
    }
}