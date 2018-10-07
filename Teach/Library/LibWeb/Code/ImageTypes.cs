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
	public class ImageTypes
	{
		private byte _ImageTypeId;
		private string _ImageTypeName;
		private string _ImageTypeDesc;
		private DBAccess db;
		//---------------------------------------------------------------
		public ImageTypes(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//-----------------------------------------------------------------------
		~ImageTypes()
		{
		}
		//-----------------------------------------------------------------------
		public virtual void Dispose()
		{
		}
		public byte ImageTypeId { get { return _ImageTypeId; } set { _ImageTypeId = value; } }
		public string ImageTypeName { get { return _ImageTypeName; } set { _ImageTypeName = value; } }
		public string ImageTypeDesc { get { return _ImageTypeDesc; } set { _ImageTypeDesc = value; } }
		//-----------------------------------------------------------------------
		public List<ImageTypes> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			SmartDataReader rd = new SmartDataReader(dr);
			List<ImageTypes> l_ImageTypes = new List<ImageTypes>();
			try
			{
				while (rd.Read())
				{
					ImageTypes m_ImageTypes = new ImageTypes(db.ConnectionString);
					m_ImageTypes.ImageTypeId = rd.GetByte("ImageTypeId");
					m_ImageTypes.ImageTypeDesc = rd.GetString("ImageTypeDesc");
					m_ImageTypes.ImageTypeName = rd.GetString("ImageTypeName");
					l_ImageTypes.Add(m_ImageTypes);
				}
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			finally
			{
				rd.disposeReader(dr);
				cmd.Dispose();
				db.closeConnection(con);
			}
			return l_ImageTypes;
		}
		//-----------------------------------------------------------------------
		public List<ImageTypes> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<ImageTypes> RetVal = new List<ImageTypes>();
			try
			{
				string Sql = "SELECT * FROM V$ImageTypes";
				if (!string.IsNullOrEmpty(Conditions))
				{
					Sql += " WHERE " + Conditions;
				}
				if (!string.IsNullOrEmpty(OrderBy))
				{
					Sql += " ORDER BY " + OrderBy;
				}
				SqlCommand cmd = new SqlCommand(Sql);
				RetVal = Init(LogFilePath, LogFileName, cmd);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public List<ImageTypes> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//-----------------------------------------------------------------------
		public static List<ImageTypes> Static_GetList(string LogFilePath, string LogFileName, string constr)
		{
			List<ImageTypes> RetVal = new List<ImageTypes>();
			ImageTypes m_ImageTypes = new ImageTypes(constr);
			try
			{
				RetVal = m_ImageTypes.GetList(LogFilePath, LogFileName);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + m_ImageTypes.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public List<ImageTypes> GetListByImageTypeName(string LogFilePath, string LogFileName, string ImageTypeName)
		{
			List<ImageTypes> RetVal = new List<ImageTypes>();
			ImageTypeName = StringUtils.Static_InjectionString(ImageTypeName);
			if (!string.IsNullOrEmpty(ImageTypeName))
			{
				string Conditions = "(ImageTypeName=N'" + ImageTypeName + "')";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public ImageTypes Get(string LogFilePath, string LogFileName, string ImageTypeName)
		{
			ImageTypes RetVal = new ImageTypes(db.ConnectionString);
			try
			{
				List<ImageTypes> list = GetListByImageTypeName(LogFilePath, LogFileName, ImageTypeName);
				if (list.Count > 0)
				{
					RetVal = (ImageTypes)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public static byte Static_GetIdByImageTypeName(string LogFilePath, string LogFileName, string constr, string ImageTypeName)
		{
			byte RetVal = 0;
			ImageTypes m_ImageTypes = new ImageTypes(constr);
			try
			{
				m_ImageTypes = m_ImageTypes.Get(LogFilePath, LogFileName, ImageTypeName);
				if (m_ImageTypes.ImageTypeId > 0)
				{
					RetVal = m_ImageTypes.ImageTypeId;
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + m_ImageTypes.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
	}//end ImageTypes
}//end namespace media