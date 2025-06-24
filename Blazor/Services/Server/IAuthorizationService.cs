using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.Options;

namespace Trava.Blazor.Services.Server;

public class AuthConfig
{
    public string? Passcode { get; set; }
}

public class IAuthorizationService : CircuitHandler
{
    private readonly AuthConfig Config;
    private readonly IServerLogger Logger;

    private readonly Dictionary<string, bool> AuthStates = [];

    public bool LoginAllowed = true;

    public IAuthorizationService(IServerLogger logger, IOptions<AuthConfig> config)
    {
        Logger = logger;
        Config = config.Value;
    }

    public override Task OnCircuitOpenedAsync(Circuit connection, CancellationToken cancellationToken)
    {
        AuthStates[connection.Id] = false; //Make new auth state
        Logger.Log($"New Connection: {connection.Id}");

        return Task.CompletedTask;
    }

    public override Task OnCircuitClosedAsync(Circuit connection, CancellationToken cancellationToken)
    {
        if(AuthStates.TryGetValue(connection.Id, out var isAuthorized) && isAuthorized)
            LoginAllowed = true;

        AuthStates.Remove(connection.Id); //Remove auth state
        Logger.Log($"Lost Connection: {connection.Id}");

        return Task.CompletedTask;
    }

    public bool IsAuthorized(string? connectionID)
    {
        if(connectionID == null)
            return false;

        return AuthStates.TryGetValue(connectionID, out var isAuthorized) && isAuthorized;
    }

    public void TryAuthorize(string? connectionID, string? passcode)
    {
        if (connectionID == null || passcode == null)
            return;

        if(passcode == Config.Passcode)
        {
            AuthStates[connectionID] = true;
            Logger.Log($"Connection Authorized: {connectionID}", IServerLogger.LogSource.System);
            LoginAllowed = false;
        }else
        {
            Logger.Log($"Connection Failed To Authorize: {connectionID}", IServerLogger.LogSource.Warning);
            Logger.Log($"Failed Passcode: {connectionID}", IServerLogger.LogSource.Warning);
        }
    }
}
