using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.Member.Controllers
{
    [Area(areaName: "Member")]

    public class MemberController : _BaseController
    {
        /// <summary>
        /// 會員中心View
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            return View("ShowMember");
        }
    }
}