namespace Rose.TextFramework.Parsing
{
    public interface IModuleRequestProvider
    {
        ModuleResponse GetResponse(ModuleRequest request);
    }
}
