using System.Xml;
using System.Xml.Linq;

namespace Client.Tools
{
    public static class Tools
    {
        private static readonly Type[] WriteTypes = new[]
        {
            typeof(string), typeof(DateTime), typeof(Enum),
            typeof(decimal), typeof(Guid),
        };

        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive || WriteTypes.Contains(type);
        }

        public static XElement ToXml(this object input)
        {
            return input.ToXml(null);
        }

        public static XElement ToXml(this object input, string element)
        {
            if (input == null)
                return null;

            if (string.IsNullOrEmpty(element))
                element = "Request";
            element = XmlConvert.EncodeName(element);
            var ret = new XElement(element);

            if (input == null) return ret;
            var type = input.GetType();
            var props = type.GetProperties();
            ret.Add(new XElement("user", new XAttribute("Id", props[0].GetValue(input)), new XAttribute("Name", props[1].GetValue(input)),
                new XElement("Status", props[2].GetValue(input))));

            return ret;
        }

        public static void WriteLineWithColor(this string text, ConsoleColor color)
        {
            var startColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = startColor;
        }
    }
}
