using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using Lib.Web;
using Lib.Utilities;
public partial class admin_pages_admin_articles_InsertImageToMediaItem : System.Web.UI.Page
{
	protected string WEB_CONSTR = MyConstants.WEB_CONSTR;
	protected string LogFilePath = MyConstants.LogFilePath;
	protected string LogFileName = MyConstants.LogFileName;
	private Images m_Images;
	protected int ActUserId;
	protected string imageFileName;
	protected short height;
	protected short width;
	protected string imageAlign;
	protected string imageTypeName;
	protected bool imageUploaded;
	private Aligns m_Aligns;
	private ImageTypes m_ImageTypes;
	protected string IpAddress = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			m_Images = new Images(WEB_CONSTR);
			m_Aligns = new Aligns(WEB_CONSTR);
			m_ImageTypes = new ImageTypes(WEB_CONSTR);
			IpAddress = Context.Request.UserHostAddress;
			imageUploaded = false;
			if (!IsPostBack)
			{
				cboAligns.DataSource = m_Aligns.GetList(LogFilePath, LogFileName);
				cboAligns.DataBind();
				cboImageTypes.DataSource = m_ImageTypes.GetList(LogFilePath, LogFileName);
				cboImageTypes.DataBind();
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}		
	}
	//-----------------------------------------------------------
	protected void btnInsertImage_Click(object sender, EventArgs e)
	{
		try
		{
			m_Images.ImageId = 0;
			m_Images.ImageFileSize = uploadImageFile.PostedFile.FileName.Length;
			if (m_Images.ImageFileSize > 0)
			{
				DateTime CrDateTime = System.DateTime.Now;
				m_Images.ImageHeight = 100;
				m_Images.ImageWidth = 100;
				m_Images.ImageDesc = txtDescription.Text;
				imageAlign = cboAligns.SelectedValue;
				m_Aligns = m_Aligns.Get(LogFilePath, LogFileName, imageAlign);
				m_Images.AlignId = m_Aligns.AlignId;
				imageTypeName = cboImageTypes.SelectedValue;
				m_ImageTypes = m_ImageTypes.Get(LogFilePath, LogFileName, imageTypeName);
				m_Images.ImageTypeId = m_ImageTypes.ImageTypeId;
				m_Images.CrUserId = ActUserId;
				m_Images.CrDateTime = CrDateTime;
				string date_path = DateTimeUtils.Static_yyyymm(CrDateTime, "/") + "/";
				string saveLocation = "";
				string WorkingDirectory = Server.MapPath("~");
				string ext = System.IO.Path.GetExtension(uploadImageFile.PostedFile.FileName);
				imageFileName = System.IO.Path.GetFileName(uploadImageFile.PostedFile.FileName).Replace(" ", "_");
				m_Images.ImageFileName = imageFileName;
				m_Images.ImagePath = "";
				m_Images.InsertNoInform(LogFilePath, LogFileName, MyConstants.DISTRIBUTED_PROCESS, IpAddress, ActUserId);
				if (m_Images.ImageId > 0)
				{
					string original_path = (WorkingDirectory + MyConstants.ORIGINAL_IMAGE_DIR).Replace("\\\\", "\\");
					FileUtils.MakeYYYY_MM(LogFilePath, LogFileName, ref original_path, CrDateTime);
					string thumbnail_path = (WorkingDirectory + MyConstants.THUMBNAIL_IMAGE_DIR).Replace("\\\\", "\\");
					FileUtils.MakeYYYY_MM(LogFilePath, LogFileName, ref thumbnail_path, CrDateTime);
					string icon_path = (WorkingDirectory + MyConstants.ICON_IMAGE_DIR).Replace("\\\\", "\\");
					FileUtils.MakeYYYY_MM(LogFilePath, LogFileName, ref icon_path, CrDateTime);
					switch (m_Images.ImageTypeId)
					{
						case SystemConstants.ImageTypes_SetTopImage:
							{
								saveLocation = thumbnail_path;
								break;
							}
						case SystemConstants.ImageTypes_OriginalImage:
							{
								saveLocation = original_path;
								break;
							}
						case SystemConstants.ImageTypes_Level2Image:
							{
								saveLocation = thumbnail_path;
								break;
							}
					}
					if (FileUtils.Static_IsImage(ext))
					{
						imageFileName = MyConstants.PREFIX_IMAGE + "_" + m_Images.ImageId.ToString() + "_" + imageFileName;
						m_Images.ImageFileName = imageFileName;
						uploadImageFile.PostedFile.SaveAs(saveLocation + "\\" + imageFileName);
						System.Drawing.Image imgPhotoVert = null;
						try
						{
							switch (m_Images.ImageTypeId)
							{
								case SystemConstants.ImageTypes_SetTopImage:
									{
										imgPhotoVert = System.Drawing.Image.FromFile(thumbnail_path + "\\" + imageFileName);
										break;
									}
								case SystemConstants.ImageTypes_OriginalImage:
									{
										imgPhotoVert = System.Drawing.Image.FromFile(original_path + "\\" + imageFileName);
										break;
									}
								case SystemConstants.ImageTypes_Level2Image:
									{
										imgPhotoVert = System.Drawing.Image.FromFile(thumbnail_path + "\\" + imageFileName);
										break;
									}
							}
						}
						catch (Exception ex)
						{
							LogFiles.WriteLog("03: " + ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
						}
						if (imgPhotoVert != null)
						{
							height = (short)imgPhotoVert.Height;
							width = (short)imgPhotoVert.Width;
							if (m_Images.ImageTypeId != SystemConstants.ImageTypes_SetTopImage)
							{
								System.Drawing.Image imgPhoto = null;
								height = (short)imgPhotoVert.Height;
								width = (short)imgPhotoVert.Width;
								if (height > width)
								{
									imgPhoto = ImageResize.Thumbnail(imgPhotoVert, MyConstants.H_IMG_WIDTH, MyConstants.H_IMG_HEIGHT);
								}
								else
								{
									if (height < width)
									{
										imgPhoto = ImageResize.Thumbnail(imgPhotoVert, MyConstants.V_IMG_WIDTH, MyConstants.V_IMG_HEIGHT);
									}
									else
									{
										imgPhoto = ImageResize.Thumbnail(imgPhotoVert, MyConstants.V_IMG_HEIGHT, MyConstants.V_IMG_HEIGHT);
									}
								}
								SaveImage(imgPhoto, thumbnail_path + "\\" + imageFileName, ImageFormat.Jpeg);
								imgPhoto.Dispose();
								System.Drawing.Image imgPhotoCrop = ImageResize.Crop(imgPhotoVert, MyConstants.ICON_WIDTH, MyConstants.ICON_HEIGHT, ImageResize.AnchorPosition.Center);
								SaveImage(imgPhotoCrop, icon_path + "\\" + imageFileName, ImageFormat.Jpeg);
								imgPhotoCrop.Dispose();
							}
							m_Images.ImageFileName = imageFileName;
							m_Images.ImageHeight = height;
							m_Images.ImageWidth = width;
							m_Images.ImagePath = saveLocation;
							m_Images.UpdateNoInform(LogFilePath, LogFileName, MyConstants.DISTRIBUTED_PROCESS, IpAddress, ActUserId);
						}
					}

				}
				string home_url = MyConstants.HOME_URL;
				string scriptReturn = "<script> \n" +
						 " function myReturnValue(){\n" +
						 "    var playerId = \"player_" + m_Images.ImageId.ToString() + "\";\n" +
						 "    var url_home = \"" + home_url + "\";\n" +
						 "    var imgtype=document.all('cboImageTypes').value;\n" +
						 "    var imgDesc=document.all('txtDescription').value;\n" +
						 "    var imgTag=\"\";\n" +
						 "    var imageFileName= '" + imageFileName + "';\n" +
						 "    var imageAlign= '" + imageAlign + "';\n" +
						 "    var playerpath= \"" + home_url + MyConstants.MEDIA_PATH_DIR + "\";\n" +
						 "    var flvpath= \"" + home_url + MyConstants.FLV_FLASH_FOLDER + date_path + imageFileName + "\";\n" +
						 "    var imageThumbPath = \"" + MyConstants.THUMBNAIL_IMAGE_PATH + date_path + "\";\n" +
						 "    var imageOriginalPath=\"" + MyConstants.ORIGINAL_IMAGE_PATH + date_path + "\";\n" +
						 "    var url_imageThumb = url_home + imageThumbPath + imageFileName;\n" +
						 "    var url_imageOriginal = url_home + imageOriginalPath + imageFileName;\n" +
						 "    var h = '" + m_Images.ImageHeight.ToString() + "';\n" +
						 "    var w = '" + m_Images.ImageWidth.ToString() + "';\n" +
						 "    var tmpAlt = '';\n" +
						 "    if(imgtype==\"level2Image\"){\n" +
						 "      imgTag = '<DIV align=\"'+imageAlign+'\"><TABLE cellspacing=\"0\" cellpadding=\"3\" width=\"1\" align=\"'+imageAlign+'\" border=\"0\">';\n" +
						 "      imgTag += ' <TBODY><TR><TD>';\n" +
						 "      if ((w >= 200) || (h >= 200)){\n" +
						 "        imgTag += '<a onclick=\"return viewOriginalImage(this,'+h+','+w+')\" href=\"'+url_imageOriginal+'\">';\n" +
						 "        if (h > w){\n" +
						 "            imgTag += '<img '+tmpAlt+' src=\"'+url_imageThumb+'\" height=\"200\" border=\"1\">';\n" +
						 "        }\n" +
						 "        else{\n" +
						 "            imgTag += '<img '+tmpAlt+' src=\"'+url_imageThumb+'\" width=\"200\" border=\"1\">';\n" +
						 "        }\n" +
						 "        imgTag += '</a>';\n" +
						 "      }else{\n" +
						 "        if (h > w){\n" +
						 "            imgTag += '<img '+tmpAlt+' src=\"'+url_imageThumb+'\" height=\"150\" border=\"1\">';\n" +
						 "        }else{\n" +
						 "            imgTag += '<img '+tmpAlt+' src=\"'+url_imageThumb+'\" width=\"150\" border=\"1\">';\n" +
						 "        }\n" +
						 "      }\n" +
						 "            imgTag += ' </TD></TR><TR><TD class=\"Image\" align=\"left\">';\n" +
						 "            imgTag += '<font style=\"font-size:12px; font-family:arial\"><i>' + imgDesc + '</i></font>';\n" +
						 "            imgTag += '</TD></TR></TBODY></TABLE></DIV>';\n" +
						 "      }\n" +
						 "   else if(imgtype==\"originalImage\"){\n" +
						 "            imgTag = '<DIV align=\"'+imageAlign+'\"><TABLE cellspacing=\"0\" cellpadding=\"3\" width=\"1\" align=\"'+imageAlign+'\" border=\"0\">';\n" +
						 "            imgTag += ' <TBODY><TR><TD>';\n" +
						 "            imgTag += '<img name=\"imagePhoto\" src=\"'+url_imageOriginal+'\" width=\"' + w + '\" border=\"1\">';\n" +
						 "            imgTag += ' </TD></TR><TR><TD class=\"Image\" align=\"left\">';\n" +
						 "            imgTag += '<font style=\"font-size:12px; font-family:arial\"><i>' + imgDesc + '</i></font>';\n" +
						 "            imgTag += '</TD></TR></TBODY></TABLE></DIV>';\n" +
						 "          }\n" +
						 "   else if(imgtype==\"topImage\"){ //anh view cung Lead.\n" +
						 "            imgTag = '<DIV align=\"'+imageAlign+'\"><TABLE cellspacing=\"0\" cellpadding=\"3\" width=\"1\" align=\"'+imageAlign+'\" border=\"0\">';\n" +
						 "            imgTag += ' <TBODY><TR><TD>';\n" +
						 "            imgTag += '<img name=\"imagePhoto\" src=\"'+url_imageOriginal+'\" width=\"' + w + '\" border=\"1\">';\n" +
						 "            imgTag += ' </TD></TR><TR><TD class=\"Image\" align=\"left\">';\n" +
						 "            imgTag += '<font style=\"font-size:12px; font-family:arial\"><i>' + imgDesc + '</i></font>';\n" +
						 "            imgTag += '</TD></TR></TBODY></TABLE></DIV>';\n" +
						 "          }\n" +
						 "   else if(imgtype==\"setTopImage\"){ // Anh noi bat.\n" +
						 "            imgTag = '<DIV align=\"'+imageAlign+'\"><TABLE cellspacing=\"0\" cellpadding=\"3\" width=\"1\" align=\"'+imageAlign+'\" border=\"0\">';\n" +
						 "            imgTag += ' <TBODY><TR><TD>';\n" +
						 "            imgTag += '<img name=\"imagePhoto\" src=\"'+url_imageThumb+'\" width=\"100\" height =\"100\"  border=\"1\">';\n" +
						 "            imgTag += ' </TD></TR><TR><TD class=\"Image\" align=\"left\">';\n" +
						 "            imgTag += '<font style=\"font-size:12px; font-family:arial\"><i>' + imgDesc + '</i></font>';\n" +
						 "            imgTag += '</TD></TR></TBODY></TABLE></DIV>';\n" +
						 "          }\n" +
						 "   else if(imgtype==\"LargeImageForLead\"){\n" +
						 "            imgTag = '<DIV align=\"'+imageAlign+'\"><TABLE cellspacing=\"0\" cellpadding=\"3\" width=\"1\" align=\"'+imageAlign+'\" border=\"0\">';\n" +
						 "            imgTag += ' <TBODY><TR><TD>';\n" +
						 "            imgTag += '<img name=\"imagePhoto\" src=\"'+url_imageOriginal+'\" width=\"' + w + '\" border=\"1\">';\n" +
						 "            imgTag += ' </TD></TR><TR><TD class=\"Image\" align=\"left\">';\n" +
						 "            imgTag += '<font style=\"font-size:12px; font-family:arial\"><i>' + imgDesc + '</i></font>';\n" +
						 "            imgTag += '</TD></TR></TBODY></TABLE></DIV>';\n" +
						 "          }\n" +
						 "  else if(imgtype==\"Clip-FLV\"){\n" +
						 "            imgTag = '<DIV align=\"'+imageAlign+'\"><TABLE cellspacing=\"0\" cellpadding=\"3\" width=\"1\" align=\"'+imageAlign+'\" border=\"0\">';\n" +
						 "            imgTag += ' <TBODY><TR><TD>';\n" +
						 "            imgTag +='<embed flashvars=\"file='+flvpath+'&amp;width=400&amp;height=300&amp;autostart=false&amp;volume=100&amp;repeat=false&amp;bufferlength=10\"';\n" +
						 "            imgTag +='allowscriptaccess=\"always\" allowfullscreen=\"true\" wmode=\"transparent\" quality=\"hight\"';\n" +
						 "            imgTag +='name=\"flvplayer\" id=\"flvplayer\" src=\"'+playerpath+'flvplayer.swf\"';\n" +
						 "            imgTag +='type=\"application/x-shockwave-flash\" width=\"400\" height=\"300\">';\n" +
						 "            imgTag += ' </TD></TR><TR><TD class=\"Image\" align=\"left\">';\n" +
						 "            imgTag += '<font style=\"font-size:12px; font-family:arial\"><i>' + imgDesc + '</i></font>';\n" +
						 "            imgTag += '</TD></TR></TBODY></TABLE></DIV>';\n" +
						 "          }\n" +
						 "      oEditor.FCK.InsertHtml(imgTag);\n" +
						 "      window.parent.Cancel();\n" +
						 "    }\n" +
						 " myReturnValue();" +
						 "</script>";
				imageUploaded = true;
				Page.ClientScript.RegisterStartupScript(this.GetType(), "VoteReturn", scriptReturn);
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog("04: " + ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
	}
	//-----------------------------------------------------------
	private void SaveImage(System.Drawing.Image ImgToSave, string FileName, ImageFormat mImageFormat)
	{
		try
		{
			if (!File.Exists(FileName))
			{
				if (ImgToSave == null)
				{
					LogFiles.WriteLog("Null" + FileName, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
				}
				else
				{
					ImgToSave.Save(FileName, mImageFormat);
				}
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
	}
}

