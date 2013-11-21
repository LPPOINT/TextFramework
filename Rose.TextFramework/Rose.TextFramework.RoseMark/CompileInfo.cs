using System.Collections.Generic;

namespace Rose.TextFramework.RoseMark
{
    public class CompileInfo
    {

        public CompileInfo()
        {
            EncodeDataHolder = new Dictionary<string, object>();
            PrecompiledDataHolder = new Dictionary<string, object>();
            ShouldCheckLenght = true;
        }

        public bool ShouldCheckLenght { get; set; }

        public string Input { get; set; }
        public Dictionary<string, object> EncodeDataHolder { get; set; }
        public Dictionary<string, object> PrecompiledDataHolder { get; set; } 
    }
}
