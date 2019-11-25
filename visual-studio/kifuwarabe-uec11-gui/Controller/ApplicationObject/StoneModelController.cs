﻿namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 石を操作するぜ☆（＾～＾）
    /// 
    /// こっちを設定して、あっちを設定して、また　こっちに戻ってきて設定して、というような
    /// 無限ループしないようにセットするのもコントローラーのメリットだぜ☆（＾～＾）
    /// </summary>
    public static class StoneModelController
    {
        /// <summary>
        /// 黒石に変えようぜ☆（＾～＾）
        /// </summary>
        public static void ChangeModelToBlack(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            var oldValue = appModel.Board.GetStone(zShapedIndex);
            var newValue = Stone.Black;
            // TODO: ログが大量になってしまう☆（＾～＾）
            // appModel.ModelChangeLogWriter.WriteLine($"Stones[{zShapedIndex}]", oldValue.ToString(), newValue.ToString());
            appModel.Board.SetStone(zShapedIndex, newValue);
        }

        public static void ChangeModelToWhite(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            var oldValue = appModel.Board.GetStone(zShapedIndex);
            var newValue = Stone.White;
            // TODO: ログが大量になってしまう☆（＾～＾）
            // appModel.ModelChangeLogWriter.WriteLine($"Stones[{zShapedIndex}]", oldValue.ToString(), newValue.ToString());
            appModel.Board.SetStone(zShapedIndex, newValue);
        }

        public static void ChangeModelToSpace(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            var oldValue = appModel.Board.GetStone(zShapedIndex);
            var newValue = Stone.None;
            // TODO: ログが大量になってしまう☆（＾～＾）
            // appModel.ModelChangeLogWriter.WriteLine($"Stones[{zShapedIndex}]", oldValue.ToString(), newValue.ToString());
            appModel.Board.SetStone(zShapedIndex, newValue);
        }
    }
}
