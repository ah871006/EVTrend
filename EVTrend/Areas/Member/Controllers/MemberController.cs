using EVTrend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

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
            string account;
            Request.Cookies.TryGetValue("account", out account);
            ViewData["Member"] = GetMemberModel(account);
            return View("ShowMember");
        }

        private DataTable GetMember(string account)
        {
            var sqlStr = string.Format(
                "SELECT Account, Username, Gender, Birthday from evtrend.`member` WHERE Account = {0}", SqlVal2(account));
            var data = _DB_GetData(sqlStr);
            return data;
        }

        private EVTrend.Models.Member GetMemberModel(string account)
        {
            var data = GetMember(account);
            EVTrend.Models.Member member = new EVTrend.Models.Member();
            DataRow row = data.Rows[0];

            member.Account = row.ItemArray.GetValue(0).ToString();
            member.Username = row.ItemArray.GetValue(1).ToString();
            string Gender = row.ItemArray.GetValue(2).ToString();

            if (Gender == "1")
            {
                member.Gender = "男";
            }
            else if (Gender == "2")
            {
                member.Gender = "女";
            }
            else
            {
                member.Gender = "不願透漏";
            }

            member.Birthday = DateTime.Parse(row.ItemArray.GetValue(3).ToString());

            return member;
        }
    }
}