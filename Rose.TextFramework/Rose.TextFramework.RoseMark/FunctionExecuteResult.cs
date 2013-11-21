namespace Rose.TextFramework.RoseMark
{
    public class FunctionExecuteResult
    {

        public FunctionExecuteResult()
        {
            
        }

        public FunctionExecuteResult(bool isMatch, int skipLenght)
        {
            IsMatch = isMatch;
            SkipLenght = skipLenght;
        }

        public bool IsMatch { get; set; }
        public int SkipLenght { get; set; }
    }
}
