using System;

namespace Rose.TextFramework.Moduling
{
    public class PatternAttribute : Attribute
    {
        public PatternAttribute(string patternPath)
        {
            PatternPath = patternPath;
        }

        public string PatternPath { get; private set; }
    }
}
