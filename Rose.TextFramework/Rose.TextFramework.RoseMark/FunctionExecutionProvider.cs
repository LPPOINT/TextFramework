using System.Collections.Generic;

namespace Rose.TextFramework.RoseMark
{
    public class FunctionExecutionProvider
    {
        public FunctionExecutionProvider()
        {
            ComponentsData = new Dictionary<string, object>();
        }


        public Dictionary<string, object> ComponentsData { get; private set; } 

    }
}
