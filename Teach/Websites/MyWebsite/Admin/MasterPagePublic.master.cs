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

public partial class MasterPagePublic : System.Web.UI.MasterPage
{
    protected string MAIL_USER = MyConstants.MAIL_USER;
    protected string OFFICE_ADDRESS = MyConstants.OFFICE_ADDRESS;
    protected string PHONE_USER = MyConstants.PHONE_USER;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
