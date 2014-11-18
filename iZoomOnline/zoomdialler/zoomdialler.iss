[Setup]
AppId=iZoom Online Dialer
AppName=iZoom Online Dialer 1.1
AppVerName=iZoom Online Dialer 1.1
AppPublisher=iZoom Online LLC
AppPublisherURL=http://izoomonline.com
AppSupportURL=http://izoomonline.com
AppUpdatesURL=http://izoomonline.com
;UsePreviousAppDir=no
DefaultDirName={pf}\iZoom Online
DefaultGroupName=iZoom Online
DisableProgramGroupPage=yes
OutputDir=Release
AlwaysRestart=yes
UninstallRestartComputer=yes
OutputBaseFilename=izoomonline-dialer-1.1
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin

[Files]
Source: "Release\izoomdialler.exe"; DestDir: "{app}"; Flags: ignoreversion;
Source: "resources\msvcp71.dll"; DestDir: "{app}"; Flags: onlyifdoesntexist;
Source: "resources\msvcr71.dll"; DestDir: "{app}"; Flags: onlyifdoesntexist;
Source: "resources\izoomdialler.ico"; DestDir: "{app}"; Flags: onlyifdoesntexist;
Source: "resources\izoomdialler.ini"; DestDir: "{win}"; Flags: onlyifdoesntexist;

[Icons]
Name: "{userdesktop}\iZoom Online Dialer"; Filename: "{app}\izoomdialler.exe"; WorkingDir: "{app}"; IconFilename: "{app}\izoomdialler.ico"
Name: "{commonstartup}\iZoom Online Dialer"; Filename: "{app}\izoomdialler.exe"; WorkingDir: "{app}"; IconFilename: "{app}\izoomdialler.ico"

[Run]
;Filename: "{pf}\iZoom Online\unins000.exe"; Flags: hidewizard skipifdoesntexist;
;Filename: "{app}\izoomdialler.exe"; Flags: postinstall;

[UninstallDelete]
Type: filesandordirs; Name: "{app}"
        
