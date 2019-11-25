﻿namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class StarsModelController
    {
        public static void ChangeModel(ApplicationObjectModelWrapper appModel, SetsInstructionArgument args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            switch (args.Property)
            {
                case "value":
                    var cellAddresses = args.Value.Split(',');
                    for (int i = 0; i < cellAddresses.Length; i++)
                    {
                        // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                        var token = cellAddresses[i].Trim();
                        if (1 < token.Length)
                        {
                            cellAddresses[i] = token.Substring(1, token.Length - 2);
                        }
                    }

                    var oldValue = appModel.GetStringList(ApplicationObjectModel.StarsRealName).Value;
                    var newValue = new List<string>(cellAddresses);
                    appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Join(' ', oldValue), string.Join(' ', newValue));
                    appModel.GetStringList(ApplicationObjectModel.StarsRealName).Value = newValue;
                    break;
            }
        }
    }
}
