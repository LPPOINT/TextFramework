using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rose.TextFramework.Moduling;

namespace Rose.TextFramework.UnitTests.Moduling
{
    [TestClass]
    public class PatternEncodingTest
    {
        [TestMethod]
        public void PatternEncoding_BaseEncoding()
        {
            var encoding = new BaseEncoding();

            const string pattern = "base encoding test string";
            const string input1 = "base encoding test string";
            const string input2 = "base encoding test string not by pattern";


            Assert.IsTrue(encoding.Encode(pattern, input1).Result);
            Assert.IsFalse(encoding.Encode(pattern, input2).Result);

        }

       [TestMethod]
        public void PatternEncoding_RoseEncoding_FunctionAny()
        {
            var encoding = new RoseMarkEncoding();

            const string pattern = "base encoding test [any(\"string\", \"text\")] for unit";
            const string input1 = "base encoding test text for unit";
            const string input2 = "base encoding test string for unit";

            Assert.IsTrue(encoding.Encode(input1, pattern).Result);
            Assert.IsTrue(encoding.Encode(input2, pattern).Result);

        }

       [TestMethod]
       public void PatternEncoding_RoseEncoding_FunctionMay()
       {
           var encoding = new RoseMarkEncoding();

           const string pattern = "base encoding test[may(\" string\", \" text\")] for unit";
           const string input1 = "base encoding test text for unit";
           const string input2 = "base encoding test string for unit";
           const string input3 = "base encoding test for unit";
           const string input4 = "base encoding test blablabla for unit";

           Assert.IsTrue(encoding.Encode(input1, pattern).Result);
           Assert.IsTrue(encoding.Encode(input2, pattern).Result);
           Assert.IsTrue(encoding.Encode(input3, pattern).Result);
           Assert.IsFalse(encoding.Encode(input4, pattern).Result);
       }

    }
}
