using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MSSQLClass;
using System.Data;
using MVC_Razor_Empty.Models;

using System.Web.Security;

namespace MVC_Razor_Empty.Controllers
{
    public class HomeController : Controller
    {

        RanKDistributionModel _RanKDistributionModel = new RanKDistributionModel();

        //
        // GET: /Home/Index  
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult JQueryIndex()
        {
            return View();
        }

        // POST: /Home/Login     
        [HttpPost]
        public ActionResult Login(UserInfoModel _UserInfoData)
        {

            if (_RanKDistributionModel.CheckLogin(_UserInfoData))
            {
                //TempData["message"] = "這是一個簡單的顯示結果";   
                //Session["userame"] = _UserInfoData.UserName;
                Session["auth"] = true;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    //你想要存放在 User.Identy.Name 的值，通常是使用者帳號
              _UserInfoData.UserName,
              DateTime.Now,
              DateTime.Now.AddMinutes(30),
              true,//將管理者登入的 Cookie 設定成 Session Cookie
              "userdata",//userdata看你想存放啥
              FormsAuthentication.FormsCookiePath);


                string encTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.HttpOnly = true;

                Response.Cookies.Add(cookie);


                return RedirectToAction("Index", "RanKDistribution");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult LoginOut()
        {
            Session["auth"] = false;
            return RedirectToAction("Index", "Home");

        }

        [AuthorizePlus]
        public ActionResult Backend(UserInfoModel _UserInfoData)
        {
            return Content("TEST");

        }



        // POST: /Home/LoginAJAX
        [HttpPost]
        public ActionResult LoginAJAX(UserInfoModel _UserInfoData)
        {
            LoginUrl _LoginUrl = new LoginUrl();
            if (_RanKDistributionModel.CheckLogin(_UserInfoData))
            {
                //  $(locatoin).attr('href', '~/RanKDistribution/Index');
                _LoginUrl.Url = "/RanKDistribution";
                _LoginUrl.IsLoginSuccess = true;
                Session["userame"] = _UserInfoData.UserName;
                Session["auth"] = true;



                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    //你想要存放在 User.Identy.Name 的值，通常是使用者帳號
              _UserInfoData.UserName,
              DateTime.Now,
              DateTime.Now.AddMinutes(30),
              true,//將管理者登入的 Cookie 設定成 Session Cookie
              "userdata",//userdata看你想存放啥
              FormsAuthentication.FormsCookiePath);


                string encTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.HttpOnly = true;

                Response.Cookies.Add(cookie);



                // return Json(new { msg = "密碼正確 " });
            }
            else
            {
                _LoginUrl.Url = "";
                _LoginUrl.IsLoginSuccess = false;
            }
            return Json(_LoginUrl);
        }




        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
