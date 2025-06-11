using Python.Runtime;

namespace Trava.Services;

public class MorphologyService
{
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
        }
    }

    public string Lemmatize(string word)
    {
        using (Py.GIL()) // Acquire GIL for thread safety
        {
            dynamic pymorphy2 = Py.Import("pymorphy2");
            dynamic morph = pymorphy2.MorphAnalyzer();

            dynamic parse = morph.parse(word);
            dynamic best = parse[0]; // Best guess for parse
            string lemma = best.normal_form.ToString();

            return lemma;
        }
    }
}
