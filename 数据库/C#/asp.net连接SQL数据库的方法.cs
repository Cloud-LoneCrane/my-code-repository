asp.net����SQL���ݿ�ķ��� 
����ֱ�Ӹ�������;ֵ��˵������

1��Ҫʹ��SQL���������������ռ�Data.SqlClient ���ʹ�õ���Access���ݿ���ô��Ҫ����������ռ�ΪDate.Oledb

2��ʹ��C#����ʱ��Ҫע���Сд��

���룺conn.aspx.cs
//�������ݿ�Ĵ���SQL 
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
         //���ݿ������ַ�     
String connectionString = "Server=127.0.0.1;Database=desschool;uid=sa;pwd=222;";
         //Ҫִ�е�Sql���
         String cmdText = "select * from test";
         //��������
         SqlConnection conn = new SqlConnection(connectionString);
         //��������ʵ��
         SqlCommand command = new SqlCommand(cmdText, conn);
         //�������ݼ�ʵ��
         DataSet dataSet = new DataSet();
         SqlDataAdapter sqlDA = new SqlDataAdapter();
         sqlDA.SelectCommand = command;
         //������ݼ�
         sqlDA.Fill(dataSet);
         dgProShow.DataSource = dataSet;
         //�󶨿ؼ�
         dgProShow.DataBind();


     }
}
//��conn.aspx�м�����룺
//<asp:DataGrid runat="server" ID="dgProShow"></asp:DataGrid>
//����������˶����ݿ�����ӺͶ�ȡ������

