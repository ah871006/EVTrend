using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.Content.Controllers
{
    [Area(areaName: "Content")]

    public class MgtElec : Controller
    {
        /// <summary>
        /// 電動車數據管理View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("MgtElec");
        }
    }
}