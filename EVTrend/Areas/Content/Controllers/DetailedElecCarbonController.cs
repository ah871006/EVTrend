using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVTrend.Areas.Content.Models;
using System.Data;

namespace EVTrend.Areas.Content.Controllers
{
    [Area(areaName: "Content")]

    public class DetailedElecCarbonController : _BaseController
    {
        /// <summary>
        /// 細部碳排量與電動車數據View
        /// </summary>
        /// <returns></returns>
        
        //public IActionResult Index()

        
        public IActionResult Index()
        {
            ViewData["DetailedCarbon"] = GetDetailCarbonModel();
            ViewData["DetailedElec"] = GetDetailElecModel();
            return View("DetailedElecCarbon");
        }
        private DataTable GetDetailedCarbon()
        {
            var sqlStr = string.Format(
                "SELECT YearName, CarsTypeName,CarbonNumber FROM evtrend.`carbon` as a " +
                "inner join evtrend.`years` as b " +
                "on a.CarbonYear = b.YearNo " +
                "inner join evtrend.`country_carstype` as c " +
                "on a.CarbonCountryCarsTypeNo = c.CountryCarsTypeNo " +
                "inner join evtrend.`cars_type` as d " +
                "on c.CarsTypeNo = d.CarsTypeNo " +
                "ORDER BY YearName,CarsTypeName ASC");
            var data = _DB_GetData(sqlStr);
            return data;
        }
        private DataTable GetDetailedElec()
        {
            var sqlStr = string.Format(
                "SELECT YearName, CarsTypeName,ElecNumber,TotalNumber FROM evtrend.`register_car` as a " +
                "inner join evtrend.`years` as b " +
                "on a.RegisterCarYear = b.YearNo " +
                "inner join evtrend.`country_carstype` as c " +
                "on a.RegisterCarCountryCarsTypeNo = c.CountryCarsTypeNo " +
                "inner join evtrend.`countries` as d " +
                "on c.CountryNo = d.CountryNo " +
                "inner join evtrend.`cars_type` as e " +
                "on c.CarsTypeNo = e.CarsTypeNo " +
                "ORDER BY YearName,CarsTypeName ASC");
            var data = _DB_GetData(sqlStr);
            return data;
        }
        private List<DetailedCarbonModel> GetDetailCarbonModel()
        {
            var data = GetDetailedCarbon();
            List<DetailedCarbonModel> list = new List<DetailedCarbonModel>();
            foreach (DataRow row in data.Rows)
            {
                DetailedCarbonModel model = new DetailedCarbonModel();

                model.YearName = row.ItemArray.GetValue(0).ToString();
                model.CarsTypeName = row.ItemArray.GetValue(1).ToString();
                model.CarbonNumber = (float)row.ItemArray.GetValue(2);

                list.Add(model);
            }
            return list;
        }
        private List<DetailedElecModel> GetDetailElecModel()
        {
            var data = GetDetailedElec();
            List<DetailedElecModel> list = new List<DetailedElecModel>();
            foreach (DataRow row in data.Rows)
            {
                DetailedElecModel model = new DetailedElecModel();

                model.YearName = row.ItemArray.GetValue(0).ToString();
                model.CarsTypeName = row.ItemArray.GetValue(1).ToString();
                model.ElecNumber = (float)row.ItemArray.GetValue(2);
                model.TotalNumber = (float)row.ItemArray.GetValue(3);

                list.Add(model);
            }
            return list;
        }
    }
}