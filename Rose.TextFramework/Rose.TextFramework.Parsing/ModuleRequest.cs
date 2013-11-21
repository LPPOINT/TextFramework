using System.Collections.Generic;
using Rose.Common;
using Rose.TextFramework.Moduling;

namespace Rose.TextFramework.Parsing
{
    public class ModuleRequest
    {
        public static IModuleRequestProvider RequestProvider { get; set; }

        public ModuleRequest(string text)
        {
            Text = text;
            EncodeData = new Dictionary<string, object>();
        }

        public string Text { get; private set; }
        public Pattern Pattern { get; internal set; }
        public PatternText MatchedText { get; internal set; }
        public Dictionary<string, object> EncodeData { get; internal set; } 

        public ModuleResponse GetResponse()
        {
            Check.NotNull(RequestProvider, "requestProvider");
            return RequestProvider.GetResponse(this);
        }

    }
}
