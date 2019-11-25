namespace UnitTestProject1
{
    using KifuwarabeUec11Gui.Controller.Parser;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTokenArgumentTest
    {
        /// <summary>
        /// aliasコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStartsWithKeywordParser()
        {
            var start = 0;
            StartsWithKeywordParser.Parse(
                "banana",
                "banana1",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("banana", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// aliasコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestWhiteSpaceParser()
        {
            var start = 0;
            WhiteSpaceParser.Parse(
                "",
                start,
                (matched, curr) =>
                {
                    Assert.Fail();
                    return curr;
                },
                () =>
                {
                    // Success.
                    return start;
                });

            WhiteSpaceParser.Parse(
                " ",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual(" ", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            WhiteSpaceParser.Parse(
                "  ",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("  ", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestWordParser()
        {
            var start = 0;
            WordParser.Parse(
                "apple banana chery.",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("apple", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestWordUpToDelimiterParser()
        {
            var start = 0;
            WordUpToDelimiterParser.Parse(
                ":",
                "apple,banana:chery.daikon;",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("apple,banana", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }
    }
}
