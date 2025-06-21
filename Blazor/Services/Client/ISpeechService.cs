using Microsoft.JSInterop;

namespace Trava.Blazor.Services.Client;

public class ISpeechService
{
    private readonly IJSRuntime Runtime;

    public ISpeechService(IJSRuntime IJS)
    {
        Runtime = IJS;
    }

    public void Speak(string text, bool english = false)
    {   
        _ = Runtime.InvokeVoidAsync("textToSpeech.speak", text, english).AsTask().ConfigureAwait(false);;
    }

    public async Task SpeakAsync(string text, bool english = false)
    {   
        await Runtime.InvokeVoidAsync("textToSpeech.speakAsync", text, english);
    }
}