using Microsoft.AspNetCore.Components.Server.Circuits;
using Trava.Blazor.Services.Server;

public class INetworkIdentity : CircuitHandler
{
    private readonly IAuthorizationService AuthorizationService;

    public string? ConnectionID { get; private set; } = null;

    public INetworkIdentity(IAuthorizationService authorizationService)
    {
        AuthorizationService = authorizationService;
    }

    public bool LoginAllowed() => AuthorizationService.LoginAllowed;

    public bool IsAuthorized() => AuthorizationService.IsAuthorized(ConnectionID);

    public void TryAuthorize(string passcode) => AuthorizationService.TryAuthorize(ConnectionID, passcode);

    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        ConnectionID = circuit.Id;
        return Task.CompletedTask;
    }

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        if (ConnectionID == circuit.Id)
            ConnectionID = null;
        return Task.CompletedTask;
    }

    public async Task WaitForConnection(int timout = 2000, int delay = 50)
    {
        int totalWait = 0;
        while (ConnectionID == null && totalWait < timout)
        {
            await Task.Delay(delay);
            totalWait += delay;
        }
    }
}