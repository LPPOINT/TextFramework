using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Rose.TextFramework.Parsing
{
    public class Token
    {
        private Token(string fullText)
        {
            FullText = fullText;
            Areas = new Dictionary<string, string>();
        }

        public string FullText { get; private set; }
        public Dictionary<string, string> Areas { get; private set; }

        public static Token Parse(string fullText)
        {
            try
            {
                var token = new Token(fullText);
                var s = Regex.Escape(fullText);
                var matches = Regex.Matches(s, "== .*? ==");

                foreach (Match match in matches)
                {
                    var arr = match.Value.Split(' ');
                    var name = arr[0];
                    var value = arr.Aggregate(string.Empty, (current, t) => current + t);
                    token.Areas.Add(name, value);
                }

                return token;
            }
            catch (Exception e)
            {
                return new Token(fullText);
            }

        }

    }
}
