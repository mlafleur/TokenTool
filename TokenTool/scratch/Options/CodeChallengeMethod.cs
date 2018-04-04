using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TokenTool.Microsoft.Options
{
    public enum CodeChallengeMethod
    {
        [Description("plain")]
        Plain,
        [Description("S256")]
        S256
    }

    public static class CodeChallengeMethodExtensions
    {
        public static string ToStringValue(this CodeChallengeMethod val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
