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
	public class MediaItems
	{
		private int _MediaItemId;
		private byte _SubjectTypeId;
		private string _MediaItemContent;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		//-------------------------------------------------------------------------------------
		public int MediaItemId { get { return _MediaItemId; } set { _MediaItemId = value; } }
		public byte SubjectTypeId { get { return _SubjectTypeId; } set { _SubjectTypeId = value; } }
		public string MediaItemContent { get { return _MediaItemContent; } set { _MediaItemContent = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public MediaItems(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~MediaItems()
		{
		}
		//-------------------------------------------------------------------------------------
		public virtual void Dispose()
		{
		}
		//-------------------------------------------------------------------------------------
		public bool InsertNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId)
		{
			bool RetVal = false;
			try
			{
				SqlCommand cmd = new SqlCommand("MediaItems_InsertNoInform");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@SubjectTypeId", this.SubjectTypeId));
				cmd.Parameters.Add(new SqlParameter("@MediaItemContent", this.MediaItemContent));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				SqlParameter item = new SqlParameter("@MediaItemId", this.MediaItemId);
				item.Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add(item);
				db.ExecuteSQL(cmd);
				this.MediaItemId = (cmd.Parameters["@MediaItemId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@MediaItemId"].Value);
				RetVal = (this.MediaItemId>0);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------------------------------------------------
		public bool UpdateNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId)
		{
			bool RetVal = false;
			try
			{
				SqlCommand cmd = new SqlCommand("MediaItems_UpdateNoInform");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@SubjectTypeId", this.SubjectTypeId));
				cmd.Parameters.Add(new SqlParameter("@MediaItemContent", this.MediaItemContent));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
				db.ExecuteSQL(cmd);
				RetVal = true;
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public bool DeleteNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId, int MediaItemId)
		{
			bool RetVal = false;
			try
			{
				if (MediaItemId > 0)
				{
					SqlCommand cmd = new SqlCommand("MediaItems_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@MediaItemId", MediaItemId));
					db.ExecuteSQL(cmd);
					RetVal = true;
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public bool DeleteNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId)
		{
			return DeleteNoInform(LogFilePath, LogFileName, DistributedProcess, IpAddress, ActUserId, this.MediaItemId);
		}
		//-------------------------------------------------------------------------------------------------------------------
		private List<MediaItems> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<MediaItems> l_MediaItems = new List<MediaItems>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					MediaItems m_MediaItems = new MediaItems(db.ConnectionString);
					m_MediaItems.MediaItemId = smartReader.GetInt32("MediaItemId");
					m_MediaItems.SubjectTypeId = smartReader.GetByte("SubjectTypeId");
					m_MediaItems.MediaItemContent = smartReader.GetString("MediaItemContent");
					m_MediaItems.CrUserId = smartReader.GetInt32("CrUserId");
					m_MediaItems.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_MediaItems.Add(m_MediaItems);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_MediaItems;
		}
		//--------------------------------------------------------------------
		public List<MediaItems> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<MediaItems> RetVal = new List<MediaItems>();
			try
			{
				string Sql = "SELECT * FROM V$MediaItems";
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
		
			//--------------------------------------------------------------------
		public List<MediaItems> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<MediaItems> GetListByMediaItemId(string LogFilePath, string LogFileName, int MediaItemId)
		{
			List<MediaItems> RetVal = new List<MediaItems>();
			if (MediaItemId > 0)
			{
				string Conditions = "(MediaItemId=" + MediaItemId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public MediaItems Get(string LogFilePath, string LogFileName, int MediaItemId)
		{
			MediaItems RetVal = new MediaItems(db.ConnectionString);
			try
			{
				List<MediaItems> list = GetListByMediaItemId(LogFilePath, LogFileName, MediaItemId);
				if (list.Count > 0)
				{
					RetVal = (MediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public string GetLink(string LogFilePath, string LogFileName, string ROOT_PATH, string FILE_EXTEND, string TitleName, string SubjectName, int SubjectId, byte SubjectTypeId, int MediaItemId)
		{
			string RetVal = "";
			try
			{
				RetVal = ROOT_PATH + StringUtils.TitleFormat(TitleName) + "/" + StringUtils.TitleFormat(SubjectName) + "-" + MediaItemId.ToString() + "." + FILE_EXTEND;
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
	}
}