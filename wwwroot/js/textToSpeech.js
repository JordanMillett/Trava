window.textToSpeech =
{
    speak: function (text, english) {
        speechSynthesis.cancel();
        
        var utterance = new SpeechSynthesisUtterance(text);
        utterance.volume = 0.5;
        utterance.lang = english ? "en-US" : "ru-RU";
        speechSynthesis.speak(utterance);
    },
    speakAsync: function (text, english) {
        return new Promise(function (resolve, reject) 
        {
            speechSynthesis.cancel();

            var utterance = new SpeechSynthesisUtterance(text);
            utterance.volume = 0.5;
            utterance.lang = english ? "en-US" : "ru-RU";

            utterance.onend = function () 
            {
                resolve();
            };

            utterance.onerror = function (e) 
            {
                reject(e.error);
            };

            speechSynthesis.speak(utterance);
        });
    },
    preload: function() {
        return new Promise(function(resolve, reject) 
        {
            speechSynthesis.getVoices();
            
            var utterance = new SpeechSynthesisUtterance("Preload");
            utterance.volume = 0;
            
            utterance.onend = function() 
            {
                resolve();
            };

            utterance.onerror = function(e) 
            {
                reject(e.error);
            };
            
            speechSynthesis.speak(utterance);
        });
    }
};