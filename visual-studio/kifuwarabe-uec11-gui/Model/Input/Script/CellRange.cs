﻿namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.Model;

    public delegate void IndexCallback(int index);

    /// <summary>
    /// セルの範囲指定だぜ☆（＾～＾）
    /// `i7:k9` なら `i7 j7 k7 i8 j8 k8 i9 j9 k9` な☆（＾～＾）
    /// `k7:i9` なら `k7 j7 i7 k8 j8 i8 k9 j9 i9` な☆（＾～＾）
    /// ただし要素をデータとして持ったらメモリを圧迫するんで、持たないぜ☆（＾～＾）
    /// 
    /// また、 `k10` は `k10:k10` の省略形として認めるぜ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class CellRange
    {
        public CellAddress StartsCellAddress { get; private set; }
        public CellAddress EndsCellAddress { get; private set; }

        public CellRange(CellAddress startsCellAddress, CellAddress endsCellAddress)
        {
            this.StartsCellAddress = startsCellAddress;
            this.EndsCellAddress = endsCellAddress;
        }

        public void Foreach(ApplicationObjectModelWrapper model, IndexCallback callback)
        {
            // Trace.WriteLine($"Foreach         | {this.ToDisplay()} | sr={this.StartsCellAddress.RowAddress.ToDisplay()} sc={this.StartsCellAddress.ColumnAddress.ToDisplay()} er={this.EndsCellAddress.RowAddress.ToDisplay()} ec={this.EndsCellAddress.ColumnAddress.ToDisplay()}");

            // 長さを求めようぜ☆（＾～＾）
            var horizontalLength = this.EndsCellAddress.ColumnAddress.NumberO0 - this.StartsCellAddress.ColumnAddress.NumberO0;
            var verticalLength = this.EndsCellAddress.RowAddress.NumberO0 - this.StartsCellAddress.RowAddress.NumberO0;
            // Trace.WriteLine($"Length          | horizontal={horizontalLength} vertical={verticalLength}");

            // 幅を求めようぜ☆（＾～＾）
            var widthEdges = Math.Abs(horizontalLength);
            var heightEdges = Math.Abs(verticalLength);
            // Trace.WriteLine($"Edges           | width={widthEdges} height={heightEdges}");

            // 向きを作ろうぜ☆（＾～＾） -1 とか 1 を作ってるんだぜ☆（＾～＾）
            int horizontalDirection;
            if (widthEdges==0)
            {
                horizontalDirection = 1;
            }
            else
            {
                horizontalDirection = horizontalLength / widthEdges;
            }

            int verticalDirection;
            if (heightEdges == 0)
            {
                verticalDirection = 1;
            }
            else
            {
                verticalDirection = verticalLength / heightEdges;
            }
            // Trace.WriteLine($"Direction       | horizontal={horizontalDirection} vertical={verticalDirection}");

            for (int verticalZoom = 0; verticalZoom < heightEdges + 1; verticalZoom += 1)
            {
                for (int horizontalZoom = 0; horizontalZoom < widthEdges + 1; horizontalZoom += 1)
                {
                    var rowNumberO0 = this.StartsCellAddress.RowAddress.NumberO0 + verticalZoom * verticalDirection;
                    var columnNumberO0 = this.StartsCellAddress.ColumnAddress.NumberO0 + horizontalZoom * horizontalDirection;
                    var index = rowNumberO0 * model.Board.ColumnSize + columnNumberO0;
                    // Trace.WriteLine($"Index           | sr={this.StartsCellAddress.RowAddress.Number} vz={verticalZoom} row={rowNumber} | sc={this.StartsCellAddress.ColumnAddress.Number} hz={horizontalZoom} col={columnNumber} | index={index}.");
                    callback(index);
                }
            }
        }

        public HashSet<int> ToIndexes(ApplicationObjectModelWrapper model)
        {
            var hash = new HashSet<int>();

            Foreach(model, (index)=>
            {
                hash.Add(index);
            });

            return hash;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModelWrapper model)
        {
            var starts = this.StartsCellAddress.ToDisplayTrimed(model);
            var ends = this.EndsCellAddress.ToDisplayTrimed(model);

            if (starts == ends)
            {
                return $"{starts}";
            }

            return $"{starts}:{ends}";
        }
    }
}
