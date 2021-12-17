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
                "SELECT YearName, countryName,T_RegisterNumber,TotalRegisterNumber FROM evtrend.`total_registercar` as a " +
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
        

        [HttpGet]
        public List<TotalElecModel> GetTotalElecModel()
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
                model.Percentage = (float)Math.Round((Decimal)CarPercentage, 5) * 1000;

                list.Add(model);
            }
            return list;
        }

        [HttpGet]
        public List<TotalCarbonModel> GetTotalCarbonModel()
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
                model.Percentage = (float)Math.Round((Decimal)CarbonPercentage, 5) * 100;

                list.Add(model);
            }
            return list;
        }


        /// <summary>
        /// 取得電動車數據用來畫圖
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DrawDataModel GetElecDraw()
        {
            //初始化要回傳到view的model
            DrawDataModel obj = new DrawDataModel();
            obj.List_A = new List<TotalDrawModel>();
            obj.List_T = new List<TotalDrawModel>();

            var sqlStr = string.Format(
                "SELECT YearName, T_RegisterNumber,TotalRegisterNumber,CountryNo FROM evtrend.`total_registercar` as a " +
                "inner join evtrend.`countries` as b " +
                "on a.TotalRegisterCountryNo = b.CountryNo " +
                "inner join evtrend.`years` as c " +
                "on a.TotalRegisterYear = c.YearNo " +
                "ORDER BY countryName,YearName ASC");
            var data = _DB_GetData(sqlStr);

            foreach (DataRow row in data.Rows)
            {
                
                var CounteryNo = (int)row.ItemArray.GetValue(3);
                var YearName="";
                var CarPercentage=0.0;
                

                //美國
                if (CounteryNo == 1)
                {
                    TotalDrawModel model = new TotalDrawModel();
                    YearName = row.ItemArray.GetValue(0).ToString();
                    //model.x = DateTime.ParseExact(YearName, "yyyy", null);
                    model.x = Int32.Parse(YearName);
                    //model.x = YearName;
                    //計算電動車數量/總車種數量 => 得到百分比
                    CarPercentage = (float)row.ItemArray.GetValue(1) / (float)row.ItemArray.GetValue(2);
                    model.y = (float)Math.Round((Decimal)CarPercentage, 5) * 1000;
                    obj.List_A.Add(model);

                }
                //台灣
                else if (CounteryNo == 2)
                {
                    TotalDrawModel model = new TotalDrawModel();
                    YearName = row.ItemArray.GetValue(0).ToString();
                    //model.x = DateTime.ParseExact(row.ItemArray.GetValue(0).ToString(), "yyyy", null);
                    model.x = Int32.Parse(YearName);
                    //model.x = YearName;
                    //計算電動車數量/總車種數量 => 得到百分比
                    CarPercentage = (float)row.ItemArray.GetValue(1) / (float)row.ItemArray.GetValue(2);
                    model.y = (float)Math.Round((Decimal)CarPercentage, 5) * 1000;
                    obj.List_T.Add(model);

                }

                
            }
            return obj;

        }


        /// <summary>
        /// 取得碳排量數據用來畫圖
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DrawDataModel GetCarbonDraw()
        {
            //初始化要回傳到view的model
            DrawDataModel obj = new DrawDataModel();
            obj.List_A = new List<TotalDrawModel>();
            obj.List_T = new List<TotalDrawModel>();

            var sqlStr = string.Format(
                "SELECT YearName, T_CarbonNumber,TotalCarbonNumber, countryNo FROM evtrend.`total_carbon` as a " +
                "inner join evtrend.`countries` as b " +
                "on a.TotalCarbonCountryNo = b.CountryNo " +
                "inner join evtrend.`years` as c " +
                "on a.TotalCarbonYear = c.YearNo " +
                "ORDER BY countryName,YearName ASC");
            var data = _DB_GetData(sqlStr);

            foreach (DataRow row in data.Rows)
            {

                var CounteryNo = (int)row.ItemArray.GetValue(3); //取得國家邊號
                var YearName = "";
                var CarbonPercentage = 0.0;

                //判斷是美國還是台灣
                if (CounteryNo == 1)//美國
                {
                    TotalDrawModel model = new TotalDrawModel();
                    YearName = row.ItemArray.GetValue(0).ToString();
                    //model.x = DateTime.ParseExact(YearName, "yyyy", null);
                    model.x = Int32.Parse(YearName);
                    //model.x = YearName;
                    //計算電動車數量/總車種數量 => 得到百分比
                    CarbonPercentage = (float)row.ItemArray.GetValue(1) / (float)row.ItemArray.GetValue(2);
                    model.y = (float)Math.Round((Decimal)CarbonPercentage, 5) * 100;
                    obj.List_A.Add(model);

                }
                else if (CounteryNo == 2)//台灣
                {
                    TotalDrawModel model = new TotalDrawModel();
                    YearName = row.ItemArray.GetValue(0).ToString();
                    //model.x = DateTime.ParseExact(row.ItemArray.GetValue(0).ToString(), "yyyy", null);
                    model.x = Int32.Parse(YearName);
                    //model.x = YearName;
                    //計算電動車數量/總車種數量 => 得到百分比
                    CarbonPercentage = (float)row.ItemArray.GetValue(1) / (float)row.ItemArray.GetValue(2);
                    model.y = (float)Math.Round((Decimal)CarbonPercentage, 5) * 100;
                    obj.List_T.Add(model);

                }

            }
            return obj;

        }

    }
}