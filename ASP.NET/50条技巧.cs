
1.//�����Ի���.���ת��ָ��ҳ�� 
Response.Write("<script>window.alert('�û�Աû���ύ����,�������ύ��')</script>");
Response.Write("<script>window.location ='http://www.cgy.cn/bizpulic/upmeb.aspx'</script>");

2.//�����Ի���

Response.Write("<script language='javascript'>alert('��Ʒ��ӳɹ���')</script >");

3.//ɾ���ļ�


string filename ="20059595157517.jpg";
pub.util.DeleteFile(HttpContext.Current.Server.MapPath("../file/")+filename);

4.//�������б��datalist

System.Data.DataView dv=conn.Exec_ex("select -1 as code,'��ѡ��Ӫģʽ' as content from dealin union select code,content from dealin");
this.dealincode.DataSource=dv;
this.dealincode.DataTextField="content";
this.dealincode.DataValueField="code";    
this.dealincode.DataBind();
this.dealincode.Items.FindByValue(dv[0]["dealincode"].ToString()).Selected=true;

5.//ʱ��ȥ����ʾ

<%# System.DateTime.Parse(DataBinder.Eval(Container.DataItem,"begtime").ToString()).ToShortDateString()%>

6.//���������

<%# "<a class=\"12c\" target=\"_blank\" href=\"http://www.51aspx/CV/_"+DataBinder.Eval(Container.DataItem,"procode")+".html\">"+ DataBinder.Eval(Container.DataItem,"proname")+"</a>"%>


7.//�޸�ת��

<%# "<A href=\"editpushpro.aspx?id="+DataBinder.Eval(Container.DataItem,"code")+"\">"+"�޸�"+"</A>"%>


8.//����ȷ����ť

<%# "<A id=\"btnDelete\" onclick=\"return confirm('���Ƿ�ȷ��ɾ��������¼��?');\" href=\"pushproduct.aspx?dl="+DataBinder.Eval(Container.DataItem,"code")+"\">"+"ɾ��"+"</A>"%>


9.//������ݸ�ʽ�� "{0:F2}" �Ǹ�ʽ F2��ʾС�����ʣ��λ

<%# DataBinder.Eval(Container, "DataItem.PriceMoney","{0:F2}") %>

10.//��ȡ��̬��ҳ����

Uri uri = new Uri("http://www.soAsp.net/");
  WebRequest req = WebRequest.Create(uri);
  WebResponse resp = req.GetResponse();
  Stream str = resp.GetResponseStream();
  StreamReader sr = new StreamReader(str,System.Text.Encoding.Default);
  string t = sr.ReadToEnd();
  this.Response.Write(t.ToString());

11.//��ȡ" . "������ַ�

i.ToString().Trim().Substring(i.ToString().Trim().LastIndexOf(".")+1).ToLower().Trim()

12. ���µĴ��ڲ����Ͳ����� 
�������Ͳ�����

response.write("��script��window.open(��*.aspx?id="+this.DropDownList1.SelectIndex+"&id1="+...+"��)��/script��")

���ղ�����

string a = Request.QueryString("id");
string b = Request.QueryString("id1");

12.Ϊ��ť��ӶԻ���

Button1.Attributes.Add("onclick","return confirm(��ȷ��?��)");
button.attributes.add("onclick","if(confirm(��are you sure...?��)){return true;}else{return false;}")

13.ɾ�����ѡ����¼

int intEmpID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];
string deleteCmd = "Delete from Employee where emp_id = " + intEmpID.ToString()

14.ɾ������¼����

private void DataGrid_ItemCreated(Object sender,DataGridItemEventArgs e)
{
����switch(e.Item.ItemType)
����{
����case ListItemType.Item :
����case ListItemType.AlternatingItem :
����case ListItemType.EditItem:
����TableCell myTableCell;
����myTableCell = e.Item.Cells[14];
����LinkButton myDeleteButton ;
����myDeleteButton = (LinkButton)myTableCell.Controls[0];
����myDeleteButton.Attributes.Add("onclick","return confirm(�����Ƿ�ȷ��Ҫɾ��������Ϣ��);");
����break;
����default:
����break;
����}
}

15.��������������һҳ

private void grdCustomer_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
{
����//�������
����if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
����e.Item.Attributes.Add("onclick","window.open(��Default.aspx?id=" + e.Item.Cells[0].Text + "��);");
}

˫��������ӵ���һҳ
������itemDataBind�¼���

if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
{
����string orderItemID =e.item.cells[1].Text;
����e.item.Attributes.Add("ondblclick", "location.href=��../ShippedGrid.aspx?id=" + orderItemID + "��");
}

˫��������һҳ

if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
{
����string orderItemID =e.item.cells[1].Text;
����e.item.Attributes.Add("ondblclick", "open(��../ShippedGrid.aspx?id=" + orderItemID + "��)");
}


16.��������д��ݲ���

��asp:HyperLinkColumn Target="_blank" headertext="ID��" DataTextField="id" NavigateUrl="aaa.aspx?id=��
������%# DataBinder.Eval(Container.DataItem, "�����ֶ�1")%���� & name=����%# DataBinder.Eval(Container.DataItem, "�����ֶ�2")%���� /��

17.������ı���ɫ

if (e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
{
����e.Item.Attributes.Add("onclick","this.style.backgroundColor=��#99cc00��;
������ this.style.color=��buttontext��;this.style.cursor=��default��;");
} 

д��DataGrid��_ItemDataBound��

if (e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
{
e.Item.Attributes.Add("onmouseover","this.style.backgroundColor=��#99cc00��;
����this.style.color=��buttontext��;this.style.cursor=��default��;");
e.Item.Attributes.Add("onmouseout","this.style.backgroundColor=����;this.style.color=����;");
}

18.�������ڸ�ʽ
�������ڸ�ʽ�趨
DataFormatString="{0:yyyy-MM-dd}"
�����Ҿ���Ӧ����itembound�¼���
e.items.cell["�����"].text=DateTime.Parse(e.items.cell["�����"].text.ToString("yyyy-MM-dd"))
19.��ȡ������Ϣ����ָ��ҳ��
��Ҫʹ��Response.Redirect,��Ӧ��ʹ��Server.Transfer
����e.g

// in global.asax
protected void Application_Error(Object sender, EventArgs e) {
if (Server.GetLastError() is HttpUnhandledException)
Server.Transfer("MyErrorPage.aspx");


//����ķ�HttpUnhandledException�쳣����ASP.NET�Լ������okay�� :)
}
����Redirect�ᵼ��post��back�Ĳ����Ӷ���ʧ�˴�����Ϣ������ҳ�浼��Ӧ��ֱ���ڷ�������ִ�У������Ϳ����ڴ�����ҳ��õ�������Ϣ��������Ӧ�Ĵ��� 
20.���Cookie

Cookie.Expires=[DateTime];
Response.Cookies("UserName").Expires = 0

21.�Զ����쳣����

//�Զ����쳣������ 
using System;
using System.Diagnostics;
namespace MyAppException
{
����/// ��summary��
����/// ��ϵͳ�쳣��ApplicationException�̳е�Ӧ�ó����쳣�����ࡣ
����/// �Զ����쳣���ݼ�¼��Windows NT/2000��Ӧ�ó�����־
����/// ��/summary��
����public class AppException:System.ApplicationException
����{
����public AppException()
����{
����if (ApplicationConfiguration.EventLogEnabled)LogEvent("����һ��δ֪����");
����}
����public AppException(string message)
����{
����LogEvent(message);
����}
����public AppException(string message,Exception innerException)
����{
����LogEvent(message);
����if (innerException != null)
����{
����LogEvent(innerException.Message);
����}
����}
����//��־��¼��
����using System;
����using System.Configuration;
����using System.Diagnostics;
����using System.IO;
����using System.Text;
����using System.Threading;
����namespace MyEventLog
����{
����/// ��summary��
����/// �¼���־��¼�࣬�ṩ�¼���־��¼֧�� 
����/// ��remarks��
����/// ������4����־��¼���� (error, warning, info, trace) 
����/// ��/remarks��
����/// ��/summary��
����public class ApplicationLog
����{
����/// ��summary��
����/// ��������Ϣ��¼��Win2000/NT�¼���־��
����/// ��param name="message"����Ҫ��¼���ı���Ϣ��/param��
����/// ��/summary��
����public static void WriteError(String message)
����{
����WriteLog(TraceLevel.Error, message);
����}
����/// ��summary��
����/// ��������Ϣ��¼��Win2000/NT�¼���־��
����/// ��param name="message"����Ҫ��¼���ı���Ϣ��/param��
����/// ��/summary��
����public static void WriteWarning(String message)
����{
����WriteLog(TraceLevel.Warning, message);����
����}
����/// ��summary��
����/// ����ʾ��Ϣ��¼��Win2000/NT�¼���־��
����/// ��param name="message"����Ҫ��¼���ı���Ϣ��/param��
����/// ��/summary��
����public static void WriteInfo(String message)
����{
����WriteLog(TraceLevel.Info, message);
����}
����/// ��summary��
����/// ��������Ϣ��¼��Win2000/NT�¼���־��
����/// ��param name="message"����Ҫ��¼���ı���Ϣ��/param��
����/// ��/summary��
����public static void WriteTrace(String message)
����{
����WriteLog(TraceLevel.Verbose, message);
����}
����/// ��summary��
����/// ��ʽ����¼���¼���־���ı���Ϣ��ʽ
����/// ��param name="ex"����Ҫ��ʽ�����쳣����/param��
����/// ��param name="catchInfo"���쳣��Ϣ�����ַ���.��/param��
����/// ��retvalue��
����/// ��para����ʽ����쳣��Ϣ�ַ����������쳣���ݺ͸��ٶ�ջ.��/para��
����/// ��/retvalue��
����/// ��/summary��
����public static String FormatException(Exception ex, String catchInfo)
����{
����StringBuilder strBuilder = new StringBuilder();
����if (catchInfo != String.Empty)
����{
����strBuilder.Append(catchInfo).Append("\r\n");
����}
����strBuilder.Append(ex.Message).Append("\r\n").Append(ex.StackTrace);
����return strBuilder.ToString();
����}
����/// ��summary��
����/// ʵ���¼���־д�뷽��
����/// ��param name="level"��Ҫ��¼��Ϣ�ļ���error,warning,info,trace).��/param��
����/// ��param name="messageText"��Ҫ��¼���ı�.��/param��
����/// ��/summary��
����private static void WriteLog(TraceLevel level, String messageText)
����{
����try
����{ 
����EventLogEntryType LogEntryType;
����switch (level)
����{
����case TraceLevel.Error:
����LogEntryType = EventLogEntryType.Error;
����break;
����case TraceLevel.Warning:
����LogEntryType = EventLogEntryType.Warning;
����break;
����case TraceLevel.Info:
����LogEntryType = EventLogEntryType.Information;
����break;
����case TraceLevel.Verbose:
����LogEntryType = EventLogEntryType.SuccessAudit;
����break;
����default:
����LogEntryType = EventLogEntryType.SuccessAudit;
����break;
����}
����EventLog eventLog = new EventLog("Application", ApplicationConfiguration.EventLogMachineName, ApplicationConfiguration.EventLogSourceName );
����//д���¼���־
����eventLog.WriteEntry(messageText, LogEntryType);
����}
����catch {} //�����κ��쳣
����} 
����} //class ApplicationLog
}

22.Panel ��������������Զ���չ

��asp:panel style="overflow-x:scroll;overflow-y:auto;"����/asp:panel��

23.�س�ת����Tab 
(1)


��script language="javascript" for="document" event="onkeydown"��
����if(event.keyCode==13 && event.srcElement.type!=��button�� && event.srcElement.type!=��submit�� && ��������event.srcElement.type!=��reset�� && event.srcElement.type!=����&& event.srcElement.type!=��textarea��); 
����event.keyCode=9;
��/script��

(2)  //������keydown�¼��Ŀؼ����ûس�ʱ����Ϊtab

public void Tab(System.Web .UI.WebControls .WebControl webcontrol) 
{ 
webcontrol.Attributes .Add ("onkeydown", "if(event.keyCode==13) event.keyCode=9"); 
} 
24.DataGrid����������
DataNavigateUrlField="�ֶ���" DataNavigateUrlFormatString="http://xx/inc/delete.aspx?ID={0}"

25.DataGrid��������ɫ

private void DGzf_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
{
����if (e.Item.ItemType!=ListItemType.Header)
����{
����e.Item.Attributes.Add( "onmouseout","this.style.backgroundColor=\""+e.Item.Style["BACKGROUND-COLOR"]+"\"");
����e.Item.Attributes.Add( "onmouseover","this.style.backgroundColor=\""+ "#EFF3F7"+"\"");
����}
}

26.ģ����

��ASP:TEMPLATECOLUMN visible="False" sortexpression="demo" headertext="ID"��
��ITEMTEMPLATE��
��ASP LABEL text=����%# DataBinder.Eval(Container.DataItem, "ArticleID")%���� runat="server" width="80%" id="lblColumn" /��
��/ITEMTEMPLATE��
��/ASP:TEMPLATECOLUMN��
��ASP:TEMPLATECOLUMN headertext="ѡ��"��
��HEADERSTYLE wrap="False" horiz����/HEADERSTYLE��
��ITEMTEMPLATE��
��ASP:CHECKBOX id="chkExport" runat="server" /��
��/ITEMTEMPLATE��
��EDITITEMTEMPLATE��
��ASP:CHECKBOX id="chkExportON" runat="server" enabled="true" /��
��/EDITITEMTEMPLATE��
��/ASP:TEMPLATECOLUMN��

��̨����

protected void CheckAll_CheckedChanged(object sender, System.EventArgs e)
{
����//�ı��е�ѡ����ʵ��ȫѡ��ȫ��ѡ��
����CheckBox chkExport ;
����if( CheckAll.Checked)
����{
����foreach(DataGridItem oDataGridItem in MyDataGrid.Items)
����{
����chkExport = (CheckBox)oDataGridItem.FindControl("chkExport");
����chkExport.Checked = true;
����}
����}
����else
����{
����foreach(DataGridItem oDataGridItem in MyDataGrid.Items)
����{
����chkExport = (CheckBox)oDataGridItem.FindControl("chkExport");
����chkExport.Checked = false;
����}
����}
}

27.���ָ�ʽ��


����%#Container.DataItem("price")%���Ľ����500.0000��������ʽ��Ϊ500.00?��
��%#Container.DataItem("price","{0:��#,##0.00}")%��
int i=123456;
string s=i.ToString("###,###.00");

28.���ڸ�ʽ��
������aspxҳ���ڣ���%# DataBinder.Eval(Container.DataItem,"Company_Ureg_Date")%��
������ʾΪ�� 2004-8-11 19:44:28
������ֻ��Ҫ��2004-8-11 ��
��%# DataBinder.Eval(Container.DataItem,"Company_Ureg_Date","{0:yyyy-M-d}")%��
����Ӧ����θģ�
��������ʽ�����ڡ�
����ȡ����,һ����object((DateTime)objectFromDB).ToString("yyyy-MM-dd");
���������ڵ���֤���ʽ��
����A.������ȷ�������ʽ�� [2004-2-29], [2004-02-29 10:29:39 pm], [2004/12/31] 
^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$
����B.������ȷ�������ʽ��[0001-12-31], [9999 09 30], [2002/03/03] 
^\d{4}[\-\/\s]?((((0[13578])|(1[02]))[\-\/\s]?(([0-2][0-9])|(3[01])))|(((0[469])|(11))[\-\/\s]?(([0-2][0-9])|(30)))|(02[\-\/\s]?[0-2][0-9]))$ 
��������Сдת����
HttpUtility.HtmlEncode(string);
HttpUtility.HtmlDecode(string)
29.����趨ȫ�ֱ���
����Global.asax��
����Application_Start()�¼���
�������Application[������] �� xxx;
�����������ȫ�ֱ���
30.��������HyperLinkColumn���ɵ����Ӻ󣬵�����ӣ����´��ڣ�
����HyperLinkColumn�и�����Target,����ֵ���ó�"_blank"����.(Target="_blank")
������ASPNETMENU������˵�����´���
���������menuData.xml�ļ��Ĳ˵����м���URLTarget="_blank"���磺

��?xml version="1.0" encoding="GB2312"?��
��MenuData ImagesBaseURL="images/"�� 
��MenuGroup��
��MenuItem Label="�ڲ���Ϣ" URL="Infomation.aspx" ��
��MenuGroup ID="BBC"��
��MenuItem Label="������Ϣ" URL="Infomation.aspx" URLTarget="_blank" LeftIcon="file.gif"/��
��MenuItem Label="������Ϣ��" URL="NewInfo.aspx" LeftIcon="file.gif" /��

��ý����aspnetmenu������1.2��
31.��ȡDataGrid�ؼ�TextBoxֵ

foreach(DataGrid dgi in yourDataGrid.Items)
{
����TextBox tb = (TextBox)dgi.FindControl("yourTextBoxId");
����tb.Text....
}

33.��DataGrid����3��ģ���а���Textbox�ֱ�Ϊ DG_ShuLiang (����) DG_DanJian(����) DG_JinE(���)�ֱ���5.6.7�У�Ҫ����¼�����������۵�ʱ���Զ������:����*����=��Ҫ��¼��ʱ����Ϊ ��ֵ��.������ÿͻ��˽ű�ʵ���������?

��asp:TemplateColumn HeaderText="����"�� 
��ItemTemplate��
��asp:TextBox id="ShuLiang" runat=��server�� Text=����%# DataBinder.Eval(Container.DataItem,"DG_ShuLiang")%���� 

/��
��asp:RegularExpressionValidator id="revS" runat="server" C ErrorMessage="must be integer" Validati /��
��/ItemTemplate��
��/asp:TemplateColumn��
��asp:TemplateColumn HeaderText="����"�� 
��ItemTemplate��
��asp:TextBox id="DanJian" runat=��server�� Text=����%# DataBinder.Eval(Container.DataItem,"DG_DanJian")%���� 

/��
��asp:RegularExpressionValidator id="revS2" runat="server" C ErrorMessage="must be numeric" Validati /��
��/ItemTemplate��
��/asp:TemplateColumn��
��asp:TemplateColumn HeaderText="���"�� 
��ItemTemplate��
��asp:TextBox id="JinE" runat=��server�� Text=����%# DataBinder.Eval(Container.DataItem,"DG_JinE")%���� /��
��/ItemTemplate��
��/asp:TemplateColumn����script language="javascript"��
function DoCal()
{
����var e = event.srcElement;
����var row = e.parentNode.parentNode;
����var txts = row.all.tags("INPUT");
����if (!txts.length || txts.length �� 3)
����return;
����var q = txts[txts.length-3].value;
����var p = txts[txts.length-2].value;
����if (isNaN(q) || isNaN(p))
����return;
����q = parseInt(q);
����p = parseFloat(p);
����txts[txts.length-1].value = (q * p).toFixed(2);
}
��/script��

34.datagridѡ���Ƚϵ��µ���ʱ��Ϊʲô����ˢ��һ�£�Ȼ��͹������������棬�ղ�ѡ����������Ļ�Ĺ�ϵ�Ϳ������ˡ�
page_load 
page.smartNavigation=true
35.��Datagrid���޸����ݣ�������༭��ʱ�����ݳ������ı����У���ô�����ı���Ĵ�С ?

private void DataGrid1_ItemDataBound(obj sender,DataGridItemEventArgs e)
{
����for(int i=0;i��e.Item.Cells.Count-1;i++)
����if(e.Item.ItemType==ListItemType.EditType)
����{
����e.Item.Cells.Attributes.Add("Width", "80px")
����} 
}

36.�Ի���

private static string ScriptBegin = "��script language=\"JavaScript\"��";
private static string ScriptEnd = "��/script��";
public static void ConfirmMessageBox(string PageTarget,string Content)
{
����string C+Content+"��);"+"if(retValue){window.location=��"+PageTarget+"��;}";
����ConfirmContent=ScriptBegin + ConfirmContent + ScriptEnd;
����Page ParameterPage = (Page)System.Web.HttpContext.Current.Handler;
����ParameterPage.RegisterStartupScript("confirm",ConfirmContent);
����//Response.Write


(strScript);
}
37. ��ʱ���ʽ����string aa=DateTime.Now.ToString("yyyy��MM��dd��"); 
����1.1 ȡ��ǰ������ʱ���� 
currentTime=System.DateTime.Now;
����1.2 ȡ��ǰ�� 
int ��= DateTime.Now.Year;
����1.3 ȡ��ǰ�� 
int ��= DateTime.Now.Month; 
����1.4 ȡ��ǰ�� 
int ��= DateTime.Now.Day; 
����1.5 ȡ��ǰʱ 
int ʱ= DateTime.Now.Hour; 
����1.6 ȡ��ǰ�� 
int ��= DateTime.Now.Minute; 
����1.7 ȡ��ǰ�� 
int ��= DateTime.Now.Second; 
����1.8 ȡ��ǰ���� 
int ����= DateTime.Now.Millisecond; 
38���Զ����ҳ���룺
�����ȶ������ ��

 

public static int pageCount; //��ҳ���� 
public static int curPageIndex=1; //��ǰҳ�� 
������һҳ�� 
if(DataGrid1.CurrentPageIndex �� (DataGrid1.PageCount - 1)) 
{ 
����DataGrid1.CurrentPageIndex += 1; 
����curPageIndex+=1; 
} 
bind(); // DataGrid1���ݰ󶨺��� 
������һҳ�� 
if(DataGrid1.CurrentPageIndex ��0) 
{ 
����DataGrid1.CurrentPageIndex += 1; 
����curPageIndex-=1; 
} 
bind(); // DataGrid1���ݰ󶨺��� 
����ֱ��ҳ����ת�� 
int a=int.Parse(JumpPage.Value.Trim());//JumpPage.Value.Trim()Ϊ��תֵ 
if(a��DataGrid1.PageCount) 
{ 
����this.DataGrid1.CurrentPageIndex=a; 
} 
bind(); 

39��DataGridʹ�ã� 
�������ɾ��ȷ�ϣ� 

 

private void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) 
{ 
����foreach(DataGridItem di in this.DataGrid1.Items) 
����{ 
����if(di.ItemType==ListItemType.Item||di.ItemType==ListItemType.AlternatingItem) 
����{ 
����((LinkButton)di.Cells[8].Controls[0]).Attributes.Add("onclick","return confirm(��ȷ��ɾ��������?��);"); 
����} 
����} 
} 

������ʽ���棺 

ListItemType itemType = e.Item.ItemType; 
if (itemType == ListItemType.Item ) 
{ 
����e.Item.Attributes["onmouseout"] = "javascript:this.style.backgroundColor=��#FFFFFF��;"; 
����e.Item.Attributes["onmouseover"] = "javascript:this.style.backgroundColor=��#d9ece1��;cursor=��hand��;" ; 
} 
else if( itemType == ListItemType.AlternatingItem) 
{ 
����e.Item.Attributes["onmouseout"] = "javascript:this.style.backgroundColor=��#a0d7c4��;"; 
����e.Item.Attributes["onmouseover"] = "javascript:this.style.backgroundColor=��#d9ece1��;cursor=��hand��;" ; 
} 

�������һ������У� 

DataTable dt= c.ExecuteRtnTableForAccess(sqltxt); //ִ��sql���ص�DataTable 
DataColumn dc=dt.Columns.Add("number",System.Type.GetType("System.String")); 
for(int i=0;i��dt.Rows.Count;i++) 
{ 
����dt.Rows["number"]=(i+1).ToString(); 
} 
DataGrid1.DataSource=dt; 
DataGrid1.DataBind(); 
����DataGrid1�����һ��CheckBox��ҳ�������һ��ȫѡ�� 
private void CheckBox2_CheckedChanged(object sender, System.EventArgs e) 
{ 
����foreach(DataGridItem thisitem in DataGrid1.Items) 
����{ 
����((CheckBox)thisitem.Cells[0].Controls[1]).Checked=CheckBox2.Checked; 
����} 
} 

[i]��������ǰҳ����DataGrid1��ʾ������ȫ��ɾ�� 
[/i]

foreach(DataGridItem thisitem in DataGrid1.Items) 
{ 
����if(((CheckBox)thisitem.Cells[0].Controls[1]).Checked) 
����{ 
����string strloginid= DataGrid1.DataKeys[thisitem.ItemIndex].ToString(); 
����Del (strloginid); //ɾ������ 
����} 
} 


[i]40�����ļ��ڲ�ͬĿ¼�£���Ҫ��ȡ���ݿ������ַ�������������ַ�������Web.config��Ȼ����Global.asax�г�ʼ���� 
������Application_Start��������´��룺 
[/i]

Application["ConnStr"]=this.Context.Request.PhysicalApplicationPath+ConfigurationSettings.
����AppSettings["ConnStr"].ToString();

[i]3[/i]
[i]41�� ����.ToString() 
�����ַ���ת�� תΪ�ַ��� 
[/i]

12345.ToString("n"); //���� 12,345.00 
12345.ToString("C"); //���� ��12,345.00 
12345.ToString("e"); //���� 1.234500e+004 
12345.ToString("f4"); //���� 12345.0000 
12345.ToString("x"); //���� 3039 (16����) 
12345.ToString("p"); //���� 1,234,500.00% 

[i]42������.Substring(����1,����2); 
������ȡ�ִ���һ���֣�����1Ϊ����ʼλ��������2Ϊ��ȡ��λ�� �磺string s1 = str.Substring(0,2); 
43�����Լ�����վ�ϵ�½������վ��(������ҳ����ͨ��Ƕ�׷�ʽ�Ļ�����Ϊһ��ҳ��ֻ����һ��FORM����ʱ���Ե�������һ��ҳ�����ύ��½��Ϣ) 
[/i]

��SCRIPT language="javascript"�� 
��!-- 
����function gook(pws) 
����{ 
����frm.submit(); 
����} 
//--�� 
��/SCRIPT�� ��body leftMargin="0" topMargin="0"  marginwidth="0" marginheight="0"�� 
��form name="frm" action=" http://www.51aspx.com " method="post"�� 
��tr�� 
��td��
��input id="f_user" type="hidden" size="1" name="f_user" runat="server"��
��input id="f_domain" type="hidden" size="1" name="f_domain" runat="server"��
��input class="box" id="f_pass" type="hidden" size="1" name="pwshow" runat="server"�� 
��INPUT id="lng" type="hidden" maxLength="20" size="1" value="5" name="lng"��
��INPUT id="tem" type="hidden" size="1" value="2" name="tem"�� 
��/td�� 
��/tr�� 
��/form�� 

[i]�����ı�������Ʊ�������Ҫ��½����ҳ�ϵ����ƣ����Դ�벻�п�����vsniffer ������ 
���������ǻ�ȡ�û�����ĵ�½��Ϣ�Ĵ��룺 
[/i]

string name; 
name=Request.QueryString["EmailName"]; 
try 
{ 
����int a=name.IndexOf("@",0,name.Length); 
����f_user.Value=name.Substring(0,a); 
����f_domain.Value=name.Substring(a+1,name.Length-(a+1)); 
����f_pass.Value=Request.QueryString["Psw"]; 
} 
catch 
{ 
����Script.Alert("���������!"); 
����Server.Transfer("index.aspx"); 
} 


[i]44.datagrid��ҳ�����ɾ��ʱ���ֳ������� 
[/i]

public void jumppage(System.Web.UI.WebControls.DataGrid dg) 
{ 
int int_PageLess; //����ҳ����ת��ҳ�� 
//�����ǰҳ�����һҳ 
if(dg.CurrentPageIndex == dg.PageCount-1) 
{ 
//�����ֻ��һҳ 
if(dg.CurrentPageIndex == 0) 
{ 
//ɾ����ҳ��ͣ�ڵ�ǰҳ 
dg.CurrentPageIndex = dg.PageCount-1; 
} 
else 
{ 
//������һҳֻ��һ����¼ 
if((dg.Items.Count % dg.PageSize == 1) || dg.PageSize == 1) 
{ 
//�����һҳ���һ����¼ɾ����ҳ��Ӧ��ת��ǰһҳ 
int_PageLess = 2; 
} 
else //������һҳ�ļ�¼������1����ô�����һҳɾ����¼����Ȼͣ�ڵ�ǰҳ 
{ 
int_PageLess = 1; 
} 
dg.CurrentPageIndex = dg.PageCount - int_PageLess; 
} 
} 
} 


[i]45.���洰�� 
/**//// <summary> 
/// �������˵���alert�Ի��� 
/// </summary> 
/// <param name="str_Message">��ʾ��Ϣ,���ӣ�"����Ϊ��!"</param> 
/// <param name="page">Page��</param> 
public void Alert(string str_Message,Page page) 
{ 
page.RegisterStartupScript("","<script>alert('"+str_Message+"');</script>"); 
} 
36.���ش˾��洰��,ʹĳ�ؼ���ý���
[/i]

/**//// <summary> 
/// �������˵���alert�Ի��򣬲�ʹ�ؼ���ý��� 
/// </summary> 
/// <param name="str_Ctl_Name">��ý���ؼ�Idֵ,���磺txt_Name</param> 
/// <param name="str_Message">��ʾ��Ϣ,���ӣ�"������������!"</param> 
/// <param name="page">Page��</param> 
public void Alert(string str_Ctl_Name,string str_Message,Page page) 
{ 
page.RegisterStartupScript("","<script>alert('"+str_Message+"');document.forms(0)."+str_Ctl_Name+".focus(); document.forms(0)."+str_Ctl_Name+".select();</script>"); 
} 


[i]47.ȷ�϶Ի��� 
[/i]

/**//// <summary> 
/// �������˵���confirm�Ի��� 
/// </summary> 
/// <param name="str_Message">��ʾ��Ϣ,���ӣ�"���Ƿ�ȷ��ɾ��!"</param> 
/// <param name="btn">����Botton��ťIdֵ,���磺btn_Flow</param> 
/// <param name="page">Page��</param> 
public void Confirm(string str_Message,string btn,Page page) 
{ 
page.RegisterStartupScript("","<script> if (confirm('"+str_Message+"')==true){document.forms(0)."+btn+".click();}</script>"); 
} 


[i]48.����ȷ�϶Ի��򣬵��ȷ������һ�����ذ�ť�¼������ȡ������һ�����ذ�ť�¼�
[/i]

/**//// <summary> 
/// �������˵���confirm�Ի���,ѯ���û�׼��ת����Щ������������ȷ�����͡�ȡ����ʱ�Ĳ��� 
/// </summary> 
/// <param name="str_Message">��ʾ��Ϣ�����磺"�ɹ���������,����\"ȷ��\"��ť��д����,����\"ȡ��\"�޸�����"</param> 
/// <param name="btn_Redirect_Flow">"ȷ��"��ťidֵ</param> 
/// <param name="btn_Redirect_Self">"ȡ��"��ťidֵ</param> 
/// <param name="page">Page��</param> 
public void Confirm(string str_Message,string btn_Redirect_Flow,string btn_Redirect_Self,Page page) 
{ 
page.RegisterStartupScript("","<script> if (confirm('"+str_Message+"')==true){document.forms(0)."+btn_Redirect_Flow+".click();}else{document.forms(0)."+btn_Redirect_Self+".click();}</script>"); 
} 

[i]49.��ý��� 
[/i]

/**//// <summary> 
/// ʹ�ؼ���ý��� 
/// </summary> 
/// <param name="str_Ctl_Name">��ý���ؼ�Idֵ,���磺txt_Name</param> 
/// <param name="page">Page��</param> 
public void GetFocus(string str_Ctl_Name,Page page) 
{ 
page.RegisterStartupScript("","<script>document.forms(0)."+str_Ctl_Name+".focus(); document.forms(0)."+str_Ctl_Name+".select();</script>"); 
} 


[i]50.�Ӵ��巵��������
/[/i]

**////<summary> 
///���ƣ�redirect 
///���ܣ��Ӵ��巵�������� 
///������url 
///����ֵ���� 
///</summary> 
public void redirect(string url,Page page) 
{ 
if ( Session["IfDefault"]!=(object)"Default") 
{ 
page.RegisterStartupScript("","<script>window.top.document.location.href='"+url+"';</script>"); 
} 
} 


[i]
51.�ж��Ƿ�Ϊ���� 
[/i]

/**//// <summary> 
/// ���ƣ�IsNumberic 
/// ���ܣ��ж�������Ƿ������� 
/// ������string oText��Դ�ı� 
/// ����ֵ����bool true:�ǡ�false:�� 
/// </summary> 
public bool IsNumberic(string oText) 
{ 
try 
{ 
int var1=Convert.ToInt32 (oText); 
return true; 
} 
catch 
{ 
return false; 
} 
} 


[i]����ַ���ʵ�ʳ��ȣ����������ַ��� 
[/i]

//����ַ���oString��ʵ�ʳ��� 
public int StringLength(string oString) 
{ 
byte[] strArray=System.Text .Encoding.Default .GetBytes (oString); 
int res=strArray.Length ; 
return res; 
} 
 
 
    
 
   

