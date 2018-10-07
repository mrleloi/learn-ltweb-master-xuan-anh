using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Reflection;
using Lib.Utilities;
public partial class _Default : System.Web.UI.Page
{
	protected string LogFilePath = MyConstants.LogFilePath;
	protected string LogFileName = MyConstants.LogFileName;
	private int ActUserId = 0;
	protected void Page_Load(object sender, EventArgs e)
	{
		string Redirect = "";
		try
		{
			if (!IsPostBack)
			{
				Redirect = MyConstants.PRJ_ROOT + "pages/Sites/AdmCategories.aspx";
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
		if (!string.IsNullOrEmpty(Redirect))
		{
			Response.Redirect(Redirect);
		}
	}
}