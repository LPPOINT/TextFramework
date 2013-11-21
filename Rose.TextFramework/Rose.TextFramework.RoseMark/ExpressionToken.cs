using System;
using System.Collections.Generic;

namespace Rose.TextFramework.RoseMark
{
    public class ExpressionToken
    {

        public ExpressionToken()
        {
            Elements = new List<ExpressionElement>();
        }

        public string ExpressionText { get; private set; }
        public List<ExpressionElement> Elements { get; private set; }

        public static ExpressionToken Parse(string str)
        {
            try
            {
                var token = new ExpressionToken();
                token.ExpressionText = str;
                var elements = new List<ExpressionElement>();
                var current = string.Empty;
                var miss = 0;
                var isFunc = false;
                var temp = false;
                foreach (var ch in str)
                {
                    if (ch == '[' && !isFunc)
                    {
                        if(current != string.Empty)
                             elements.Add(new ExpressionTextElement(current));
                        current = string.Empty;
                        isFunc = true;
                    }
                    else if (ch == '[' && isFunc)
                    {
                        miss++;
                    }
                    else if (ch == ']' && isFunc && miss == 0)
                    {
                        current += ch;
                        temp = true;
                        isFunc = false;
                        elements.Add(new ExpressionFunctionElement(current));
                        current = string.Empty;
                    }
                    else if (ch == ']' && isFunc && miss != 0)
                    {
                        miss--;
                    }

                    if(!temp)
                        current += ch;
                    temp = false;

                }

                if (current != string.Empty)
                {
                    if (isFunc)
                    {
                        elements.Add(new ExpressionFunctionElement(current));
                    }
                    else
                    {
                        elements.Add(new ExpressionTextElement(current));
                    }
                }
                token.Elements = elements;
                return token;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Ошибка создания токена. Подробности в InnerException", e);
            }
        }

    }

    public class ExpressionElement
    {
        public string Text { get; protected set; }
    }

    public class ExpressionTextElement : ExpressionElement
    {
        public ExpressionTextElement(string text)
        {
            Text = text;
        }
    }

    public class ExpressionFunctionElement : ExpressionElement
    {

        public ExpressionFunctionElement(string str)
        {
            Text = str;
            Function = RoseMarkFunction.Parse(str);
        }
        public RoseMarkFunction Function { get; private set; }
    }


}
