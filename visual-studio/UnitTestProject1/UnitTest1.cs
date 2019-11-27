namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// �ȒP�Ȏ��s���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            var boardModel = new BoardModelWrapper(new BoardModel());

            /*
            // �ȒP�Ȏ��s���i�O�`�O�j
            var builder = new StringBuilder();
            builder.Append("[");
            for (int i=0; i<100; i++)
            {
                builder.Append($"{i},");
            }
            builder.Append("]");
            Trace.WriteLine(builder.ToString());
            // */

            //*
            // �ȒP�Ȏ��s���i�O�`�O�j
            var builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < boardModel.ColumnSize; i++)
            {
                builder.Append($"{i * boardModel.ColumnSize},");
            }
            builder.Append("]");
            Trace.WriteLine(builder.ToString());
            // */
        }

        /// <summary>
        /// ���ێ��͌�̃Z���Ԓn�\�L���e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestInternationalCellAddress()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# ���ۈ͌�ł� I��͖����񂾂����i�O�`�O�j
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            // �Ƃ肠�������̃e�X�g�̃X�^�[�g��0�ɑ����Ă��������i�O�`�O�j
            var start = 0;

            // 2�����i�O�`�O�j
            var list1 = new List<string>()
            {
                "A1","B2","C3","D4","E5","F6","G7","H8","J9"
            };

            foreach (var item in list1)
            {
                Assert.AreEqual(2, CellAddressParser.Parse(
                    item,
                    start,
                    appModel,
                    (matched, curr) =>
                    {
                        Assert.AreEqual(item, matched?.ToDisplayTrimed(appModel));
                        if (matched != null)
                        {
                            return curr;
                        }
                        else
                        {
                            return start;
                        }
                    },
                    () =>
                    {
                        return start;
                    }));
            }

            // 3�����i�O�`�O�j
            var list2 = new List<string>()
            {
                "K10",
                "L11",
                "M12",
                "N13",
                "O14",
                "P15",
                "Q16",
                "R17",
                "S18",
                "T19"
            };

            foreach (var item in list2)
            {
                Assert.AreEqual(3, CellAddressParser.Parse(
                    item,
                    start,
                    appModel,
                    (matched, curr) =>
                    {
                        Assert.AreEqual(item, matched?.ToDisplayTrimed(appModel));
                        if (matched == null)
                        {
                            return start;
                        }

                        return curr;
                    },
                    () =>
                    {
                        return start;
                    }));
            }

            // �啶���E�������͋�ʂ��邺���i�O�`�O�j�����Z�b�g�̗�ԍ��ɏ������͖��������i�O�`�O�j
            Assert.AreEqual(start, CellAddressParser.Parse(
                "a1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.IsNull(matched?.ToDisplayTrimed(appModel));
                    if (matched == null)
                    {
                        return start;
                    }

                    return curr;
                },
                () =>
                {
                    return start;
                }));

            // �啶���E�������͋�ʂ��邺���i�O�`�O�j
            Assert.AreEqual(3, CellAddressParser.Parse(
                "T19",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreNotEqual("t19", matched?.ToDisplayTrimed(appModel));
                    if (matched == null)
                    {
                        return start;
                    }

                    return curr;
                },
                () =>
                {
                    return start;
                }));
        }

        /// <summary>
        /// ���ێ��͌�̗�ԍ��̃e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestInternationalColumnAddress()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# ���ۈ͌�ł� I��͖����񂾂����i�O�`�O�j
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var start = 0;
            ColumnAddressParser.Parse(
                "A",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("A", matched.ToDisplay(appModel));
                    Assert.AreEqual(1, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            ColumnAddressParser.Parse(
                "ABCDEFGHIJKLMNOPQRST",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("D", matched.ToDisplay(appModel));
                    Assert.AreEqual(4, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // I is a null.
            start = 0;
            ColumnAddressParser.Parse(
                "I",
                start,
                appModel,
                (matched, curr) =>
                {
                    // �s�������i�O�`�O�j
                    Assert.Fail();
                    return curr;
                },
                () =>
                {
                    // �������i�O���O�j
                    return start;
                });
        }

        /// <summary>
        /// JSON�̓Ǎ��e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestReadJson()
        {
            var appModel = ApplicationObjectModel.Parse("{\"board\":{\"rowSize\":15,\"columnSize\":13}}");

            Assert.AreEqual(15, appModel.Board.RowSize);
            Assert.AreEqual(13, appModel.Board.ColumnSize);
        }

        /// <summary>
        /// JSON�̓Ǎ��e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestReadStarsJson()
        {
            var appModel = ApplicationObjectModel.Parse("{\"stringLists\":{\"stars\":{\"value\":[\"A1\",\"B2\",\"C3\"]}}}");
            Assert.AreEqual(@"""A1"",""B2"",""C3""", appModel.StringLists[ApplicationObjectModel.StarsRealName.Value].ValueAsText());
        }

        /// <summary>
        /// �Z���̃C���f�b�N�X�̃e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestIndexOfCell()
        {
            var appModel = new ApplicationObjectModelWrapper();

            // �C���f�b�N�X�� ����� 0 �Ƃ��� Z�����B

            // 19�H��
            appModel.Board.RowSize = 19;
            appModel.Board.ColumnSize = 19;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(19 - 1, CellAddress.ToIndex(0, 19 - 1, appModel));
            Assert.AreEqual(19, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(19 * (19 - 1), CellAddress.ToIndex(19 - 1, 0, appModel));
            Assert.AreEqual(20 * (19 - 1), CellAddress.ToIndex(19 - 1, 19 - 1, appModel));

            // 15�H��
            appModel.Board.RowSize = 15;
            appModel.Board.ColumnSize = 15;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(15 - 1, CellAddress.ToIndex(0, 15 - 1, appModel));
            Assert.AreEqual(15, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(15 * (15 - 1), CellAddress.ToIndex(15 - 1, 0, appModel));
            Assert.AreEqual(16 * (15 - 1), CellAddress.ToIndex(15 - 1, 15 - 1, appModel));

            // 13�H��
            appModel.Board.RowSize = 13;
            appModel.Board.ColumnSize = 13;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(13 - 1, CellAddress.ToIndex(0, 13 - 1, appModel));
            Assert.AreEqual(13, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(13 * (13 - 1), CellAddress.ToIndex(13 - 1, 0, appModel));
            Assert.AreEqual(14 * (13 - 1), CellAddress.ToIndex(13 - 1, 13 - 1, appModel));

            // 9�H��
            appModel.Board.RowSize = 9;
            appModel.Board.ColumnSize = 9;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(9 - 1, CellAddress.ToIndex(0, 9 - 1, appModel));
            Assert.AreEqual(9, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(9 * (9 - 1), CellAddress.ToIndex(9 - 1, 0, appModel));
            Assert.AreEqual(10 * (9 - 1), CellAddress.ToIndex(9 - 1, 9 - 1, appModel));
        }

        /// <summary>
        /// ��ԍ��̃e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestColumnNumbers()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# ���ۈ͌�ł� I��͖����񂾂����i�O�`�O�j
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var columnNumbers = appModel.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).Value;

            Assert.AreEqual(0, columnNumbers.IndexOf("A"));
            Assert.AreEqual(1, columnNumbers.IndexOf("B"));
            Assert.AreEqual(2, columnNumbers.IndexOf("C"));
            Assert.AreEqual(3, columnNumbers.IndexOf("D"));
            Assert.AreEqual(4, columnNumbers.IndexOf("E"));
            Assert.AreEqual(5, columnNumbers.IndexOf("F"));
            Assert.AreEqual(6, columnNumbers.IndexOf("G"));
            Assert.AreEqual(7, columnNumbers.IndexOf("H"));
            Assert.AreNotEqual(8, columnNumbers.IndexOf("I"));
            Assert.AreEqual(8, columnNumbers.IndexOf("J"));
            Assert.AreEqual(9, columnNumbers.IndexOf("K"));
            Assert.AreEqual(10, columnNumbers.IndexOf("L"));
            Assert.AreEqual(11, columnNumbers.IndexOf("M"));
            Assert.AreEqual(12, columnNumbers.IndexOf("N"));
            Assert.AreEqual(13, columnNumbers.IndexOf("O"));
            Assert.AreEqual(14, columnNumbers.IndexOf("P"));
            Assert.AreEqual(15, columnNumbers.IndexOf("Q"));
            Assert.AreEqual(16, columnNumbers.IndexOf("R"));
            Assert.AreEqual(17, columnNumbers.IndexOf("S"));
            Assert.AreEqual(18, columnNumbers.IndexOf("T"));
        }
    }
}
