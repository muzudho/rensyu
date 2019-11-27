﻿namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ApplicationObjectModelProperty1
    {
        /// <summary>
        /// 論理型プロパティ my-visible の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBoolProperty()
        {
            var appModel = new ApplicationObjectModelWrapper();

            appModel.AddBool(new RealName("my-visible"), new PropertyBool("", true));

            Assert.AreEqual(
                "true",
                appModel.GetBool(new RealName("my-visible")).ValueAsText()
            );
        }

        /// <summary>
        /// 数値型プロパティ my-age の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestNumberProperty()
        {
            var appModel = new ApplicationObjectModelWrapper();

            appModel.AddNumber(new RealName("my-age"), new PropertyNumber("", 39));

            Assert.AreEqual(
                "39",
                appModel.GetNumber(new RealName("my-age")).ValueAsText()
            );
        }

        /// <summary>
        /// 文字列型プロパティ my-name の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringProperty()
        {
            var appModel = new ApplicationObjectModelWrapper();

            appModel.AddString(new RealName("my-name"), new PropertyString("", "warabemoti"));

            Assert.AreEqual(
                "warabemoti",
                appModel.GetString(new RealName("my-name")).ValueAsText()
            );
        }

        /// <summary>
        /// 文字列リスト型プロパティ my-array の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringListProperty()
        {
            var appModel = new ApplicationObjectModelWrapper();

            appModel.AddStringList(new RealName("my-array"), new PropertyStringList("", new List<string>() { "a", "b", "c" }));

            Assert.AreEqual(
                @"""a"",""b"",""c""",
                appModel.GetStringList(new RealName("my-array")).ValueAsText()
            );
        }
    }

    [TestClass]
    public class ApplicationObjectModelProperty2
    {
        /// <summary>
        /// 論理型プロパティ my-visible の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBoolType()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-visible.type = bool";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.IsTrue(appModel.ContainsKeyOfBooleans(new RealName("my-visible")));
        }

        /// <summary>
        /// 数値型プロパティ my-age の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestNumberType()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-age.type = number";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.IsTrue(appModel.ContainsKeyOfNumbers(new RealName("my-age")));
        }

        /// <summary>
        /// 文字列型プロパティ my-name の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringType()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-name.type = string";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.IsTrue(appModel.ContainsKeyOfStrings(new RealName("my-name")));
        }

        /// <summary>
        /// 文字列リスト型プロパティ my-array の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringListType()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-array.type = string-list";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.IsTrue(appModel.ContainsKeyOfStringLists(new RealName("my-array")));
        }
    }

    [TestClass]
    public class ApplicationObjectModelProperty3
    {
        /// <summary>
        /// 論理型プロパティ my-visible の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBoolValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-visible.type = bool
set my-visible.value = true";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                "true",
                appModel.GetBool(new RealName("my-visible")).ValueAsText()
            );
        }

        /// <summary>
        /// 数値型プロパティ my-age の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestNumberValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-age.type = number
set my-age.value = 39";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                "39",
                appModel.GetNumber(new RealName("my-age")).ValueAsText()
            );
        }

        /// <summary>
        /// 文字列型プロパティ my-name の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-name.type = string
set my-name.value = warabemoti";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }


            Assert.AreEqual(
                "warabemoti",
                appModel.GetString(new RealName("my-name")).ValueAsText()
            );
        }

        /// <summary>
        /// 文字列リスト型プロパティ my-array の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringListValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-array.type = string-list
set my-array.value = ""a"",""b"",""c""";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                @"""a"",""b"",""c""",
                appModel.GetStringList(new RealName("my-array")).ValueAsText()
            );
        }
    }

    [TestClass]
    public class ApplicationObjectModelProperty4
    {
        /// <summary>
        /// 論理型プロパティ my-visible の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBoolValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-visible.type = bool
set my-visible = true";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                "true",
                appModel.GetBool(new RealName("my-visible")).ValueAsText()
            );
        }

        /// <summary>
        /// 数値型プロパティ my-age の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestNumberValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-age.type = number
set my-age = 39";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                "39",
                appModel.GetNumber(new RealName("my-age")).ValueAsText()
            );
        }

        /// <summary>
        /// 文字列型プロパティ my-name の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-name.type = string
set my-name = warabemoti";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }


            Assert.AreEqual(
                "warabemoti",
                appModel.GetString(new RealName("my-name")).ValueAsText()
            );
        }

        /// <summary>
        /// 文字列リスト型プロパティ my-array の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStringListValue()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set my-array.type = string-list
set my-array = ""a"",""b"",""c""";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                @"""a"",""b"",""c""",
                appModel.GetStringList(new RealName("my-array")).ValueAsText()
            );
        }
    }

    [TestClass]
    public class ApplicationObjectModelTest
    {
        /// <summary>
        /// 数値型プロパティの ply の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void PlyTestByCommand2()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var text = @"
alias top2 = ply
set top2.type = number
set top2.title = 手目
set top2.value = 2
";

            foreach (var line in text.Split(Environment.NewLine))
            {
                InputLineModelController.ParseLine(appModel, line);
            }

            var plyAliasName = new AliasName("ply");
            appModel.MatchObjectRealName(
                plyAliasName.Value,
                (plyRealName) =>
                {
                    Assert.IsTrue(appModel.ContainsKeyOfProperty(plyRealName));

                    appModel.MatchPropertyOption(
                        plyRealName,
                        (value) =>
                        {
                            Assert.IsTrue(value is PropertyNumber);
                            Assert.AreEqual("手目", value.Title);
                            Assert.AreEqual("2", value.ValueAsText());
                        },
                        () =>
                        {
                        });
                });
        }

        /// <summary>
        /// 数値型プロパティの ply の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void RealNameTest()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var top2RealName = new RealName("top2");
            Assert.IsFalse(appModel.ContainsKeyOfNumbers(top2RealName));

            appModel.MatchObjectRealName(
                top2RealName.Value,
                (RealName realName) =>
                {
                    // 指定した文字列が、そのまま出てくる☆（＾～＾）
                    Assert.AreEqual("top2", realName.Value);
                });

            var line = "alias top2 = ply";
            InputLineModelController.ParseLine(appModel, line);

            var plyAliasName = new AliasName("ply");
            appModel.MatchObjectRealName(
                plyAliasName.Value,
                (RealName realName) =>
                {
                    // 本名に変換されて出てくる☆（＾～＾）
                    Assert.AreEqual("top2", realName.Value);
                });

            appModel.MatchObjectRealName(
                top2RealName.Value,
                (RealName realName) =>
                {
                    // 本名は、そのまま出てくる☆（＾～＾）
                    Assert.AreEqual("top2", realName.Value);
                });
        }

        /// <summary>
        /// 数値型プロパティの ply の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void PlyTest()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var plyRealName = new RealName("top2");
            Assert.IsFalse(appModel.ContainsKeyOfNumbers(plyRealName));

            appModel.AddProperty(plyRealName, new PropertyNumber("手目"));
            Assert.IsTrue(appModel.ContainsKeyOfNumbers(plyRealName));

            appModel.RemoveProperty(
                plyRealName,
                (value) =>
                {
                    Assert.IsTrue(value is PropertyNumber);
                    Assert.AreEqual("手目", value.Title);
                    Assert.AreEqual("0", value.ValueAsText());
                },
                () =>
                {
                });
        }

        /// <summary>
        /// 文字列型プロパティの info の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void InfoTest()
        {
            var appModel = new ApplicationObjectModelWrapper();

            // info は初期実装☆（＾～＾）
            var infoRealName = new RealName("info");
            Assert.IsTrue(appModel.ContainsKeyOfStrings(infoRealName));

            appModel.AddProperty(infoRealName, new PropertyString("#info", "Hello, world!"));
            Assert.IsTrue(appModel.ContainsKeyOfStrings(infoRealName));

            // 消してもいいけど困るだけだぜ☆（＾～＾）
            appModel.RemoveProperty(
                infoRealName,
                (value) =>
                {
                    Assert.IsTrue(value is PropertyString);
                    Assert.AreEqual("#info", value.Title);
                    Assert.AreEqual("Hello, world!", value.ValueAsText());
                },
                () =>
                {
                });
        }
    }
}
