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

    public class TotalElecCarbonController : _BaseController
    {


        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["TotalElec"] = GetTotalElecModel();
            ViewData["TotalCarbon"] = GetTotalCarbonModel();
            return View("TotalElecCarbon");

        }


        private DataTable GetTotalElec()
        {
            var sqlStr = string.Format(
                "SELECT YearName, countryName,T_RegisterNumber,TotalRegisterNumber FROM evtrend.`total_ registercar` as a " +
                "inner join evtrend.`countries` as b " +
                "on a.TotalRegisterCountryNo = b.CountryNo " +
                "inner join evtrend.`years` as c " +
                "on a.TotalRegisterYear = c.YearNo " +
                "ORDER BY countryName,YearName ASC");
            var data = _DB_GetData(sqlStr);
            return data;
        }

        private DataTable GetTotalCarbon()
        {
            var sqlStr = string.Format(
                "SELECT YearName, countryName,T_CarbonNumber,TotalCarbonNumber FROM evtrend.`total_carbon` as a " +
                "inner join evtrend.`countries` as b " +
                "on a.TotalCarbonCountryNo = b.CountryNo " +
                "inner join evtrend.`years` as c " +
                "on a.TotalCarbonYear = c.YearNo " +
                "ORDER BY countryName,YearName ASC");
            var data = _DB_GetData(sqlStr);
            return data;
        }


        private List<TotalElecModel> GetTotalElecModel()
        {
            var data = GetTotalElec();
            List<TotalElecModel> list = new List<TotalElecModel>();
            foreach (DataRow row in data.Rows)
            {
                TotalElecModel model = new TotalElecModel();

                model.Year = row.ItemArray.GetValue(0).ToString();
                model.Country = row.ItemArray.GetValue(1).ToString();
                model.ElecRegisterNumber = (float)row.ItemArray.GetValue(2);
                model.TotalRegisterNumber = (float)row.ItemArray.GetValue(3);

                //計算電動車數量/總車種數量 => 得到百分比
                var CarPercentage = (float)row.ItemArray.GetValue(2) / (float)row.ItemArray.GetValue(3);
                model.Percentage = (float)Math.Round((Decimal)CarPercentage, 7);

                list.Add(model);
            }
            return list;
        }

        private List<TotalCarbonModel> GetTotalCarbonModel()
        {
            var data = GetTotalCarbon();
            List<TotalCarbonModel> list = new List<TotalCarbonModel>();
            foreach (DataRow row in data.Rows)
            {
                TotalCarbonModel model = new TotalCarbonModel();
                model.Year = row.ItemArray.GetValue(0).ToString();
                model.Country = row.ItemArray.GetValue(1).ToString();
                model.T_CarbonNumber = (float)row.ItemArray.GetValue(2);
                model.TotalCarbonNumber = (float)row.ItemArray.GetValue(3);

                //計算運輸業碳排數量/總碳排數量 => 得到百分比
                var CarbonPercentage = (float)row.ItemArray.GetValue(2) / (float)row.ItemArray.GetValue(3);
                model.Percentage = (float)Math.Round((Decimal)CarbonPercentage, 7);

                list.Add(model);
            }
            return list;
        }



    }
}