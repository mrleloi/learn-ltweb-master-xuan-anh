using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
namespace Lib.SqlServer
{
	public class DBAccess
	{
		private string _ConnectionString;
		//---------------------------------------------------------------------------------------------------
		public DBAccess(string connStr)
		{
			ConnectionString = connStr;
		}
		//---------------------------------------------------------------------------------------------------
		public string ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
		//---------------------------------------------------------------------------------------------------
		public bool CheckConnection(string connStr)
		{
			bool retVal = false;
			try
			{
				SqlConnection mySqlConnection = new SqlConnection(connStr);
				mySqlConnection.Open();
				if (mySqlConnection.State == ConnectionState.Open)
				{
					mySqlConnection.Close();
					retVal = true;
				}
			}
			catch
			{
				retVal = false;
			}
			return retVal;
		}		
		//---------------------------------------------------------------------------------------------------
		public SqlConnection getConnection()
		{
			try
			{
				return new SqlConnection(ConnectionString);
			}
			catch (SqlException sqlEx)
			{
				throw new ApplicationException("Connecting Failed " + sqlEx.Message);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public OleDbConnection getOLEConnection()
		{
			try
			{
				return new OleDbConnection(ConnectionString);
			}
			catch (SqlException sqlEx)
			{
				throw new ApplicationException("Connecting to database failed: " + sqlEx.Message);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public void closeConnection(SqlConnection con)
		{
			try
			{
				if (con != null || con.State == ConnectionState.Open)
				{
					con.Close();
					con.Dispose();
				}
			}
			catch (SqlException sqlEx)
			{
				throw sqlEx;
			}
		}
		//---------------------------------------------------------------------------------------------------        #region Management Methods
		public SqlConnection OpenConnection(string ConnectionString)
		{
			try
			{
				SqlConnection mySqlConnection = new SqlConnection(ConnectionString);
				mySqlConnection.Open();
				return mySqlConnection;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public void CloseConnection(SqlConnection mySqlConnection)
		{
			try
			{
				if (mySqlConnection.State == ConnectionState.Open)
				{
					mySqlConnection.Close();
					mySqlConnection.Dispose();
				}
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public DataSet getDataSet(string strSQL)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				SqlDataAdapter myDataAdapter = new SqlDataAdapter(strSQL, myConn);
				DataSet myDataSet = new DataSet();
				myDataAdapter.Fill(myDataSet);
				myDataAdapter.Dispose();
				myBase.CloseConnection(myConn);
				return myDataSet;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public SqlDataReader getReader(SqlCommand cmd)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				cmd.Connection = myConn;
				SqlDataReader reader = cmd.ExecuteReader();
				return reader;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + cmd.CommandText));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public SqlDataReader getReader(SqlCommand cmd, SqlConnection con)
		{
			try
			{
				cmd.Connection = con;
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				return reader;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + cmd.CommandText));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public DataTable getDataTable(string strSQL)
		{
			return getDataSet(strSQL).Tables[0];
		}
		//---------------------------------------------------------------------------------------------------
		public DataSet getDataSet(string strSQL, SqlParameter[] Parameters)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				SqlCommand myCommand = new SqlCommand(strSQL, myConn);
				myCommand.Parameters.Clear();
				foreach (SqlParameter par in Parameters)
				{
					myCommand.Parameters.Add(par);
				}
				SqlDataAdapter myDataAdapter = new SqlDataAdapter(myCommand);
				DataSet myDataSet = new DataSet();
				myDataAdapter.Fill(myDataSet);
				myBase.CloseConnection(myConn);
				return myDataSet;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public DataTable getDataTable(string strSQL, params SqlParameter[] Parameters)
		{
			return getDataSet(strSQL, Parameters).Tables[0];
		}
		//---------------------------------------------------------------------------------------------------
		public DataSet getDataSet(SqlCommand mCommand)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				mCommand.Connection = myConn;
				SqlDataAdapter myDataAdapter = new SqlDataAdapter(mCommand);
				DataSet myDataSet = new DataSet();
				myDataAdapter.Fill(myDataSet);
				myBase.CloseConnection(myConn);
				return myDataSet;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + mCommand.CommandText));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public DataTable getDataTable(SqlCommand mCommand)
		{
			return getDataSet(mCommand).Tables[0];
		}
		//---------------------------------------------------------------------------------------------------
		public void ExecuteSQL(string strSQL)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			SqlConnection myConn = myBase.OpenConnection(ConnectionString);
			SqlTransaction tran = myConn.BeginTransaction();
			try
			{
				SqlCommand myCommand = new SqlCommand(strSQL, myConn);
				myCommand.Transaction = tran;
				myCommand.ExecuteNonQuery();
				tran.Commit();
				myCommand.Dispose();
			}
			catch (Exception myException)
			{
				tran.Rollback();
				throw (new Exception(myException.Message + " => " + strSQL));
			}
			finally
			{
				myBase.CloseConnection(myConn);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public void ExecuteSQL(string strSQL, params SqlParameter[] Parameters)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				SqlCommand myCommand = new SqlCommand(strSQL, myConn);
				myCommand.Parameters.Clear();
				foreach (SqlParameter par in Parameters)
				{
					myCommand.Parameters.Add(par);
				}
				myCommand.ExecuteNonQuery();
				myCommand.Dispose();
				myBase.CloseConnection(myConn);
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}

		}
		//---------------------------------------------------------------------------------------------------
		public void ExecuteSQL(string strSQL, IList paraList)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				SqlCommand myCommand = new SqlCommand(strSQL, myConn);
				myCommand.Parameters.Clear();
				foreach (SqlParameter par in paraList)
				{
					myCommand.Parameters.Add(par);
				}
				myCommand.ExecuteNonQuery();

				myCommand.Dispose();
				myBase.CloseConnection(myConn);
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}

		}
		//---------------------------------------------------------------------------------------------------
		public void ExecuteSQL(SqlCommand mCommand)
		{
			DBAccess myBase = new DBAccess(ConnectionString);
			SqlConnection myConn = myBase.OpenConnection(ConnectionString);
			try
			{
				mCommand.Connection = myConn;
				mCommand.ExecuteNonQuery();
				mCommand.Dispose();
			}
			catch (Exception myException)
			{
				mCommand.Dispose();
				throw (new Exception(myException.Message + " => " + mCommand.CommandText));
			}
			finally
			{
				myBase.CloseConnection(myConn);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public void ExecuteSQL(string strSQL, SqlConnection mConn)
		{
			try
			{
				SqlCommand myCom = new SqlCommand(strSQL, mConn);
				myCom.ExecuteNonQuery();
				myCom.Dispose();
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public int ExecuteScalar(string strSQL)
		{
			int temp = 0;
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				SqlCommand myCommand = new SqlCommand(strSQL, myConn);
				temp = Convert.ToInt32(myCommand.ExecuteScalar().ToString());

				myCommand.Dispose();
				myBase.CloseConnection(myConn);

				return temp;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}

		}
		//---------------------------------------------------------------------------------------------------
		public int ExecuteScalar(string strSQL, params SqlParameter[] Parameters)
		{
			int temp = 0;
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				SqlCommand myCommand = new SqlCommand(strSQL, myConn);
				myCommand.Parameters.Clear();
				foreach (SqlParameter par in Parameters)
				{
					myCommand.Parameters.Add(par);
				}

				temp = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
				myCommand.Dispose();
				myBase.CloseConnection(myConn);
				return temp;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public int ExecuteScalar(string strSQL, IList paraList)
		{
			int temp = 0;
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				SqlCommand myCommand = new SqlCommand(strSQL, myConn);
				myCommand.Parameters.Clear();
				foreach (SqlParameter par in paraList)
				{
					myCommand.Parameters.Add(par);
				}
				temp = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
				myCommand.Dispose();
				myBase.CloseConnection(myConn);
				return temp;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public int ExecuteScalar(SqlCommand mCommand)
		{
			int temp = 0;
			DBAccess myBase = new DBAccess(ConnectionString);
			try
			{
				SqlConnection myConn = myBase.OpenConnection(ConnectionString);
				mCommand.Connection = myConn;
				temp = Convert.ToInt32(mCommand.ExecuteScalar().ToString());
				mCommand.Dispose();
				myBase.CloseConnection(myConn);
				return temp;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + mCommand.CommandText));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public int ExecuteScalar(string strSQL, SqlConnection mConn)
		{
			int temp = 0;
			try
			{
				SqlCommand myCom = new SqlCommand(strSQL, mConn);
				temp = Convert.ToInt32(myCom.ExecuteScalar().ToString());
				myCom.Dispose();
				return temp;
			}
			catch (Exception myException)
			{
				throw (new Exception(myException.Message + " => " + strSQL));
			}
		}
		//---------------------------------------------------------------------------------------------------
		public DataTable SelectDBRows(string query)
		{
			try
			{
				SqlConnection sqlcon = new SqlConnection(ConnectionString);
				DataTable dt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter();
				sqlcon.Open();
				da.SelectCommand = new SqlCommand(query, sqlcon);
				da.Fill(dt);
				sqlcon.Close();
				da.Dispose();
				sqlcon.Dispose();
				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public long GetNumber(string Query)
		{
			try
			{
				Object num;
				SqlConnection sqlcon = new SqlConnection(ConnectionString);
				SqlCommand Com = new SqlCommand(Query, sqlcon);
				sqlcon.Open();
				num = (long)Com.ExecuteScalar();
				sqlcon.Close();
				sqlcon.Dispose();
				Com.Dispose();
				return (long)num;
			}
			catch
			{
				return 0;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public string GetString(string Query)
		{
			try
			{
				Object num;
				SqlConnection sqlcon = new SqlConnection(ConnectionString);
				SqlCommand Com = new SqlCommand(Query, sqlcon);
				sqlcon.Open();
				num = (string)Com.ExecuteScalar();
				sqlcon.Close();
				sqlcon.Dispose();
				Com.Dispose();
				return (string)num;
			}
			catch
			{
				return "";
			}
		}
	}
}