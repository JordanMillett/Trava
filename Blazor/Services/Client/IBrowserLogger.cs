using Microsoft.JSInterop;

namespace Trava.Blazor.Services.Client;

public class IBrowserLogger
{
    private readonly IJSRuntime Runtime;

    public IBrowserLogger(IJSRuntime IJS)
    {
        Runtime = IJS;
    }
    
    public void Log(string message)
    {
        _ = Runtime.InvokeVoidAsync("console.log", message).AsTask().ConfigureAwait(false);
    }
}