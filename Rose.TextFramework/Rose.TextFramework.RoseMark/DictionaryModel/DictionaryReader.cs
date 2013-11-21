using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Rose.TextFramework.RoseMark.DictionaryModel
{
    public class DictionaryReader
    {
        public DictionaryReader()
        {
            Dictionaries = new List<SectionsDictionary>();
        }

        public List<SectionsDictionary> Dictionaries { get; private set; }

        public void LoadDictionary(string dictionaryPath)
        {
            Dictionaries.Add(SectionsDictionary.Parse(XDocument.Load(dictionaryPath)));
        }

        public string[] GetStrings(string sectionName)
        {
            return (from sectionsDictionary in Dictionaries from section in sectionsDictionary.Sections where section.Name == sectionName from s in section.Strings select s.Text).ToArray();
        }


    }
}
