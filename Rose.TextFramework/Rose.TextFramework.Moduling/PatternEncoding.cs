using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Rose.TextFramework.Moduling
{
    public abstract class PatternEncoding
    {
        protected PatternEncoding(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract EncodingResult Encode(string pattern, string input, Dictionary<string, object> resources);

        public EncodingResult Encode(string pattern, string input)
        {
            return Encode(pattern, input, new Dictionary<string, object>());
        }
    }

    public class EncodingResult
    {
        public EncodingResult()
        {
            EncodeData = new Dictionary<string, object>();
        }

        public EncodingResult(bool result)
        {
            Result = result;
            EncodeData = new Dictionary<string, object>();
        }

        public bool Result { get; set; }
        public Dictionary<string, object> EncodeData { get;  set; } 
    }

    public class BaseEncoding : PatternEncoding
    {
        public BaseEncoding() : base("base")
        {
            
        }

        public override EncodingResult Encode(string origin, string s2, Dictionary<string, object> resources)
        {
            return new EncodingResult(String.Equals(origin, s2, StringComparison.CurrentCultureIgnoreCase));
        }
    }

    public class RegexEncoding : PatternEncoding
    {
        public RegexEncoding() : base("regex")
        {
        }

        public override EncodingResult Encode(string pattern, string input, Dictionary<string, object> resources)
        {
            return new EncodingResult(Regex.IsMatch(pattern, input));
        }
    }

}
