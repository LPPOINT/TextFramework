using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace Rose.TextFramework.Lingvo.Synonyms
{
    public class WebSynonymsSource : SynonymsSource
    {

        private HtmlNode DeepSearch(HtmlNode node, string name, string value)
        {
            if (node.Attributes.Contains(name) && node.Attributes[name].Value == value)
                return node;
            foreach (var child in node.ChildNodes)
            {
                var result = DeepSearch(child, name, value);
                if (result != null)
                    return result;
            }
            return null;
        }

        public override IEnumerable<string> GetSynonyms(string str)
        {
            try
            {
                var request = WebRequest.Create("http://synonymonline.ru/о/" + str);
                var response = (HttpWebResponse)request.GetResponse();
                var html = new HtmlDocument();
                html.Load(response.GetResponseStream(), Encoding.UTF8);
                var result = new List<string>();
                var table = DeepSearch(html.DocumentNode, "class", "syn");
                if (table != null)
                {
                    foreach (var child in table.ChildNodes)
                    {
                        if (child != null && child.Name == "li")
                        {
                            var span = child.ChildNodes.FindFirst("span");
                            if(span != null)
                                result.Add(span.InnerText);
                        }
                    }
                }
                return result;
            }
            catch(Exception e)
            {
                return new List<string>();
            }
        }
    }
}
