# BveEx.Toukaitetudou.RotateBackGround

背景を回転させるプラグインです.

## 利用方法

マップ構文, 設定ファイルを適切に記述してください.
尚, 動作には [BveEx](https://bveex.okaoka-depot.com/) が別途必要です.[![BveEx](https://www.okaoka-depot.com/contents/bve/banner_AtsEX.svg)](https://bveex.okaoka-depot.com/)

### 構文解説

```BveEx.User.Toukaitetudou.RotateBackGround.Change(r);```

この距離程から角速度`r`[rad/sec]で背景が右回転します.

## サンプルシナリオについて

サンプルシナリオでは内部でBveExサンプルシナリオを参照しています.
BveExサンプルシナリオと同階層に配置する等, 参照依存を適切に解決してください.

## よくあるであろうお問い合わせ

### 左回転させたい/停止させたいです

0以下の値も使用可能です.
負の値を指定すれば左回転, 0を指定すれば停止できます.

### ～ができません

殆どの場合仕様です.

気が向いたら作るかもですが, それが待てないならば是非ご自身で実装してください.

## ライセンス

[PolyForm Noncommercial License 1.0.0](LICENSE.md)

## 使用ライブラリ等

### [BveEx.CoreExtensions](https://github.com/automatic9045/BveEX)(PolyForm NonCommercial 1.0.0)

Copyright (c) 2022 automatic9045

### [BveEx.PluginHost](https://github.com/automatic9045/BveEX) (PolyForm NonCommercial 1.0.0)

Copyright (c) 2022 automatic9045

### [Harmony](https://github.com/pardeike/Harmony) (MIT)

Copyright (c) 2017 Andreas Pardeike

### [ObjectiveHarmonyPatch](https://github.com/automatic9045/ObjectiveHarmonyPatch) (MIT)

Copyright (c) 2022 automatic9045

### [SlimDX](https://www.nuget.org/packages/SlimDX/) (MIT)

Copyright (c) 2013  exDreamDuck
