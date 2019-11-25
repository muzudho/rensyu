﻿namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using System.Text.RegularExpressions;
    using KifuwarabeUec11Gui.InputScript;

    public static class WhiteSpaceParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(WhiteSpace matched, int curr);
        public delegate int NoneCallback();

        /// <summary>
        /// ホワイト・スペース☆（＾～＾）
        /// </summary>
        private static Regex regex = new Regex("^(\\s+)", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="someCallback"></param>
        /// <returns>Next.</returns>
        public static int Parse(
            string text,
            int start,
            SomeCallback someCallback,
            NoneCallback noneCallback)
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (text == null || text.Length <= start)
            {
                return noneCallback();
            }

            var m = regex.Match(text.Substring(start));
            if (m.Success)
            {
                // 一致。
                var whiteSpaces = m.Groups[1].Value;
                return someCallback(new WhiteSpace(whiteSpaces), start + whiteSpaces.Length);
            }
            else
            {
                return noneCallback();
            }
        }
    }
}
