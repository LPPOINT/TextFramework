using System;
using System.Collections.Generic;
using System.Reflection;
using Rose.TextFramework.RoseMark.Functions;

namespace Rose.TextFramework.RoseMark
{
    public class RoseMarkExpression
    {
        public RoseMarkExpression(string expressionString)
        {
            ExpressionString = expressionString;

            executors = new List<FunctionExecutor> {new AnyFunctionExecutor(), new TextFunctionExecutor(), new InputFunctionExecutor(), new MayFunctionExecutor()};


            foreach (var functionExecutor in CommonExecutors)
            {
                executors.Add(functionExecutor);
            }

        }


        public static readonly List<FunctionExecutor> CommonExecutors = new List<FunctionExecutor>();

        private readonly List<FunctionExecutor> executors; 

        public string ExpressionString { get; private set; }

        public CompileResult Compile(CompileInfo compileInfo)
        {
            try
            {
                var token = ExpressionToken.Parse(ExpressionString);
                var res = new CompileResult();
                var startIndex = 0;
                var provider = new FunctionExecutionProvider();

                for (var i = 0; i < token.Elements.Count; i++)
                {
                    if (token.Elements[i] is ExpressionTextElement)
                    {
                        token.Elements[i] = new ExpressionFunctionElement("[text(\"" + token.Elements[i].Text + "\")]");
                    }
                }

                var tokenIndex = 0;

                foreach (var e in token.Elements)
                {
                    var f = e as ExpressionFunctionElement;
                    foreach (var functionExecutor in executors)
                    {

                        if (functionExecutor.FunctionName == f.Function.FunctionName)
                        {
                            var toCheck = compileInfo.Input.Substring(startIndex);

                            var info = new FunctionExecuteArgs(f.Function.FunctionArgs, f.Function.FunctionAttributes,
                                provider, token, tokenIndex, toCheck, compileInfo.PrecompiledDataHolder);

                            FunctionExecuteResult result;

                            try
                            {
                                result = functionExecutor.Execute(info);
                            }
                            catch 
                            {
                                return new CompileResult
                                       {
                                           IsCompiled = false,
                                           Lenght = 0
                                       };
                            }

                            compileInfo.EncodeDataHolder.Clear();

                            foreach (var kv in provider.ComponentsData)
                            {
                                compileInfo.EncodeDataHolder.Add(kv.Key, kv.Value);
                            }

                            if (result.IsMatch == false)
                                return new CompileResult
                                       {
                                           IsCompiled = false
                                       };
                            startIndex += result.SkipLenght;

                        }
                    }
                    tokenIndex++;
                }

                if (startIndex != compileInfo.Input.Length && compileInfo.ShouldCheckLenght)
                {
                    Console.WriteLine(startIndex + " => " + compileInfo.Input.Length);
                    return new CompileResult
                           {
                               IsCompiled = false,
                               Lenght = 0
                           };
                }

                return new CompileResult
                       {
                           IsCompiled = true,
                           Lenght = startIndex
                       };
            }
            catch (Exception re)
            {
                Console.WriteLine(re);
                return new CompileResult()
                       {
                           IsCompiled = false
                       };
            }
        }

    }
}
