using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Rose.TextFramework.Parsing;

namespace Rose.Test
{
    public class HistoryLog
    {
        public static string HistoryFilePath { get; set; }

        public static HistoryLog Open()
        {
            return new HistoryLog();
        }

        private HistoryLog()
        {
            
        }

        public void WriteResponse(ModuleResponse response, TimeSpan elapsed)
        {
            var builder = new StringBuilder();

            var time = DateTime.Now;
            var q = response.Request.Text;
            var responseType = response.Content.GetType();

            builder.AppendLine();
            builder.AppendLine(time.ToString("T") + ": " + q);
            builder.AppendLine("Тип ответа: " + responseType);
            builder.AppendLine("Строка ответа: " + response.Content);
            builder.AppendLine("Состояние: " + response.Status);
            builder.AppendLine("Времени затраченно: " + elapsed.TotalMilliseconds + "ms");
            builder.AppendLine();

            File.AppendAllText(HistoryFilePath, builder.ToString());


        }

    }
}
