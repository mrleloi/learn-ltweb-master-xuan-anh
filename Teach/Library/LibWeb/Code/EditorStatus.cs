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
	public class EditorStatus
	{
		private byte _EditorStatusId;
		private string _EditorStatusName;
		private string _EditorStatusDesc;
		private DBAccess db;
		public byte EditorStatusId { get { return _EditorStatusId; } set { _EditorStatusId = value; } }
		public string EditorStatusName { get { return _EditorStatusName; } set { _EditorStatusName = value; } }
		public string EditorStatusDesc { get { return _EditorStatusDesc; } set { _EditorStatusDesc = value; } }
		//-------------------------------------------------------------------------------------
		public EditorStatus(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~EditorStatus()
		{
		}
		//-------------------------------------------------------------------------------------
		public virtual void Dispose()
		{
		}
		//-------------------------------------------------------------------------------------------------------------------
		private List<EditorStatus> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<EditorStatus> l_EditorStatus = new List<EditorStatus>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					EditorStatus m_EditorStatus = new EditorStatus(db.ConnectionString);
					m_EditorStatus.EditorStatusId = smartReader.GetByte("EditorStatusId");
					m_EditorStatus.EditorStatusName = smartReader.GetString("EditorStatusName");
					m_EditorStatus.EditorStatusDesc = smartReader.GetString("EditorStatusDesc");
					l_EditorStatus.Add(m_EditorStatus);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_EditorStatus;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<EditorStatus> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<EditorStatus> RetVal = new List<EditorStatus>();
			try
			{
				string Sql = "SELECT * FROM V$EditorStatus";
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
		//--------------------------------------------------------------------------------------------------------------------
		public List<EditorStatus> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<EditorStatus> GetListByEditorStatusId(string LogFilePath, string LogFileName, byte EditorStatusId)
		{
			List<EditorStatus> RetVal = new List<EditorStatus>();
			if (EditorStatusId > 0)
			{
				string Conditions = "(EditorStatusId=" + EditorStatusId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public EditorStatus Get(string LogFilePath, string LogFileName, byte EditorStatusId)
		{
			EditorStatus RetVal = new EditorStatus(db.ConnectionString);
			try
			{
				List<EditorStatus> list = GetListByEditorStatusId(LogFilePath, LogFileName, EditorStatusId);
				if (list.Count > 0)
				{
					RetVal = (EditorStatus)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
	}
}
