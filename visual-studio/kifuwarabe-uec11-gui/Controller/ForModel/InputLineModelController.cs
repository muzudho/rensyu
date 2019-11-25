namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// メイン・ウィンドウがでかくなるから　こっちへ切り離すぜ☆（＾～＾）
    /// </summary>
    public static class InputLineModelController
    {
        public delegate void ReadsCallback(string text);

        public static void Read(ApplicationObjectModelWrapper appModel, MainWindow appView, ReadsCallback callback)
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (null == appView)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (null == callback)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var text = appView.InputTextReader.ReadToEnd();

            // 空行は無視☆（＾～＾）
            if (!string.IsNullOrWhiteSpace(text))
            {
                // ログに取るぜ☆（＾～＾）
                Trace.WriteLine($"Text            | {text}");
                appView.CommunicationLogWriter.WriteLine(text);
                appView.CommunicationLogWriter.Flush();
            }

            foreach (var line in text.Split(Environment.NewLine))
            {
                callback(line);
            }
        }

        public delegate void CommentViewCallback(string commentLine);
        public delegate void InfoViewCallback(string text);
        public delegate void JsonViewCallback(ApplicationObjectModelWrapper appModel);
        public delegate void PutsViewCallback(PutsInstructionArgument args);
        public delegate void SetsViewCallback(SetsInstructionArgument args);

        public static void ParseByLine(
            ApplicationObjectModelWrapper appModel,
            string line,
            CommentViewCallback commentViewCallback,
            InfoViewCallback infoViewCallback,
            JsonViewCallback jsonViewCallback,
            PutsViewCallback putsViewCallback,
            SetsViewCallback setsViewCallback
        )
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            InputLineParser.ParseByLine(
                line,
                appModel,
                (aliasInstruction) =>
                {
                    var args = (AliasInstructionArgument)aliasInstruction.Argument;
                    Trace.WriteLine($"Info            | Alias1 RealName=[{args.RealName.Value}] args=[{args.ToDisplay()}]");

                    foreach (var alias in args.AliasList)
                    {
                        Trace.WriteLine($"Info            | Alias2 [{alias.Value}] = [{args.RealName.Value}]");
                        if (!appModel.TryAddObjectRealName(alias, args.RealName))
                        {
                            Trace.WriteLine($"Info            | Alias2b [{alias.Value}] is already exists.");
                        }
                    }
                    Trace.WriteLine($"Info            | Alias3 {aliasInstruction.Command} RealName={args.RealName.Value} args=[{args.ToDisplay()}]");
                },
                (boardInstruction) =>
                {
                    var args = (BoardInstructionArgument)boardInstruction.Argument;
                    int zShapedIndex = CellAddress.ToIndex(args.RowAddress.NumberO0, 0, appModel);
                    int length = zShapedIndex + appModel.Board.ColumnSize;
                    // Trace.WriteLine($"Command            | {instruction.Command} row={args.RowAddress.NumberO0} cellIndex={cellIndex} columns={args.Columns}");

                    // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                    foreach (var columnChar in args.Columns)
                    {
                        // Trace.WriteLine($"Column          | Ch=[{columnChar}]");
                        if (length <= zShapedIndex)
                        {
                            break;
                        }

                        switch (columnChar)
                        {
                            case 'b':
                                // 黒石にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToBlack(appModel, zShapedIndex);
                                zShapedIndex++;
                                break;
                            case 'w':
                                // 白石にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToWhite(appModel, zShapedIndex);
                                zShapedIndex++;
                                break;
                            case '.':
                                // 空点にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToSpace(appModel, zShapedIndex);
                                zShapedIndex++;
                                break;
                        }
                    }
                },
                (commentLine) =>
                {
                    commentViewCallback(commentLine);
                },
                (exitsInstruction) =>
                {
                    // このアプリケーションを終了します。
                    System.Windows.Application.Current.Shutdown();
                },
                (infoInstruction) =>
                {
                    // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                    // プロパティ☆（＾～＾）
                    var args = (InfoInstructionArgument)infoInstruction.Argument;

                    // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                    infoViewCallback(MainWindow.SoluteNewline(args.Text));
                },
                (jsonInstruction) =>
                {
                    var args = (JsonInstructionArgument)jsonInstruction.Argument;
                    Trace.WriteLine($"Json            | {jsonInstruction.Command} args.Json.Length={args.Json.Length}");

                    jsonViewCallback(new ApplicationObjectModelWrapper(ApplicationObjectModel.Parse(args.Json)));
                },
                (putsInstruction) =>
                {
                    // モデルに値をセットしようぜ☆（＾～＾）
                    var args = (PutsInstructionArgument)putsInstruction.Argument;

                    // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                    appModel.MatchObjectRealName(
                        args.Name,
                        (RealName realName) =>
                        {
                            if (realName.Value == InputLineParser.BlackObject)
                            {
                                var args = (PutsInstructionArgument)putsInstruction.Argument;
                                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                        foreach (var cellRange in args.Destination.CellRanges)
                                {
                                    foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                                    {
                                                // 黒石にするぜ☆（＾～＾）
                                                StoneModelController.ChangeModelToBlack(appModel, zShapedIndex);
                                    }
                                }
                            }
                            else if (realName.Value == InputLineParser.WhiteObject)
                            {
                                var args = (PutsInstructionArgument)putsInstruction.Argument;
                                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                        foreach (var cellRange in args.Destination.CellRanges)
                                {
                                    foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                                    {
                                                // 白石にするぜ☆（＾～＾）
                                                StoneModelController.ChangeModelToWhite(appModel, zShapedIndex);
                                    }
                                }
                            }
                            else if (realName.Value == InputLineParser.SpaceObject)
                            {
                                var args = (PutsInstructionArgument)putsInstruction.Argument;
                                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                        foreach (var cellRange in args.Destination.CellRanges)
                                {
                                    foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                                    {
                                                // 石を取り除くぜ☆（＾～＾）
                                                StoneModelController.ChangeModelToSpace(appModel, zShapedIndex);
                                    }
                                }
                            }
                            else
                            {
                                Trace.WriteLine($"Warning         | {putsInstruction.Command} RealName=[{realName.Value}] args=[{args.ToDisplay(appModel)}] are not implemented.");
                            }
                        });

                    // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                    putsViewCallback(args);
                },
                (setsInstruction) =>
                {
                    // モデルに値をセットしようぜ☆（＾～＾）
                    var args = (SetsInstructionArgument)setsInstruction.Argument;

                    // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                    appModel.MatchObjectRealName(
                        args.Name,
                        (RealName realName) =>
                        {
                                    // これが参照渡しになっているつもりだが……☆（＾～＾）
                                    appModel.MatchPropertyOption(
                                realName,
                                (propModel) =>
                                {
                                            // .typeプロパティなら、propModelはヌルで構わない。
                                            PropertyModelController.ChangeModel(appModel, realName, propModel, args);
                                },
                                () =>
                                {
                                            // モデルが無くても働くプロパティはある☆（＾～＾）
                                            PropertyModelController.ChangeModel(appModel, realName, null, args);
                                });
                        });

                    // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                    setsViewCallback(args);
                },
                ()=>
                {
                    // 何もしないぜ☆（＾～＾）
                });
        }
    }
}
