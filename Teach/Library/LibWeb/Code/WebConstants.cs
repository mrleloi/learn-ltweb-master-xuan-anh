using System;
using System.Text;
using System.Configuration;
namespace Lib.Web
{
	public class WebConstants
	{
		public static string WEB_CONSTR = (ConfigurationManager.AppSettings["WEB_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["WEB_CONSTR"];
	}
}
