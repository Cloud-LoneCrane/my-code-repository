//
// Created by SharpDevelop.
// User: jiftle
// Date: 2010-4-9
// Time: 15:51

//导入操作数据库需要的命名空间
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

//自定义命名空间DBConfig
namespace DBConfig
{
	public class DBConnection
	{
		//声明一个受到保护变量存储连接数据库的信息
		public string ConnStr;

		//声明用于数据库连接的公共成员
			//默认是Object类型
		public  conn;


		/// <summary>
		/// 连接数据库
		/// </summary>
		public bool Open()
		{

			string strErr = Constants.vbNullString;

			//判断连接字符串是否为空
			if (ConnStr == null | string.IsNullOrEmpty(ConnStr)) {
				strErr = "Connection String can't be Empty!";
				ErrorHandler(strErr);
				return false;
			}

			//实例化Connection类
			if (g_DataAccessType == enmDataAccessType.DB_OLEDB) {
				conn = new OleDbConnection(ConnStr);
			} else if (g_DataAccessType == enmDataAccessType.DB_ODBC) {
				conn = new OleDbConnection(ConnStr);
			} else if (g_DataAccessType == enmDataAccessType.DB_SQL) {
				conn = new SqlConnection(ConnStr);
			} else if (g_DataAccessType == enmDataAccessType.DB_MYSQL) {
				conn = new MySqlConnection(ConnStr);
			} else {
				conn = new MySqlConnection(ConnStr);
			}



			try {
				//打开数据库
				conn.Open();
			} catch (OleDb.OleDbException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return false;
			} catch (Odbc.OdbcException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return false;
			} catch (SqlException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return false;
			} catch (MySqlException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return false;
			} catch (SystemException ex) {
				strErr = Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return false;
			}

			return true;
		}

		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		/// <remarks></remarks>

		public void Close()
		{
			if ((conn != null)) {
				//关闭连接
				conn.Close();
				conn = null;
			}
		}


		public DBConnection()
		{
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}

