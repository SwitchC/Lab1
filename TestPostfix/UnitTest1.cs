using NUnit.Framework;
using ParserSpace;
using System.Collections.Generic;
namespace TestPostfix
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Parser parser = new Parser();
            string line = "12+11";
            List<string> a = parser.postfix(line);
            string actual = "";
            foreach (string i in a)
            {
                actual += " " + i;
            }
            string expect = " 12 11 +";
            Assert.AreEqual(expect, actual);
        }
        [Test]
        public void Test2()
        {
            Parser parser = new Parser();
            string line = "max(12.11)";
            List<string> a = parser.postfix(line);
            string actual = "";
            foreach (string i in a)
            {
                actual += " " + i;
            }
            string expect = " 12 11 max";
            Assert.AreEqual(expect, actual);
        }
        [Test]
        public void Test3()
        {
            Parser parser = new Parser();
            string line = "~1+25^13";
            List<string> a = parser.postfix(line);
            string actual = "";
            foreach (string i in a)
            {
                actual += " " + i;
            }
            string expect = " 1 ~ 25 13 ^ +";
            Assert.AreEqual(expect, actual);
        }
    }
}