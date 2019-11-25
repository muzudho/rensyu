﻿namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 行番号だぜ☆（＾～＾）
    /// 
    /// 実質、1～2桁の数字だぜ☆（＾～＾）
    /// </summary>
    public class RowAddress
    {

        /// <summary>
        /// 0から始まる（Origin 0）列番号☆（＾～＾）
        /// </summary>
        public int NumberO0 { get; private set; }

        public RowAddress(int numberO0)
        {
            this.NumberO0 = numberO0;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayNoTrim(ApplicationObjectModelWrapper model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var rowNumbers = model.GetStringList(ApplicationObjectModel.RowNumbersRealName).Value;

            if (this.NumberO0 < 0 || rowNumbers.Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return rowNumbers[this.NumberO0];
            }
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayTrimed(ApplicationObjectModelWrapper model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.NumberO0 < 0 || model.GetRowNumbersTrimed().Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return model.GetRowNumbersTrimed()[this.NumberO0];
            }
        }
    }
}
