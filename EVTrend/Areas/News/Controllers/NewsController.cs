using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.News.Controllers
{
    [Area(areaName: "News")]

    public class NewsController : Controller
    {
        /// <summary>
        /// 最新消息View
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            return View("ShowNews");
        }
    }
}