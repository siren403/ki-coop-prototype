using System.Text;
using System.IO;

namespace Util
{
    public static class StringArrayToEnum
    {
        public static void Generate(string[] array, string name, string path, string nameSpace = null, string prefix = null)
        {
            if (array == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(path))
                return;

            bool isNameSpace = !string.IsNullOrEmpty(nameSpace);

            StringBuilder enumString = new StringBuilder();

            if (isNameSpace)
            {
                enumString.Append("\t");
            }

            enumString.AppendFormat("public enum {0}\n", name);

            if (isNameSpace)
            {
                enumString.Append("\t");
            }

            enumString.Append("{");

            foreach (var str in array)
            {
                enumString.Append("\n\t");

                if (isNameSpace)
                {
                    enumString.Append("\t");
                }

                if (string.IsNullOrEmpty(prefix) == false)
                    enumString.AppendFormat("{0}_", prefix);

                enumString.AppendFormat("{0},", str);
            }

            enumString.AppendLine();

            if (isNameSpace)
            {
                enumString.Append("\t");
            }

            enumString.Append("}");

            string result = enumString.ToString();
            enumString.Remove(0, enumString.Length);
            //Debug.Log(result);

            if (isNameSpace)
            {
                result = enumString
                    .AppendFormat("namespace {0}\n", nameSpace)
                    .Append("{")
                    .AppendLine()
                    .AppendFormat("{0}", result)
                    .AppendLine()
                    .Append("}")
                    .ToString();
            }

            //Debug.Log(result);
            //Debug.Log(path);
            //return;

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(result);
            }
        }
    }
}