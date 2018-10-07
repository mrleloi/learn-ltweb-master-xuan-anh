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
	public class ImageMediaItems
	{
		private int _ImageMediaItemId;
		private int _ImageId;
		private int _MediaItemId;
		private int _CrUserId;
		private DateTime _CrDateTime;
		private DBAccess db;
		public int ImageMediaItemId { get { return _ImageMediaItemId; } set { _ImageMediaItemId = value; } }
		public int ImageId { get { return _ImageId; } set { _ImageId = value; } }
		public int MediaItemId { get { return _MediaItemId; } set { _MediaItemId = value; } }
		public int CrUserId { get { return _CrUserId; } set { _CrUserId = value; } }
		public DateTime CrDateTime { get { return _CrDateTime; } set { _CrDateTime = value; } }
		//-------------------------------------------------------------------------------------
		public ImageMediaItems(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-------------------------------------------------------------------------------------
		~ImageMediaItems()
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
				SqlCommand cmd = new SqlCommand("ImageMediaItems_InsertNoInform");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
				cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
				cmd.Parameters.Add(new SqlParameter("@ImageId", this.ImageId));
				cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
				cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
				cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
				cmd.Parameters.Add("@ImageMediaItemId", SqlDbType.Int).Direction = ParameterDirection.Output;
				db.ExecuteSQL(cmd);
				this.ImageMediaItemId = Convert.ToInt32((cmd.Parameters["@ImageMediaItemId"].Value == null) ? "0" : cmd.Parameters["@ImageMediaItemId"].Value.ToString().Trim());
				RetVal = (this.ImageMediaItemId > 0);
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
				if (this.ImageMediaItemId > 0)
				{
					SqlCommand cmd = new SqlCommand("ImageMediaItems_UpdateNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@ImageId", this.ImageId));
					cmd.Parameters.Add(new SqlParameter("@MediaItemId", this.MediaItemId));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
					cmd.Parameters.Add(new SqlParameter("@ImageMediaItemId", this.ImageMediaItemId));
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
				if (this.ImageMediaItemId > 0)
				{
					SqlCommand cmd = new SqlCommand("ImageMediaItems_DeleteNoInform");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@DistributedProcess", DistributedProcess));
					cmd.Parameters.Add(new SqlParameter("@IpAddress", IpAddress));
					cmd.Parameters.Add(new SqlParameter("@CrUserId", ActUserId));
					cmd.Parameters.Add(new SqlParameter("@ImageMediaItemId", this.ImageMediaItemId));
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
		private List<ImageMediaItems> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			List<ImageMediaItems> l_ImageMediaItems = new List<ImageMediaItems>();
			try
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					ImageMediaItems m_ImageMediaItems = new ImageMediaItems(db.ConnectionString);
					m_ImageMediaItems.ImageMediaItemId = smartReader.GetInt32("ImageMediaItemId");
					m_ImageMediaItems.ImageId = smartReader.GetInt32("ImageId");
					m_ImageMediaItems.MediaItemId = smartReader.GetInt32("MediaItemId");
					m_ImageMediaItems.CrUserId = smartReader.GetInt32("CrUserId");
					m_ImageMediaItems.CrDateTime = smartReader.GetDateTime("CrDateTime");
					l_ImageMediaItems.Add(m_ImageMediaItems);
				}
				smartReader.disposeReader(reader);
				db.closeConnection(con);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return l_ImageMediaItems;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ImageMediaItems> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<ImageMediaItems> RetVal = new List<ImageMediaItems>();
			try
			{
				string Sql = "SELECT * FROM V$ImageMediaItems";
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
		public List<ImageMediaItems> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ImageMediaItems> GetListByImageMediaItemId(string LogFilePath, string LogFileName, int ImageMediaItemId)
		{
			List<ImageMediaItems> RetVal = new List<ImageMediaItems>();
			if (ImageMediaItemId > 0)
			{
				string Conditions = "(ImageMediaItemId=" + ImageMediaItemId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ImageMediaItems> GetListByImageIdMediaItemId(string LogFilePath, string LogFileName, int ImageId, int MediaItemId)
		{
			List<ImageMediaItems> RetVal = new List<ImageMediaItems>();
			if (ImageId > 0)
			{
				if (MediaItemId > 0)
				{
					string Conditions = "(ImageId=" + ImageId.ToString() + ") AND (MediaItemId=" + MediaItemId.ToString() + ")";
					string OrderBy = "";
					return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
				}
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ImageMediaItems> GetListByImageId(string LogFilePath, string LogFileName, int ImageId)
		{
			List<ImageMediaItems> RetVal = new List<ImageMediaItems>();
			if (ImageId > 0)
			{
				string Conditions = "(ImageId=" + ImageId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public List<ImageMediaItems> GetListByMediaItemId(string LogFilePath, string LogFileName, int MediaItemId)
		{
			List<ImageMediaItems> RetVal = new List<ImageMediaItems>();
			if (MediaItemId > 0)
			{
				string Conditions = "(MediaItemId=" + MediaItemId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ImageMediaItems Get(string LogFilePath, string LogFileName, int ImageId, int MediaItemId)
		{
			ImageMediaItems RetVal = new ImageMediaItems(db.ConnectionString);
			try
			{
				List<ImageMediaItems> list = GetListByImageIdMediaItemId(LogFilePath, LogFileName, ImageId, MediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ImageMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ImageMediaItems Get(string LogFilePath, string LogFileName, int ImageMediaItemId)
		{
			ImageMediaItems RetVal = new ImageMediaItems(db.ConnectionString);
			try
			{
				List<ImageMediaItems> list = GetListByImageMediaItemId(LogFilePath, LogFileName, ImageMediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ImageMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ImageMediaItems GetByImageId(string LogFilePath, string LogFileName, int ImageId)
		{
			ImageMediaItems RetVal = new ImageMediaItems(db.ConnectionString);
			try
			{
				List<ImageMediaItems> list = GetListByImageId(LogFilePath, LogFileName, ImageId);
				if (list.Count > 0)
				{
					RetVal = (ImageMediaItems)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------------------------------------------------------------
		public ImageMediaItems GetByMediaItemId(string LogFilePath, string LogFileName, int MediaItemId)
		{
			ImageMediaItems RetVal = new ImageMediaItems(db.ConnectionString);
			try
			{
				List<ImageMediaItems> list = GetListByMediaItemId(LogFilePath, LogFileName, MediaItemId);
				if (list.Count > 0)
				{
					RetVal = (ImageMediaItems)list[0];
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
