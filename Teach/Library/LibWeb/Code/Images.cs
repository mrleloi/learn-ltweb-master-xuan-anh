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
	public class Images
	{
		private int _ImageId;
		private string _ImagePath;
		private string _ImageFileName;
		private int _ImageFileSize;
		private int _ImageHeight;
		private int _ImageWidth;
		private string _ImageDesc;
		private byte _AlignId;
		private byte _ImageTypeId;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		//-------------------------------------------------------------------------------------
		public int ImageId { get { return _ImageId; } set { _ImageId = value; } }
		public string ImagePath { get { return _ImagePath; } set { _ImagePath = value; } }
		public string ImageFileName { get { return _ImageFileName; } set { _ImageFileName = value; } }
		public int ImageFileSize { get { return _ImageFileSize; } set { _ImageFileSize = value; } }
		public int ImageHeight { get { return _ImageHeight; } set { _ImageHeight = value; } }
		public int ImageWidth { get { return _ImageWidth; } set { _ImageWidth = value; } }
		public string ImageDesc { get { return _ImageDesc; } set { _ImageDesc = value; } }
		public byte AlignId { get { return _AlignId; } set { _AlignId = value; } }
		public byte ImageTypeId { get { return _ImageTypeId; } set { _ImageTypeId = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public Images(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~Images()
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
				SqlCommand cmd = new SqlCommand("Images_InsertNoInform");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
				cmd.Parameters.Add(new SqlParameter("@ImageFileName", this.ImageFileName));
				cmd.Parameters.Add(new SqlParameter("@ImageFileSize", this.ImageFileSize));
				cmd.Parameters.Add(new SqlParameter("@ImageHeight", this.ImageHeight));
				cmd.Parameters.Add(new SqlParameter("@ImageWidth", this.ImageWidth));
				cmd.Parameters.Add(new SqlParameter("@ImageDesc", this.ImageDesc));
				cmd.Parameters.Add(new SqlParameter("@AlignId", this.AlignId));
				cmd.Parameters.Add(new SqlParameter("@ImageTypeId", this.ImageTypeId));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add("@ImageId", SqlDbType.Int).Direction = ParameterDirection.Output;
				db.ExecuteSQL(cmd);
				this.ImageId = (cmd.Parameters["@ImageId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@ImageId"].Value);
				RetVal = (this.ImageId > 0);
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
				if (this.ImageId > 0)
				{
					SqlCommand cmd = new SqlCommand("Images_UpdateNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@ImageFileName", this.ImageFileName));
					cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
					cmd.Parameters.Add(new SqlParameter("@ImageFileSize", this.ImageFileSize));
					cmd.Parameters.Add(new SqlParameter("@ImageHeight", this.ImageHeight));
					cmd.Parameters.Add(new SqlParameter("@ImageWidth", this.ImageWidth));
					cmd.Parameters.Add(new SqlParameter("@ImageDesc", this.ImageDesc));
					cmd.Parameters.Add(new SqlParameter("@AlignId", this.AlignId));
					cmd.Parameters.Add(new SqlParameter("@ImageTypeId", this.ImageTypeId));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
					cmd.Parameters.Add(new SqlParameter("@ImageId", this.ImageId));
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
		public bool DeleteNoInform(string LogFilePath, string LogFileName, byte DistributedProcess, string IpAddress, int ActUserId, int ImageId)
		{
			bool RetVal = false;
			try
			{
				if (ImageId > 0)
				{
					SqlCommand cmd = new SqlCommand("Images_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@ImageId", ImageId));
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
			return DeleteNoInform(LogFilePath, LogFileName, DistributedProcess, IpAddress, ActUserId, this.ImageId);
		}
		//-------------------------------------------------------------------------------------------------------------------
		private List<Images> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<Images> l_Images = new List<Images>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					Images m_Images = new Images(db.ConnectionString);
					m_Images.ImageId = smartReader.GetInt32("ImageId");
					m_Images.ImageFileName = smartReader.GetString("ImageFileName");
					m_Images.ImagePath = smartReader.GetString("ImagePath");
					m_Images.ImageFileSize = smartReader.GetInt32("ImageFileSize");
					m_Images.ImageHeight = smartReader.GetInt32("ImageHeight");
					m_Images.ImageWidth = smartReader.GetInt32("ImageWidth");
					m_Images.ImageDesc = smartReader.GetString("ImageDesc");
					m_Images.AlignId = smartReader.GetByte("AlignId");
					m_Images.ImageTypeId = smartReader.GetByte("ImageTypeId");
					m_Images.CrUserId = smartReader.GetInt32("CrUserId");
					m_Images.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_Images.Add(m_Images);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_Images;
		}		
		//--------------------------------------------------------------------------------------------------------------------
		public List<Images> GetList(string LogFilePath, string LogImageFileName, string Conditions, string OrderBy)
		{
			List<Images> RetVal = new List<Images>();
			try
			{
				string Sql = "SELECT * FROM V$Images";
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
				RetVal = Init(LogFilePath, LogImageFileName, cmd);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogImageFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<Images> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<Images> GetListByImageId(string LogFilePath, string LogFileName, int ImageId)
		{
			List<Images> RetVal = new List<Images>();
			if (ImageId > 0)
			{
				string Conditions = "(ImageId=" + ImageId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//---------------------------------------------------
		public List<Images> GetListByImageFileName(string LogFilePath, string LogFileName, string ImageFileName)
		{
			List<Images> RetVal = new List<Images>();
			if (!string.IsNullOrEmpty(ImageFileName))
			{
				string Conditions = "(ImageFileName=N'" + ImageFileName + "')";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public Images Get(string LogFilePath, string LogFileName, int ImageId)
		{
			Images RetVal = new Images(db.ConnectionString);
			try
			{
				List<Images> list = RetVal.GetListByImageId(LogFilePath, LogFileName, ImageId);
				if (list.Count > 0)
				{
					RetVal = (Images)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public Images Get(string LogFilePath, string LogFileName, string ImageFileName)
		{
			Images RetVal = new Images(db.ConnectionString);
			try
			{
				List<Images> list = GetListByImageFileName(LogFilePath, LogFileName, ImageFileName);
				if (list.Count > 0)
				{
					RetVal = (Images)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public string GetFolderPath(string RootPath)
		{
			string RetVal = this.ImagePath;
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public string GetFullFileName(string RootPath)
		{
			string RetVal=GetFolderPath(RootPath)+"\\"+this.ImageFileName;
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public static string Static_GetImageFileName(string LogFileImagePath, string LogImageFileName, string constr, int ImageId)
		{
			string RetVal = "";
			Images m_Images = new Images(constr);
			try
			{
				m_Images = m_Images.Get(LogFileImagePath, LogImageFileName, ImageId);
				if (!string.IsNullOrEmpty(m_Images.ImageFileName))
				{
					RetVal = m_Images.ImageFileName;
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFileImagePath + "\\Exception", LogImageFileName + "." + m_Images.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
	}
}