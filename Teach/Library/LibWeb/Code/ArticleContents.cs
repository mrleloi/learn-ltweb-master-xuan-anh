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
	public class ArticleContents
	{
		private int _ArticleContentId;
		private int _ArticleId;
		private string _RawContent;
		private int _MediaItemId;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		//-------------------------------------------------------------------------------------
		public int ArticleContentId { get { return _ArticleContentId; } set { _ArticleContentId = value; } }
		public int ArticleId { get { return _ArticleId; } set { _ArticleId = value; } }
		public string RawContent { get { return _RawContent; } set { _RawContent = value; } }
		public int MediaItemId { get { return _MediaItemId; } set { _MediaItemId = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public ArticleContents(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~ArticleContents()
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
				SqlCommand cmd = new SqlCommand("ArticleContents_Insert");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
				cmd.Parameters.Add(new SqlParameter("@RawContent", this.RawContent));
				cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add("@ArticleContentId", SqlDbType.Int).Direction = ParameterDirection.Output;
				db.ExecuteSQL(cmd);
				this.ArticleContentId = (cmd.Parameters["@ArticleContentId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@ArticleContentId"].Value);
				RetVal = (this.ArticleContentId > 0);
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
				if (this.ArticleContentId > 0)
				{
					SqlCommand cmd = new SqlCommand("ArticleContents_UpdateNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
					cmd.Parameters.Add(new SqlParameter("@RawContent", this.RawContent));
					cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
					cmd.Parameters.Add(new SqlParameter("@ArticleContentId", this.ArticleContentId));
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
		public bool DeleteNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId, int ArticleContentId)
		{
			bool RetVal = false;
			try
			{
				if (this.ArticleContentId > 0)
				{
					SqlCommand cmd = new SqlCommand("ArticleContents_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@ArticleContentId", ArticleContentId));
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
			return DeleteNoInform(LogFilePath, LogFileName, DistributedProcess, IpAddress, ActUserId, this.ArticleContentId);
		}
		//-------------------------------------------------------------------------------------------------------------------
		private List<ArticleContents> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<ArticleContents> l_ArticleContents = new List<ArticleContents>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					ArticleContents m_ArticleContents = new ArticleContents(db.ConnectionString);
					m_ArticleContents.ArticleContentId = smartReader.GetInt32("ArticleContentId");
					m_ArticleContents.ArticleId = smartReader.GetInt32("ArticleId");
					m_ArticleContents.RawContent = smartReader.GetString("RawContent");
					m_ArticleContents.MediaItemId = smartReader.GetInt32("MediaItemId");
					m_ArticleContents.CrUserId = smartReader.GetInt32("CrUserId");
					m_ArticleContents.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_ArticleContents.Add(m_ArticleContents);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_ArticleContents;
		}
		//--------------------------------------------------------------------
		public List<ArticleContents> GetList(string LogFilePath, string LogFileName, int RowAmount, string Conditions, string OrderBy)
		{
			List<ArticleContents> RetVal = new List<ArticleContents>();
			try
			{
				string Sql = "SELECT ";
				if (RowAmount > 0)
				{
					Sql += " TOP (" + RowAmount.ToString() + ")";
				}
				Sql += " * FROM V$ArticleContents";
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
		public List<ArticleContents> GetList(string LogFilePath, string LogFileName)
		{
			int RowAmount = 0;
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, RowAmount, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleContents> GetListByArticleContentId(string LogFilePath, string LogFileName, int ArticleContentId)
		{
			List<ArticleContents> RetVal = new List<ArticleContents>();
			if (ArticleContentId > 0)
			{
				int RowAmount = 0;
				string Conditions = "(ArticleContentId=" + ArticleContentId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, RowAmount, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ArticleContents> GetListByArticleId(string LogFilePath, string LogFileName, int ArticleId)
		{
			List<ArticleContents> RetVal = new List<ArticleContents>();
			if (ArticleId > 0)
			{
				int RowAmount = 0;
				string Conditions = "(ArticleId=" + ArticleId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, RowAmount, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleContents Get(string LogFilePath, string LogFileName, int ArticleContentId)
		{
			ArticleContents RetVal = new ArticleContents(db.ConnectionString);
			try
			{
				List<ArticleContents> list = GetListByArticleContentId(LogFilePath, LogFileName, ArticleContentId);
				if (list.Count > 0)
				{
					RetVal = (ArticleContents)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ArticleContents GetByArticleId(string LogFilePath, string LogFileName, int ArticleId)
		{
			ArticleContents RetVal = new ArticleContents(db.ConnectionString);
			try
			{
				List<ArticleContents> list = GetListByArticleId(LogFilePath, LogFileName, ArticleId);
				if (list.Count > 0)
				{
					RetVal = (ArticleContents)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-------------------------------------------------------------------------------------------------------------------------------
		public string GetArticleLink(string LogFilePath, string LogFileName, string ROOT_PATH, string FILE_EXTEND, string CategoryName)
		{
			string RetVal = "";
			try
			{
				Articles m_Articles = new Articles(this.db.ConnectionString);
				m_Articles = m_Articles.Get(LogFilePath, LogFileName, this.ArticleId);
				if (m_Articles.ArticleId > 0)
				{
					RetVal = m_Articles.GetArticleLink(LogFilePath,  LogFileName,  ROOT_PATH,  FILE_EXTEND, CategoryName);
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