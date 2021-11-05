using Humanizer;
using NUnit.Framework;

namespace PortersonToXml.Tests
{
    public class HumanizerLearningTests
    {
        [Test]
        [TestCase("aaa", "aaa")]
        [TestCase("Bbb", "bbb")]
        [TestCase("CccDdd", "ccc_ddd")]
        [TestCase("EFGggHhh", "ef_ggg_hhh")]
        public void Pascal_to_snake(string input, string expected)
        {
            Assert.AreEqual(expected, input.Underscore());
        }

        [Test]
        [TestCase("Aaa", "Aaa")]
        [TestCase("bbb", "Bbb")]
        [TestCase("ccc_ddd", "CccDdd")]
        [TestCase("EF_ggg_hhh", "EFGggHhh")]
        [TestCase("e_f_ggg_hhh", "EFGggHhh")]
        [TestCase("ef_ggg_hhh", "EfGggHhh")]
        public void Snake_to_pascal(string input, string expected)
        {
            Assert.AreEqual(expected, input.Pascalize());
        }
    }
}
