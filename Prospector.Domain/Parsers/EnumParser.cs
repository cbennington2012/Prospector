using System;
using System.ComponentModel;

namespace Prospector.Domain.Parsers
{
    public static class EnumParser
    {
        public static string GetDescription(Enum e)
        {
            var result = e.ToString();
            var type = e.GetType();
            var memberInfo = type.GetMember(e.ToString());

            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    result = ((DescriptionAttribute)attributes[0]).Description;
            }

            return result;
        }

        public static Enum GetEnumFromValue(string value, Type type)
        {
            return Enum.Parse(type, value ?? "0") as Enum;
        }
    }
}