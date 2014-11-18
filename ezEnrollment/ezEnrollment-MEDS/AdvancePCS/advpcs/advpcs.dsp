# Microsoft Developer Studio Project File - Name="advpcs" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Application" 0x0101

CFG=advpcs - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "advpcs.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "advpcs.mak" CFG="advpcs - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "advpcs - Win32 Release" (based on "Win32 (x86) Application")
!MESSAGE "advpcs - Win32 Debug" (based on "Win32 (x86) Application")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "advpcs - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "bin"
# PROP Intermediate_Dir "obj/Release"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /YX /FD /c
# ADD CPP /nologo /MD /W3 /GX /O2 /I "include" /I "../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /I "../../tools/expat" /I "C:\wxWindows-2.3.3\contrib\include" /I "../../tools/ZipArchive" /D "NDEBUG" /D "WIN32" /D "_WINDOWS" /D WINVER=0x400 /D "_MT" /D wxUSE_GUI=1 /D "_MBCS" /D "_LIB" /D "_AFXDLL" /YX /FD /c
# ADD BASE MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x419 /d "NDEBUG"
# ADD RSC /l 0x419 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:windows /machine:I386
# ADD LINK32 C:\wxWindows-2.3.3\lib\wxmsw.lib C:\wxWindows-2.3.3\lib\fl.lib C:\wxWindows-2.3.3\lib\regex.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib wininet.lib ws2_32.lib rpcrt4.lib ZipArchive_STL.lib /nologo /subsystem:windows /machine:I386 /libpath:"../../tools/ZipArchive/Release"

!ELSEIF  "$(CFG)" == "advpcs - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "bin"
# PROP Intermediate_Dir "obj/Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /YX /FD /GZ /c
# ADD CPP /nologo /MDd /W3 /Gm /GX /ZI /Od /I "include" /I "../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /I "../../tools/expat" /I "C:\wxWindows-2.3.3\contrib\include" /I "../../tools/ZipArchive" /I "C:\wxWindows-2.3.3\lib\mswd" /D "_DEBUG" /D "WIN32" /D "_WINDOWS" /D WINVER=0x400 /D "_MT" /D wxUSE_GUI=1 /D "_MBCS" /D "_LIB" /D "_AFXDLL" /D "__WXDEBUG__" /D WXDEBUG=1 /YX /FD /GZ /c
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x419 /d "_DEBUG"
# ADD RSC /l 0x419 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:windows /debug /machine:I386 /pdbtype:sept
# ADD LINK32 C:\wxWindows-2.3.3\lib\wxmswd.lib C:\wxWindows-2.3.3\lib\fld.lib C:\wxWindows-2.3.3\lib\regexd.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib wininet.lib ws2_32.lib rpcrt4.lib ZipArchive_STL.lib /nologo /subsystem:windows /debug /machine:I386 /out:"bin/advpcs_d.exe" /pdbtype:sept /libpath:"../../tools/ZipArchive/Debug"

!ENDIF 

# Begin Target

# Name "advpcs - Win32 Release"
# Name "advpcs - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Group "cmd"

# PROP Default_Filter ".cpp"
# Begin Source File

SOURCE=.\src\cmd\AddRow.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\ChangeValue.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\ClearCells.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\DeleteRow.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\DeleteRows.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\InsertRow.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\Paste.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\SelectRange.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\SortTable.cpp
# End Source File
# Begin Source File

SOURCE=.\src\cmd\SwapRows.cpp
# End Source File
# End Group
# Begin Source File

SOURCE=.\src\Agent.cpp
# End Source File
# Begin Source File

SOURCE=.\src\AgentException.cpp
# End Source File
# Begin Source File

SOURCE=.\src\AgentExecutor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\App.cpp
# End Source File
# Begin Source File

SOURCE=.\SRC\CellRenderer.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Choice.cpp
# End Source File
# Begin Source File

SOURCE=.\SRC\ComposerException.cpp
# End Source File
# Begin Source File

SOURCE=.\src\CrossFiller.cpp
# End Source File
# Begin Source File

SOURCE=.\src\CsvComposer.cpp
# End Source File
# Begin Source File

SOURCE=.\src\DateEditor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\DocManager.cpp
# End Source File
# Begin Source File

SOURCE=.\src\DocTable.cpp
# End Source File
# Begin Source File

SOURCE=.\src\EdiComposer.cpp
# End Source File
# Begin Source File

SOURCE=.\src\EdiDocument.cpp
# End Source File
# Begin Source File

SOURCE=.\src\EdiField.cpp
# End Source File
# Begin Source File

SOURCE=.\src\EdiResponse.cpp
# End Source File
# Begin Source File

SOURCE=.\src\FieldEditor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Grid.cpp
# End Source File
# Begin Source File

SOURCE=.\src\GroupLogger.cpp
# End Source File
# Begin Source File

SOURCE=.\SRC\HeaderFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HeaderPanel.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HttpAgent.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ListLogger.cpp
# End Source File
# Begin Source File

SOURCE=.\src\LoginFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\MainFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\NumericEditor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\PassUpdateFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Resources.cpp
# End Source File
# Begin Source File

SOURCE=.\src\SftpAgent.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StatusBar.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StatusList.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Wizard.cpp
# End Source File
# Begin Source File

SOURCE=.\src\XmlDescriptor.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Group "cmd No. 1"

# PROP Default_Filter ""
# Begin Source File

SOURCE=.\include\advpcs\cmd\AddRow.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\ChangeValue.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\ClearCells.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\DeleteRow.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\DeleteRows.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\InsertRow.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\Paste.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\SelectRange.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\SortTable.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\cmd\SwapRows.h
# End Source File
# End Group
# Begin Source File

SOURCE=.\include\advpcs\Agent.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\AgentException.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\AgentExecutor.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\App.h
# End Source File
# Begin Source File

SOURCE=.\INCLUDE\ADVPCS\CellRenderer.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Choice.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Composer.h
# End Source File
# Begin Source File

SOURCE=.\INCLUDE\ADVPCS\ComposerException.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\CrossFiller.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\CsvComposer.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\DateEditor.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Descriptor.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Disabler.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\DocManager.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\DocTable.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Document.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\EdiComposer.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\EdiDocument.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\EdiField.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\EdiResponse.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\EdiStatus.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\EdiStatusStream.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\FieldEditor.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Grid.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\GroupLogger.h
# End Source File
# Begin Source File

SOURCE=.\INCLUDE\ADVPCS\HeaderFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\HeaderPanel.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\HttpAgent.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\ListLogger.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\LoginFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\MainFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\NetAutoHdr.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\NumericEditor.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\PassUpdateFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\ProcessIndicator.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Resources.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\SftpAgent.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\StatusBar.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\StatusList.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\Wizard.h
# End Source File
# Begin Source File

SOURCE=.\include\advpcs\XmlDescriptor.h
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# Begin Source File

SOURCE=.\rc\wx\msw\blank.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\bullseye.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\cdrom.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\computer.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\drive.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\file1.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\floppy.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\folder1.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\folder2.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\hand.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\magnif1.cur
# End Source File
# Begin Source File

SOURCE=.\rc\bitmaps\mondrian.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\noentry.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pbrush.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pencil.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pntleft.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pntright.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\query.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\removble.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\rightarr.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\roller.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\size.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\watch1.cur
# End Source File
# Begin Source File

SOURCE=.\rc\wizard.rc
# ADD BASE RSC /l 0x419 /i "rc"
# ADD RSC /l 0x419 /i "rc" /i "C:\wxWindows-2.3.3\include"
# End Source File
# End Group
# End Target
# End Project
