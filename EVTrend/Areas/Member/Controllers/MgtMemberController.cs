using EVTrend.Areas.Member.Models;
using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

namespace EVTrend.Areas.Member.Controllers
{
    [Area(areaName: "Member")]

    public class MgtMemberController : _BaseController
    {
        /// <summary>
        /// 使用者管理View
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (getUserStatusNo() == "0") //管理員
            {
                ViewData["Member"] = GetMemberModel();
                return View("MgtMember");
            }
            else
            {
                return Redirect("~/Home/Error");
            }
        }

        private List<MemberModels> GetMemberModel()
        {
            var data = GetMember();
            List<MemberModels> list = new List<MemberModels>();

            foreach (DataRow row in data.Rows)
            {
                var gender = row.ItemArray.GetValue(2).ToString();
                var statusNo = row.ItemArray.GetValue(4).ToString();

                MemberModels model = new MemberModels();

                model.Account = row.ItemArray.GetValue(0).ToString();
                model.Username = row.ItemArray.GetValue(1).ToString();
                model.Gender = (gender == "1" ? "男" : (gender == "2" ? "女" : "不願透漏"));
                model.Birthday = DateTime.Parse(row.ItemArray.GetValue(3).ToString());
                model.StatusNo = (statusNo == "0" ? "管理員" : (statusNo == "1" ? "會員" : "停權"));

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 撈DB會員資料
        /// </summary>
        /// <returns></returns>
        private DataTable GetMember()
        {
            var sqlStr = string.Format("SELECT Account, Username, Gender, Birthday, StatusNo FROM member ORDER BY StatusNo, Account ASC");

            var data = _DB_GetData(sqlStr);
            return data;
        }
    }
}