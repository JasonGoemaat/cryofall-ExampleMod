using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMod.Scripts.Models
{
    public static class CustomSerializer
    {
        public static void Serialize<T>(StringBuilder sb, string name, T value)
        {
            if (value is null)
            {
                sb.AppendFormat("\"{0}\":null,", name);
            }
            else if (value is string)
            {
                sb.AppendFormat("\"{0}\":\"{1}\",", name, (value as string).Replace("\"", "\\\""));
            }
            else if (value is TimeSpan)
            {
                sb.AppendFormat("\"{0}\":\"{1}\",", name, value);
            }
            else if (value is bool)
            {
                bool? boolValue = value as bool?;

                sb.AppendFormat("\"{0}\":\"{1}\",", name, (boolValue == true) ? "true" : "false");
            } else
            {
                sb.AppendFormat("\"{0}\":{1},", name, value);
            }
        }

        public static void SerializeArray(StringBuilder sb, string name, IList<ICustomSerializable> list)
        {
            sb.AppendLine(string.Format("\"{0}\": [", name));
            foreach (var item in list)
            {
                sb.AppendLine(string.Format("    {0},", item.Serialize()));
            }
            sb.Length--; // remove lsat lf
            sb.Length--; // remove lsat cr
            sb.Length--; // remove last comma
            sb.AppendLine("],");
        }
    }
}
