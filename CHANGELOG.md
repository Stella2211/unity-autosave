# Changelog / 変更履歴

All notable changes to this project will be documented in this file.
このプロジェクトの注目すべき変更はすべてこのファイルに記録されます。

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

フォーマットは[Keep a Changelog](https://keepachangelog.com/ja/1.0.0/)に基づいており、
このプロジェクトは[セマンティック バージョニング](https://semver.org/lang/ja/)に準拠しています。

## [1.0.0] - 2025-01-22

### Added / 追加機能
- Initial release of Unity AutoSave Extension / Unity AutoSave Extensionの初回リリース
- Automatic scene saving at configurable intervals / 設定可能な間隔での自動シーン保存
- Auto-save before entering play mode / プレイモード開始前の自動保存
- Backup mode with timestamped copies / タイムスタンプ付きコピーによるバックアップモード
- Console notifications for save operations / 保存操作時のコンソール通知
- Visual settings window / ビジュアル設定ウィンドウ
- Manual save command from menu / メニューからの手動保存コマンド
- Support for Unity 6 (2023.x and later) / Unity 6（2023.x以降）のサポート

### Technical Details / 技術詳細
- Minimum save interval of 1 minute / 最小保存間隔1分
- Automatic deactivation during play mode and compilation / プレイモードおよびコンパイル中の自動無効化
- Persistent settings storage using EditorPrefs / EditorPrefsを使用した永続的な設定保存
- Automatic initialization via [InitializeOnLoad] / [InitializeOnLoad]による自動初期化