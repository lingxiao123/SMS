using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data;
using ProjectToYou.Code;
using SMS.Utility;
namespace SMS
{
    public class Global : HttpApplication
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 60000*5;
            timer.Enabled = true;
            GC.KeepAlive(timer);


        }
        void Session_End(object sender, EventArgs e)
        {
            //下面的代码是关键，可解决IIS应用程序池自动回收的问题
            System.Threading.Thread.Sleep(1000);
            //触发事件, 写入提示信息
            //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
            //使用您自己的URL
            string url = "http://localhost:49556/Login.aspx";
            System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
            System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流

            // 在会话结束时运行的代码。
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为 InProc 时，才会引发 Session_End 事件。
            // 如果会话模式设置为 StateServer
            // 或 SQLServer，则不会引发该事件。
        }
        public void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd");
            //获取所有当天要发送的发货短信
            string sql =string.Format("select * from DispatchList where dDate='{0}'",nowTime);
            DataTable dt = DBAccessSystem.QueryDataTable(sql); 
            string[] arr = new string[11];
            string clientphone = "";            
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                #region 判断是否已发送(已发送跳出当前循环) 
                string sql_exits =string.Format("select * from SendLog where DLCode='{0}' and Status=1", dt.Rows[i]["cDLCode"].ToString());
                DataTable dt_exits = DBAccess.QueryDataTable(sql_exits);
                if (dt_exits.Rows.Count > 0)
                {
                    continue;
                }
                #endregion
                #region 判断是否审核 未审核不发送
                if(dt.Rows[i]["cVerifier"].ToString()==""|| dt.Rows[i]["cVerifier"].ToString()==null){
                    continue;
                }
                #endregion
                arr[0] = dt.Rows[i]["cCusName"].ToString();//客户名称
                #region 获取客户手机号码                
                    string sql_cusphone = string.Format("select cCusPhone from Customer where cCusCode='{0}'", dt.Rows[i]["cCusCode"].ToString());
                    DataTable dt_cusphone = DBAccessSystem.QueryDataTable(sql_cusphone);
                    if (dt_cusphone.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt_cusphone.Rows.Count; j++)
                        {
                            clientphone = dt_cusphone.Rows[0]["cCusPhone"].ToString().Trim();
                        }
                    }
                    if (clientphone=="")
                    {
                        continue;
                    }
                #endregion
                arr[1] =Convert.ToDateTime(dt.Rows[i]["dDate"].ToString()).ToString("yyyy-MM-dd");// 发货日期
                arr[2] = dt.Rows[i]["cShipAddress"].ToString();//发货地址
                #region 获取存货名称
                string invname = "";
                string sql_invname = string.Format("select cInvName from SO_SODetails where cInvCode in (select cInvCode from DispatchLists where DLID={0})",dt.Rows[i]["DLID"]);
                DataTable dt_invname = DBAccessSystem.QueryDataTable(sql_invname);
                if (dt_invname.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_invname.Rows.Count; j++)
                    {
                        if(j>1)
                        {
                            break;
                        }
                        invname += dt_invname.Rows[j]["cInvName"].ToString() + ",";
                    }
                    invname = invname.Substring(0,invname.LastIndexOf(','));
                    invname = invname + "等";
                }
                #endregion
                arr[3] = invname;// 存货名称
                arr[4] = "";//规格型号
                arr[5] = "";//主计量
                arr[6] = "";//数量
                arr[7] = dt.Rows[i]["cDefine3"].ToString();//物流单号
                arr[8] = dt.Rows[i]["cDefine8"].ToString();//总件数
                #region 获取业务员名称和联系电话
                string personname = "";
                string personphone = "";
                string sql_person = string.Format("select cPersonName,cPersonPhone from Person where cPersonCode='{0}'", dt.Rows[i]["cPersonCode"].ToString());
                DataTable dt_person = DBAccessSystem.QueryDataTable(sql_person);
                if (dt_person.Rows.Count>0)
                {
                    for (int j = 0; j < dt_person.Rows.Count; j++)
                    {
                        personname = dt_person.Rows[0]["cPersonName"].ToString();
                        personphone = dt_person.Rows[0]["cPersonPhone"].ToString();
                    }
                }
                #endregion
                arr[9] = personname;//业务员
                arr[10] = personphone;//业务员联系电话
                bool flag = SendMsg.SendMsgs(clientphone,arr);
                #region 添加发送日志                
                    if (flag)
                    {
                        string sql_log = string.Format("insert into SendLog values('{0}','{1}','{2}',{3},'{4}')", dt.Rows[i]["cCusName"].ToString(), clientphone, dt.Rows[i]["cDLCode"].ToString(),1,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        int count = DBAccess.ExecTransSql(sql_log);
                    }
                    else
                    {
                        string logexits = string.Format("select * from SendLog where DLCode='{0}' and AddTime like '%{1}%'", dt.Rows[i]["cDLCode"].ToString(),nowTime);
                        DataTable dt_logexits = DBAccess.QueryDataTable(logexits);
                        if (dt_logexits.Rows.Count==0)
                        {
                            string sql_log = string.Format("insert into SendLog values('{0}','{1}','{2}',{3},'{4}')", dt.Rows[i]["cCusName"].ToString(), clientphone, dt.Rows[i]["cDLCode"].ToString(), 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            int count = DBAccess.ExecTransSql(sql_log);
                        }                       
                    }
                #endregion
            }

        }
     }
}