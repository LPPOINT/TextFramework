using System;

namespace Rose.TextFramework.RoseMark.DictionaryModel
{
    public class DictionaryParseException : Exception
    {
        public DictionaryParseException()
        {
        }

        public DictionaryParseException(string message) : base(message)
        {
        }

        public DictionaryParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
