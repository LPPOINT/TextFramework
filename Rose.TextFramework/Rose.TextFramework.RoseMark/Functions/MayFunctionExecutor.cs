using System;
using System.Collections.Generic;

namespace Rose.TextFramework.RoseMark.Functions
{
    public class MayFunctionExecutor : FunctionExecutor
    {
        public MayFunctionExecutor() : base("may")
        {
        }

        public override FunctionExecuteResult Execute(FunctionExecuteArgs args)
        {
            try
            {

                var argss = new List<string>();
                if (args.Attributes.ContainsKey("type") && args.Attributes["type"].ToLower() == "word")
                {
                    foreach (var a in args.Args)
                    {
                        var fa = string.Empty;
                        if (!a.StartsWith(" ") && args.CurrentTokenIndex != 0)
                        {
                            fa += " ";
                        }
                        fa += a;
                        if (!a.EndsWith(" "))
                        {
                            fa += " ";
                        }
                        argss.Add(fa);
                    }
                }

                else
                {
                    argss.AddRange(args.Args);
                }

                args.Args = argss.ToArray();

                var any = new AnyFunctionExecutor();
                var result = any.Execute(args);
                if (result.IsMatch)
                {
                    return result;
                }
                return new FunctionExecuteResult(true, 0);
            }
            catch (Exception e)
            {
                return new FunctionExecuteResult(true, 0);
            }
        }
    }
}
