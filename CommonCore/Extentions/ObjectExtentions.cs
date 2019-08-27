using System;
using System.IO;
using System.Xml.Serialization;

namespace CommonCore.Extentions
{
    public static class ObjectExtentions
    {
        public static Boolean IsNull(this Object Expr)
        {
            return (Expr == null || Expr == DBNull.Value);
        }

        public static Int32? ToInt32Nullable(this Object Expr)
        {
            Int32? result = null;

            if (Expr.IsNull())
                return result;

            result = Expr is Enum ? (Int32)Expr : Convert.ToInt32(Expr);

            return result;
        }
        public static Int32 ToInt32(this Object Expr, Int32 DefaultValue = 0)
        {
            Int32 result = Expr is Enum ? (Int32)Expr : ToInt32Nullable(Expr) ?? DefaultValue;

            return result;
        }

        public static string SerializeObject(this Object Expr)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Expr.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, Expr);
                return textWriter.ToString();
            }
        }
    }
}
