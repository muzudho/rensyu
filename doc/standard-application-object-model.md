# Standard application object model


C# と Rust でデータ構造を共通化させるための仕様だぜ☆（＾～＾） JSONで投げつけてデシリアライズを狙うぜ☆（＾～＾）


# Application object model

## 定数

|name         |description|
|-------------|-----------|
|b-hama       |           |
|b-name       |           |
|b-time       |           |
|column-size  |           |
|info         |           |
|interval-msec|           |
|komi         |           |
|move         |           |
|ply          |           |
|row-size     |           |
|w-hama       |           |
|w-name       |           |
|w-time       |           |


## プロパティ

|name         |type                                  |
|-------------|--------------------------------------|
|Board        |BoardModel                            |
|Booleans     |Dictionary<string, PropertyBool>      |
|Numbers      |Dictionary<string, PropertyNumber>    |
|Strings      |Dictionary<string, PropertyString>    |
|StringLists  |Dictionary<string, PropertyStringList>|


## メソッド

* Parse
* ToJson


# Board model

## プロパティ

|name         |type       |
|-------------|-----------|
|RowSize      |int        |
|ColumnSize   |int        |
|Stones       |List<Stone>|


# Stone

## 定数

|name         |int|description|
|-------------|---|-----------|
|None         |  0|空点        |
|Black        |  1|黒石        |
|White        |  2|白石        |
|Wall         |  3|壁          |


# Property bool

## プロパティ

|name         |type       |
|-------------|-----------|
|Title        |title      |
|Value        |bool       |
|Visible      |bool       |

## メソッド

* ValueAsText()


# Property number

## プロパティ

|name         |type       |
|-------------|-----------|
|Title        |title      |
|Value        |double     |
|Visible      |bool       |

## メソッド

* ValueAsText()


# Property string

## プロパティ

|name         |type       |
|-------------|-----------|
|Title        |title      |
|Value        |string     |
|Visible      |bool       |

## メソッド

* ValueAsText()


# Property string list

## プロパティ

|name         |type        |
|-------------|------------|
|Title        |title      |
|Value        |List<string>|
|Visible      |bool        |

## メソッド

* ValueAsText()
    * `","` で Join して `"` で挟む。エスケープはまだやってない。
        * $"\"{string.Join("\",\"", this.Value)}\""


書きかけ
