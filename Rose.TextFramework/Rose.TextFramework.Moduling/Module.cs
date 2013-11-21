using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Rose.TextFramework.Moduling
{
    public abstract class Module
    {

        public Dictionary<string, object> Resources { get; private set; }

        public static Dictionary<string, object> ParseResources(XDocument document)
        {
            try
            {
                var res =
                    document.Root.Nodes()
                        .First(node => node.NodeType == XmlNodeType.Element && (node as XElement).Name == "res");
                if(res == null)
                    return new Dictionary<string, object>();
                var rese = res as XElement;
                var result = new Dictionary<string, object>();
                foreach (var child in rese.Nodes())
                {
                    if (child.NodeType == XmlNodeType.Element)
                    {
                        var ce = child as XElement;
                        if(ce.Attribute(XName.Get("name")) == null)
                            throw new ArgumentException("Ресурс должен иметь имя");
                        if (ce.Name == "text")
                        {
                            var resultText = PatternText.Parse(ce);
                            result.Add(ce.Attribute(XName.Get("name")).Value, resultText);
                        }
                    }
                }
                return result;
            }
            catch 
            {
                return new Dictionary<string, object>();
            }
        }

        public Module(XDocument xml)
        {
            Patterns = new List<Pattern>();
            Resources = ParseResources(xml);
            Name = xml.Root.Attribute(XName.Get("name")).Value;

            var patterns =
                xml.Root.Nodes()
                    .Where(
                        node => node.NodeType == XmlNodeType.Element && (node as XElement).Name == XName.Get("pattern"));

            foreach (var patternNode in patterns)
            {
                var pattern = Pattern.Parse(patternNode as XElement);
                Patterns.Add(pattern);
            }


        }

        public string Name { get; set; }
        public List<Pattern> Patterns { get; private set; } 


    }
}
