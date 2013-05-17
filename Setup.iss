; -- Setup.iss --
; Replicator 3D setup program

[Setup]
AppName=Replicator 3D
AppVersion=0.1
; By installing in {userpf} no extra privileges are needed
DefaultDirName={userpf}\Replicator 3D
DefaultGroupName=Replicator 3D
UninstallDisplayIcon={app}\_3DScanner.exe
Compression=lzma2
SolidCompression=yes
OutputDir=setup
; No extra privilegs are needed by the installer
PrivilegesRequired=lowest

[Files]
; Executable files
Source: "3d scanner client\bin\Release\*.exe"; DestDir: "{app}"
; DLL files
Source: "3d scanner client\bin\Release\*.dll"; DestDir: "{app}"

[Icons]
Name: "{group}\Replicator 3D"; Filename: "{app}\_3DScanner.exe"
