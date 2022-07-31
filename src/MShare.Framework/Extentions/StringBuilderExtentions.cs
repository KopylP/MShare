using System;
using System.Text;

namespace MShare.Framework.Extentions
{
    public static class StringBuilderExtentions
    {
        public static StringBuilder AppendIf(this StringBuilder stringBuilder, bool condition, string str)
        {
            if (condition)
            {
                stringBuilder.Append(str);
            }

            return stringBuilder;
        }
    }
}

