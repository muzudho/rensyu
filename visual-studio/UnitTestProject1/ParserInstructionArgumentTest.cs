namespace UnitTestProject1
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserInstructionArgumentTest
    {
        /// <summary>
        /// aliasコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestAliasInstructionArgumentParser()
        {
            var start = 5;
            AliasInstructionArgumentParser.Parse(
                "alias top2 = ply",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("top2 = ply", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 5;
            AliasInstructionArgumentParser.Parse(
                "alias right3 = b-name black-name player1-name",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("right3 = b-name black-name player1-name", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            AliasInstructionArgumentParser.Parse(
                "alias  right3  =  b-name  black-name  player1-name  ",
                5,
                (matched, curr) =>
                {
                    Assert.AreEqual("right3 = b-name black-name player1-name", matched?.ToDisplay());
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
        public void TestBoardInstructionArgumentParser()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseByLine(
                        appModel,
                        line,
                        (infoText) =>
                        {
                        },
                        (newAppModel) =>
                        {
                        },
                        (commentLine) =>
                        {
                            Trace.WriteLine($"Info            | Comment=[{commentLine}].");
                        },
                        (args) =>
                        {
                            // Puts.
                        },
                        (args) =>
                        {
                            // Sets.
                        });
                }
            }

            var start = 5;
            BoardInstructionArgumentParser.Parse(
                "board 19 ...................",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("19 ...................", matched?.ToDisplay(appModel));
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
