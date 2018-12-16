ASP.net连接access数据库例程
作者:   文章来源:   点击数:4188   更新时间:2008-08-05 

注意：要运行上述ACCESS例程请先下载数据库, 与例程放在同一目录下。
<%@ Import Namespace="System.Data" %>
<%@ Import NameSpace="System.Data.OleDb" %>
<script laguage="VB" runat="server">
Dim myConnection As OleDbConnection 
Dim myCommand As OleDbCommand
sub page_load(sender as Object,e as EventArgs) 

'1.连接数据库
dim dbname as string
dbname=server.mappath("authors.mdb")
myConnection = New OleDbConnection( "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA Source="&dbname )
myConnection.Open()
la1.text="Connection Opened!"

'2.添加记录
myCommand = New OleDbCommand( "Insert INTO Authors(Authors,country) Values('Simson','usa')", myConnection )
myCommand.ExecuteNonQuery()
la2.text="New Record Inserted!"

'3 更新数据(Access) 
myCommand = New OleDbCommand( "UPDATE Authors SET Authors='Bennett' WHERE Authors = 'Simson'", myConnection )
myCommand.ExecuteNonQuery()
la3.text="Record Updated!"

'4 删除数据（access） 
myCommand = New OleDbCommand( "DELETE FROM Authors WHERE Authors = 'David'", myConnection )
myCommand.ExecuteNonQuery()
la4.text="Record Deleted!"

'5 使用DateGrid显示数据 
myCommand = New OleDbCommand( "select * FROM Authors", myConnection )
MyDataGrid.DataSource=myCommand.Executereader()
MyDataGrid.DataBind()

end sub 
</script>
<html>
<body>
<asp:label id="la1" runat="server" /><br>
<asp:label id="la2" runat="server" /><br>
<asp:label id="la3" runat="server" /><br>
<asp:label id="la4" runat="server" /><br>
<ASP:DataGrid id="MyDataGrid" runat="server"
BorderColor="black"
BorderWidth="1"
GridLines="Both"
CellPadding="3"
CellSpacing="0"
Font-Name="Verdana"
Font-Size="10pt"
HeaderStyle-BackColor="#aaaadd"
AlternatingItemStyle-BackColor="#eeeeee"
> 
</asp:DataGrid>

</body>
</html>