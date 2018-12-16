using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;//MySql���ݿ�����

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //����һ��MySqlConnection���ݿ����ӱ���
        MySqlConnection cnn = new MySqlConnection();

        string strServer, strUser, strPwd,strDB;

        strServer = "localhost";
        strUser = "root";
        strPwd = "jift";
        strDB = "yqsnew";

        //����MySqlConnection�������ӱ���oc�������ַ���
        cnn.ConnectionString = string.Format(@"Server={0};User Id={1};Password={2};Persist Security Info=True;Database={3}",
            strServer,strUser,strPwd,strDB );

        ////����MySqlConnection�������ӱ���oc�������ַ���
        //cnn.ConnectionString = @"Server=localhost;User Id=root;Password=jift;Persist Security Info=True;Database=yqsnew"; 

        try
        {
            Response.Write("<p>");
            cnn.Open(); //����
            Response.Write(cnn.Database);
            Response.Write("���ݿ����ӳɹ�!");
            Response.Write("</p>");


            //����Command����
            MySqlCommand cmd = cnn.CreateCommand();

            //����sql���
            string strSQL = "";

            strSQL = "SELECT * FROM zhxx";

            cmd.CommandText = strSQL;
              
            //ִ�� 
            MySqlDataReader read= cmd.ExecuteReader();

            //��ʾ����
            while(read.Read())
            {

                Response.Write( read["zh"]);

                Response.Write("<br>");
            }

            read.Close();
            cnn.Close();
            Response.Write("ִ�����");


        }catch(MySqlException ex)
        {
            Response.Write("<p>");
            Response.Write("���ݿ�����ʧ��");
            Response.Write(ex.Message);
            Response.Write("</p>");

        }

       
    }
}
