using EVTrend.Areas.News.Models;
using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.News.Controllers
{
    [Area(areaName: "News")]

    public class MgtNewsController : _BaseController
    {
        /// <summary>
        /// 最新消息管理View
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            return View("MgtNews");
        }
    }
}