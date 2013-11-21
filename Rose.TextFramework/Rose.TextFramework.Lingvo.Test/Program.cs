using System;
using System.Linq;
using System.Reflection;
using SolarixGrammarEngineNET;
using System.Text;
using Rose.TextFramework.Lingvo.Synonyms;

namespace Rose.TextFramework.Lingvo.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var i = SolarixGrammarEngineNET.GrammarEngineAPI.PERSON_3_jap;
            //Console.WriteLine("Введи слово, к которому требуется подобрать синоним:");
            //var input = Console.ReadLine();
            //var source = new WebSynonymsSource();
            //var result = source.GetSynonyms(input);
            //if (result.Count() != 0)
            //{
            //    Console.WriteLine("Синонимы к слову '" + input + "'");
            //    foreach (var s in result)
            //    {

            //        Console.WriteLine(s);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Синонимы не найдены");
            //}
            var a = GrammarEngine.sol_Stemmer((IntPtr) 11, "test");
            Console.ReadKey();
        }

    }
}
