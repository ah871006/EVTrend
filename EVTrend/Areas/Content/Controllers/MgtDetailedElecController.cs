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

    public class MgtDetailedElecController : _BaseController
    {
        /// <summary>
        /// 細部電動車數據管理View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("MgtDetailedElec");
        }
        private List<MgtDetailedElecModel> GetMgtDetailedElec()
        {
            var sqlStr = string.Format(
                "SELECT RegisterCarNo, RegisterCarCountryCarsTypeNo,RegisterCarYear,ElecNumber,TotalNumber,RegisterCarCreateUser,RegisterCarCreateTime,RegisterCarModifyTime FROM evtrend.`register_car` as a " +
                "inner join evtrend.`years` as b " +
                "on a.RegisterCarYear = b.YearNo " +
                "inner join evtrend.`country_carstype` as c " +
                "on a.RegisterCarCountryCarsTypeNo = c.CountryCarsTypeNo " +
                "ORDER BY RegisterCarNo,RegisterCarYear,RegisterCarCountryCarsTypeNo ASC");
            var data = _DB_GetData(sqlStr);
            List<MgtDetailedElecModel> list = new List<MgtDetailedElecModel>();
            foreach (DataRow row in data.Rows)
            {
                MgtDetailedElecModel model = new MgtDetailedElecModel();

                model.RegisterCarNo = (int)row.ItemArray.GetValue(0);
                model.RegisterCarCountryCarsTypeNo = (int)row.ItemArray.GetValue(1);
                model.RegisterCarYear = (int)row.ItemArray.GetValue(2);
                model.ElecNumber = (float)row.ItemArray.GetValue(3);
                model.TotalNumber = (float)row.ItemArray.GetValue(4);
                model.RegisterCarCreateUser = (int)row.ItemArray.GetValue(5);
                model.RegisterCarCreateTime = row.ItemArray.GetValue(6).ToString();
                if (row.ItemArray.GetValue(7).ToString() == "")
                {
                    model.RegisterCarModifyTime = "NULL";
                }
                else
                {
                    model.RegisterCarModifyTime = row.ItemArray.GetValue(7).ToString();
                }
                list.Add(model);
            }
            return list;
        }
        [HttpPost]
        public bool InsertDetailedElec(MgtDetailedElecModel Model)
        {
            //admin check
            if (getUserStatusNo() != "0")
            {
                return false;
            }

            // 檢查是否重複新增
            var sqlSelect = string.Format("SELECT 1 from evtrend.`register_car` " +
                "WHERE RegisterCarCountryCarsTypeNo={0} and RegisterCarYear={1}", SqlVal2(Model.RegisterCarCountryCarsTypeNo), SqlVal2(Model.RegisterCarYear));

            var dataSelect = _DB_GetData(sqlSelect);
            if (dataSelect.Rows.Count > 0)
            {
                return false;
            }


            //SQL Insert TotalElec
            var sqlStr = string.Format(
                @"INSERT INTO evtrend.`register_car` (" +
                    "RegisterCarCountryCarsTypeNo," +
                    "RegisterCarYear," +
                    "ElecNumber," +
                    "TotalNumber," +
                    "RegisterCarCreateUser," +
                    "RegisterCarCreateTime," +
                ")VALUES(" +
                    "{0}," +
                    "{1}," +
                    "{2}," +
                    "{3}," +
                    "{4}," +
                    "{5}," +
                    SqlVal2(Model.RegisterCarCountryCarsTypeNo),
                    SqlVal2(Model.RegisterCarYear),
                    SqlVal2(Model.ElecNumber),
                    SqlVal2(Model.TotalNumber),
                    SqlVal2(Model.RegisterCarCreateUser),
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
        public bool UpdateDetailedElec(MgtDetailedElecModel Model)
        {
            // admin check
            if (getUserStatusNo() != "0")
            {
                return false;
            }
            var sqlStr = string.Format("UPDATE evtrend.`register_car` " +
                "SET ElecNumber = {0} ,TotalNumber ={1}" +
                ",CarbonModifyTime  = {2} " +
                "WHERE " +
                "RegisterCarNo={3}",
                SqlVal2(Model.ElecNumber),
                SqlVal2(Model.TotalNumber),
                DBC.ChangeTimeZone(),
                SqlVal2(Model.RegisterCarNo));

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
        public bool DeleteTotalCarbon(MgtDetailedElecModel Model)
        {

            // admin check
            if (getUserStatusNo() != "0")
            {
                return false;
            }

            var sqlStr = string.Format("DELETE FROM evtrend.`register_car`" +
                "WHERE RegisterCarNo={0} ",
                SqlVal2(Model.RegisterCarNo));

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