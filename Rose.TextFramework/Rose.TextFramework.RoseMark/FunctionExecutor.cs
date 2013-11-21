using System.Collections.Generic;

namespace Rose.TextFramework.RoseMark
{
    public abstract class FunctionExecutor
    {
        protected FunctionExecutor(string functionName)
        {
            FunctionName = functionName;
        }

        public string FunctionName { get; private set; }

        public abstract FunctionExecuteResult Execute(FunctionExecuteArgs args);
    }
}
