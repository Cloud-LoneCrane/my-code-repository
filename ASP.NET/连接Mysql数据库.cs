using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;//MySql数据库连接

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //创建一个MySqlConnection数据库连接变量
        MySqlConnection cnn = new MySqlConnection();

        string strServer, strUser, strPwd,strDB;

        strServer = "localhost";
        strUser = "root";
        strPwd = "jift";
        strDB = "yqsnew";

        //定义MySqlConnection数据连接变量oc的连接字符串
        cnn.ConnectionString = string.Format(@"Server={0};User Id={1};Password={2};Persist Security Info=True;Database={3}",
            strServer,strUser,strPwd,strDB );

        ////定义MySqlConnection数据连接变量oc的连接字符串
        //cnn.ConnectionString = @"Server=localhost;User Id=root;Password=jift;Persist Security Info=True;Database=yqsnew"; 

        try
        {
            Response.Write("<p>");
            cnn.Open(); //连接
            Response.Write(cnn.Database);
            Response.Write("数据库连接成功!");
            Response.Write("</p>");


            //创建Command对象
            MySqlCommand cmd = cnn.CreateCommand();

            //定义sql语句
            string strSQL = "";

            strSQL = "SELECT * FROM zhxx";

            cmd.CommandText = strSQL;
              
            //执行 
            MySqlDataReader read= cmd.ExecuteReader();

            //显示数据
            while(read.Read())
            {

                Response.Write( read["zh"]);

                Response.Write("<br>");
            }

            read.Close();
            cnn.Close();
            Response.Write("执行完毕");


        }catch(MySqlException ex)
        {
            Response.Write("<p>");
            Response.Write("数据库连接失败");
            Response.Write(ex.Message);
            Response.Write("</p>");

        }

       
    }
}
