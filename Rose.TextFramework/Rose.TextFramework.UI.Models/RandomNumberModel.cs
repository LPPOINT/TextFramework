namespace Rose.TextFramework.UI.Models
{
    public class RandomNumberModel
    {
        public RandomNumberModel()
        {
            
        }

        public RandomNumberModel(int @from, int to, int number)
        {
            From = @from;
            To = to;
            Number = number;
        }

        public int From { get; set; }
        public int To { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return "Случайное значение от " + From + " до " + To + ": " + Number;
        }
    }
}
