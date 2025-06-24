using Trava.Scripts.Models;
using Python.Runtime;

namespace Trava.Blazor.Services.Server;

public class ILemmaService
{
    private readonly IServerLogger Logger;

    private dynamic? morphAnalyzer;

    public ILemmaService(IServerLogger logger)
    {
        Logger = logger;
    }

    public async Task TryStartPython()
    {
        if(!PythonEngine.IsInitialized)
        {
            Logger.Log("Python Loading...", IServerLogger.LogSource.System);

            Runtime.PythonDLL = @"C:\Users\jello\AppData\Local\Programs\Python\Python38\python38.dll";
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();

            while(!PythonEngine.IsInitialized)
                await Task.Delay(100);

            Logger.Log("Python Started.", IServerLogger.LogSource.System);

            using (Py.GIL())
            {
                dynamic pymorphy2 = Py.Import("pymorphy2");
                morphAnalyzer = pymorphy2.MorphAnalyzer();
            }
        }
    }

    public bool PythonRunning()
    {
        return PythonEngine.IsInitialized;
    }

    public Lemma? Lemmatize(string displayText)
    {
        if (morphAnalyzer == null)
            return null;

        using (Py.GIL())
        {
            dynamic parse = morphAnalyzer.parse(displayText);
            dynamic best = parse[0];

            List<string> grammemes = [];
            foreach (PyObject item in best.tag.grammemes)
                grammemes.Add(item.ToString()!);

            Lemma created = LemmaParser.ExtractLemma(displayText, best, grammemes);

            return created; 
        }
    }
}
