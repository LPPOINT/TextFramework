using System.Collections.Generic;

namespace Rose.TextFramework.Lingvo
{
    public class TextLine
    {


        public static string Format(string input)
        {
            return input;
        }

        public TextLine(string text)
        {
            Text = Format(text);
            Words = new List<Word>();

            var ws = Text.Split(' ');
            for (var i = 0; i < ws.Length; i++)
            {
                Words.Add(new Word(ws[i], this, i));
            }

        }

        public string Text { get; private set; }

        public List<Word> Words { get; private set; } 

    }
}
