using System;
using System.Collections.Generic;
using System.Linq;
using Rose.TextFramework.Lingvo.Synonyms;

namespace Rose.TextFramework.RoseMark.Functions
{
    public class AnyFunctionExecutor : FunctionExecutor
    {
        public AnyFunctionExecutor() : base(RoseMarkConstants.AnyFunctionName)
        {
        }

        public override FunctionExecuteResult Execute(FunctionExecuteArgs args)
        {

            var res = new List<string>();
            if (args.Args[0].StartsWith("@"))
            {
                var source = new WebSynonymsSource();
                Console.WriteLine("Соединяюсь с интернетом, на выполнение запроса уйдет немного больше времени...");
                var ss = source.GetSynonyms(args.Args[0].Substring(1));
                res.AddRange(ss);
                res.Add(args.Args[0].Substring(1));
            }
            else
            {
                res = new List<string>(args.Args);
            }

            var result = new List<string>();

            foreach (var arg in res)
            {
                try
                {
                    for (var i = 0; i < arg.Length; i++)
                    {
                        if (i == arg.Length - 1 && char.ToLower(args.Input[i]) == char.ToLower(arg[i]))
                        {
                            if (args.Attributes.ContainsKey("id"))
                            {
                                args.Provider.ComponentsData.Add(args.Attributes["id"], arg);
                            }
                            result.Add(arg);
                        }
                        if (char.ToLower(args.Input[i]) != char.ToLower(arg[i]))
                            break;
                    }
                }
                catch 
                {
                }
            }

            if (result.Count == 0)
            {
                return new FunctionExecuteResult(false, 0);
            }

            if (result.Count == 1)
            {
                return new FunctionExecuteResult(true, result.First().Length);
            }

            result.Sort((s1, s2) =>
                        {
                            if (s1.Length > s2.Length)
                                return 1;
                            if (s1.Length == s2.Length)
                                return 0;
                            return -1;
                        });

            return new FunctionExecuteResult(true, result.Last().Length);
        }
    }
}
