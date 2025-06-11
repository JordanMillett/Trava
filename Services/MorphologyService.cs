using Python.Runtime;

namespace Trava.Services;

public class MorphologyService
{
    private dynamic? morphAnalyzer;

    public MorphologyService()
    {
        
    }

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

    public string Lemmatize(string word)
    {
        if (morphAnalyzer == null)
            return "";

        using (Py.GIL())
        {
            dynamic parse = morphAnalyzer.parse(word);
            dynamic best = parse[0];
            return best.normal_form.ToString();
        }
    }
}
