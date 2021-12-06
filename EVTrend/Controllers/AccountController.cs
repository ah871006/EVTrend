using EVTrend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Controllers
{
    public class AccountController : _BaseController
    {
        /// <summary>
        /// 使用者登入View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 註冊會員View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 檢查是否有重複的帳號
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DuplicateAccountCheck(string Account)
        {
            //是否為空
            if (string.IsNullOrEmpty(Account)) return false;

            //SQL Select Member
            var sqlStr = string.Format("SELECT Account FROM member WHERE Account = {0}", SqlVal2(Account));

            //SQL Check
            var data = _DB_GetData(sqlStr);

            //資料庫內是否有此帳號
            if (data.Rows.Count > 0)
            {
                //已經有該帳號
                return true;
            }

            //資料庫內目前沒此帳號
            return false;
        }

        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(Member Model)
        {
            Model.ok = true;
            Model.StatusNo = "0";

            //SQL Insert Member
            var sqlStr = string.Format(
                @"INSERT INTO member (" +
                    "Account," +
                    "Password," +
                    "Username," +
                    "Gender," +
                    "Birthday," +
                    "CreateTime," +
                    "AccountStart," +
                    "StatusNo" +
                ")VALUES(" +
                    "{0}," +
                    "{1}," +
                    "{2}," +
                    "{3}," +
                    "{4}," +
                    "{5}," +
                    "{6}," +
                    "{7}",
                    SqlVal2(Model.Account),
                    SqlVal2(SHA256_Encryption(Model.Password)),
                    SqlVal2(Model.Username),
                    SqlVal2(Model.Gender),
                    SqlVal2(Model.Birthday),
                    DBC.ChangeTimeZone(),
                    DBC.ChangeTimeZone(),
                    SqlVal2(Model.StatusNo) + ")"
                );

            //SQL Check
            var check = _DB_Execute(sqlStr);

            //新增是否成功
            if (check == 1)
            {
                Model.ResultMessage = "註冊成功";
            }
            else
            {
                Model.ok = false;
                Model.ResultMessage = "註冊失敗";
            }

            return View(Model);
        }
    }
}
