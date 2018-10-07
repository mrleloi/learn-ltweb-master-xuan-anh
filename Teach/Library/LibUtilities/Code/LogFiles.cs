using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
namespace Lib.Utilities
{
	public class LogFiles
	{
		private static object o = new object();
		private static Mutex mut = new Mutex();
		private static StreamWriter m_StreamWriter;
		//----------------------------------------------------------------------------
		public static void WriteLog(string LineContent, string Path, string FileName)
		{
			try
			{
				DateTime dateTime = DateTime.Now;
				string LineHeader = DateTimeUtils.Static_yyyymmddhhmissms(dateTime);
				FileName = FileName + DateTimeUtils.Static_YYYYMMDDHH(dateTime) + ".log";
				WriteOrAppend(LineHeader, LineContent, Path, FileName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//----------------------------------------------------------------------------
		public static void WriteOrAppend(string LineHeader, string LineContent, string Path, string FileName)
		{
			try
			{
				if (!string.IsNullOrEmpty(FileName))
				{
					if (!Path.EndsWith("\\")) Path = Path + "\\";
					string PathFileName = Path + FileName;
					if (!string.IsNullOrEmpty(LineHeader))
					{
						LineContent = LineHeader + "-" + LineContent;
					}
					if (!Directory.Exists(Path))
					{
						Directory.CreateDirectory(Path);
					}
					if (!File.Exists(PathFileName))
					{
						FileStream fs = File.Create(PathFileName);
						fs.Close();
					}
					mut.WaitOne();
					lock (o)
					{
						m_StreamWriter = File.AppendText(PathFileName);
						m_StreamWriter.WriteLine(LineContent);
						m_StreamWriter.Flush();
						m_StreamWriter.Close();
					}
					mut.ReleaseMutex();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}		
	}
}
