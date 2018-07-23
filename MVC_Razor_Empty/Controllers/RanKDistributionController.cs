using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MSSQLClass;
using System.Data;
using MVC_Razor_Empty.Models;
/*Json.NET相關的命名空間*/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Security;

namespace MVC_Razor_Empty.Controllers
{
    public class RanKDistributionController : Controller
    {

        RanKDistributionModel _RanKDistributionModel = new RanKDistributionModel();
        //
        // GET: /RanKDistribution/        
        public ActionResult Index()
        {
            
            if (_RanKDistributionModel.SelectSetting() != null)
                ViewData["RankDistributionSetting"] = _RanKDistributionModel.SelectSetting().AsEnumerable();

            //AuthorizationContext
            bool test = User.Identity.IsAuthenticated;
            if (Session["userame"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

          
        }

     
        //
        // GET: /RanKDistribution/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RanKDistribution/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RanKDistribution/Create

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
        // GET: /RanKDistribution/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RanKDistribution/Edit/5
        [ValidateAntiForgeryToken]
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
        // GET: /RanKDistribution/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RanKDistribution/Delete/5

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

   



        [HttpPost]
        public ActionResult DataShowView()
        {            
            return View("Index");
        }

        // POST: /RankDistribution/DataShowAJAX
        [HttpPost]
        public ActionResult DataShowAJAX(string SchoolLevel, string SchoolCode)
        {
            DataTable _dt = _RanKDistributionModel.SelectData(SchoolLevel, SchoolCode);

            if (_dt != null)
            {
                string Data_Thead = string.Empty;
                Data_Thead = "<thead > <tr>";
                string Thead = string.Empty;

                //Thead
                for (int i = 0; i < _dt.Columns.Count; i++)
                {
                    Thead = _dt.Columns[i].ToString();

                    Data_Thead = Data_Thead + "<th>" + Thead + "</th>";
                }
                Data_Thead = Data_Thead + " </tr>  </thead> ";
                int ColumnsCount = _dt.Columns.Count;

                string Data_Tbody = string.Empty;
                string TBody = string.Empty;
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        TBody = _dt.Rows[i][j].ToString();
                        Data_Tbody = Data_Tbody + " <td>" + TBody + "</td>";
                     
                    }
                }
                Data_Tbody = "<tbody>  <tr>" + Data_Tbody + " </tr>  </tbody> ";

                return Content(Data_Thead + Data_Tbody);
            }
            else
            {
                return Content("");
            }

        }

        [HttpPost]
        public ActionResult DataShowAJAXJson(string SchoolLevel, string SchoolCode)
        {
            DataTable _dt = _RanKDistributionModel.SelectData(SchoolLevel, SchoolCode);

            string str_json = JsonConvert.SerializeObject(_dt, Formatting.Indented);

            if(_dt!=null)
            {
               // return Json(_dt);
                return Content(str_json);
            }
            else
            {
                return Content(str_json);
                //return Content(_dt);
            }

        }

        public List<Dictionary<string, object>> DatatableToDictinary(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            //return serializer.Serialize(rows);
            return rows;
        }

        [HttpPost]
        public ActionResult SelectSchoolData(string SchoolLevel, string CityName)
        {
            string Select_School = string.Empty;
            string Result = string.Empty;
            DataTable _dt = _RanKDistributionModel.SelectSchool(SchoolLevel, CityName);
            if (_dt != null)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Select_School = " <option>" + _dt.Rows[i]["SchoolName"].ToString().Trim() + " </option>";
                    Result = Result + Select_School;

                }

            }
            return Content(Result);
        }

        [HttpPost]
        public ActionResult SelectCityData(string SchoolLevel)
        {
            string Select_City = string.Empty;
            string Result = string.Empty;
            DataTable _dt = _RanKDistributionModel.CitySelect(SchoolLevel);
            if (_dt != null)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Select_City = " <option>" + _dt.Rows[i]["CityName"].ToString().Trim() + " </option>";
                    Result = Result + Select_City;
                }

            }
            return Content(Result);
        }

        [HttpPost]
        public ActionResult IsNeedCity(string ReportName)
        {
            IsBool _IsBool = new IsBool();
            ReportName = ReportName.Trim();

            if (SqlCmd.ReportName.IndexOf( ReportName)>=0)          
            {
                _IsBool.IsTrue = true;
            }
            else
            {
                _IsBool.IsTrue = false;
            }




            return Json(_IsBool);
            // return _IsNeedCity;
        }

        [HttpPost]
        public ActionResult DownLoadFile()
        {
            //上傳與下載還沒練過
            string result =Server.MapPath( @"~/Content/960.css");
            //return new FilePathResult(Server.MapPath("/Content/網頁學習.docx"), "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            return  File(result, "text/css","123.css");
      
        }

    }
}
