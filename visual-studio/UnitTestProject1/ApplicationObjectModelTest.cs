﻿namespace UnitTestProject1
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            appModel.ModelChangeLogWriter.Enable = false;

            var text = @"
alias top2 = ply
set top2.type = number
set top2.title = 手目
set top2.value = 2
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
            appModel.ModelChangeLogWriter.Enable = false;

            var top2RealName = new RealName("top2");
            Assert.IsFalse(appModel.ContainsKeyOfNumbers(top2RealName));

            appModel.MatchObjectRealName(
                top2RealName.Value,
                (RealName realName)=>
                {
                    // 指定した文字列が、そのまま出てくる☆（＾～＾）
                    Assert.AreEqual("top2", realName.Value);
                });

            var line = "alias top2 = ply";
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
            appModel.ModelChangeLogWriter.Enable = false;

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
                ()=>
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
            appModel.ModelChangeLogWriter.Enable = false;

            var infoRealName = new RealName("info");
            Assert.IsFalse(appModel.ContainsKeyOfStrings(infoRealName));

            appModel.AddProperty(infoRealName, new PropertyString("#info", "Hello, world!"));
            Assert.IsTrue(appModel.ContainsKeyOfStrings(infoRealName));

            appModel.RemoveProperty(
                infoRealName,
                (value) =>
                {
                    Assert.IsTrue(value is PropertyString);
                    Assert.AreEqual("#info", value.Title);
                    Assert.AreEqual("Hello, world!", value.ValueAsText());
                },
                ()=>
                {
                });
        }
    }
}
