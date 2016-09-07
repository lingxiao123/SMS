using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Utility;
using ProjectToYou.Code;
using FineUI;
namespace SMS.User
{
    public partial class User :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnImport.OnClientClick = Window1.GetShowReference("~/User/AddUser.aspx");
                BindGrid();
            }
        }

        #region BindGrid

        private void BindGrid()
        {
            // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
            Grid1.RecordCount = GetTotalCount();

            // 2.获取当前分页数据
            DataTable table = GetPagedDataTable();
            // 3.绑定到Grid
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount()
        {
            //return DataSourceUtil.GetDataTable2().Rows.Count;
            string sql = "select * from UserTable";
            DataTable dt = DBAccess.QueryDataTable(sql);
            return dt.Rows.Count;
        }

        /// <summary>
        /// 模拟数据库分页
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable()
        {
            int pageIndex = Grid1.PageIndex;
            int pageSize = Grid1.PageSize;

            string sortField = Grid1.SortField;
            string sortDirection = Grid1.SortDirection;
            string sql = "select * from UserTable";
            DataTable dt = DBAccess.QueryDataTable(sql);
            //DataTable table2 = DataSourceUtil.GetDataTable2();
            DataTable table2 = dt;
            DataView view2 = table2.DefaultView;
            view2.Sort = String.Format("{0} {1}", sortField, sortDirection);

            DataTable table = view2.ToTable();

            DataTable paged = table.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > table.Rows.Count)
            {
                rowend = table.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(table.Rows[i]);
            }

            return paged;
        }

        #endregion

        #region Events

        protected void Button1_Click(object sender, EventArgs e)
        {
            //labResult.Text = HowManyRowsAreSelected(Grid1);
        }


        protected void Grid1_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            //Grid1.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            //Grid1.SortDirection = e.SortDirection;
            //Grid1.SortField = e.SortField;

            BindGrid();
        }

        #endregion


        protected void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            try
            {
                int count = 0;
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    int rowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                    GridRow row = Grid1.Rows[rowIndex];
                    int status = Convert.ToInt32(Grid1.Rows[rowIndex].Values[2]);
                    string datetime = DateTime.Now.ToString("yyyy-MM-dd");
                    string sql = string.Format("update UserTable set Status={0},UpdateTime='{1}' where Id={2}", status, datetime, rowID);
                    count += DBAccess.ExecTransSql(sql);
                }
                if (count > 0)
                {
                    Alert.ShowInTop("修改成功");
                    BindGrid();
                }
                else
                {
                    Alert.ShowInTop("修改失败");
                }
            }
            catch (Exception ex)
            {
                WriteLog.WriteError(ex);
            }
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Action1")
            {
                try
                {
                    object[] keys = Grid1.DataKeys[e.RowIndex];
                    int id = Convert.ToInt32(keys[0]);
                    string sql = string.Format("update UserTable set Pwd='{0}' where Id={1}", DESEncrypt.Encrypt("123456"), id);
                    int count = DBAccess.ExecTransSql(sql);
                    if (count > 0)
                    {
                        Alert.ShowInTop("重置成功");
                    }
                }
                catch (Exception ex)
                {
                    WriteLog.WriteError(ex);
                }

            }
        }
    }
}