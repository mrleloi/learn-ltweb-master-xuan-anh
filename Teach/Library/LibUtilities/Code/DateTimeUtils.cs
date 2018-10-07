using System;
namespace Lib.Utilities
{
	public class DateTimeUtils
	{
		public DateTimeUtils()
		{
		}
		//---------------------------------------------------------------------
		public static string Static_yyyymm(DateTime dateTime, string Delimiter)
		{
			string retVal = "";
			retVal = dateTime.Year.ToString() + Delimiter;
			if (dateTime.Month <= 9) retVal += "0";
			retVal += dateTime.Month.ToString();
			return retVal;
		}
		//---------------------------------------------------------------------
		public static string Static_yyyymmddhhmissms(DateTime dateTime)
		{
			string retVal = Static_yyyymmddhhmiss(dateTime);
			int Ms = dateTime.Millisecond;
			if (Ms <= 9)
			{
				retVal += "00";
			}
			else
			{
				if (Ms <= 99)
				{
					retVal += "0";
				}
			}
			retVal += Ms.ToString();
			return retVal;
		}
		//---------------------------------------------------------------------
		public static string Static_yyyymmddhhmiss(DateTime dateTime)
		{
			string DateDelimiter = "";
			string TimeDelimiter = "";
			return Static_yyyymmddhhmiss(dateTime, DateDelimiter, TimeDelimiter);
		}
		//---------------------------------------------------------------------
		public static string Static_yyyymmddhhmiss(DateTime dateTime, string DateDelimiter, string TimeDelimiter)
		{
			string day = "";
			if (DateDelimiter == null) DateDelimiter = "";
			if (TimeDelimiter == null) TimeDelimiter = "";
			string Gap = (DateDelimiter.Length > 0) ? " " : "";
			if (dateTime == DateTime.MinValue)
			{
				day = "";
			}
			else
			{
				day = dateTime.Year.ToString() + DateDelimiter;
				if (dateTime.Month <= 9) day += "0";
				day += dateTime.Month.ToString();
				day += DateDelimiter;
				if (dateTime.Day <= 9) day += "0";
				day += dateTime.Day.ToString();
				day += Gap;
				if (dateTime.Hour <= 9) day += "0";
				day += dateTime.Hour.ToString();
				day += TimeDelimiter;
				if (dateTime.Minute <= 9) day += "0";
				day += dateTime.Minute.ToString();
				day += TimeDelimiter;
				if (dateTime.Second <= 9) day += "0";
				day += dateTime.Second.ToString();
			}
			return day;
		}
		//---------------------------------------------------------------------
		public static string Static_YYYYMMDDHH(DateTime dateTime)
		{
			string DateDelimiter = "";
			string RetVal = Static_YYYYMMDD(dateTime, DateDelimiter);
			if (dateTime.Hour <= 9)
			{
				RetVal += "0";
			}
			RetVal += dateTime.Hour.ToString();
			return RetVal;
		}
		//---------------------------------------------------------------------
		public static string Static_YYYYMMDD(DateTime dateTime, string DateDelimiter)
		{
			string day = "";
			try
			{
				if (DateDelimiter == null) DateDelimiter = "";
				if (dateTime == DateTime.MinValue)
				{
					day = "";
				}
				else
				{
					day = dateTime.Year.ToString() + DateDelimiter;
					if (dateTime.Month <= 9)
					{
						day = day + "0";
					}
					day = day + dateTime.Month.ToString() + DateDelimiter;
					if (dateTime.Day <= 9)
					{
						day = day + "0";
					}
					day = day + dateTime.Day.ToString();
				}
			}
			catch
			{

			}
			return day;
		}
		//---------------------------------------------------------------------
		public static string Static_YYYYMMDD(DateTime dateTime)
		{
			string DateDelimiter = "";
			return Static_YYYYMMDD(dateTime, DateDelimiter);
		}
	}
}