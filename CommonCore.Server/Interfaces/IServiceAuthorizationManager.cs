namespace CommonCore.Server.Interfaces
{
    public interface IServiceAuthorizationManager
    {
        bool IsAuthorisedForFunctionAsync(int[] functionGroupId);
        bool IsAuthorisedForService(int[] serviceGroupId);
    }
}
