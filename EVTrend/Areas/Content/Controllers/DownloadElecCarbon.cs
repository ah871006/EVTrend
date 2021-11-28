﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.Content.Controllers
{
    [Area(areaName: "Content")]

    public class DownloadElecCarbon : Controller
    {
        /// <summary>
        /// 碳排量與電動車數據下載View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("DownloadElecCarbon");
        }
    }
}