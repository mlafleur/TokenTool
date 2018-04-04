using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TokenTool.Microsoft.Options
{
    public enum ResponseType
    {
        [Description("code")]
        Code,

        [Description("id_token")]
        IdToken,

        [Description("id_token code")]
        IdToken_Code
    }

    public static class ResponseTypeExtensions
    {
        public static string ToStringValue(this ResponseType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}