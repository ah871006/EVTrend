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

    public class MgtDetailedCarbonController : _BaseController
    {
        /// <summary>
        /// 細部碳排量管理View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            if (getUserStatusNo() != "0")
            {
                return Redirect("~/Home/Error");
            }
            ViewData["GetMgtDetailedCarbon"] = GetMgtDetailedCarbon();

            return View("MgtDetailedCarbon");
        }
        private List<MgtDetailedCarbonModel> GetMgtDetailedCarbon()
        {
            var sqlStr = string.Format(
                "SELECT CarbonNo, CarbonCountryCarsTypeNo,CarbonYear,YearName,CarbonNumber,CarbonCreateUser,CarbonCreateTime,CarbonModifyTime FROM evtrend.`carbon` as a " +
                "inner join evtrend.`years` as b " +
                "on a.CarbonYear = b.YearNo " +
                "inner join evtrend.`country_carstype` as c " +
                "on a.CarbonCountryCarsTypeNo = c.CountryCarsTypeNo " +
                "ORDER BY CarbonNo,CarbonYear,CarbonCountryCarsTypeNo ASC");
            var data = _DB_GetData(sqlStr);
            List<MgtDetailedCarbonModel> list = new List<MgtDetailedCarbonModel>();
            foreach (DataRow row in data.Rows)
            {
                MgtDetailedCarbonModel model = new MgtDetailedCarbonModel();

                model.CarbonNo = (int)row.ItemArray.GetValue(0);
                model.CarbonCountryCarsTypeNo = (int)row.ItemArray.GetValue(1);
                model.CarbonYear = (int)row.ItemArray.GetValue(2);
                model.YearName = row.ItemArray.GetValue(3).ToString();
                model.CarbonNumber = (float)row.ItemArray.GetValue(4);
                model.CarbonCreateUser = (int)row.ItemArray.GetValue(5);
                model.CarbonCreateTime = row.ItemArray.GetValue(6).ToString();
                if (row.ItemArray.GetValue(7).ToString() == "")
                {
                    model.CarbonModifyTime = "NULL";
                }
                else
                {
                    model.CarbonModifyTime = row.ItemArray.GetValue(7).ToString();
                }
                list.Add(model);
            }
            return list;
        }
        [HttpPost]
        public bool InsertDetailedCarbon(MgtDetailedCarbonModel Model)
        {
            //admin check
            if (getUserStatusNo() != "0")
            {
                return false;
            }

            // 檢查是否重複新增
            var sqlSelect = string.Format("SELECT 1 from evtrend.`carbon` " +
                "WHERE CarbonCountryCarsTypeNo={0} and CarbonYear={1}", SqlVal2(Model.CarbonCountryCarsTypeNo), SqlVal2(Model.CarbonYear));

            var dataSelect = _DB_GetData(sqlSelect);
            if (dataSelect.Rows.Count > 0)
            {
                return false;
            }


            //SQL Insert TotalElec
            var sqlStr = string.Format(
                @"INSERT INTO evtrend.`carbon` (" +
                    "CarbonCountryCarsTypeNo," +
                    "CarbonYear," +
                    "CarbonNumber," +
                    "CarbonCreateUser," +
                ")VALUES(" +
                    "{0}," +
                    "{1}," +
                    "{2}," +
                    "{3}," +
                    SqlVal2(Model.CarbonCountryCarsTypeNo),
                    SqlVal2(Model.CarbonYear),
                    SqlVal2(Model.CarbonNumber),
                    SqlVal2(Model.CarbonCreateUser),
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
        [HttpPost]
        public bool UpdateDetailedCarbon(MgtDetailedCarbonModel Model)
        {
            // admin check
            if (getUserStatusNo() != "0")
            {
                return false;
            }
            var sqlStr = string.Format("UPDATE evtrend.`carbon` " +
                "SET CarbonNumber = {0} " +
                ",CarbonModifyTime  = {1} " +
                "WHERE " +
                "CarbonNo={2}",
                SqlVal2(Model.CarbonNumber),
                DBC.ChangeTimeZone(),
                SqlVal2(Model.CarbonNo));

            var check = _DB_Execute(sqlStr);

            //是否成功
            if (check == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public bool DeleteTotalCarbon(MgtDetailedCarbonModel Model)
        {

            // admin check
            if (getUserStatusNo() != "0")
            {
                return false;
            }

            var sqlStr = string.Format("DELETE FROM evtrend.`carbon`" +
                "WHERE CarbonNo={0} ",
                SqlVal2(Model.CarbonNo));

            var check = _DB_Execute(sqlStr);

            //是否成功
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