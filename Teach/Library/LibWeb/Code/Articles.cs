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
	public class Articles
	{
		private int _ArticleId;
		private string _ArticleTitle;
		private string _ArticleLead;
		private byte _EditorStatusId;
		private short _CategoryId;
		private int _AuthorUserId;
		private DateTime _AuthorDateTime;
		private DateTime _PublishDateTime;
		private string _ArticleSource;
		private int _MediaItemId;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		//-------------------------------------------------------------------------------------
		public int ArticleId { get { return _ArticleId; } set { _ArticleId = value; } }
		public string ArticleTitle { get { return _ArticleTitle; } set { _ArticleTitle = value; } }
		public string ArticleLead { get { return _ArticleLead; } set { _ArticleLead = value; } }
		public byte EditorStatusId { get { return _EditorStatusId; } set { _EditorStatusId = value; } }
		public short CategoryId { get { return _CategoryId; } set { _CategoryId = value; } }
		public int AuthorUserId { get { return _AuthorUserId; } set { _AuthorUserId = value; } }
		public DateTime AuthorDateTime { get { return _AuthorDateTime; } set { _AuthorDateTime = value; } }
		public DateTime PublishDateTime { get { return _PublishDateTime; } set { _PublishDateTime = value; } }
		public string ArticleSource { get { return _ArticleSource; } set { _ArticleSource = value; } }
		public int MediaItemId { get { return _MediaItemId; } set { _MediaItemId = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public Articles(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~Articles()
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
				SqlCommand cmd = new SqlCommand("Articles_Insert");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@ArticleTitle", this.ArticleTitle));
				cmd.Parameters.Add(new SqlParameter("@ArticleLead", this.ArticleLead));
				cmd.Parameters.Add(new SqlParameter("@EditorStatusId", this.EditorStatusId));
				cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
				cmd.Parameters.Add(new SqlParameter("@AuthorUserId", this.AuthorUserId));
				cmd.Parameters.Add(new SqlParameter("@AuthorDateTime", this.AuthorDateTime));
				cmd.Parameters.Add(new SqlParameter("@PublishDateTime", this.PublishDateTime));
				cmd.Parameters.Add(new SqlParameter("@ArticleSource", this.ArticleSource));
				cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add("@ArticleId", SqlDbType.Int).Direction = ParameterDirection.Output;
				db.ExecuteSQL(cmd);
				this.ArticleId = (cmd.Parameters["@ArticleId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@ArticleId"].Value);
				RetVal = (this.ArticleId>0);
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
				if (this.ArticleId > 0)
				{
					SqlCommand cmd = new SqlCommand("Articles_UpdateNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@ArticleTitle", this.ArticleTitle));
					cmd.Parameters.Add(new SqlParameter("@ArticleLead", this.ArticleLead));
					cmd.Parameters.Add(new SqlParameter("@EditorStatusId", this.EditorStatusId));
					cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
					cmd.Parameters.Add(new SqlParameter("@AuthorUserId", this.AuthorUserId));
					cmd.Parameters.Add(new SqlParameter("@AuthorDateTime", this.AuthorDateTime));
					cmd.Parameters.Add(new SqlParameter("@PublishDateTime", this.PublishDateTime));
					cmd.Parameters.Add(new SqlParameter("@ArticleSource", this.ArticleSource));
					cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
					cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
		public bool DeleteNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId, int ArticleId)
		{
			bool RetVal = false;
			try
			{
				if (this.ArticleId > 0)
				{
					SqlCommand cmd = new SqlCommand("Articles_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
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
			return DeleteNoInform(LogFilePath, LogFileName, DistributedProcess, IpAddress, ActUserId, this.ArticleId);
		}
		//-------------------------------------------------------------------------------------------------------------------
		private List<Articles> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<Articles> l_Articles = new List<Articles>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					Articles m_Articles = new Articles(db.ConnectionString);
					m_Articles.ArticleId = smartReader.GetInt32("ArticleId");
					m_Articles.ArticleTitle = smartReader.GetString("ArticleTitle");
					m_Articles.ArticleLead = smartReader.GetString("ArticleLead");
					m_Articles.EditorStatusId = smartReader.GetByte("EditorStatusId");
					m_Articles.CategoryId = smartReader.GetInt16("CategoryId");
					m_Articles.AuthorUserId = smartReader.GetInt32("AuthorUserId");
					m_Articles.AuthorDateTime = smartReader.GetDateTime("AuthorDateTime");
					m_Articles.PublishDateTime = smartReader.GetDateTime("PublishDateTime");
					m_Articles.ArticleSource = smartReader.GetString("ArticleSource");
					m_Articles.MediaItemId = smartReader.GetInt32("MediaItemId");
					m_Articles.CrUserId = smartReader.GetInt32("CrUserId");
					m_Articles.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_Articles.Add(m_Articles);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_Articles;
		}
		//--------------------------------------------------------------------
		public List<Articles> GetList(string LogFilePath, string LogFileName, int RowAmount, string Conditions, string OrderBy)
		{
			List<Articles> RetVal = new List<Articles>();
			try
			{
				string Sql = "SELECT ";
				if (RowAmount > 0)
				{
					Sql += " TOP (" + RowAmount.ToString() + ")";
				}
				Sql += " * FROM V$Articles";
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
		public List<Articles> GetList(string LogFilePath, string LogFileName)
		{
			int RowAmount = 0;
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName,RowAmount, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<Articles> GetListByCategoryIdEditorStatusId(string LogFilePath, string LogFileName, int RowAmount, short CategoryId, byte EditorStatusId)
		{
			List<Articles> RetVal = new List<Articles>();
			if (CategoryId > 0)
			{
				if (EditorStatusId > 0)
				{
					string Conditions = "(CategoryId=" + CategoryId.ToString() + ") AND (EditorStatusId=" + EditorStatusId.ToString() + ")";
					string OrderBy = "";
					return GetList(LogFilePath, LogFileName, RowAmount, Conditions, OrderBy);
				}
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<Articles> GetListByArticleId(string LogFilePath, string LogFileName, int ArticleId)
		{
			List<Articles> RetVal = new List<Articles>();
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
		public Articles Get(string LogFilePath, string LogFileName, int ArticleId)
		{
			Articles RetVal = new Articles(db.ConnectionString);
			try
			{
				List<Articles> list = GetListByArticleId(LogFilePath, LogFileName, ArticleId);
				if (list.Count > 0)
				{
					RetVal = (Articles)list[0];
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
				if (string.IsNullOrEmpty(CategoryName))
				{
					CategoryName=Categories.Static_GetName(LogFilePath, LogFileName, db.ConnectionString, this.CategoryId);
				}
				RetVal = ROOT_PATH + ((string.IsNullOrEmpty(CategoryName)) ? "Unknown" : StringUtils.TitleFormat(CategoryName)) + "/" + StringUtils.TitleFormat(this.ArticleTitle) + "-" + this.ArticleId + "." + FILE_EXTEND;
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
	}
}