﻿namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InputScriptDocumentTest
    {
        [TestMethod]
        public void TestCommentLine()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var line = @"# This is a comment line.";

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
                        Assert.AreEqual(line, commentLine);
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

        [TestMethod]
        public void TestAliasCommand()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var line = @"alias top2 = ply sasite";

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
                        Assert.AreEqual(line, commentLine);
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
    }
}