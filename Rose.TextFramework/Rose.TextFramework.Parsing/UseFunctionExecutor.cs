using System;
using System.Collections.Generic;
using Rose.TextFramework.Moduling;
using Rose.TextFramework.RoseMark;

namespace Rose.TextFramework.Parsing
{
    public class UseFunctionExecutor : FunctionExecutor
    {
        public UseFunctionExecutor() : base("use")
        {
        }

        public override FunctionExecuteResult Execute(FunctionExecuteArgs args)
        {
            try
            {
                var resName = args.Args[0];
                var res = args.Resources[resName];
                if (res is PatternText)
                {
                    var expression = new RoseMarkExpression((res as PatternText).Text);
                    var encode = new Dictionary<string, object>();
                    var compileResult = expression.Compile(new CompileInfo
                                                           {
                                                               EncodeDataHolder = encode,
                                                               Input = args.Input,
                                                               PrecompiledDataHolder = args.Resources,
                                                               ShouldCheckLenght = false
                                                           });
                    if (compileResult.IsCompiled)
                    {
                        foreach (var o in encode)
                        {
                            args.Provider.ComponentsData.Add(o.Key, o.Value);
                        }   
                        return new FunctionExecuteResult(true, compileResult.Lenght);
                    }

                }
                return new FunctionExecuteResult(false, 0);
            }
            catch 
            {
                return new FunctionExecuteResult(false, 0);
            }
        }
    }
}
