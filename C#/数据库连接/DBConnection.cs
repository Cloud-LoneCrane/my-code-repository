//
// Created by SharpDevelop.
// User: jiftle
// Date: 2010-4-9
// Time: 15:51

//����������ݿ���Ҫ�������ռ�
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

//�Զ��������ռ�DBConfig
namespace DBConfig
{
	public class DBConnection
	{
		//����һ���ܵ����������洢�������ݿ����Ϣ
		public string ConnStr;

		//�����������ݿ����ӵĹ�����Ա
			//Ĭ����Object����
		public  conn;


		/// <summary>
		/// �������ݿ�
		/// </summary>
		public bool Open()
		{

			string strErr = Constants.vbNullString;

			//�ж������ַ����Ƿ�Ϊ��
			if (ConnStr == null | string.IsNullOrEmpty(ConnStr)) {
				strErr = "Connection String can't be Empty!";
				ErrorHandler(strErr);
				return false;
			}

			//ʵ����Connection��
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
				//�����ݿ�
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
		/// �ر����ݿ�����
		/// </summary>
		/// <remarks></remarks>

		public void Close()
		{
			if ((conn != null)) {
				//�ر�����
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

