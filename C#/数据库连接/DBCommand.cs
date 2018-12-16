//
// Created by SharpDevelop.
// User: jiftle
// Date: 2010-4-9
// Time: 15:51
// 
//导入操作数据库需要的命名空间
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

//自定义命名空间DBConfig
namespace DBConfig
{
	public class DBCommand : DBConnection
	{

		/// <summary>
		/// 插入
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns>受影响的行数 返回 -1 表示执行失败</returns>
		/// <remarks></remarks>
		public int ExecuteSQL(string strSQL)
		{
			string strErr = null;
			object cmd = null;

			//创建SqlCommand实例            
			switch (g_DataAccessType) {
				case enmDataAccessType.DB_OLEDB:
					cmd = new OleDbCommand(strSQL, conn);
					break;
				case enmDataAccessType.DB_ODBC:
					cmd = new OdbcCommand(strSQL, conn);
					break;
				case enmDataAccessType.DB_SQL:
					cmd = new SqlCommand(strSQL, conn);
					break;
				case enmDataAccessType.DB_MYSQL:
					cmd = new MySqlCommand(strSQL, conn);
					break;
				default:
					cmd = new MySqlCommand(strSQL, conn);
					break;
			}

			//count 表示受影响的行数，初始化为0 
			int count = 0;

			try {
				//执行SQL命令
				count = cmd.ExecuteNonQuery();

			} catch (OleDb.OleDbException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return -1;
			} catch (Odbc.OdbcException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return -1;
			} catch (SqlException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return -1;
			} catch (MySqlException ex) {
				strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return -1;
			} catch (SystemException ex) {
				strErr = Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);
				return -1;
			}

			return count;
		}


		/// <summary>
		/// 根据参数中sql语句，创建一个DataTable
		/// </summary>
		/// <param name="strSQL">sql语句eg."select * from zhTmp"</param>
		/// <returns>返回DataTable类型变量 </returns>
		public DataTable CreateDataTable(string strSQL)
		{

			object daTmp = null;
			DataTable dtblTmp = new DataTable();


			try {
				switch (g_DataAccessType) {
					case enmDataAccessType.DB_OLEDB:
						OleDbConnection connTmp = null;
						connTmp = new OleDbConnection();
						connTmp = conn;
						daTmp = new OleDbDataAdapter(strSQL, connTmp);

						break;
					case enmDataAccessType.DB_ODBC:
						OdbcConnection connTmp = null;
						connTmp = new OdbcConnection();
						connTmp = conn;
						daTmp = new OdbcDataAdapter();

						break;
					case enmDataAccessType.DB_SQL:
						SqlConnection connTmp = null;
						connTmp = new SqlConnection();
						connTmp = conn;
						daTmp = new SqlDataAdapter(strSQL, connTmp);

						break;
					case enmDataAccessType.DB_MYSQL:
						MySqlConnection connTmp = default(MySqlConnection);
						connTmp = new MySqlConnection();
						connTmp = conn;
						daTmp = new MySqlDataAdapter(strSQL, connTmp);

						break;
					default:
						MySqlConnection connTmp = default(MySqlConnection);
						connTmp = new MySqlConnection();
						connTmp = conn;
						daTmp = new MySqlDataAdapter(strSQL, connTmp);

						break;
				}


				try {
					daTmp.Fill(dtblTmp);

				} catch (OleDb.OleDbException ex) {
					string strErr = "";
					strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
					ErrorHandler(strErr);
					return null;
				} catch (Odbc.OdbcException ex) {
					string strErr = "";
					strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
					ErrorHandler(strErr);
					return null;
				} catch (SqlException ex) {
					string strErr = "";
					strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
					ErrorHandler(strErr);
					return null;
				} catch (MySqlException ex) {
					string strErr = "";
					strErr = ex.ErrorCode + Strings.Chr(32) + ex.Message;
					ErrorHandler(strErr);
					return null;
				} catch (MySql.Data.Types.MySqlConversionException ex) {
					string strErr = "";
					strErr = Strings.Chr(32) + ex.Message;
					ErrorHandler(strErr);
					return null;
				} catch (System.StackOverflowException ex) {
					string strErr = "";
					strErr = Strings.Chr(32) + ex.Message;
					ErrorHandler(strErr);
					return null;
				} catch (SystemException ex) {
					string strErr = "";
					strErr = Strings.Chr(32) + ex.Message;
					ErrorHandler(strErr);
					return null;
				}

				//返回一个DataTable(可以编辑的)
				return dtblTmp;

			} catch (Exception ex) {
				string strErr = "";
				strErr = Strings.Chr(32) + ex.Message;
				ErrorHandler(strErr);

				return null;
			}


			return dtblTmp;
		}



	}
}
