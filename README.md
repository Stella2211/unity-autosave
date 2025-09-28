# 🎮 Unity 6 AutoSave Extension

*[日本語版 README はこちら](README_ja.md)*

A lightweight auto-save editor extension for Unity 6 that prevents data loss from unexpected editor crashes.

## ✨ Features

- **🔄 Automatic Scene Saving**: Automatically saves your scene at configurable intervals (default: 10 minutes)
- **🎬 Play Mode Protection**: Automatically saves before entering play mode
- **💾 Backup Mode**: Option to save timestamped copies without overwriting the original
- **📢 Console Notifications**: Optional logging of save operations
- **⚡ Manual Save Command**: Quick save command from the menu
- **🎨 Visual Settings Window**: User-friendly configuration interface

## 📦 Installation

### Method 1: Unity Package Manager (Git URL) ⭐ Recommended

1. Open `Window > Package Manager` in the Unity Editor
2. Click the `+` button in the top left corner
3. Select `Add package from git URL...`
4. Enter the following URL:
   ```
   https://github.com/Stella2211/unity-autosave.git
   ```
5. Click `Add`

### Method 2: Manual Installation ⚠️ Not Recommended
This method does not support automatic updates, so it's not recommended.
1. Copy `AutoSaveExtension.cs` and `AutoSaveSettingsWindow.cs` files to your project's `Assets/Editor/` folder
2. Unity will automatically compile and initialize the extension
3. The auto-save feature is enabled by default

## 🚀 Usage

### 🔧 Accessing Settings
- Navigate to `Tools > AutoSave > Settings` in the Unity menu bar
- Configure your preferences in the settings window

### ⚙️ Configuration Options
- **✅ Enable AutoSave**: Toggle the auto-save feature on/off
- **⏱️ Save Interval**: Set the interval for automatic saves (1-60 minutes)
- **📋 Save as Copy**: Create timestamped backup files without overwriting
- **🔔 Show Notifications**: Display console messages when saving

### 🎯 Manual Controls
- **💾 Save Now**: `Tools > AutoSave > Save Now` - Execute an immediate save
- **🔄 Reset Timer**: Available in the settings window - Reset the countdown timer

## ⚠️ Important Notes

- This extension only works with scenes that have been manually saved at least once
- Auto-save is disabled during play mode and compilation
- Minimum save interval is 1 minute to prevent excessive saving
- Backup files use the format: `SceneName_AutoSave_YYYYMMDD_HHMMSS.unity`

## 📁 Package Structure

```
unity-autosave/
├── package.json
├── README.md
├── LICENSE
├── CHANGELOG.md
└── Editor/
    ├── AutoSaveExtension.cs
    └── AutoSaveSettingsWindow.cs
```

## 📄 License

This extension is provided under the MIT License.

## 🔍 Troubleshooting

If auto-save is not working:
1. ✔️ Ensure the scene has been manually saved at least once
2. ✔️ Verify the extension is enabled in the settings
3. ✔️ Check the Unity console for error messages
4. ✔️ Confirm you're not in play mode or during compilation

## 🤝 Contributing

Feel free to submit issues and pull requests to improve this extension!

## 📝 Changelog

### v1.0.0 (2025-01-22)
- 🎉 Initial release
- 🔄 Basic auto-save functionality
- 🎬 Auto-save before play mode
- 💾 Backup mode support
- 🎨 Settings window UI