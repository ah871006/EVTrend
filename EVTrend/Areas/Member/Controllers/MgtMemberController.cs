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
            string account;
            Request.Cookies.TryGetValue("account", out account);
            ViewData["Member"] = GetMemberModel(account);
            return View("MgtMember");
        }

        private DataTable GetMember(string account)
        {
            var sqlStr = string.Format(
                "SELECT Account, Username, Gender, Birthday from evtrend.`member` WHERE Account = {0}", SqlVal2(account));
            var data = _DB_GetData(sqlStr);
            return data;
        }

        private MemberModels GetMemberModel(string account)
        {
            var data = GetMember(account);
            MemberModels member = new MemberModels();
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