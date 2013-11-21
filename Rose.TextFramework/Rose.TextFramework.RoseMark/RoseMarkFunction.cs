using System;
using System.Collections.Generic;
using Rose.Common;

namespace Rose.TextFramework.RoseMark
{


    public class RoseMarkFunctionParseException : Exception
    {
        public RoseMarkFunctionParseException()
        {
        }

        public RoseMarkFunctionParseException(string message) : base(message)
        {
        }

        public RoseMarkFunctionParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class RoseMarkFunction
    {
        public string FunctionName { get; set; }
        public string[] FunctionArgs { get; set; }
        public Dictionary<string, string> FunctionAttributes { get; set; }

        private static string[] ParseArgsLine(string argsLine)
        {
            var inStr = false;
            var result = new List<string>();
            var current = string.Empty;
            foreach (var ch in argsLine)
            {
                if (ch == ',' && !inStr)
                {
                    result.Add(current);
                    current = string.Empty;
                }

                else if (ch == '"' && !inStr)
                    inStr = true;
                else if (ch == '"' && inStr)
                    inStr = false;
                else
                {
                    if(ch != ' ' || (ch == ' ' && inStr))
                        current += ch;
                }
            }

            if(current != string.Empty)
                result.Add(current);

            return result.ToArray();
        }

        public static RoseMarkFunction Parse(string str)
        {

           // str = str.RemoveOutherSpaces();

            if(!str.StartsWith("[") || !str.EndsWith("]"))
                throw new ArgumentException( str +  ": Функция должна начинаться с '[' и кончаться на ']'");
            var functionName = string.Empty;
            int index;
            for (index = 1; index < str.Length; index++)
            {
                if (str[index] == '(')
                {
                    index += 1;
                    break;
                }
                functionName += str[index];
            }

            var function = new RoseMarkFunction();

            function.FunctionName = functionName;
            var args = string.Empty;
            var miss = 0;
            for (index = index; index < str.Length; index++)
            {
                if (str[index] == ')' && miss == 0)
                {
                    index += 1;
                    break;
                }
                if(str[index] == ')' && miss != 0)
                {
                    miss--;
                }
                if (str[index] == '(')
                {
                    miss++;
                }
                args += str[index];
            }

            function.FunctionArgs = ParseArgsLine(args);

            // 0 - ничего; 1 - название; 2 - значение

            var currentState = 0;
            var currentName = string.Empty;
            var miss2 = 0;
            var currentValue = string.Empty;
            var attributes = new Dictionary<string, string>();

            for (index = index; index < str.Length; index++)
            {

                if(str[index] == ')' || str[index] == ']')
                    break;

                if (str[index] != ' ' && currentState == 0)
                {
                    currentState = 1;
                    currentName += str[index];
                }
                else if (str[index] != ' ' && currentState == 2 && str[index] != '"')
                {
                    currentValue += str[index];
                }
                else if (str[index] == '"' && index != 0 && str[index - 1] == '=' && currentState == 1)
                {
                    currentState = 2;
                }
                else if(currentState == 1 && str[index] != ' ' && str[index] != '=')
                {
                    currentName += str[index];
                }
                else if (str[index] == '"' && currentState == 2)
                {
                    currentState = 0;
                    attributes.Add(currentName, currentValue);
                    currentName = string.Empty;
                    currentValue = string.Empty;
                }


            }


            function.FunctionAttributes = attributes;

            return function;
        }

    }
}
