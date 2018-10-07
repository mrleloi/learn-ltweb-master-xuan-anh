using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Reflection;
using Lib.SqlServer;
using Lib.Utilities;
namespace Lib.Web
{
	public class CategoryStatus
	{
		private byte _CategoryStatusId;
		private string _CategoryStatusName;
		private string _CategoryStatusDesc;
		private DBAccess db;
		//---------------------------------------------------------------------------------------------------
		public byte CategoryStatusId { get { return _CategoryStatusId; } set { _CategoryStatusId = value; } }
		public string CategoryStatusName { get { return _CategoryStatusName; } set { _CategoryStatusName = value; } }
		public string CategoryStatusDesc { get { return _CategoryStatusDesc; } set { _CategoryStatusDesc = value; } }
		//-------------------------------------------------------------------------------------
		public CategoryStatus(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~CategoryStatus()
		{
		}
		//-------------------------------------------------------------------------------------
		public virtual void Dispose()
		{
		}
		//-----------------------------------------------------------------------
		private List<CategoryStatus> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<CategoryStatus> l_CategoryStatus = new List<CategoryStatus>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					CategoryStatus m_CategoryStatus = new CategoryStatus(db.ConnectionString);
					m_CategoryStatus.CategoryStatusId = smartReader.GetByte("CategoryStatusId");
					m_CategoryStatus.CategoryStatusName = smartReader.GetString("CategoryStatusName");
					m_CategoryStatus.CategoryStatusDesc = smartReader.GetString("CategoryStatusDesc");
					l_CategoryStatus.Add(m_CategoryStatus);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_CategoryStatus;
		}
		//-----------------------------------------------------------------------
		public List<CategoryStatus> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<CategoryStatus> RetVal = new List<CategoryStatus>();
			try
			{
				string Sql = "SELECT * FROM V$CategoryStatus";
				if (!string.IsNullOrEmpty(Conditions))
				{
					Sql += " WHERE " + Conditions;
				}
				if (!string.IsNullOrEmpty(OrderBy))
				{
					Sql += " ORDER BY " + OrderBy;
				}
				SqlCommand cmd = new SqlCommand(Sql);
				cmd.CommandType = CommandType.Text;
				RetVal = Init(LogFilePath, LogFileName, cmd);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public List<CategoryStatus> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}		
		//-----------------------------------------------------
		public List<CategoryStatus> GetListByCategoryStatusId(string LogFilePath, string LogFileName, byte CategoryStatusId)
		{
			List<CategoryStatus> RetVal = new List<CategoryStatus>();
			if (CategoryStatusId > 0)
			{
				string Conditions = "(CategoryStatusId=" + CategoryStatusId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public CategoryStatus Get(string LogFilePath, string LogFileName, byte CategoryStatusId)
		{
			CategoryStatus RetVal = new CategoryStatus(db.ConnectionString);
			try
			{
				List<CategoryStatus> list = GetListByCategoryStatusId(LogFilePath, LogFileName, CategoryStatusId);
				if (list.Count > 0)
				{
					RetVal = (CategoryStatus)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public CategoryStatus Get(List<CategoryStatus> lCategoryStatus, byte CategoryStatusId)
		{
			CategoryStatus RetVal = new CategoryStatus(db.ConnectionString);
			foreach (CategoryStatus mCategoryStatus in lCategoryStatus)
			{
				if (mCategoryStatus.CategoryStatusId == CategoryStatusId)
				{
					RetVal = mCategoryStatus;
					break;
				}
			}
			return RetVal;
		}
	}
}