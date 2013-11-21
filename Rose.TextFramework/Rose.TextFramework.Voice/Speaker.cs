using System;
using System.Speech.Synthesis;

namespace Rose.TextFramework.Voice
{
    public class Speaker
    {

        public Speaker()
        {
            ss = new SpeechSynthesizer();

            
        }

        private SpeechSynthesizer ss;

        public void Speak(string text)
        {
            ss.Volume = 100;
            ss.Speak(text);
        }
    }
}
