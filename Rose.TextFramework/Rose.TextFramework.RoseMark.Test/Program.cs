using System;
using System.Collections.Generic;
using Rose.TextFramework.RoseMark.Functions;

namespace Rose.TextFramework.RoseMark.Test
{
    class Program
    {
        static void Main()
        {
            var expression = new RoseMarkExpression("скажи [input(\"true\") id=\"test\"]");
            var compileResult = expression.Compile(new CompileInfo
                               {
                                   Input = "скажи привет",
                                   EncodeDataHolder = new Dictionary<string, object>(),
                                   PrecompiledDataHolder = new Dictionary<string, object>()
                               });

            Console.WriteLine(compileResult.IsCompiled);
            Console.ReadKey();

        }
    }
}
