using Rose.TextFramework.Moduling;

namespace Rose.TextFramework.Parsing
{
    public class MatchedPatternInfo
    {
        public MatchedPatternInfo(TextMatchInfo textMatchInfo, Module module, Pattern pattern)
        {
            TextMatchInfo = textMatchInfo;
            Module = module;
            Pattern = pattern;
        }

        public MatchedPatternInfo()
        {
            
        }

        public TextMatchInfo TextMatchInfo { get; set; }
        public Module Module { get; set; }
        public Pattern Pattern { get; set; }
    }
}
