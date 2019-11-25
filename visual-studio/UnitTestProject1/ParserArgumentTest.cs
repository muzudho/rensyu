namespace UnitTestProject1
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserArgumentTest
    {
        /// <summary>
        /// setコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetsInstructionArgument()
        {
            var start = 3;
            SetsInstructionArgumentParser.Parse(
                "set b-name.value = Kifuwarabe",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-name.value = Kifuwarabe", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            SetsInstructionArgumentParser.Parse(
                "set b-name = Kifuwarabe",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-name.value = Kifuwarabe", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            SetsInstructionArgumentParser.Parse(
                "set  b-time  =  10:00  ",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-time.value = 10:00", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            SetsInstructionArgumentParser.Parse(
                "set b-hama =",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-hama.value = ", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestColorInstructionArgumentTest()
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

            var start = 5;
            CellRangeListArgumentParser.Parse(
                "black A19 K1 T1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("A19 K1 T1", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 5;
            CellRangeListArgumentParser.Parse(
                "white B19 K2 S1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("B19 K2 S1", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 5;
            CellRangeListArgumentParser.Parse(
                "space C19 K3 R1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("C19 K3 R1", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // 混合型☆（＾～＾）
            start = 5;
            CellRangeListArgumentParser.Parse(
                "space A2:B1 C4:D3 E5",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("A2:B1 C4:D3 E5", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // 大文字・小文字は区別するぜ☆（＾～＾）
            start = 5;
            CellRangeListArgumentParser.Parse(
                "black a19 k1 t1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreNotEqual("A19 K1 T1", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// 国際式囲碁の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestRowAddressParser()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var text = @"
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
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

            Assert.AreEqual(
                @"""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""",
                appModel.GetStringList(ApplicationObjectModel.RowNumbersRealName).ValueAsText());

            int start;

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾）"19" が 0行目、 "1" が 18行目だぜ☆（＾～＾）
            start = 0;
            RowAddressParser.Parse(
                "1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual(18, matched?.NumberO0);
                    Assert.AreEqual(start + 1, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾） "15" は 4行目だぜ☆（＾～＾）
            start = 14;
            RowAddressParser.Parse(
                "1234567890123415",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual(4, matched?.NumberO0);
                    Assert.AreEqual(start + 2, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // 表記確認☆（＾～＾）
            start = 0;
            RowAddressParser.Parse(
                "1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("1", matched?.ToDisplayTrimed(appModel));
                    Assert.AreEqual(start + 1, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 14;
            RowAddressParser.Parse(
               "1234567890123415",
               start,
               appModel,
               (matched, curr) =>
               {
                   Assert.AreEqual("15", matched?.ToDisplayTrimed(appModel));
                   Assert.AreEqual(start + 2, curr);
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
