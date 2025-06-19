using Trava.Scripts.Models;
using Python.Runtime;

namespace Trava.Blazor.Services.Server;

public class ILemmaService
{
    private dynamic? morphAnalyzer;

    public async Task TryStartPython()
    {
        if(!PythonEngine.IsInitialized)
        {
            Console.WriteLine("Python Loading...");

            Runtime.PythonDLL = @"C:\Users\jello\AppData\Local\Programs\Python\Python38\python38.dll";
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();

            while(!PythonEngine.IsInitialized)
                await Task.Delay(100);

            Console.WriteLine("Python Started.");

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
