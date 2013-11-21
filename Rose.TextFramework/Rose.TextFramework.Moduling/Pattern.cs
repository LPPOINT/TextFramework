using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Rose.Common;

namespace Rose.TextFramework.Moduling
{
    public class Pattern
    {

        public Pattern()
        {
            Headers = new List<PatternHeader>();
            Texts = new List<PatternText>();
            Contexts = new List<Pattern>();
        }

        public Pattern(string name, IEnumerable<PatternHeader> headers, IEnumerable<PatternText> texts, IEnumerable<Pattern> contexts)
        {
            Name = name;
            Headers = new List<PatternHeader>(headers);
            Texts = new List<PatternText>(texts);
            Contexts = new List<Pattern>(contexts);
        }

        public string Name { get;  set; }
        public List<PatternHeader> Headers { get; private set; }
        public List<PatternText> Texts { get; private set; }
        public List<Pattern> Contexts { get; private set; }

        public XElement ToXmlElement()
        {
            try
            {
                var elem = new XElement(XName.Get("pattern"));
                elem.SetAttributeValue(XName.Get("name"), Name);

                foreach (var patternHeader in Headers)
                {
                    var header = new XElement(XName.Get("header"));
                    header.SetAttributeValue(XName.Get("name"), patternHeader.Name);
                    header.SetAttributeValue(XName.Get("value"), patternHeader.Value);

                    elem.AddFirst(header);
                }

                if (Contexts.Count != 0)
                {
                    var contexts = new XElement(XName.Get("contexts"));

                    foreach (var celem in Contexts.Select(context => context.ToXmlElement()))
                    {
                        contexts.AddFirst(celem);
                    }
                }

                foreach (var patternText in Texts)
                {
                    var text = new XElement(XName.Get("text"));
                    text.SetAttributeValue(XName.Get("type"), patternText.Encoding);
                    text.Value = patternText.Text;
                }

                return elem;
            }
            catch (Exception e)
            {
                throw new PatternXmlException("Ошибка трансформации паттерна в xml формат. Подробнее в InnerException", e);
            }

        }

        public static Pattern Parse(XElement xml)
        {
            try
            {
                Check.NotNull(xml);

                var pattern = new Pattern();

                if(xml.Name != "pattern")
                    throw new ArgumentException("Неверный формат паттерна");

                if(xml.Attribute(XName.Get("name")) == null)
                    throw new ArgumentException("Не найденно имя паттерна");

                pattern.Name = xml.Attribute(XName.Get("name")).Value;

                var headers =
                    xml.Nodes()
                        .Where(
                            node =>
                                node.NodeType == XmlNodeType.Element && (node as XElement).Name == XName.Get("header"));

                try
                {
                    foreach (var header in headers)
                    {
                        if (header.NodeType == XmlNodeType.Element)
                        {
                            var he = header as XElement;
                            var name = he.Attribute(XName.Get("name")).Value;
                            var value = he.Attribute(XName.Get("value")).Value;

                            pattern.Headers.Add(new PatternHeader(name, value));

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Ошибка построения заголовков", e);
                }


                var texts =
                    xml.Nodes()
                        .Where(
                            node => node.NodeType == XmlNodeType.Element && (node as XElement).Name == XName.Get("text"));

                try
                {
                    foreach (var text in texts)
                    {
                        if (text.NodeType == XmlNodeType.Element)
                        {
                            var te = text as XElement;
                            var result = PatternText.Parse(te);
                            pattern.Texts.Add(result);

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Ошибка построения текстовых узлов", e);
                }


                var contextsNode =
                    xml.Nodes()
                        .FirstOrDefault(
                            node =>
                                node.NodeType == XmlNodeType.Element && (node as XElement).Name == XName.Get("contexts"));

                if (contextsNode != null)
                {
                    var cne = contextsNode as XElement;
                    var array =
                        cne.Nodes()
                            .Where(
                                node =>
                                    node.NodeType == XmlNodeType.Element &&
                                    (node as XElement).Name == XName.Get("pattern"));

                    foreach (var contextPattern in array.Select(patternNode => Parse(patternNode as XElement)))
                    {
                        pattern.Contexts.Add(contextPattern);
                    }


                }

                return pattern;
            }
            catch (Exception e)
            {
                throw new PatternXmlException("Ошибка получения паттерна из xml. Подробнее в InnerException", e);
            }
        }

    }

    public class PatternXmlException : Exception
    {
        public PatternXmlException()
        {
        }

        public PatternXmlException(string message) : base(message)
        {
        }

        public PatternXmlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class PatternPath
    {
        public PatternPath(string path)
        {
            Path = path;
        }

        public string[] Segments
        {
            get
            {
                return Path.Split('/');
            }
        }

        public PatternPath GetPattern(IEnumerable<Pattern> rootPatterns)
        {
            var deep = 0;
            throw new NotImplementedException();
        }

        public string Path { get; private set; }
    }

    public enum PatternTextType
    {
        Base,
        Regex
    }

    public class PatternText
    {
        public PatternText(string encoding, string text)
        {
            Encoding = encoding;
            Text = text;
        }

        public float Weight { get; set; }

        public PatternText()
        {
            
        }

        public static PatternText Parse(XElement te)
        {
            var value = te.Attribute(XName.Get("value")) != null ? te.Attribute(XName.Get("value")).Value : te.Value;

            var type = "base";
            if (te.Attribute(XName.Get("type")) != null)
                type = te.Attribute(XName.Get("type")).Value;


            var result = new PatternText(type, value);

            result.Weight = te.Attribute(XName.Get("weight")) != null ? Convert.ToSingle(te.Attribute(XName.Get("weight")).Value) : 1;

            return result;

        }

        public string Encoding { get;  set; }
        public string Text { get; set; }
    }

    public class PatternHeader
    {
        public PatternHeader(string name, string value)
        {
            Value = value;
            Name = name;
        }

        public static readonly PatternHeader Empty = new PatternHeader(string.Empty, string.Empty);

        public string Name { get;  set; }
        public string Value { get;  set; }
    }

}
