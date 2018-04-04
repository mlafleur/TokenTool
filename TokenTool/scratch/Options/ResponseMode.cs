using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TokenTool.Microsoft.Options
{
  


    public static class ResponseModeExtensions
    {
        public static string ToStringValue(this ResponseMode val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}