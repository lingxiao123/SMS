using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data.SqlClient;
using SMS.Utility;
using System.Data;
using ProjectToYou.Code;

namespace SMS
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request["username"].Trim();
            string pwd = context.Request["pwd"].Trim();
            int value = 0;
            //开发测试时使用
            if (username == "admin" && pwd == "admin")
            {
                value = 1;
                context.Session["UserName"] = username;
                context.Session["DateTime"] = DateTime.Now;
            }
            else
            {
                pwd = DESEncrypt.Encrypt(pwd);
                SqlParameter[] parameters = new SqlParameter[]
                    {
                                    new SqlParameter{ParameterName="@UserName",SqlDbType=SqlDbType.VarChar,Size=30,Value=username},
                                    new SqlParameter{ParameterName="@Password",SqlDbType=SqlDbType.VarChar,Size=30,Value=pwd}
                    };
                string sql = string.Format("select * from UserTable where Name=@UserName and Pwd=@Password");
                DataTable dt = DBAccess.QueryDataTable(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    int status = Convert.ToInt32(dt.Rows[0]["Status"]);
                    if (status == 1)
                    {
                        context.Session["UserName"] = username;
                        context.Session["DateTime"] = DateTime.Now;
                        value = 1;
                    }
                    else
                    {
                        value = 0;
                    }
                }
                else
                {
                    value = 2;
                }
            }

            context.Response.ContentType = "applocation/text;chartset=utf-8;";
            context.Response.Write(value);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}