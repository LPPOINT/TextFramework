using System.Text;

namespace Rose.TextFramework.Lingvo
{
    public class Word
    {
        public Word(string text, TextLine textLine, int number)
        {
            Number = number;
            TextLine = textLine;
            Text = text;
        }

        public string Text { get; private set; }
        public TextLine TextLine { get; private set; }
        public int Number { get; private set; }

        public string ToEnd
        {
            get
            {
                var builder = new StringBuilder();
                for (var i = Number; i < TextLine.Words.Count; i++)
                {
                    builder.Append(TextLine.Words[i].Text + " ");
                }
                return builder.ToString().Trim();
            }
        }

        public string ToStart
        {
            get
            {
                var builder = new StringBuilder();
                for (var i = 0; i < Number; i++)
                {
                    builder.Append(TextLine.Words[i].Text + " ");
                }
                return builder.ToString().Trim();
            }
        }

    }
}
