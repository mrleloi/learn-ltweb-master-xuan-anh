using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MyConstants
/// </summary>
public  class MyConstants
{
	public static string ROOT_PATH = ConfigurationManager.AppSettings["ROOT_PATH"];
	public static string PRJ_ROOT = ROOT_PATH + "admin/"; //admin root path
	public static byte DISTRIBUTED_PROCESS = Convert.ToByte((ConfigurationManager.AppSettings["DISTRIBUTED_PROCESS"] == null) ? "0" : ConfigurationManager.AppSettings["DISTRIBUTED_PROCESS"]);
	public static int ActUserId = 777;

	public static string LogFilePath = (ConfigurationManager.AppSettings["LogFilePath"] == null) ? "" : ConfigurationManager.AppSettings["LogFilePath"].ToString();
	public static string LogFileName = (ConfigurationManager.AppSettings["LogFileName"] == null) ? "" : ConfigurationManager.AppSettings["LogFileName"].ToString();
	
	public static string MAIL_USER = (ConfigurationManager.AppSettings["MAIL_USER"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_USER"];
	public static string OFFICE_ADDRESS = (ConfigurationManager.AppSettings["OFFICE_ADDRESS"] == null) ? "" : ConfigurationManager.AppSettings["OFFICE_ADDRESS"];
	public static string PHONE_USER = (ConfigurationManager.AppSettings["PHONE_USER"] == null) ? "" : ConfigurationManager.AppSettings["PHONE_USER"];

	public static string WEB_CONSTR = (ConfigurationManager.AppSettings["WEB_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["WEB_CONSTR"];

	public static string ICON_IMAGE_DIR =(ConfigurationManager.AppSettings["ICON_IMAGE_FOLDER"]==null)?"": ConfigurationManager.AppSettings["ICON_IMAGE_FOLDER"].Replace("/", "\\");
	public static string HOME_URL = (ConfigurationManager.AppSettings["HOME_URL"]==null)? "":ConfigurationManager.AppSettings["HOME_URL"].Trim();
	
	public static string PREFIX_IMAGE = ConfigurationManager.AppSettings["PREFIX_IMAGE"];
	public static string THUMBNAIL_IMAGE_DIR = (ConfigurationManager.AppSettings["THUMBNAIL_IMAGE_FOLDER"]==null)?"":ConfigurationManager.AppSettings["THUMBNAIL_IMAGE_FOLDER"].Replace("/", "\\");
	public static short H_IMG_HEIGHT = Convert.ToInt16((ConfigurationManager.AppSettings["H_IMG_HEIGHT"]==null)?"100":ConfigurationManager.AppSettings["H_IMG_HEIGHT"].Trim());
	public static short H_IMG_WIDTH = Convert.ToInt16(ConfigurationManager.AppSettings["H_IMG_WIDTH"]);
	public static short V_IMG_HEIGHT = Convert.ToInt16(ConfigurationManager.AppSettings["V_IMG_HEIGHT"]);
	public static short V_IMG_WIDTH = Convert.ToInt16(ConfigurationManager.AppSettings["V_IMG_WIDTH"]);
	public static short ICON_HEIGHT = Convert.ToInt16(ConfigurationManager.AppSettings["ICON_HEIGHT"]);
	public static short ICON_WIDTH = Convert.ToInt16(ConfigurationManager.AppSettings["ICON_WIDTH"]);
	public static short IMG_LEVEL2_HEIGHT = Convert.ToInt16(ConfigurationManager.AppSettings["IMG_LEVEL2_HEIGHT"]);
	public static short IMG_LEVEL2_WIDTH = Convert.ToInt16(ConfigurationManager.AppSettings["IMG_LEVEL2_WIDTH"]);
	public static string MEDIA_FOLDER = (ConfigurationManager.AppSettings["MEDIA_FOLDER"] != null) ? ConfigurationManager.AppSettings["MEDIA_FOLDER"] : "newsimage/flash/";
	public static string MEDIA_PATH_DIR = ROOT_PATH + MEDIA_FOLDER;
	public static string FLV_FLASH_FOLDER = (ConfigurationManager.AppSettings["FLV_FLASH_FOLDER"] != null) ? ConfigurationManager.AppSettings["FLV_FLASH_FOLDER"] : "newsimage/flash/";
	public static string FLV_FLASH_PATH = ROOT_PATH + FLV_FLASH_FOLDER;
	public static string THUMBNAIL_IMAGE_FOLDER = (ConfigurationManager.AppSettings["THUMBNAIL_IMAGE_FOLDER"] == null) ? "" : ConfigurationManager.AppSettings["THUMBNAIL_IMAGE_FOLDER"];
	public static string THUMBNAIL_IMAGE_PATH = ROOT_PATH + ((ConfigurationManager.AppSettings["THUMBNAIL_IMAGE_FOLDER"]==null)?"":ConfigurationManager.AppSettings["THUMBNAIL_IMAGE_FOLDER"].Trim());
	public static string ORIGINAL_IMAGE_PATH = (ConfigurationManager.AppSettings["ORIGINAL_IMAGE_FOLDER"]==null)?"":ConfigurationManager.AppSettings["ORIGINAL_IMAGE_FOLDER"];
	public static string ORIGINAL_IMAGE_DIR = (ConfigurationManager.AppSettings["ORIGINAL_IMAGE_FOLDER"]==null)?"":ConfigurationManager.AppSettings["ORIGINAL_IMAGE_FOLDER"].Replace("/", "\\");
	

}
