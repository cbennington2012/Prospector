using System;
using System.ComponentModel.DataAnnotations;

namespace Prospector.Presentation.Formatters
{
    public static class SplitStringFormatter
    {
        public static String Convert(String input)
        {
            var splitInput = input.ToCharArray();
            var result = "";

            foreach (var item in splitInput)
            {
                if (Char.IsUpper(item))
                {
                    result = result + " " + item;
                }
                else
                {
                    result += item;
                }
            }

            return result.Trim();
        }
    }
}
