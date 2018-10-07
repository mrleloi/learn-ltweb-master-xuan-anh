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
public partial class MasterPageAdmin : System.Web.UI.MasterPage
{
	protected string LogFilePath = MyConstants.LogFilePath;
	protected string LogFileName = MyConstants.LogFileName;
	protected string ROOT_PATH = MyConstants.ROOT_PATH;
	protected string MAIL_USER = MyConstants.MAIL_USER;
	protected string OFFICE_ADDRESS = MyConstants.OFFICE_ADDRESS;
	protected string PHONE_USER = MyConstants.PHONE_USER;
	protected void Page_Load(object sender, EventArgs e)
	{
		
	}
}
