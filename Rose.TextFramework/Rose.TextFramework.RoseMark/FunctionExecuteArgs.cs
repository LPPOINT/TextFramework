using System.Collections.Generic;
using System.Reflection;

namespace Rose.TextFramework.RoseMark
{
    public class FunctionExecuteArgs
    {
        public FunctionExecuteArgs(string[] args, Dictionary<string, string> attributes, FunctionExecutionProvider provider, ExpressionToken token, int currentTokenIndex, string input, Dictionary<string, object> resources)
        {
            Args = args;
            Attributes = attributes;
            Provider = provider;
            Token = token;
            CurrentTokenIndex = currentTokenIndex;
            Input = input;
            Resources = resources;
        }

        public FunctionExecuteArgs()
        {
            Resources = new Dictionary<string, object>();
        }

        public string[] Args { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public FunctionExecutionProvider Provider { get; set; }
        public ExpressionToken Token { get; set; }
        public int CurrentTokenIndex { get; set; }

        public Dictionary<string, object> Resources { get; set; }

        public string Input { get; set; }
    }
}
