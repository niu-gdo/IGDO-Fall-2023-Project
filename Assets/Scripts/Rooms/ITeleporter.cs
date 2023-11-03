/// <summary>
/// A Teleporter is the beginning of a transition to an endpoint.
/// </summary>
public interface ITeleporter
{
    // Access to destination for validation purposes in the future.
    public (string roomId, string endpointId) GetDestination(string roomId, string endpointId);
}