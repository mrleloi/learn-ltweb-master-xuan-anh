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
	public class Aligns
	{
		private byte _AlignId;
		private string _AlignName;
		private string _AlignDesc;
		DBAccess db;
		//---------------------------------------------------------------
		public Aligns(string constr)
		{
			db = new DBAccess((string.IsNullOrEmpty(constr)) ? WebConstants.WEB_CONSTR : constr);
		}
		//----------------------------------------------------------------
		public byte AlignId		{			get			{				return _AlignId;			}			set			{				_AlignId = value;			}		}
		public string AlignName		{			get			{				return _AlignName;			}			set			{				_AlignName = value;			}		}
		public string AlignDesc		{			get			{				return _AlignDesc;			}			set			{				_AlignDesc = value;			}		}
		//----------------------------------------------------------        
		private List<Aligns> Init(string LogFilePath, string LogFileName, SqlCommand cmd)
		{
			List<Aligns> AlignList = new List<Aligns>();
			SqlConnection con = db.getConnection();
			cmd.Connection = con;
			try
			{
				if (con.State == ConnectionState.Closed) con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				SmartDataReader smartReader = new SmartDataReader(reader);
				while (smartReader.Read())
				{
					Aligns m_Aligns = new Aligns(db.ConnectionString);
					m_Aligns.AlignId = smartReader.GetByte("AlignId");
					m_Aligns.AlignName = smartReader.GetString("AlignName");
					m_Aligns.AlignDesc = smartReader.GetString("AlignDesc");
					AlignList.Add(m_Aligns);
				}
				smartReader.DisposeReader(reader);
			}
			catch (SqlException ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			finally
			{
				con.Close();
			}
			return AlignList;
		}
		//--------------------------------------------------------------
		public List<Aligns> GetList(string LogFilePath, string LogFileName, string Conditions, string OrderBy)
		{
			List<Aligns> RetVal = new List<Aligns>();
			try
			{
				string Sql = "SELECT * FROM V$Aligns";
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
		//--------------------------------------------------------------
		public List<Aligns> GetList(string LogFilePath, string LogFileName)
		{
			string Conditions = "";
			string OrderBy = "";
			return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
		}
		//-----------------------------------------------------------------------
		public static List<Aligns> Static_GetList(string LogFilePath, string LogFileName, string constr)
		{
			List<Aligns> RetVal = new List<Aligns>();
			Aligns m_Aligns = new Aligns(constr);
			try
			{
				RetVal = m_Aligns.GetList(LogFilePath, LogFileName);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + m_Aligns.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//--------------------------------------------------------------
		public List<Aligns> GetListByAlignId(string LogFilePath, string LogFileName, byte AlignId)
		{
			List<Aligns> RetVal = new List<Aligns>();
			if (AlignId > 0)
			{
				string Conditions = "(AlignId = " + AlignId.ToString() + ")";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//--------------------------------------------------------------
		public List<Aligns> GetListByAlignName(string LogFilePath, string LogFileName, string AlignName)
		{
			List<Aligns> RetVal = new List<Aligns>();
			AlignName = StringUtils.Static_InjectionString(AlignName);
			if (!string.IsNullOrEmpty(AlignName))
			{
				string Conditions = "(AlignName = N'" + AlignName + "')";
				string OrderBy = "";
				return GetList(LogFilePath, LogFileName, Conditions, OrderBy);
			}
			return RetVal;
		}
		//-------------------------------------------------------------
		public Aligns Get(string LogFilePath, string LogFileName, byte AlignId)
		{
			Aligns RetVal = new Aligns(db.ConnectionString);
			try
			{
				List<Aligns> list = GetListByAlignId(LogFilePath, LogFileName, AlignId);
				if (list.Count > 0)
				{
					RetVal = (Aligns)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-------------------------------------------------------------
		public Aligns Get(string LogFilePath, string LogFileName, string AlignName)
		{
			Aligns RetVal = new Aligns(db.ConnectionString);
			try
			{
				List<Aligns> list = GetListByAlignName(LogFilePath, LogFileName, AlignName);
				if (list.Count > 0)
				{
					RetVal = (Aligns)list[0];
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
		//-----------------------------------------------------------------------
		public static string Static_GetName(string LogFilePath, string LogFileName, string constr, byte AllignId)
		{
			string RetVal = "";
			Aligns m_Aligns = new Aligns(constr);
			try
			{
				m_Aligns = m_Aligns.Get(LogFilePath, LogFileName, AllignId);
				if (!string.IsNullOrEmpty(m_Aligns.AlignName))
				{
					RetVal = m_Aligns.AlignName;
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + m_Aligns.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
			}
			return RetVal;
		}
	}
}
