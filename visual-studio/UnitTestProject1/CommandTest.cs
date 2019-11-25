﻿namespace UnitTestProject1
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandTest
    {
        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetRowNumbers()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var line = @"set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""";
                InputController.ParseByLine(
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

            Assert.AreEqual(
                @"""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""",
                appModel.GetStringList(ApplicationObjectModel.RowNumbersRealName).ValueAsText());
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBlackA1()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
black K10
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputController.ParseByLine(
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

            // TODO 番地を指定して石を取るメソッドがない？
        }

        /// <summary>
        /// Putsコマンドのテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestPutsCommand()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
put black to A10
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputController.ParseByLine(
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
                            Assert.IsTrue(args is PutsInstructionArgument);
                        },
                        (args) =>
                        {
                            // Sets.
                        });
                }
            }

            var start = 3;
            PutsInstructionArgumentParser.Parse(
                "put black to A19",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("black to A19", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            PutsInstructionArgumentParser.Parse(
                "put white to A1:B4 K11 S16:T19",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("white to A1:B4 K11 S16:T19", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            PutsInstructionArgumentParser.Parse(
                "put space to K11",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("space to K11", matched?.ToDisplay(appModel));
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