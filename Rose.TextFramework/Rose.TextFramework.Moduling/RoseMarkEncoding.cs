using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Rose.TextFramework.RoseMark;

namespace Rose.TextFramework.Moduling
{
    /// <summary>
    /// <example>
    /// привет, мир[may("у", "ам", "ку")] => привет, мир; привет, миру
    /// привет[may[","]]мир => привет, мир; привет , мир; привет мир
    /// [may["@intro"]][may[","]]привет => Итак, привет; Чтож, привет
    /// [may("@intro")] привет[may(",")] [any()] привет блаблабла
    /// 
    /// [may("@intro")][may(",")][any("@launch") ignore="Запуск"][may["@please"]][any("браузер", "интернет", "chrome")]
    /// [may("[any("@actionsSplit")] перейди [may("@please")] [any("по адресу", "на адрес", "на", "на сайт")] [url() id="url"]")]
    /// 
    /// </example>
    /// </summary>
    public class RoseMarkEncoding : PatternEncoding
    {
        public RoseMarkEncoding() : base("rose")
        {
        }

        public override EncodingResult Encode(string pattern, string input, Dictionary<string, object> resources)
        {
            var expression = new RoseMarkExpression(input);
            var data = new Dictionary<string, object>();
            var matchResult = expression.Compile(new CompileInfo
                                                 {
                                                     EncodeDataHolder = data,
                                                     PrecompiledDataHolder = resources,
                                                     Input = pattern
                                                 });
            var result = new EncodingResult {Result = matchResult.IsCompiled, EncodeData = data};
            return result;
        }
    }
}
