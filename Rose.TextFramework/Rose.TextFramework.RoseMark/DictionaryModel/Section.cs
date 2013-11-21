using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Rose.TextFramework.RoseMark.DictionaryModel
{
    public class Section
    {
        public Section(string name, List<SectionString> strings)
        {
            Strings = strings;
            Name = name;
        }

        private Section()
        {
            
        }

        public string Name { get; private set; }
        public List<SectionString> Strings { get; private set; }

        public static Section Parse(XElement element)
        {
            var section = new Section();
            section.Strings = new List<SectionString>();
            if(element.Name != XName.Get("section"))
                throw new ArgumentException("Неопознанное имя секции");
            var name = element.Attribute("name").Value;
            section.Name = name;
            foreach (var node in element.Nodes())
            {
                if (node.NodeType == XmlNodeType.Element && (node as XElement).Name == "string")
                {
                    section.Strings.Add(SectionString.Parse((node as XElement)));
                }
            }
            return section;
        }

    }
}
