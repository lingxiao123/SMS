using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using SMS.Utility;
using ProjectToYou.Code;
namespace SMS.User
{
    public partial class AddUser : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

        }
        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑
            try
            {
                string username = UserName.Text.Trim();
                string pwd = PassWord.Text.Trim();
                pwd = DESEncrypt.Encrypt(pwd);
                string addtime = DateTime.Now.ToString("yyyy-MM-dd");
                string sql = string.Format("insert into UserTable values('{0}','{1}',{2},'{3}','{4}')", username, pwd, 1, addtime, "");
                int count = DBAccess.ExecTransSql(sql);
                if (count > 0)
                {
                    Alert.ShowInTop("新增成功");
                }
                else
                {
                    Alert.ShowInTop("新增失败");
                }
            }
            catch (Exception ex)
            {
                WriteLog.WriteError(ex);
            }

            // 2. 关闭本窗体，然后刷新父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}