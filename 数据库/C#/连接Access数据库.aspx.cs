using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class AccInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
           //oledb连接对象
        OleDbConnection g_cnn = new OleDbConnection();

        string strConnStr = "";
        string strDataBasePath = "";

        strConnStr = ConfigurationManager.AppSettings["ConnenctionString"];
        strDataBasePath = ConfigurationManager.AppSettings["datapath"];

        strConnStr += HttpContext.Current.Request.MapPath("~") + strDataBasePath;

        strConnStr += ";jet oledb:database password=jfmoonsun";

        g_cnn.ConnectionString = strConnStr;

        g_cnn.Open();

        string strSQLt = "";

        strSQLt = "SELECT bh as 编号 ,zh as 帐号, xm as 姓名 FROM zhxx order by bh";

        OleDbDataAdapter Adapter1 = new OleDbDataAdapter(strSQLt,g_cnn);
        DataSet dtsTmp1 = new DataSet();

        Adapter1.Fill(dtsTmp1, "zhxx");

        Response.Write("Access数据库内容</p>");

        DataView viewTmp = dtsTmp1.Tables["zhxx"].DefaultView;
        GridView1.DataSource = viewTmp;

        GridView1.DataBind();

        g_cnn.Close();


    
     
    }
}
