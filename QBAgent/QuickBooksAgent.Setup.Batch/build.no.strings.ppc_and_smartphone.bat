@echo off

rem Main body

if not exist temp md temp
del /Q temp\*.*

call :read_settings common/CommonSettings.ini || exit /b 1

if not exist "%NSISINSTALLDIR%makensis.exe" call:build_failed "Could not find NSIS. Please install NSIS (http://nsis.sourceforge.net/Download) and set path in CommonSettings.ini file"
if not exist "%VSINSTALLDIR%" call:build_failed "Please specify path to the Visual Studio 2005 in CommonSettings.ini file"

C:\work\Q-Agent\Bin\WinCE\install\tools\signtool.exe sign /f C:\work\Q-Agent\Bin\WinCE\install\tools\Affilia.pfx C:\work\Q-Agent\Bin\WinCE\install\tools\tktmgr.exe
call :process_installation armv4.wce40.ppc.settings.ini
call :process_installation armv4i.wm50.ppc.settings.ini
call :process_installation armv4i.wm50.phone.settings.ini


echo.
echo BUILD SUCCEEDED!

exit /b 0

rem Process installation///////////////////////////////////////////////////////
rem Input parameter - instalation settings file name
:process_installation
call :read_settings %1 || exit /b 1
call :create_netcf %NETCABFILENAME%
call :create_sqlce %SQLCABFILENAME%
call :create_qa
FOR /F "tokens=*" %%i in ('C:\work\Q-Agent\QAgent.AssemblyVersion\bin\Debug\QAgent.AssemblyVersion.exe') do SET ASSEMBLYVERSION=%%i
FOR /F "tokens=*" %%i in ('C:\work\Q-Agent\QAgent.AssemblyVersion\bin\Debug\QAgent.AssemblyVersion.exe full') do SET ASSEMBLYVERSIONFULL=%%i
set OUTPUTFILENAME=%OUTPUTFILENAME%_v%ASSEMBLYVERSION%.exe
call :create_install %OUTPUTFILENAME% "%VSINSTALLDIR%%NETCABFOLDERNAME%%NETCABFILENAME%" "%VSINSTALLDIR%%SQLCABFOLDERNAME%%SQLCABFILENAME%" "%VSINSTALLDIR%%NETSTRINGSCABFOLDERNAME%%NETSTRINGSCABFILENAME%"

copy common\conntkt.ini temp
cd temp
"%NSISINSTALLDIR%makensis.exe" install.nsi
if not %ERRORLEVEL% == 0 call :build_failed "Error processing %OUTPUTFILENAME%"

C:\work\Q-Agent\Bin\WinCE\install\tools\signtool.exe sign /f C:\work\Q-Agent\Bin\WinCE\install\tools\Affilia.pfx %OUTPUTFILENAME%
copy %OUTPUTFILENAME% ..\..\Bin\WinCE\install
del /Q *.*
cd ..
exit /b 0

rem Settings reading function//////////////////////////////////////////////////
rem Input - settings file name
:read_settings

set SETTINGSFILE=%1

if not exist %SETTINGSFILE% (
    echo FAIL: No settings file
    exit /b 1
)

for /f "eol=# delims== tokens=1,2" %%i in (%SETTINGSFILE%) do (
    set %%i=%%j
)

exit /b 0

rem Creating netcf.ini /////////////////////////////////////////////////////////
rem Input parameter - cab file name
:create_netcf
if exist temp\netcf.ini del temp\netcf.ini
>>temp\netcf.ini ECHO [CEAppManager]
>>temp\netcf.ini ECHO Version = 1.0
>>temp\netcf.ini ECHO Component = netcf
>>temp\netcf.ini ECHO.
>>temp\netcf.ini ECHO [netcf]
>>temp\netcf.ini ECHO Description = Microsoft .NET Compact Framework
>>temp\netcf.ini ECHO Uninstall = Microsoft .NET CF
>>temp\netcf.ini ECHO CabFiles = %1
exit /b 0

rem Creating netcfstr.ini //////////////////////////////////////////////////////
rem Input parameter - cab file name
:create_netcfstr
if exist temp\netcfstr.ini del temp\netcfstr.ini
>>temp\netcfstr.ini ECHO [CEAppManager]
>>temp\netcfstr.ini ECHO Version = 1.0
>>temp\netcfstr.ini ECHO Component = netcfstr
>>temp\netcfstr.ini ECHO.
>>temp\netcfstr.ini ECHO [netcfstr]
>>temp\netcfstr.ini ECHO Description = Microsoft .NET CF Strings
>>temp\netcfstr.ini ECHO Uninstall = Microsoft .NET CF Strings
>>temp\netcfstr.ini ECHO CabFiles = %1
exit /b 0

rem Creating sqlce.ini /////////////////////////////////////////////////////////
rem Input parameter - cab file name
:create_sqlce
if exist temp\sqlce.ini del temp\sqlce.ini
>>temp\sqlce.ini ECHO [CEAppManager]
>>temp\sqlce.ini ECHO Version = 1.0
>>temp\sqlce.ini ECHO Component = SqlCE
>>temp\sqlce.ini ECHO.
>>temp\sqlce.ini ECHO [SqlCE]
>>temp\sqlce.ini ECHO Description = Microsoft SQL Server Compact Edition
>>temp\sqlce.ini ECHO Uninstall = Microsoft SQL Server CE
>>temp\sqlce.ini ECHO CabFiles = %1
exit /b 0

rem Creating qa.ini ////////////////////////////////////////////////////////////
:create_qa
if exist temp\qa.ini del temp\qa.ini
>>temp\qa.ini ECHO [CEAppManager]
>>temp\qa.ini ECHO Version = 1.0
>>temp\qa.ini ECHO Component = Q-Agent
>>temp\qa.ini ECHO.
>>temp\qa.ini ECHO [Q-Agent]
>>temp\qa.ini ECHO Description = Affilia Q-Agent
>>temp\qa.ini ECHO Uninstall = Affilia Q-Agent
>>temp\qa.ini ECHO CabFiles = Q-Agent.Setup.CAB
exit /b 0

rem Creating install.nsi ///////////////////////////////////////////////////////
rem Params
rem 1 - Output File Name
rem 2 - Fully qualified .NET framework cab file name in quotes
rem 3 - Fully qualified SQL Server CE cab file name in quotes
rem 4 - Fully qualified .NET strings cab file name in quotes

:create_install
if exist temp\install.nsi del temp\install.nsi
>>temp\install.nsi ECHO ; NOTE: this .NSI script is designed for NSIS v1.8+
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO Name "Q-Agent"
>>temp\install.nsi ECHO Icon "..\common\Q-Agent.install.ico"
>>temp\install.nsi ECHO UninstallIcon "..\common\Q-Agent.install.ico"
>>temp\install.nsi ECHO OutFile "%1"
>>temp\install.nsi ECHO InstallDir "$PROGRAMFILES\Affilia\Q-Agent"
>>temp\install.nsi ECHO InstallDirRegKey HKEY_LOCAL_MACHINE "SOFTWARE\Affilia\Q-Agent" "Install_Dir"
>>temp\install.nsi ECHO BrandingText " "
>>temp\install.nsi ECHO CRCCheck on
>>temp\install.nsi ECHO LicenseText "Q-Agent" "Next"
>>temp\install.nsi ECHO LicenseData "..\common\eula_Q-Agent.txt"
>>temp\install.nsi ECHO VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "Q-Agent"
>>temp\install.nsi ECHO VIAddVersionKey /LANG=${LANG_ENGLISH} "Comments" "Q-Agent for Pocket PC"
>>temp\install.nsi ECHO VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "Q-Agent for Pocket PC Installer"
>>temp\install.nsi ECHO VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "Affilia, Inc"
>>temp\install.nsi ECHO VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" %ASSEMBLYVERSIONFULL%
>>temp\install.nsi ECHO VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "©2007 Affilia, Inc"
>>temp\install.nsi ECHO VIProductVersion %ASSEMBLYVERSIONFULL%
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO ;Main section
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO Section "Main"
>>temp\install.nsi ECHO SetOutPath "$INSTDIR"
>>temp\install.nsi ECHO SetOverwrite on
>>temp\install.nsi ECHO File "..\common\eula_Q-Agent.txt"
>>temp\install.nsi ECHO File %2
>>temp\install.nsi ECHO File %3
>>temp\install.nsi ECHO File %4
>>temp\install.nsi ECHO File "..\..\Bin\WinCE\install\Q-Agent.Setup.CAB"
>>temp\install.nsi ECHO File "..\..\Bin\WinCE\install\tools\tktmgr.exe"
>>temp\install.nsi ECHO File "..\..\Doc\pdf\Installation manual.pdf"
>>temp\install.nsi ECHO File "..\..\Doc\pdf\Users Guide.pdf"
>>temp\install.nsi ECHO File "netcf.ini"
>>temp\install.nsi ECHO File "sqlce.ini"
>>temp\install.nsi ECHO File "qa.ini"
>>temp\install.nsi ECHO ; one-time initialization needed for InstallCAB subroutine
>>temp\install.nsi ECHO ReadRegStr $1 HKEY_LOCAL_MACHINE "software\Microsoft\Windows\CurrentVersion\App Paths\CEAppMgr.exe" ""
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO IfErrors 0 +2
>>temp\install.nsi ECHO Call ShowError
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO ; install pocket pc cabs
>>temp\install.nsi ECHO StrCpy $0 "$INSTDIR\netcf.ini"
>>temp\install.nsi ECHO Call InstallCAB
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO StrCpy $0 "$INSTDIR\sqlce.ini"
>>temp\install.nsi ECHO Call InstallCAB
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO StrCpy $0 "$INSTDIR\qa.ini"
>>temp\install.nsi ECHO Call InstallCAB
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO ExecWait '"tktmgr.exe" CheckSQL' 
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO SectionEnd ; end of default section
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO Section "Local installer" 
>>temp\install.nsi ECHO SectionIn RO 
>>temp\install.nsi ECHO WriteRegStr HKLM SOFTWARE\Affilia\Q-Agent "Install_Dir" "$INSTDIR"
>>temp\install.nsi ECHO WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent" "DisplayName" "Q-Agent"
>>temp\install.nsi ECHO WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent" "UninstallString" '"$INSTDIR\uninstall.exe"'
>>temp\install.nsi ECHO WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent" "Publisher" "Affilia, Inc"
>>temp\install.nsi ECHO WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent" "URLInfoAbout" "http://www.affilia.com/"
>>temp\install.nsi ECHO WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent" "HelpLink" "http://www.affilia.com/q-agent.html"
>>temp\install.nsi ECHO WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent" "NoModify" 1
>>temp\install.nsi ECHO WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent" "NoRepair" 1
>>temp\install.nsi ECHO WriteUninstaller "uninstall.exe"
>>temp\install.nsi ECHO SectionEnd ; end of Local installer section 
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO Section "Start Menu Shortcuts"
>>temp\install.nsi ECHO CreateDirectory "$SMPROGRAMS\Q-Agent"
>>temp\install.nsi ECHO CreateShortCut "$SMPROGRAMS\Q-Agent\Ticket Manager.lnk" "$INSTDIR\tktmgr.exe" "" "$INSTDIR\tktmgr.exe" 0
>>temp\install.nsi ECHO CreateShortCut "$SMPROGRAMS\Q-Agent\Installation Manual.lnk" "$INSTDIR\Installation manual.pdf" "" "$INSTDIR\Installation manual.pdf" 0
>>temp\install.nsi ECHO CreateShortCut "$SMPROGRAMS\Q-Agent\Users Guide.lnk" "$INSTDIR\Users Guide.pdf" "" "$INSTDIR\Users Guide.pdf" 0
>>temp\install.nsi ECHO CreateShortCut "$SMPROGRAMS\Q-Agent\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
>>temp\install.nsi ECHO SectionEnd ; end of Start Menu Shortcuts section
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO Section "Uninstall"
>>temp\install.nsi ECHO DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Q-Agent"
>>temp\install.nsi ECHO DeleteRegKey HKLM SOFTWARE\Affilia\Q-Agent
>>temp\install.nsi ECHO DeleteRegKey /ifempty HKLM SOFTWARE\Affilia
>>temp\install.nsi ECHO Delete $INSTDIR\eula_Q-Agent.txt
>>temp\install.nsi ECHO Delete $INSTDIR\netcf.ini
>>temp\install.nsi ECHO Delete $INSTDIR\%NETCABFILENAME%
>>temp\install.nsi ECHO Delete $INSTDIR\Q-Agent.Setup.CAB
>>temp\install.nsi ECHO Delete $INSTDIR\qa.ini
>>temp\install.nsi ECHO Delete $INSTDIR\tktmgr.exe
>>temp\install.nsi ECHO Delete $INSTDIR\sqlce.ini
>>temp\install.nsi ECHO Delete $INSTDIR\%SQLCABFILENAME%
>>temp\install.nsi ECHO Delete $INSTDIR\%NETSTRINGSCABFILENAME%
>>temp\install.nsi ECHO Delete "$INSTDIR\Installation manual.pdf"
>>temp\install.nsi ECHO Delete "$INSTDIR\Users Guide.pdf"
>>temp\install.nsi ECHO Delete $INSTDIR\uninstall.exe
>>temp\install.nsi ECHO RMDir "$INSTDIR"
>>temp\install.nsi ECHO Delete "$SMPROGRAMS\Q-Agent\*.*"
>>temp\install.nsi ECHO RMDir "$SMPROGRAMS\Q-Agent"
>>temp\install.nsi ECHO SectionEnd ; end of uninstall section
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO ShowInstDetails nevershow ;never show installation details
>>temp\install.nsi ECHO. 
>>temp\install.nsi ECHO Page license
>>temp\install.nsi ECHO Page directory
>>temp\install.nsi ECHO Page instfiles
>>temp\install.nsi ECHO.
>>temp\install.nsi ECHO UninstPage uninstConfirm
>>temp\install.nsi ECHO UninstPage instfiles
>>temp\install.nsi ECHO.  
>>temp\install.nsi ECHO.  
>>temp\install.nsi ECHO ; Installs a PocketPC cab-application
>>temp\install.nsi ECHO Function InstallCAB
>>temp\install.nsi ECHO ExecWait '"$1" "$0"'
>>temp\install.nsi ECHO FunctionEnd
>>temp\install.nsi ECHO.  
>>temp\install.nsi ECHO.  
>>temp\install.nsi ECHO ; Shows error that ActiveSync wasn't found and exits installation
>>temp\install.nsi ECHO Function ShowError
>>temp\install.nsi ECHO MessageBox MB_OK^|MB_ICONEXCLAMATION \
>>temp\install.nsi ECHO "Unable to find Application Manager for Pocket PC applications. \
>>temp\install.nsi ECHO Please install ActiveSync and reinstall Q-Agent."
>>temp\install.nsi ECHO Quit
>>temp\install.nsi ECHO FunctionEnd

exit /b 0

rem Build failed ////////////////////////////////////////////////////////////
rem Param string message
:build_failed
echo.
echo %~1
echo.
echo BUILD FAILED!
exit
exit /b 0
