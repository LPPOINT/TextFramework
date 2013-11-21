using System;
using System.Collections.Generic;
using Rose.TextFramework.Moduling;

namespace Rose.TextFramework.Parsing
{

    public enum CompareResult
    {
        Equals,
        Less,
        Bigger
    }

    public class PatternComparer
    {
        public int Compare<T>(PatternText origin, PatternText second)
        {
            if(origin.Weight > second.Weight)
                return 1;
            if(origin.Weight < second.Weight)
                return -1;
            return 0;
        }
    }
}
