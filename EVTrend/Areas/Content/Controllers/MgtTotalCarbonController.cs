using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.Content.Controllers
{
    [Area(areaName: "Content")]

    public class MgtTotalCarbonController : _BaseController
    {
        /// <summary>
        /// 總體碳排量管理View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("MgtTotalCarbon");
        }
    }
}