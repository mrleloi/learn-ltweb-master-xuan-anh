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
	public class ArticleMediaItems
	{
		private int _ArticleMediaItemId;
		private int _ArticleId;
		private int _MediaItemId;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		public int ArticleMediaItemId { get { return _ArticleMediaItemId; } set { _ArticleMediaItemId = value; } }
		public int ArticleId { get { return _ArticleId; } set { _ArticleId = value; } }
		public int MediaItemId { get { return _MediaItemId; } set { _MediaItemId = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public ArticleMediaItems(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~ArticleMediaItems()
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
				SqlCommand cmd = new SqlCommand("ArticleMediaItems_InsertNoInform");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
				cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add("@ArticleMediaItemId", SqlDbType.Int).Direction = ParameterDirection.Output;
				db.ExecuteSQL(cmd);
				this.ArticleMediaItemId = Convert.ToInt32((cmd.Parameters["@ArticleMediaItemId"].Value == null) ? "0" : cmd.Parameters["@ArticleMediaItemId"].Value.ToString().Trim());
				RetVal = (this.ArticleMediaItemId > 0);
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
				if (this.ArticleMediaItemId > 0)
				{
					SqlCommand cmd = new SqlCommand("ArticleMediaItems_UpdateNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
					cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
					cmd.Parameters.Add(new SqlParameter("@ArticleMediaItemId", this.ArticleMediaItemId));
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
			bool RetVal = false;
			try
			{
				if (this.ArticleMediaItemId > 0)
				{
					SqlCommand cmd = new SqlCommand("ArticleMediaItems_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@ArticleMediaItemId", this.ArticleMediaItemId));
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
		//-------------------------------------------------------------------------------------------------------------------
		private List<ArticleMediaItems> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<ArticleMediaItems> l_ArticleMediaItems = new List<ArticleMediaItems>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					ArticleMediaItems m_ArticleMediaItems = new ArticleMediaItems(db.ConnectionString);
					m_ArticleMediaItems.ArticleMediaItemId = smartReader.GetInt32("ArticleMediaItemId");
					m_ArticleMediaItems.ArticleId = smartReader.GetInt32("ArticleId");
					m_ArticleMediaItems.MediaItemId = smartReader.GetInt32("MediaItemId");
					m_ArticleMediaItems.CrUserId = smartReader.GetInt32("CrUserId");
					m_ArticleMediaItems.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_ArticleMediaItems.Add(m_ArticleMediaItems);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_ArticleMediaItems;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleMediaItems> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<ArticleMediaItems> RetVal = new List<ArticleMediaItems>();
			try
			{
				string Sql = "SELECT * FROM V$ArticleMediaItems";
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
		public List<ArticleMediaItems> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleMediaItems> GetListByArticleMediaItemId(string LogFilePath, string LogFileName, int ArticleMediaItemId)
		{
			List<ArticleMediaItems> RetVal = new List<ArticleMediaItems>();
			if (ArticleMediaItemId > 0)
			{
				string Conditions = "(ArticleMediaItemId=" + ArticleMediaItemId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleMediaItems> GetListByArticleIdMediaItemId(string LogFilePath, string LogFileName, int ArticleId, int MediaItemId)
		{
			List<ArticleMediaItems> RetVal = new List<ArticleMediaItems>();
			if (ArticleId > 0)
			{
				if (MediaItemId > 0)
				{
					string Conditions = "(ArticleId=" + ArticleId.ToString() + ") AND (MediaItemId=" + MediaItemId.ToString() + ")";
					string OrderBy = "";
					return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
				}
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleMediaItems> GetListByArticleId(string LogFilePath, string LogFileName, int ArticleId)
		{
			List<ArticleMediaItems> RetVal = new List<ArticleMediaItems>();
			if (ArticleId > 0)
			{
				string Conditions = "(ArticleId=" + ArticleId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleMediaItems> GetListByMediaItemId(string LogFilePath, string LogFileName, int MediaItemId)
		{
			List<ArticleMediaItems> RetVal = new List<ArticleMediaItems>();
			if (MediaItemId > 0)
			{
				string Conditions = "(MediaItemId=" + MediaItemId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleMediaItems Get(string LogFilePath, string LogFileName, int ArticleId, int MediaItemId)
		{
			ArticleMediaItems RetVal = new ArticleMediaItems(db.ConnectionString);
			try
			{
				List<ArticleMediaItems> list = GetListByArticleIdMediaItemId(LogFilePath, LogFileName, ArticleId, MediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ArticleMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleMediaItems Get(string LogFilePath, string LogFileName, int ArticleMediaItemId)
		{
			ArticleMediaItems RetVal = new ArticleMediaItems(db.ConnectionString);
			try
			{
				List<ArticleMediaItems> list = GetListByArticleMediaItemId(LogFilePath, LogFileName, ArticleMediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ArticleMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleMediaItems GetByArticleId(string LogFilePath, string LogFileName, int ArticleId)
		{
			ArticleMediaItems RetVal = new ArticleMediaItems(db.ConnectionString);
			try
			{
				List<ArticleMediaItems> list = GetListByArticleId(LogFilePath, LogFileName, ArticleId);
				if (list.Count > 0)
				{
					RetVal = (ArticleMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleMediaItems GetByMediaItemId(string LogFilePath, string LogFileName, int MediaItemId)
		{
			ArticleMediaItems RetVal = new ArticleMediaItems(db.ConnectionString);
			try
			{
				List<ArticleMediaItems> list = GetListByMediaItemId(LogFilePath, LogFileName, MediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ArticleMediaItems)list[0];
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
