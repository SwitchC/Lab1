using NUnit.Framework;
using ParserSpace;
namespace TestResult
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
            string line = "max( 1 2. 1 5)*min(~1.12)+12^2";
            string actual = parser.result(line);
            string expected = "129";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Test2()
        {
            Parser parser = new Parser();
            string line = "12+11*~1";
            string actual = parser.result(line);
            string expected = "1";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Test3()
        {
            Parser parser = new Parser();
            string line = "max(12.))";
            string actual = parser.result(line);
            string expected = "Error";
            Assert.AreEqual(expected, actual);
        }
    }
}