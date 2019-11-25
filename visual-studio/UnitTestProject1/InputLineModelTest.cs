namespace UnitTestProject1
{
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InputLineModelTest
    {
        [TestMethod]
        public void TestAliasCommand()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var line = @"alias top2 = ply sasite";

                InputLineModelController.ParseLine(appModel, line).ThenAlias(
                    (aliasInstruction) =>
                    {
                        Assert.IsTrue(appModel.ContainsKeyOfObjectRealName(new AliasName("ply")));
                        Assert.IsTrue(appModel.ContainsKeyOfObjectRealName(new AliasName("sasite")));
                        Assert.IsFalse(appModel.ContainsKeyOfObjectRealName(new AliasName("dog-food")));
                    },
                    () => { Assert.Fail(); }
                    ).ThenComment(
                        (commentLine) => { Assert.Fail(); },
                        () => { }
                    ).ThenInfo(
                        (infoText) => { Assert.Fail(); },
                        () => { }
                    ).ThenJson(
                        (jsonAppModel) => { Assert.Fail(); },
                        () => { }
                    ).ThenPut(
                        (putsArgs) => { Assert.Fail(); },
                        () => { }
                    ).ThenSet(
                        (setsArgs) => { Assert.Fail(); },
                        () => { }
                    );
            }
        }

        [TestMethod]
        public void TestCommentLine()
        {
            var appModel = new ApplicationObjectModelWrapper();
            appModel.ModelChangeLogWriter.Enable = false;

            {
                var line = @"# This is a comment line.";

                InputLineModelController.ParseLine(appModel, line).ThenAlias(
                        (aliasInstruction) => { Assert.Fail(); },
                        () => { }
                    ).ThenComment(
                        (commentLine) =>
                        {
                            Assert.AreEqual(line, commentLine);
                        },
                        () => { Assert.Fail(); }
                    ).ThenInfo(
                        (infoText) => { Assert.Fail(); },
                        () => { }
                    ).ThenJson(
                        (jsonAppModel) => { Assert.Fail(); },
                        () => { }
                    ).ThenPut(
                        (putsArgs) => { Assert.Fail(); },
                        () => { }
                    ).ThenSet(
                        (setsArgs) => { Assert.Fail(); },
                        () => { }
                    );
            }
        }
    }
}
