using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using Lib.Utilities;
public partial class admin_showspmenu : System.Web.UI.UserControl
{
    protected string LogFilePath = MyConstants.LogFilePath;
    protected string LogFileName = MyConstants.LogFileName;
    protected StringBuilder strMenu =new StringBuilder();
    protected string banner = "";
    protected string Fullname = "";
    private string UserName = "";
    private string UserPass = "";
    protected string IpAddress = "";
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string redirect = "";
        try
        {
            UserPass = (Session["UserPass"] == null) ? "" : Session["UserPass"].ToString();
            IpAddress = Request.UserHostAddress.ToString();
            
        }
        catch (Exception ex)
        {
            LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
        }
        if (!string.IsNullOrEmpty(redirect))
        {
            Response.Redirect(redirect);
        }
    }
}