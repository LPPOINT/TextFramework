using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Rose.TextFramework.Lingvo.Synonyms;
using Rose.TextFramework.Moduling;
using Rose.TextFramework.Parsing;
using Rose.TextFramework.UI.Models;

namespace Rose.Test
{
    public class TestModule : Module
    {
        public TestModule()
            : base(XDocument.Load(@"C:\Users\Sasha\documents\visual studio 2013\Projects\Rose.TextFramework\Rose.TextFramework.UI.Win\ModuleModel.xml"))
        {
        }

        [Pattern("Hello")]
        public ModuleResponse Hello(ModuleRequest request)
        {
            return new ModuleResponse(request, "HELLO WORLD");
        }

        [Pattern("OpenChrome")]
        public ModuleResponse OpenChrome(ModuleRequest request)
        {
            Process.Start("chrome.exe", "http://www.google.ru");
            return new ModuleResponse(request, "Браузер открыт");
        }


        [Pattern("SearchImgs")]
        public ModuleResponse SearchImgs(ModuleRequest request)
        {
            if (!request.EncodeData.ContainsKey("searchResource"))
                return new ModuleResponse(request, "Требуется указать поисковую машину");
            if (!request.EncodeData.ContainsKey("q"))
                return new ModuleResponse(request, "Требуется сформулировать запрос");

            switch (request.EncodeData["searchResource"].ToString())
            {
                case "www.google.ru":
                case "goolge":
                case "гугл":
                case "гугле":
                case "www.google.com":
                    Process.Start("chrome.exe", "http://www.google.ru/search?q=" + request.EncodeData["q"].ToString().Replace(" ", "+") + "&tbm=isch");
                    break;
                case "www.yandex.ru":
                case "яндекс":
                case "яндексе":
                    Process.Start("chrome.exe", "http://www.yandex.ru/yandsearch?text=" + request.EncodeData["q"].ToString().Replace(" ", "+"));
                    break;
                default:
                    return new ModuleResponse(request, "Поисковая машина с именем '" + request.EncodeData["searchResource"] + "' не найдена");
            }
            return new ModuleResponse(request, "Осуществляется поиск в '" + request.EncodeData["searchResource"] + "' по запросу '" + request.EncodeData["q"].ToString().Replace(" ", "+") + "'");
        }


        [Pattern("Search")]
        public ModuleResponse Search(ModuleRequest request)
        {
            if(!request.EncodeData.ContainsKey("searchResource"))
                return new ModuleResponse(request, "Требуется указать поисковую машину");
            if(!request.EncodeData.ContainsKey("q"))
                return new ModuleResponse(request, "Требуется сформулировать запрос");

            switch (request.EncodeData["searchResource"].ToString())
            {
                case "www.google.ru":
                case "goolge":
                case "гугл":
                case "гугле":
                case "www.google.com":
                    Process.Start("chrome.exe", "http://www.google.ru/search?q=" + request.EncodeData["q"]);
                    break;
                case "www.yandex.ru":
                case "яндекс":
                case "яндексе":
                    Process.Start("chrome.exe", "http://www.yandex.ru/yandsearch?text=" + request.EncodeData["q"]);
                    break;
                default:
                    return new ModuleResponse(request, "Поисковая машина с именем '" + request.EncodeData["searchResource"] + "' не найдена");
            }
            return new ModuleResponse(request, "Осуществляется поиск в '" + request.EncodeData["searchResource"] + "' по запросу '" + request.EncodeData["q"] + "'");
        }


        [Pattern("Say")]
        public ModuleResponse Write(ModuleRequest request)
        {
            var toSay = request.EncodeData["toSay"].ToString();
            toSay = char.ToUpper(toSay[0]) + toSay.Substring(1);
            return new ModuleResponse(request, "Ладно. " + toSay);
        }

        [Pattern("Time")]
        public ModuleResponse WriteTime(ModuleRequest request)
        {
            return new ModuleResponse(request, "Its " + DateTime.Now);
        }

        [Pattern("Time2")]
        public ModuleResponse WriteTimeRu(ModuleRequest request)
        {
            return new ModuleResponse(request, "Сейчас " + DateTime.Now);
        }

        [Pattern("RandomInt")]
        public ModuleResponse WriteRandom(ModuleRequest request)
        {


            try
            {
                int from;
                int to;

                if(!request.EncodeData.ContainsKey("from"))
                    return new ModuleResponse(ResponseStatus.Error, request, "Не обнаружено начало отсчета");
                if(!request.EncodeData.ContainsKey("to"))
                    return new ModuleResponse(ResponseStatus.Error, request, "Не обнаружено завершение отсчета");

                try
                {
                    from = Convert.ToInt32(request.EncodeData["from"].ToString());
                    to = Convert.ToInt32(request.EncodeData["to"].ToString());
                }
                catch(Exception e)
                {
                    return new ModuleResponse(ResponseStatus.Error, request, "Неверный формат границ случайного числа");
                }

                var random = new Random();
                var number = random.Next(from, to);
                return new ModuleResponse(request, new RandomNumberModel(from, to, number));
            }
            catch (Exception e)
            {
                return new ModuleResponse(ResponseStatus.Error, request, "Ошибка вычесления случайного числа:\n " + e.Message);
            }
        }

        [Pattern("Syns")]
        public ModuleResponse GetSyns(ModuleRequest request)
        {
            try
            {
                var q = request.EncodeData["sync"].ToString();
                if (!string.IsNullOrEmpty(q))
                {
                    var source = new WebSynonymsSource();
                    var syncs = source.GetSynonyms(q);
                    if (!syncs.Any())
                    {
                        return new ModuleResponse(request, "Синонимы к слову '" + q + "' не найдены");
                    }
                    var builder = new StringBuilder();
                    builder.Append("Синонимы к слову '" + q + "'. Всего: " + syncs.Count() + Environment.NewLine);
                    foreach (var sync in syncs)
                    {
                        builder.Append(sync + Environment.NewLine);
                    }
                    return new ModuleResponse(request, builder.ToString());
                }
                else
                {
                    return new ModuleResponse(request, "Не могу вывести синонимы - отсутствует исходное слово");
                }
            }
            catch (Exception e)
            {
                return new ModuleResponse(request, "Ошибка: " + e.Message);
            }
        }

 

    }
}
