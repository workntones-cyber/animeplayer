[Setup]
AppName=AnimePlayer
AppVersion=1.0.0
DefaultDirName={autopf}\AnimePlayer
DefaultGroupName=AnimePlayer
OutputDir=C:\Users\infon\source\repos\animeplayer\installer_output
OutputBaseFilename=AnimePlayer_Setup
Compression=lzma
SolidCompression=yes
SetupIconFile=app.ico
ChangesAssociations=yes

[Files]
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net6.0-windows\AnimePlayer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net6.0-windows\AnimePlayer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net6.0-windows\AnimePlayer.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net6.0-windows\LibVLCSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net6.0-windows\LibVLCSharp.WPF.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net6.0-windows\libvlc\win-x64\*"; DestDir: "{app}\libvlc\win-x64"; Flags: ignoreversion recursesubdirs
Source: "C:\Users\infon\source\repos\animeplayer\app.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\AnimePlayer"; Filename: "{app}\AnimePlayer.exe"
Name: "{commondesktop}\AnimePlayer"; Filename: "{app}\AnimePlayer.exe"

[Registry]
Root: HKCR; Subkey: ".mp4\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".mkv\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".avi\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".mov\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".wmv\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".flv\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".webm\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".ts\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".m2ts\OpenWithProgids"; ValueType: string; ValueName: "AnimePlayer"; ValueData: ""; Flags: uninsdeletevalue
Root: HKCR; Subkey: "AnimePlayer"; ValueType: string; ValueName: ""; ValueData: "Video File"; Flags: uninsdeletekey
Root: HKCR; Subkey: "AnimePlayer\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\app.ico"
Root: HKCR; Subkey: "AnimePlayer\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\AnimePlayer.exe"" ""%1"""

[Run]
Filename: "{app}\AnimePlayer.exe"; Description: "AnimePlayer を起動"; Flags: nowait postinstall skipifsilent