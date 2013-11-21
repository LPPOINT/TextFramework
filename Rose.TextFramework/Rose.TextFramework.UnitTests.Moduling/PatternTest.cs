using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rose.TextFramework.Moduling;

namespace Rose.TextFramework.UnitTests.Moduling
{
    [TestClass]
    public class PatternTest
    {
        [TestMethod]
        public void Pattern_ParseFromXml()
        {

            const string xml = @"<pattern name='patternName'>
                        <text type='rose' weight='3'>patternText</text>
                        </pattern>";

            var element = XElement.Parse(xml);

            const string exprectName = "patternName";
            const string exprectTextEncoding = "rose";
            const int exprectTextWeight = 3;
            const string exprectText = "patternText";

            try
            {
                var pattern = Pattern.Parse(element);
                Assert.AreEqual(pattern.Name, exprectName, "Несоответствие ожидаемого и актуального имени патерна");
                Assert.AreEqual(pattern.Texts.Count, 1);
                var text = pattern.Texts.First();

                Assert.AreEqual(text.Text, exprectText, "Несоответствие ожидаемого и актуального текста патерна");
                Assert.AreEqual(text.Weight, exprectTextWeight, "Несоответствие ожидаемого и актуального индекса текста патерна");
                Assert.AreEqual(text.Encoding, exprectTextEncoding, "Несоответствие ожидаемой и актуальнй кодировки текста патерна");

            }
            catch (Exception e)
            {
                Assert.Fail("При создании паттерна произошла ошибка: " + e.Message);
            }

        }
    }
}
