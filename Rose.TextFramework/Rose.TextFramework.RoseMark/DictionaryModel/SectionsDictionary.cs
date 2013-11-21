using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Rose.Common;

namespace Rose.TextFramework.RoseMark.DictionaryModel
{
    public class SectionsDictionary
    {
        public SectionsDictionary(List<Section> sections)
        {
            Sections = sections;
        }

        private SectionsDictionary()
        {
            
        }
        public List<Section> Sections { get; private set; }

        public static SectionsDictionary Parse(XDocument document)
        {
            Check.NotNull(document, "document");
            var dictionary = new SectionsDictionary();
            dictionary.Sections = new List<Section>();
            var root = document.Root;
            if(root.Name != "dictionary")
                throw new ArgumentException("Неопознанное имя словаря");
            foreach (var node in root.Nodes())
            {
                if (node.NodeType == XmlNodeType.Element && (node as XElement).Name == "section")
                {
                    dictionary.Sections.Add(Section.Parse(node as XElement));
                }
            }
            return dictionary;
        }

    }
}
