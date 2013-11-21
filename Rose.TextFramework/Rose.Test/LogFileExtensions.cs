using Rose.Common;
using Rose.TextFramework.Parsing;

namespace Rose.Test
{
    public static class LogFileExtensions
    {
        public static void WriteRequestInfo(this LogFile file, ModuleRequest request)
        {
            file.WriteLine("Запрос '" + request.Text + "'");
            file.WriteLine("Имя паттерна: " + request.Pattern.Name);
            file.WriteLine("Текст патерна: " + request.MatchedText.Text);
            file.WriteLine("Кодировка текста патерна: " + request.MatchedText.Encoding);
            file.WriteLine("Количество данных кодировщика: " + request.EncodeData.Count);
            foreach (var data in request.EncodeData)
            {
                file.WriteLine("Данные кодировщика: Имя: " + data.Key + "   Значение: " + data.Value);
            }
        }
    }
}
