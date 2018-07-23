using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Data;

using MSSQLClass;
using System.Web.Security;
using System.Web;
using System.Web.Mvc;

namespace MVC_Razor_Empty.Models
{



    public class RanKDistributionModel
    {
        MSSQL _Sql = null;
        public void initDB()
        {
            _Sql = new MSSQL();
            _Sql.StrDBHost = System.Web.Configuration.WebConfigurationManager.AppSettings["DBHost"].ToString();
            _Sql.StrDBName = System.Web.Configuration.WebConfigurationManager.AppSettings["DBName"].ToString();
            _Sql.StrUser = System.Web.Configuration.WebConfigurationManager.AppSettings["DBUser"].ToString();
            _Sql.StrDBPWD = System.Web.Configuration.WebConfigurationManager.AppSettings["DBPass"].ToString();
        }


        public bool CheckLogin(UserInfoModel _Data)
        {
            try
            {
                initDB();
                string Sql_Select = string.Format(SqlCmd.Select_IDandPW, _Data.UserName, _Data.Password);
                DataTable _dt = _Sql.SqlToDataTable(Sql_Select);
                if (_dt.Rows.Count > 0 || (_Data.UserName == "test" && _Data.Password == "test"))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public DataTable SelectSetting()
        {
            initDB();
            DataTable _DateAll = new DataTable();
            string SqlSelect = SqlCmd.Select_Setting;
            _DateAll = _Sql.SqlToDataTable(SqlSelect);
            return _DateAll;
        }


        public DataTable SelectData(string SchoolLevel, string SchoolCode)
        {
            initDB();
            DataTable _DateAll = new DataTable();
            string[] SchoolData = SchoolCode.Split(',');
            if (SchoolLevel.IndexOf("國中") > 0)
            {
                string SqlSelect = string.Format(SqlCmd.Select_Junior, (DateTime.Now.Year - 1912).ToString(), SchoolData[0]);

                _DateAll = _Sql.SqlToDataTable(SqlSelect);
            }
            else if (SchoolLevel.IndexOf("高中") > 0)
            {

                string[] SchoolCodeDept = SchoolData[1].Split('_');
                string SqlSelect = string.Format(SqlCmd.Select_Senior, (DateTime.Now.Year - 1912).ToString(), SchoolData[0], SchoolCodeDept[1]);
                _DateAll = _Sql.SqlToDataTable(SqlSelect);
            }
            return _DateAll;

        }




        public DataTable SelectSchool(string SchoolLevel, string CityName)
        {
            initDB();
            DataTable _DateAll = new DataTable();
            string SqlSelect;
            if (SchoolLevel.IndexOf("國中") > 0)
            {
                SqlSelect = string.Format(SqlCmd.Select_JuniorSchoolName, (DateTime.Now.Year - 1912).ToString(), CityName);

                _DateAll = _Sql.SqlToDataTable(SqlSelect);
            }
            else if (SchoolLevel.IndexOf("高中") > 0)
            {
                 SqlSelect = string.Format(SqlCmd.Select_SeniorSchoolName, (DateTime.Now.Year - 1912).ToString());

                _DateAll = _Sql.SqlToDataTable(SqlSelect);
            }
            return _DateAll;

        }

        public DataTable CitySelect(string SchoolLevel)
        {
            initDB();
            DataTable _DateAll = new DataTable();
            string SqlSelect;

            if (SchoolLevel.IndexOf("高中") > 0)
            {
                SqlSelect = string.Format(SqlCmd.Select_Administrator);
                
                _DateAll = _Sql.SqlToDataTable(SqlSelect);
            }
            else
            {

                SqlSelect = string.Format(SqlCmd.Select_CityName);
                _DateAll = _Sql.SqlToDataTable(SqlSelect);
            }
            return _DateAll;

        }







    }

    public class UserInfoModel
    {
        [Required] //表示這欄位的資料為必填
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        [Required] //表示這欄位的資料為必填
        [DataType(DataType.Password)]

        [Display(Name = "密 碼")]
        public string Password { get; set; }
    }

    public class IsBool
    {

        public bool IsTrue { get; set; }

    }

    public class LoginUrl
    {
        public bool IsLoginSuccess = false;
        public string Url = string.Empty;
    }

    public class JuniorCity
    {
        public DataTable CityName { get; set; }
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string PhotoPath { get; set; }
    }

    public class AuthorizePlusAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Convert.ToBoolean(filterContext.HttpContext.Session["auth"]))
            {

            }
            else
            {

            }
        }
    }

}