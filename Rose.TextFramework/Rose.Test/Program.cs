using System;
using System.Diagnostics;
using Rose.Common;
using Rose.TextFramework.Parsing;
using Rose.TextFramework.RoseMark;
using Rose.TextFramework.Voice;

namespace Rose.Test
{
    class Program
    {
        static void Main()
        {
            try
            {

                var module = new TestModule();
                HistoryLog.HistoryFilePath = "history.log";
                var history = HistoryLog.Open();
                RoseMarkExpression.CommonExecutors.Add(new UseFunctionExecutor());
                var provider = new TextMappingEngine();
                provider.Modules.Add(module);
                ModuleRequest.RequestProvider = provider;

                LogFile.Default = new LogFile(@"C:\Users\Sasha\Documents\visual studio 2013\Projects\Rose.TextFramework\Rose.Test\LogFile.log");

                while (true)
                {
                    Console.WriteLine("\n\nВведи запрос:");
                    var q = Console.ReadLine();
                    var stopwatch = new Stopwatch();
                    var request = new ModuleRequest(q);
                    var response = request.GetResponse();
                    var speaker = new Speaker();

                    if (response != null)
                    {
                        stopwatch.Stop();
                        history.WriteResponse(response, stopwatch.Elapsed);
                    }

                    if(response.Status == ResponseStatus.Ok)
                        LogFile.Default.WriteRequestInfo(request);

                    if (response.Content.ToString() != string.Empty)
                    {
                        Console.WriteLine("Ответ: " + response.Content);
                        try
                        {
                            speaker.Speak(response.Content.ToString());
                        }
                        catch { }
                    }
                    if(response.Status == ResponseStatus.Exit)
                        break;
                }

                LogFile.Default.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine("Что-то не так.\n" + e.Message);
                Console.ReadKey();
            }



        }
    }
}
