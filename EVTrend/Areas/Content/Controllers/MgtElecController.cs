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

    public class MgtElecController : _BaseController
    {
        /// <summary>
        /// 電動車數據管理View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["GetTotalElec"] = GetTotalElec();
            ViewData["GetYear"] = GetYear();
            ViewData["GetCountry"] = GetCountry();
            //ViewData["GetCountry"] = GetCountry();
            return View("MgtElec");
        }


        /// <summary>
        /// 取得年分數據
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TimeModel> GetYear()
        {
            
            var sqlStr = string.Format(
                "SELECT YearNo, YearName FROM evtrend.`years`");
            var data = _DB_GetData(sqlStr);
            List<TimeModel> list = new List<TimeModel>();
            foreach (DataRow row in data.Rows)
            {
                TimeModel model = new TimeModel();
                model.YearNo = (int)row.ItemArray.GetValue(0);
                model.YearName = row.ItemArray.GetValue(1).ToString();
                list.Add(model);
            }
            return list;

        }

        /// <summary>
        /// 取得國家數據
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CountryModel> GetCountry()
        {

            var sqlStr = string.Format(
                "SELECT CountryNo, CountryName FROM evtrend.`countries`");
            var data = _DB_GetData(sqlStr);
            List<CountryModel> list = new List<CountryModel>();
            foreach (DataRow row in data.Rows)
            {
                CountryModel model = new CountryModel();
                model.CountryNo = (int)row.ItemArray.GetValue(0);
                model.CountryName = row.ItemArray.GetValue(1).ToString();
                list.Add(model);
            }
            return list;

        }


        /// <summary>
        /// 取得總體電動車數據
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<MgtTotalElecModel> GetTotalElec()
        {

            var sqlStr = string.Format(
                "SELECT TotalRegisterNo, TotalRegisterYear, YearName, TotalRegisterCountryNo, countryName,T_RegisterNumber,TotalRegisterNumber,TotalRegisterCreateTime,TotalRegisterModifyTime FROM evtrend.`total_ registercar` as a " +
                "inner join evtrend.`countries` as b " +
                "on a.TotalRegisterCountryNo = b.CountryNo " +
                "inner join evtrend.`years` as c " +
                "on a.TotalRegisterYear = c.YearNo " +
                "ORDER BY CountryName,YearName ASC");
            var data = _DB_GetData(sqlStr);
            List<MgtTotalElecModel> list = new List<MgtTotalElecModel>();
            foreach (DataRow row in data.Rows)
            {
                MgtTotalElecModel model = new MgtTotalElecModel();
                model.TotalRegisterNo = (int)row.ItemArray.GetValue(0);
                model.YearNo = (int)row.ItemArray.GetValue(1);
                model.Year = row.ItemArray.GetValue(2).ToString();
                model.CountryNo = (int)row.ItemArray.GetValue(3);
                model.Country = row.ItemArray.GetValue(4).ToString();
                model.ElecRegisterNumber = (float)row.ItemArray.GetValue(5);
                model.TotalRegisterNumber = (float)row.ItemArray.GetValue(6);
                model.CreateTime = row.ItemArray.GetValue(7).ToString();
                if (row.ItemArray.GetValue(8).ToString() == "")
                {
                    model.ModifyTime = "NULL";
                }
                else
                {
                    model.ModifyTime = row.ItemArray.GetValue(8).ToString();
                }
                list.Add(model);
            }
            return list;

        }



        /// <summary>
        /// 更新總體電動車數據
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool InsertTotalElec(MgtTotalElecModel Model)
        {
            // 檢查是否重複新增
            var sqlSelect = string.Format("SELECT 1 from evtrend.`total_ registercar` " +
                "WHERE TotalRegisterCountryNo={0} and TotalRegisterYear={1}", SqlVal2(Model.YearNo), SqlVal2(Model.CountryNo));

            var dataSelect = _DB_GetData(sqlSelect);
            if (dataSelect.Rows.Count > 0)
            {
                return false;
            }
            
            //驗證是否輸入不為負的數字
            if(Model.ElecRegisterNumber<0 || Model.TotalRegisterNumber < 0)
            {
                return false;
            }
            
            //SQL Insert TotalElec
            var sqlStr = string.Format(
                @"INSERT INTO evtrend.`total_ registercar` (" +
                    "TotalRegisterCountryNo," +
                    "TotalRegisterYear," +
                    "T_RegisterNumber," +
                    "TotalRegisterNumber," +
                    "TotalRegisterCreateTime" +
                ")VALUES(" +
                    "{0}," +
                    "{1}," +
                    "{2}," +
                    "{3}," +
                    "{4}",
                    SqlVal2(Model.CountryNo),
                    SqlVal2(Model.YearNo),
                    SqlVal2(Model.ElecRegisterNumber),
                    SqlVal2(Model.TotalRegisterNumber),
                    DBC.ChangeTimeZone() + ")"
                );

            //SQL Check
            var check = _DB_Execute(sqlStr);

            //新增是否成功
            if (check == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}