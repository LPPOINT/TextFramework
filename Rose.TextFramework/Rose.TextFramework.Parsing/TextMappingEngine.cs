using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rose.TextFramework.Moduling;
using Module = Rose.TextFramework.Moduling.Module;

namespace Rose.TextFramework.Parsing
{
    public class TextMatchInfo
    {
        public TextMatchInfo(bool isMatch, PatternText text, Dictionary<string, object> encodeData)
        {
            IsMatch = isMatch;
            Text = text;
            EncodeData = encodeData;
        }

        public Dictionary<string, object> EncodeData { get; set; } 
        public bool IsMatch { get; set; }
        public PatternText Text { get; set; }
    }

    public class TextMappingEngine : IModuleRequestProvider
    {
        public TextMappingEngine()
        {
            Modules = new List<Module>();
            Encodings = new List<PatternEncoding>();
            Encodings.Add(new BaseEncoding());
            Encodings.Add(new RegexEncoding());
            Encodings.Add(new RoseMarkEncoding());

        }

        public List<Module> Modules { get; private set; }
        public List<PatternEncoding> Encodings { get; private set; } 

        private TextMatchInfo IsMatch(string str, PatternText text, Module module)
        {

                var encoding = Encodings.FirstOrDefault(patternEncoding => patternEncoding.Name == text.Encoding);
                var encodingInfo = encoding.Encode(str, text.Text, module.Resources);
                if (encoding != null && encodingInfo.Result)
                    return new TextMatchInfo(true, text, encodingInfo.EncodeData);
            return new TextMatchInfo(false, null, null);
        }

        public ModuleResponse GetResponse(ModuleRequest request)
        {

            try
            {

                var matchedPatterns = new List<MatchedPatternInfo>();

                foreach (var module in Modules)
                {
                    foreach (var pattern in module.Patterns)
                    {

                        foreach (var text in pattern.Texts)
                        {
                            var matchInfo = IsMatch(request.Text, text, module);

                            if (matchInfo.IsMatch)
                            {

                                matchedPatterns.Add(new MatchedPatternInfo(matchInfo, module, pattern));
                            } 
                        }


                    }
                }

                if(matchedPatterns.Count == 0)
                    return new ModuleResponse(ResponseStatus.Error, request, "Sorry, but im not found this");
                if (matchedPatterns.Count == 1)
                {
                    var info = matchedPatterns.First();
                    var methods = info.Module.GetType().GetTypeInfo().GetMethods();

                    foreach (var methodInfo in methods)
                    {
                        if (methodInfo.GetCustomAttribute(typeof(PatternAttribute)) != null)
                        {
                            var attribute = methodInfo.GetCustomAttribute<PatternAttribute>();
                            if (attribute.PatternPath == info.Pattern.Name)
                            {

                                request.Pattern = info.Pattern;
                                request.MatchedText = info.TextMatchInfo.Text;
                                request.EncodeData = info.TextMatchInfo.EncodeData;

                                var response = methodInfo.Invoke(info.Module, new object[] { request });
                                return response as ModuleResponse;
                            }
                        }
                    }
                }

                else
                {
                    matchedPatterns.Sort((info1, patternInfo) =>
                                         {
                                             if (info1.TextMatchInfo.Text.Weight > patternInfo.TextMatchInfo.Text.Weight)
                                                 return -1;
                                             if (info1.TextMatchInfo.Text.Weight < patternInfo.TextMatchInfo.Text.Weight)
                                                 return 1;
                                             return 0;
                                         });

                    var info = matchedPatterns.First();
                    var methods = info.Module.GetType().GetTypeInfo().GetMethods();

                    foreach (var methodInfo in methods)
                    {
                        if (methodInfo.GetCustomAttribute(typeof(PatternAttribute)) != null)
                        {
                            var attribute = methodInfo.GetCustomAttribute<PatternAttribute>();
                            if (attribute.PatternPath == info.Pattern.Name)
                            {

                                request.Pattern = info.Pattern;
                                request.MatchedText = info.TextMatchInfo.Text;
                                request.EncodeData = info.TextMatchInfo.EncodeData;

                                var response = methodInfo.Invoke(info.Module, new object[] { request });
                                return response as ModuleResponse;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                return new ModuleResponse(ResponseStatus.Error, request, "К сожалению, произошла ошибка во время выполнения запроса. \n" + e.Message);
            }
            return new ModuleResponse(ResponseStatus.Error, request, "Sorry, but im not found this");
        }
    }
}
