using System.Collections.Generic;

namespace Rose.TextFramework.Lingvo.Synonyms
{
    public abstract class SynonymsSource
    {
        public abstract IEnumerable<string> GetSynonyms(string str);
    }
}
