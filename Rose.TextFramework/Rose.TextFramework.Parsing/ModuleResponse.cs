namespace Rose.TextFramework.Parsing
{


    public enum ResponseStatus
    {
        Ok,
        Error,
        Exit
    }

    public class ModuleResponse
    {
        public ModuleResponse(ModuleRequest request, object content)
        {
            Content = content;
            Request = request;
            Status = ResponseStatus.Ok;;
        }

        public ModuleResponse(ResponseStatus status, ModuleRequest request, object content)
        {
            Status = status;
            Request = request;
            Content = content;
        }

        public ResponseStatus Status { get; private set; }

        public ModuleRequest Request { get; private set; }
        public object Content { get; private set; }
    }
}
