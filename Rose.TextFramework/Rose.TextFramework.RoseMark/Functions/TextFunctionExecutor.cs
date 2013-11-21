using System;
using System.Collections.Generic;
using System.Linq;

namespace Rose.TextFramework.RoseMark.Functions
{
    public class TextFunctionExecutor : FunctionExecutor
    {
        public TextFunctionExecutor() : base("text")
        {
        }

        public override FunctionExecuteResult Execute(FunctionExecuteArgs args)
        {
            try
            {
                for (var i = 0; i < args.Args[0].Length; i++)
                {
                    if (char.ToLower(args.Args[0][i]) != char.ToLower(args.Input[i]))
                        return new FunctionExecuteResult(false, 0);
                }
                return new FunctionExecuteResult(true, args.Args[0].Length);
            }
            catch (Exception e)
            {
                return new FunctionExecuteResult(false, 0);
            }
        }
    }
}
