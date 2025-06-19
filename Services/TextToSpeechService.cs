using Microsoft.JSInterop;

public class TextToSpeechService
{
    IJSRuntime Runtime;

    public TextToSpeechService(IJSRuntime JS)
    {
        Runtime = JS;
    }

    public void Preload()
    {   
        _ = Runtime.InvokeVoidAsync("textToSpeech.preload");
    }

    public void Speak(string text, bool english = false)
    {   
        _ = Runtime.InvokeVoidAsync("textToSpeech.speak", text, english);
    }

    public async Task SpeakAsync(string text, bool english = false)
    {   
        await Runtime.InvokeVoidAsync("textToSpeech.speakAsync", text, english);
    }
}