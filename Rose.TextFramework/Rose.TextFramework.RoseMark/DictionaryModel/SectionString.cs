using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Rose.TextFramework.RoseMark.DictionaryModel
{
    public class SectionString
    {

        public SectionString(string text, IEnumerable<string> groups)
        {
            Text = text;
            Groups = new List<string>(groups);
        }

        private SectionString()
        {
            
        }

        public string Text { get; private set; }
        public List<string> Groups { get; private set; }

        public static SectionString Parse(XElement element)
        {
            try
            {
                if(element.Name != "string")
                    throw new ArgumentException("Неопознанное имя строки секции");
                var text = element.Value;
                var section = new SectionString();


                if (element.Attribute(XName.Get("groups")) != null)
                {
                    var groups = element.Attribute("groups").Value;
                    var groupsArray = groups.Split(' ');
                    section.Groups = new List<string>(groupsArray);
                }
                else
                {
                    section.Groups = new List<string>();
                }

                section.Text = text;
                return section;
            }
            catch (Exception e)
            {
                throw new DictionaryParseException("Ошибка создания элемента секции из xml узла. Подробности в InnerException.", e);
            }

        }

    }
}
