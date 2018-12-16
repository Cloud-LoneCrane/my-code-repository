asp.net连接SQL数据库的方法 
这里直接给出代码;值得说明的是

1、要使用SQL必须先引入命名空间Data.SqlClient 如果使用的是Access数据库那么需要引入的命名空间为Date.Oledb

2、使用C#语言时候要注意大小写。

代码：conn.aspx.cs
//连接数据库的代码SQL 
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
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
     {
         //数据库连接字符     
String connectionString = "Server=127.0.0.1;Database=desschool;uid=sa;pwd=222;";
         //要执行的Sql语句
         String cmdText = "select * from test";
         //建立连接
         SqlConnection conn = new SqlConnection(connectionString);
         //生成命名实例
         SqlCommand command = new SqlCommand(cmdText, conn);
         //生成数据集实例
         DataSet dataSet = new DataSet();
         SqlDataAdapter sqlDA = new SqlDataAdapter();
         sqlDA.SelectCommand = command;
         //填充数据集
         sqlDA.Fill(dataSet);
         dgProShow.DataSource = dataSet;
         //绑定控件
         dgProShow.DataBind();


     }
}
//在conn.aspx中加入代码：
//<asp:DataGrid runat="server" ID="dgProShow"></asp:DataGrid>
//这样就完成了对数据库的连接和读取操作。

