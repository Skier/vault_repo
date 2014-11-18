# Microsoft Developer Studio Project File - Name="Recap2" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Application" 0x0101

CFG=Recap2 - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "Recap2.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "Recap2.mak" CFG="Recap2 - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "Recap2 - Win32 Release" (based on "Win32 (x86) Application")
!MESSAGE "Recap2 - Win32 Debug" (based on "Win32 (x86) Application")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "Recap2 - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "obj/Release"
# PROP Intermediate_Dir "obj/Release"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W4 /O2 /D "NDEBUG" /D "WIN32" /D "_WINDOWS" /D WINVER=0x400 /YX /FD /c
# ADD CPP /nologo /MD /GX /O1 /I "include" /I "../../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /I "../../../tools/expat" /D "NDEBUG" /D "WIN32" /D "_WINDOWS" /D WINVER=0x400 /D "_MT" /D wxUSE_GUI=1 /D "_MBCS" /D "_LIB" /D "_AFXDLL" /FD /IC:\wxWindows-2.3.3\lib\msw /c
# ADD BASE MTL /nologo /D "NDEBUG" /mktyplib203 /o "NUL" /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /o "NUL" /win32
# ADD BASE RSC /l 0x409 /i "C:\wxWindows-2.3.3\include" /d "NDEBUG"
# ADD RSC /l 0x409 /i "C:\wxWindows-2.3.3\include" /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib comctl32.lib rpcrt4.lib wsock32.lib /nologo /subsystem:windows /machine:I386
# ADD LINK32 C:\wxWindows-2.3.3\lib\zlib.lib C:\wxWindows-2.3.3\lib\regex.lib C:\wxWindows-2.3.3\lib\png.lib C:\wxWindows-2.3.3\lib\jpeg.lib C:\wxWindows-2.3.3\lib\tiff.lib C:\wxWindows-2.3.3\lib\wxmsw.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib comctl32.lib rpcrt4.lib wsock32.lib wininet.lib /nologo /subsystem:windows /machine:I386 /out:"bin/Recap2.exe"

!ELSEIF  "$(CFG)" == "Recap2 - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "obj/Debug"
# PROP Intermediate_Dir "obj/Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W4 /Zi /Od /D "_DEBUG" /D "WIN32" /D "_WINDOWS" /D WINVER=0x400 /YX /FD /c
# ADD CPP /nologo /MDd /GX /Zi /Od /I "include" /I "../../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /I "../../../tools/expat" /D "_DEBUG" /D "WIN32" /D "_WINDOWS" /D WINVER=0x400 /D "_MT" /D wxUSE_GUI=1 /D "__WXDEBUG__" /D WXDEBUG=1 /D "_MBCS" /D "_LIB" /D "_AFXDLL" /YX /FD /IC:\wxWindows-2.3.3\lib\mswd /c
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /o "NUL" /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /o "NUL" /win32
# ADD BASE RSC /l 0x409 /i "C:\wxWindows-2.3.3\include" /d "_DEBUG"
# ADD RSC /l 0x409 /i "C:\wxWindows-2.3.3\include" /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib comctl32.lib rpcrt4.lib wsock32.lib /nologo /subsystem:windows /debug /machine:I386 /pdbtype:sept
# ADD LINK32 C:\wxWindows-2.3.3\lib\zlibd.lib C:\wxWindows-2.3.3\lib\regexd.lib C:\wxWindows-2.3.3\lib\pngd.lib C:\wxWindows-2.3.3\lib\jpegd.lib C:\wxWindows-2.3.3\lib\tiffd.lib C:\wxWindows-2.3.3\lib\wxmswd.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib comctl32.lib rpcrt4.lib wsock32.lib wininet.lib /nologo /subsystem:windows /debug /machine:I386 /out:"bin/Recap2_d.exe" /pdbtype:sept

!ENDIF 

# Begin Target

# Name "Recap2 - Win32 Release"
# Name "Recap2 - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=.\src\ClipbrdMatrix.cpp
# End Source File
# Begin Source File

SOURCE=.\src\composer.cpp
# End Source File
# Begin Source File

SOURCE=.\Src\ComposerDescriptor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\DateEditor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\detail.cpp
# End Source File
# Begin Source File

SOURCE=.\src\FileMatrix.cpp
# End Source File
# Begin Source File

SOURCE=.\src\header.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HeaderFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HeaderPanel.cpp
# End Source File
# Begin Source File

SOURCE=.\src\LoginFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\main.cpp
# End Source File
# Begin Source File

SOURCE=.\src\MainFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Matrix.cpp
# End Source File
# Begin Source File

SOURCE=.\Src\Messenger.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StdAfx.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StringParser.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Wizard.cpp
# End Source File
# Begin Source File

SOURCE=.\src\xmldescriptor.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=.\include\client\ClipbrdMatrix.h
# End Source File
# Begin Source File

SOURCE=.\include\client\composer.h
# End Source File
# Begin Source File

SOURCE=.\Include\Client\ComposerDescriptor.h
# End Source File
# Begin Source File

SOURCE=.\include\client\DateEditor.h
# End Source File
# Begin Source File

SOURCE=.\include\client\descriptor.h
# End Source File
# Begin Source File

SOURCE=.\include\client\detail.h
# End Source File
# Begin Source File

SOURCE=.\include\client\FileMatrix.h
# End Source File
# Begin Source File

SOURCE=.\include\client\header.h
# End Source File
# Begin Source File

SOURCE=.\include\client\HeaderFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\client\HeaderPanel.h
# End Source File
# Begin Source File

SOURCE=.\include\client\LoginFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\client\MainFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\client\Matrix.h
# End Source File
# Begin Source File

SOURCE=.\include\client\Messages.h
# End Source File
# Begin Source File

SOURCE=.\Include\Client\Messenger.h
# End Source File
# Begin Source File

SOURCE=.\include\client\NetAutoHdr.h
# End Source File
# Begin Source File

SOURCE=.\include\client\StdAfx.h
# End Source File
# Begin Source File

SOURCE=.\include\client\StringParser.h
# End Source File
# Begin Source File

SOURCE=.\include\client\Wizard.h
# End Source File
# Begin Source File

SOURCE=.\include\client\xmldescriptor.h
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# Begin Source File

SOURCE=.\rc\wx\msw\blank.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\blank.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\bullseye.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\bullseye.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\cdrom.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\cdrom.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\computer.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\computer.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\drive.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\drive.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\file1.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\file1.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\floppy.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\floppy.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\folder1.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\folder1.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\folder2.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\folder2.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\hand.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\hand.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\magnif1.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\magnif1.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\bitmaps\mondrian.ico
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\noentry.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\noentry.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pbrush.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\pbrush.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pencil.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\pencil.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pntleft.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\pntleft.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\pntright.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\pntright.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\query.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\query.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\removble.ico
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\removble.ico"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\rightarr.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\rightarr.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\roller.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\roller.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\size.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\size.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wx\msw\watch1.cur
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx\msw\watch1.cur"
# End Source File
# Begin Source File

SOURCE=.\rc\wizard.rc
# End Source File
# Begin Source File

SOURCE="C:\wxWindows-2.3.3\include\wx\msw\wx.rc"
# PROP Exclude_From_Build 1
# End Source File
# End Group
# End Target
# End Project
