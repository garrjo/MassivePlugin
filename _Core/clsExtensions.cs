using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsExtensions
/// </summary>
namespace Massive
{
    public static class Extensions
    {
        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }
        public static bool IsNumeric(this String str)
        {
            try
            {
                Double.Parse(str.ToString());
                return true;
            }
            catch { }
            return false;
        }
        public static Int32 ToInt32(this string strVar)
        {
            int intOutput = 0;
            int.TryParse(strVar, out intOutput);
            return intOutput;
        }
        public static string StatusLookup(this string Status)
        {
            String status = String.Empty;
            int intStatus = 0;
            int.TryParse(Status,out intStatus);
            switch (intStatus)
            {
                default:
                case 0:
                    status = "<span class='bold text-info'>Incomplete</span>";
                break;
                case 1:
                status = "<span class='bold text-success'>Active</span>";
                break;
                case 2:
                status = "<span class='bold text-warning'>Inactive</span>";
                break;
                case 3:
                status = "<span class='bold text-danger'>Ended</span>";
                break;
                case 999:
                status = "<span class='bold text-danger'>Cancelled</span>";
                break;
            }
            return status;
        }
    }
}
