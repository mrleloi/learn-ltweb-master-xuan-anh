using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
namespace Lib.Utilities
{
	public class FileUtils
	{
		public static bool Static_IsImage(string Extension)
		{
			bool RetVal = false;
			if (!string.IsNullOrEmpty(Extension))
			{
				Extension = Extension.ToLower();
				switch (Extension)
				{
					case ".png":
						{
							RetVal = true;
							break;
						}
					case ".gif":
						{
							RetVal = true;
							break;
						}
					case ".jpeg":
						{
							RetVal = true;
							break;
						}
					case ".jpg":
						{
							RetVal = true;
							break;
						}
					case ".bmp":
						{
							RetVal = true;
							break;
						}
				}
			}
			return RetVal;
		}
		//---------------------------------------------
		public static bool Static_IsValidAttachedFileExtension(string Extension)
		{
			bool RetVal = false;
			if (!string.IsNullOrEmpty(Extension))
			{
				Extension = Extension.ToLower();
				switch (Extension)
				{
					case ".rar":
						{
							RetVal = true;
							break;
						}
					case ".zip":
						{
							RetVal = true;
							break;
						}
					case ".doc":
						{
							RetVal = true;
							break;
						}
					case ".docx":
						{
							RetVal = true;
							break;
						}
					case ".pdf":
						{
							RetVal = true;
							break;
						}
				}
			}
			return RetVal;
		}
		//---------------------------------------------
		public static bool Static_IsValidForAttached(string FileName)
		{
			bool RetVal = false;
			FileName = FileName.ToLower();
			if (!string.IsNullOrEmpty(FileName))
			{
				if (FileName.EndsWith(".rar"))
				{
					RetVal = true;
				}
				else
				{
					if (FileName.EndsWith(".zip"))
					{
						RetVal = true;
					}
					else
					{
						if (FileName.EndsWith(".doc"))
						{
							RetVal = true;
						}
						else
						{
							if (FileName.EndsWith(".docx"))
							{
								RetVal = true;
							}
							else 
							{
								if (FileName.EndsWith(".pdf"))
								{
									RetVal = true;
								}
							}
						}
					}
				}
			}
			return RetVal;
		}
		//---------------------------------------------
		public static void DeleteFile(string LogFilePath, string LogFileName, string FileName)
		{
			try
			{
				if (File.Exists(FileName))
				{
					File.Delete(FileName);
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + ".FileUtils." + MethodBase.GetCurrentMethod().Name);
			}
		}
		//----------------------------------------------------------------------------
		public static void DeleteFile(string LogFilePath, string LogFileName, string SourcePath, string FileName)
		{

			try
			{
				FileName = (SourcePath.EndsWith("\\")) ? SourcePath + FileName : SourcePath + "\\" + FileName;
				DeleteFile(LogFilePath, LogFileName, FileName);
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + ".FileUtils." + MethodBase.GetCurrentMethod().Name);
			}
		}
		//----------------------------------------------------------------------------
		public static void MoveFile(string LogFilePath, string LogFileName, string SourcePath, string FileName, string DestPath)
		{

			try
			{
				string OriginalFile = (SourcePath.EndsWith("\\")) ? SourcePath + FileName : SourcePath + "\\" + FileName;
				if (File.Exists(OriginalFile))
				{
					string DestinationFile = (DestPath.EndsWith("\\")) ? DestPath + FileName : DestPath + "\\" + FileName;
					File.Move(OriginalFile, DestinationFile);
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog("SourcePath: " + SourcePath + " FileName: " + FileName + "DestPath: " + DestPath + " >>" + ex.Message, LogFilePath + "\\Exception", LogFileName + ".FileUtils." + MethodBase.GetCurrentMethod().Name);
			}
		}
		//----------------------------------------------------------------------------
		public static bool FileExists(string LogFilePath, string LogFileName, string FileName, FileInfo[] l_FileInfo)
		{
			bool retVal = false;
			try
			{
				if (l_FileInfo.Length > 0)
				{
					for (int i = 0; i < l_FileInfo.Length; i++)
					{
						if (l_FileInfo[i].Name == FileName)
						{
							retVal = true;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + ".FileUtils." + MethodBase.GetCurrentMethod().Name);
			}
			return retVal;
		}
		//---------------------------------------------------
		public static void MakeDir(string LogFilePath, string LogFileName, string DirectorName)
		{
			try
			{
				if (!Directory.Exists(DirectorName))
				{
					Directory.CreateDirectory(DirectorName);
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + ".FileUtils." + MethodBase.GetCurrentMethod().Name);
			}
		}
		//-------------------------------------------------------
		public static void MakeDir(string LogFilePath, string LogFileName, string RootPath, DateTime time)
		{
			try
			{
				if (time != null)
				{
					if (Directory.Exists(RootPath))
					{
						RootPath = (RootPath.EndsWith("\\")) ? RootPath + time.Year.ToString() : RootPath + "\\" + time.Year.ToString();
						MakeDir(LogFilePath, LogFileName, RootPath);
						if (Directory.Exists(RootPath))
						{
							RootPath = RootPath + ((time.Month < 10) ? "0" + time.Month.ToString() : time.Month.ToString());
							MakeDir(LogFilePath, LogFileName, RootPath);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + ".FileUtils." + MethodBase.GetCurrentMethod().Name);
			}
		}
		//-------------------------------------------------------
		public static void MakeYYYY_MM(string LogFilePath, string LogFileName,ref string RootPath, DateTime time)
		{
			try
			{
				if (time != null)
				{
					if (Directory.Exists(RootPath))
					{
						RootPath = (RootPath.EndsWith("\\")) ? RootPath + time.Year.ToString() : RootPath + "\\" + time.Year.ToString();
						MakeDir(LogFilePath, LogFileName, RootPath);
						if (Directory.Exists(RootPath))
						{
							RootPath = RootPath + "\\" + ((time.Month < 10) ? "0" + time.Month.ToString() : time.Month.ToString());
							MakeDir(LogFilePath, LogFileName, RootPath);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + ".FileUtils." + MethodBase.GetCurrentMethod().Name);
			}
		}
	}
}
