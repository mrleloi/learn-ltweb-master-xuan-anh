using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using Lib.Utilities;
public partial class LoginPage : System.Web.UI.Page
{
	protected string LogFilePath = MyConstants.LogFilePath;
	protected string LogFileName = MyConstants.LogFileName;
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (!IsPostBack)
			{
				m_login.Focus();
				if (Request.Cookies["myCookie"] != null)
				{
					HttpCookie icookie = Request.Cookies.Get("myCookie");
					m_login.UserName = icookie.Values["uname"].ToString();
				}
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
	}

	protected void m_login_Authenticate(object sender, AuthenticateEventArgs e)
	{
		try
		{
			string UserName = m_login.UserName;
			string UserPass = m_login.Password;
			if ((!string.IsNullOrEmpty(UserName)) && (!string.IsNullOrEmpty(UserPass)))
			{
				if (DoLogin(UserName, UserPass))
				{
					m_login.DestinationPageUrl = MyConstants.PRJ_ROOT + "Pages/sites/AdmCategories.aspx";
					e.Authenticated = true;
				}				
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
	}
	//------------------------------------------------------------------------------------
	private bool DoLogin(string UserName, string UserPass)
	{
		bool retVal = false;
		try
		{
			string IpAddress = Request.UserHostAddress;
			int ActUserId = MyConstants.ActUserId;
			short DefaultActionId = 0;
			long UserLogId = 0;
			string FullName = "";
			retVal =true;
			if (retVal)
			{
				Session["UserName"] = UserName;
				Session["ActUserId"] = ActUserId.ToString();
				Session["DefaultActionId"] = DefaultActionId.ToString();
				Session["UserLogId"] = UserLogId.ToString();
				Session["FullName"] = FullName;
				Session["LogString"] = UserLogId.ToString() + "_" + ActUserId.ToString() + "_" + UserName;
				CheckBox rm = (CheckBox)m_login.FindControl("RememberMe");
				if (rm.Checked)
				{
					HttpCookie myCookie = new HttpCookie("myCookie");
					Response.Cookies.Remove("myCookie");
					Response.Cookies.Add(myCookie);
					myCookie.Values.Add("uname", this.m_login.UserName);
					myCookie.Values.Add("pass", this.m_login.Password);
					DateTime dtExpiry = DateTime.Now.AddDays(15);
					Response.Cookies["myCookie"].Expires = dtExpiry;
				}
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
		return retVal;
	}
}
