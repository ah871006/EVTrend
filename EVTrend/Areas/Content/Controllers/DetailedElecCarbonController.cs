using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.Content.Controllers
{
    [Area(areaName: "Content")]

    public class DetailedElecCarbonController : _BaseController
    {
        /// <summary>
        /// 細部碳排量與電動車數據View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("DetailedElecCarbon");
        }
        [HttpGet]
        public JsonResult test(int car_type_id)
        {
            List<double> data1 = new List<double>();
            List<double> data2 = new List<double>();
            List<double> data3 = new List<double>();
            if (car_type_id == 1)
            {
                data1 = new List<double> { 1.1, 2.2, 3.3, 4.4};
                data2 = new List<double> { 2.2, 3.3, 4.4, 1.1 };
                data3 = new List<double> { 2.2, 3.3, 4.4, 1.1 };
            } 
            else if (car_type_id == 2) 
            {
                data1 = new List<double> { 1.1, 2.2, 3.3, 4.4 };
                data2 = new List<double> { 2.2, 3.3, 4.4, 2.2 };
                data3 = new List<double> { 2.2, 3.3, 4.4, 2.2 };
            }
            else if (car_type_id == 3)
            {
                data1 = new List<double> { 1.1, 2.2, 3.3, 4.4 };
                data2 = new List<double> { 2.2, 3.3, 4.4, 3.3 };
                data3 = new List<double> { 2.2, 3.3, 4.4, 3.3 };
            }
            else if (car_type_id == 4)
            {
                data1 = new List<double> { 1.1, 2.2, 3.3, 4.4 };
                data2 = new List<double> { 2.2, 3.3, 4.4, 4.4 };
                data3 = new List<double> { 2.2, 3.3, 4.4, 4.4 };
            }
            else if (car_type_id == 5)
            {
                data1 = new List<double> { 1.1, 2.2, 3.3, 4.4 };
                data2 = new List<double> { 2.2, 3.3, 4.4, 5.5 };
                data3 = new List<double> { 2.2, 3.3, 4.4, 5.5 };
            }
            return Json(new {total_car=data1, elec_car = data2, carbon = data3});;
        }
    }
}