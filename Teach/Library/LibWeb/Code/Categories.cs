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
	public class Categories
	{
		private short _CategoryId;
		private short _DisplayOrder;
		private short _ParentCategoryId;
		private string _CategoryName;
		private string _CategoryDesc;		
		private string _Url;
		private byte _CategoryStatusId;
		private string _ImageIcon;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		//-------------------------------------------------------------------------------------
		public short CategoryId { get { return _CategoryId; } set { _CategoryId = value; } }
		public short DisplayOrder { get { return _DisplayOrder; } set { _DisplayOrder = value; } }
		public short ParentCategoryId { get { return _ParentCategoryId; } set { _ParentCategoryId = value; } }
		public string CategoryName { get { return _CategoryName; } set { _CategoryName = value; } }
		public string CategoryDesc { get { return _CategoryDesc; } set { _CategoryDesc = value; } }
		public string Url { get { return _Url; } set { _Url = value; } }
		public byte CategoryStatusId { get { return _CategoryStatusId; } set { _CategoryStatusId = value; } }
		public string ImageIcon { get { return _ImageIcon; } set { _ImageIcon = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public Categories(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		public Categories(string constr, string Name, string Desc)
		{
			this.CategoryId = 0;
			this.CategoryName = Name;
			this.CategoryDesc = Desc;
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~Categories()
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
				SqlCommand cmd = new SqlCommand("Categories_InsertNoInform");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
				cmd.Parameters.Add(new SqlParameter("@ParentCategoryId", this.ParentCategoryId));
				cmd.Parameters.Add(new SqlParameter("@CategoryName", this.CategoryName));
				cmd.Parameters.Add(new SqlParameter("@CategoryDesc", this.CategoryDesc));
				cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
				cmd.Parameters.Add(new SqlParameter("@CategoryStatusId", this.CategoryStatusId));
				cmd.Parameters.Add(new SqlParameter("@ImageIcon", this.ImageIcon));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add("@CategoryId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
				db.ExecuteSQL(cmd);
				this.CategoryId = Convert.ToInt16((cmd.Parameters["@CategoryId"].Value == null) ? "0" : cmd.Parameters["@CategoryId"].Value.ToString().Trim());
				RetVal = (this.CategoryId > 0);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//---------------------------------------------------------------------------------------------------------
		public bool UpdateNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId)
		{
			bool RetVal = false;
			try
			{
				if (this.CategoryId > 0)
				{
					SqlCommand cmd = new SqlCommand("Categories_UpdateNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
					cmd.Parameters.Add(new SqlParameter("@ParentCategoryId", this.ParentCategoryId));
					cmd.Parameters.Add(new SqlParameter("@CategoryName", this.CategoryName));
					cmd.Parameters.Add(new SqlParameter("@CategoryDesc", this.CategoryDesc));
					cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
					cmd.Parameters.Add(new SqlParameter("@CategoryStatusId", this.CategoryStatusId));
					cmd.Parameters.Add(new SqlParameter("@ImageIcon", this.ImageIcon));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
					cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
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
		//-----------------------------------------------------------------------------------------------------------------
		public bool DeleteNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId)
		{
			bool RetVal = false;
			try
			{
				if (this.CategoryId > 0)
				{
					SqlCommand cmd = new SqlCommand("Categories_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
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
		//---------------------------------------------------------------------------------------------------------------------
		public List<Categories> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<Categories> l_Categories = new List<Categories>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					Categories m_Categories = new Categories(db.ConnectionString);
					m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
					m_Categories.DisplayOrder = smartReader.GetInt16("DisplayOrder");
					m_Categories.CategoryName = smartReader.GetString("CategoryName");
					m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
					m_Categories.ParentCategoryId = smartReader.GetInt16("ParentCategoryId");
					m_Categories.Url = smartReader.GetString("Url");
					m_Categories.CategoryStatusId = smartReader.GetByte("CategoryStatusId");
					m_Categories.ImageIcon = smartReader.GetString("ImageIcon");
					m_Categories.CrUserId = smartReader.GetInt32("CrUserId");
					m_Categories.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_Categories.Add(m_Categories);
				}
				smartReader.disposeReader(reader);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			finally
			{
				db.closeConnection(con);
			}
			return l_Categories;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<Categories> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<Categories> retVal = new List<Categories>();
			try
			{
				string Sql = "SELECT * FROM V$Categories";
				if (!string.IsNullOrEmpty(Conditions))
				{
					Sql+=" WHERE "+Conditions;
				}
				if (!string.IsNullOrEmpty(OrderBy))
				{
					Sql += " ORDER BY " + OrderBy;
				}
				SqlCommand cmd = new SqlCommand(Sql);
				cmd.CommandType = CommandType.Text;
				retVal = Init(LogFilePath, LogFileName, cmd);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return retVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<Categories> GetList(string LogFilePath, string LogFileName, int ActUserId)
		{
			string Conditions = "(CrUserId=" + ActUserId.ToString()+")";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<Categories> GetListRootOrderByDisplayOrder(string LogFilePath, string LogFileName)
		{
			string Conditions = "(ParentCategoryId IS NULL)";
			string OrderBy = "DisplayOrder";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//----------------------------------------------------------------------------------------------------------------------
		public List<Categories> GetListByCategoryId(string LogFilePath, string LogFileName, short CategoryId)
		{
			List<Categories> RetVal = new List<Categories>();
			if (CategoryId > 0)
			{
				string Conditions = "(CategoryId=" + CategoryId.ToString() + ")";
				string OrderBy = "";
				RetVal=GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}		
		//----------------------------------------------------------------------------------------------------------------------
		public Categories Get(string LogFilePath, string LogFileName, short CategoryId)
		{
			Categories RetVal = new Categories(db.ConnectionString);
			try
			{
				List<Categories> list = GetListByCategoryId(LogFilePath, LogFileName, CategoryId);
				if (list.Count > 0)
				{
					RetVal = (Categories)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//----------------------------------------------------------------------------------------------------------------------
		public Categories Get(List<Categories> lCategories, short CategoryId)
		{
			Categories RetVal = new Categories(db.ConnectionString);
			if (CategoryId > 0)
			{
				foreach (Categories mCategories in lCategories)
				{
					if (mCategories.CategoryId == CategoryId)
					{
						RetVal = mCategories;
						break;
					}
				}
			}
			return RetVal;
		}
		//------------------------------------------------------------------------------------
		public List<Categories> Copy(List<Categories> l_Categories)
		{
			List<Categories> RetVal = new List<Categories>();
			foreach (Categories mCategories in l_Categories)
			{
				RetVal.Add(mCategories);
			}
			return RetVal;
		}
		//------------------------------------------------------------------------------------
		public List<Categories> CopyNA(List<Categories> l_Categories)
		{
			List<Categories> RetVal = new List<Categories>();
			RetVal = Copy(l_Categories);
			RetVal.Insert(0, new Categories(db.ConnectionString, StringUtils.NA, StringUtils.NA_DESC));
			return RetVal;
		}
		//----------------------------------------------------------------------------
		public static string Static_GetName(string LogFilePath, string LogFileName, string constr, short CategoryId)
		{
			string RetVal = "";
			Categories m_Categories = new Categories(constr);
			try
			{
				m_Categories = m_Categories.Get(LogFilePath, LogFileName, CategoryId);
				if (!string.IsNullOrEmpty(m_Categories.CategoryName))
				{
					RetVal = m_Categories.CategoryName.Trim();
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + m_Categories.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//----------------------------------------------------------------------------------------------------------------------
		public string GetCateLink(string ROOT_PATH, string FILE_EXTEND, int pageId)
		{
			string RetVal = "";
			if (!string.IsNullOrEmpty(this.Url))
			{
				if (this.Url.EndsWith(".aspx"))
				{
					RetVal = ROOT_PATH + this.Url;
				}
				else
				{
					RetVal = ROOT_PATH + this.CategoryId + "/" + pageId.ToString() + "/" + StringUtils.TitleFormat(this.CategoryName) + "." + FILE_EXTEND;
				}
			}
			return RetVal;
		}
	}
}