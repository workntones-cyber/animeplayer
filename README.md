# AnimePlayer

広告なしのシンプルな動画プレイヤーです。

## 概要

Windows向けに作成した軽量・シンプルな動画プレイヤーです。
VLCをベースにしているため、幅広いフォーマットに対応しています。


## 動作環境

- OS: Windows 10 以降（64bit）
- .NET 10.0 Runtime
- DirectX 11 以降対応のGPU

## 対応フォーマット

### 動画
| フォーマット | 拡張子 |
|------------|--------|
| MP4 | .mp4 |
| Matroska | .mkv |
| AVI | .avi |
| QuickTime | .mov |
| Windows Media | .wmv |
| Flash Video | .flv |
| WebM | .webm |
| MPEG-TS | .ts .m2ts |

### 音声コーデック
- AAC
- MP3
- AC3 / DTS
- FLAC
- Opus
- Vorbis

### 動画コーデック
- H.264 / AVC
- H.265 / HEVC
- AV1
- VP8 / VP9
- MPEG-2
- DivX / Xvid

## インストール

1. [Releases](https://github.com/workntones-cyber/animeplayer/releases) から最新の`AnimePlayer_Setup.exe`をダウンロード
2. `AnimePlayer_Setup.exe`を実行してインストール

> **注意:** インストール時にWindowsのセキュリティ警告が表示される場合があります。
> これはコード署名証明書がないOSSアプリでは一般的な現象です。
> 「詳細情報」→「実行」をクリックしてインストールを続行してください。

## 使い方
1. `AnimePlayer.exe`を起動
2. 📂ボタンでファイルを開く
3. 動画ファイルをウィンドウにドラッグ＆ドロップ

## 操作

| 操作 | 機能 |
|------|------|
| 📂ボタン | ファイルを開く |
| ▶ / ⏸ボタン | 再生 / 一時停止 |
| ⏹ボタン | 停止 |
| シークバー | 再生位置の移動 |
| 音量スライダー | 音量調整 |
| ドラッグ＆ドロップ | ファイルを開く |

## 使用ライブラリ

- [LibVLCSharp](https://github.com/videolan/libvlcsharp) - LGPL 2.1
- [VideoLAN.LibVLC.Windows](https://www.nuget.org/packages/VideoLAN.LibVLC.Windows) - LGPL 2.1

## ライセンス

MIT License