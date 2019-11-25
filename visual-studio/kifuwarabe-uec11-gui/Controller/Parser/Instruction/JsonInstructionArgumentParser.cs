﻿namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using KifuwarabeUec11Gui.InputScript;

    public static class JsonInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (JsonInstructionArgument, int) Parse(string text, int start)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var next = start;

            // 行の残り全部を読み取るぜ☆（＾～＾）
            string value = text.Substring(next);

            // 列と行の両方マッチ☆（＾～＾）
            return (new JsonInstructionArgument(value.Trim()), next);
        }
    }
}
