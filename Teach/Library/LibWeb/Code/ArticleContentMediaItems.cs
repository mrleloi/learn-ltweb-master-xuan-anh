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
	public class ArticleContentMediaItems
	{
		private int _ArticleContentMediaItemId;
		private int _ArticleContentId;
		private int _MediaItemId;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		public int ArticleContentMediaItemId { get { return _ArticleContentMediaItemId; } set { _ArticleContentMediaItemId = value; } }
		public int ArticleContentId { get { return _ArticleContentId; } set { _ArticleContentId = value; } }
		public int MediaItemId { get { return _MediaItemId; } set { _MediaItemId = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public ArticleContentMediaItems(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~ArticleContentMediaItems()
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
				SqlCommand cmd = new SqlCommand("ArticleContentMediaItems_InsertNoInform");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@ArticleContentId", this.ArticleContentId));
				cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add("@ArticleContentMediaItemId", SqlDbType.Int).Direction = ParameterDirection.Output;
				db.ExecuteSQL(cmd);
				this.ArticleContentMediaItemId = Convert.ToInt32((cmd.Parameters["@ArticleContentMediaItemId"].Value == null) ? "0" : cmd.Parameters["@ArticleContentMediaItemId"].Value.ToString().Trim());
				RetVal = (this.ArticleContentMediaItemId > 0);
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
				if (this.ArticleContentMediaItemId > 0)
				{
					SqlCommand cmd = new SqlCommand("ArticleContentMediaItems_UpdateNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@ArticleContentId", this.ArticleContentId));
					cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
					cmd.Parameters.Add(new SqlParameter("@ArticleContentMediaItemId", this.ArticleContentMediaItemId));
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
				if (this.ArticleContentMediaItemId > 0)
				{
					SqlCommand cmd = new SqlCommand("ArticleContentMediaItems_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@ArticleContentMediaItemId", this.ArticleContentMediaItemId));
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
		private List<ArticleContentMediaItems> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<ArticleContentMediaItems> l_ArticleContentMediaItems = new List<ArticleContentMediaItems>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					ArticleContentMediaItems m_ArticleContentMediaItems = new ArticleContentMediaItems(db.ConnectionString);
					m_ArticleContentMediaItems.ArticleContentMediaItemId = smartReader.GetInt32("ArticleContentMediaItemId");
					m_ArticleContentMediaItems.ArticleContentId = smartReader.GetInt32("ArticleContentId");
					m_ArticleContentMediaItems.MediaItemId = smartReader.GetInt32("MediaItemId");
					m_ArticleContentMediaItems.CrUserId = smartReader.GetInt32("CrUserId");
					m_ArticleContentMediaItems.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_ArticleContentMediaItems.Add(m_ArticleContentMediaItems);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_ArticleContentMediaItems;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleContentMediaItems> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<ArticleContentMediaItems> RetVal = new List<ArticleContentMediaItems>();
			try
			{
				string Sql = "SELECT * FROM V$ArticleContentMediaItems";
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
		public List<ArticleContentMediaItems> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleContentMediaItems> GetListByArticleContentMediaItemId(string LogFilePath, string LogFileName, int ArticleContentMediaItemId)
		{
			List<ArticleContentMediaItems> RetVal = new List<ArticleContentMediaItems>();
			if (ArticleContentMediaItemId > 0)
			{
				string Conditions = "(ArticleContentMediaItemId=" + ArticleContentMediaItemId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleContentMediaItems> GetListByArticleContentIdMediaItemId(string LogFilePath, string LogFileName, int ArticleContentId, int MediaItemId)
		{
			List<ArticleContentMediaItems> RetVal = new List<ArticleContentMediaItems>();
			if (ArticleContentId > 0)
			{
				if (MediaItemId > 0)
				{
					string Conditions = "(ArticleContentId=" + ArticleContentId.ToString() + ") AND (MediaItemId=" + MediaItemId.ToString() + ")";
					string OrderBy = "";
					return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
				}
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleContentMediaItems> GetListByArticleContentId(string LogFilePath, string LogFileName, int ArticleContentId)
		{
			List<ArticleContentMediaItems> RetVal = new List<ArticleContentMediaItems>();
			if (ArticleContentId > 0)
			{
				string Conditions = "(ArticleContentId=" + ArticleContentId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleContentMediaItems> GetListByMediaItemId(string LogFilePath, string LogFileName, int MediaItemId)
		{
			List<ArticleContentMediaItems> RetVal = new List<ArticleContentMediaItems>();
			if (MediaItemId > 0)
			{
				string Conditions = "(MediaItemId=" + MediaItemId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleContentMediaItems Get(string LogFilePath, string LogFileName, int ArticleContentId, int MediaItemId)
		{
			ArticleContentMediaItems RetVal = new ArticleContentMediaItems(db.ConnectionString);
			try
			{
				List<ArticleContentMediaItems> list = GetListByArticleContentIdMediaItemId(LogFilePath, LogFileName, ArticleContentId, MediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ArticleContentMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleContentMediaItems Get(string LogFilePath, string LogFileName, int ArticleContentMediaItemId)
		{
			ArticleContentMediaItems RetVal = new ArticleContentMediaItems(db.ConnectionString);
			try
			{
				List<ArticleContentMediaItems> list = GetListByArticleContentMediaItemId(LogFilePath, LogFileName, ArticleContentMediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ArticleContentMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleContentMediaItems GetByArticleContentId(string LogFilePath, string LogFileName, int ArticleContentId)
		{
			ArticleContentMediaItems RetVal = new ArticleContentMediaItems(db.ConnectionString);
			try
			{
				List<ArticleContentMediaItems> list = GetListByArticleContentId(LogFilePath, LogFileName, ArticleContentId);
				if (list.Count > 0)
				{
					RetVal = (ArticleContentMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleContentMediaItems GetByMediaItemId(string LogFilePath, string LogFileName, int MediaItemId)
		{
			ArticleContentMediaItems RetVal = new ArticleContentMediaItems(db.ConnectionString);
			try
			{
				List<ArticleContentMediaItems> list = GetListByMediaItemId(LogFilePath, LogFileName, MediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ArticleContentMediaItems)list[0];
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
