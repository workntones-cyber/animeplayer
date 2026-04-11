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

[Files]
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net10.0-windows\AnimePlayer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net10.0-windows\AnimePlayer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net10.0-windows\AnimePlayer.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net10.0-windows\LibVLCSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net10.0-windows\LibVLCSharp.WPF.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\infon\source\repos\animeplayer\bin\Release\net10.0-windows\libvlc\win-x64\*"; DestDir: "{app}\libvlc\win-x64"; Flags: ignoreversion recursesubdirs
Source: "app.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\AnimePlayer"; Filename: "{app}\AnimePlayer.exe"
Name: "{commondesktop}\AnimePlayer"; Filename: "{app}\AnimePlayer.exe"

[Run]
Filename: "{app}\AnimePlayer.exe"; Description: "AnimePlayer ���N��"; Flags: nowait postinstall skipifsilent