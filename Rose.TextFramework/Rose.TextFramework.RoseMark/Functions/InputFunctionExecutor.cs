using System;
using System.Collections.Generic;
using System.Text;
using Rose.TextFramework.Lingvo;

namespace Rose.TextFramework.RoseMark.Functions
{
    public class InputFunctionExecutor : FunctionExecutor
    {
        public InputFunctionExecutor() : base("input")
        {
        }

        public override FunctionExecuteResult Execute(FunctionExecuteArgs args)
        {
            try
            {
                var result = string.Empty;

                if (args.CurrentTokenIndex == args.Token.Elements.Count - 1)
                {
                    result = args.Input;
                }

                else
                {
                    var textLine = new TextLine(args.Input);
                    for (var i = 0; i < textLine.Words.Count; i++)
                    {
                        var str = textLine.Words[i].ToEnd;

                        var expressionBuilder = new StringBuilder();

                        for (var j = args.CurrentTokenIndex+1; j < args.Token.Elements.Count; j++)
                        {
                            expressionBuilder.Append(args.Token.Elements[j].Text);
                        }

                        var expressionText = expressionBuilder.ToString();
                        var expression = new RoseMarkExpression(expressionText);

                        var encodeDataHolder = new Dictionary<string, object>();


                        var compileResult = expression.Compile(new CompileInfo
                                                               {
                                                                   EncodeDataHolder = encodeDataHolder,
                                                                   Input = " " + str,
                                                                   PrecompiledDataHolder = args.Resources,
                                                                   ShouldCheckLenght = false
                                                               });
                        if (compileResult.IsCompiled)
                        {
                            if (args.Attributes.ContainsKey("id"))
                            {

                                args.Provider.ComponentsData.Add(args.Attributes["id"], textLine.Words[i].ToStart);
                            }
                            return new FunctionExecuteResult(true, textLine.Words[i].ToStart.Length);
                        }
                    }
                    return new FunctionExecuteResult(false, 0);
                }

                if (args.Attributes.ContainsKey("id"))
                {
                    args.Provider.ComponentsData.Add(args.Attributes["id"], result);
                }

                return new FunctionExecuteResult(true, result.Length);
            }
            catch (Exception e)
            {
                return new FunctionExecuteResult(false, 0);
            }

        }
    }
}
