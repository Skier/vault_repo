# Microsoft Developer Studio Project File - Name="RxClaim" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Application" 0x0101

CFG=RxClaim - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "RxClaim.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "RxClaim.mak" CFG="RxClaim - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "RxClaim - Win32 Release" (based on "Win32 (x86) Application")
!MESSAGE "RxClaim - Win32 Debug" (based on "Win32 (x86) Application")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "RxClaim - Win32 Release"

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
# ADD CPP /nologo /MD /W3 /GX /O2 /I "C:\wxWindows-2.3.3\lib\msw" /I "include" /I "../../../tools/expat" /I "../../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /D "NDEBUG" /D "WIN32" /D "_WINDOWS" /D "_MBCS" /D WINVER=0x400 /D wxUSE_GUI=1 /D "_AFXDLL" /YX /FD /c
# ADD BASE MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x419 /d "NDEBUG"
# ADD RSC /l 0x419 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:windows /machine:I386
# ADD LINK32 C:\wxWindows-2.3.3\lib\wxmsw.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib wininet.lib ws2_32.lib rpcrt4.lib /nologo /subsystem:windows /machine:I386

!ELSEIF  "$(CFG)" == "RxClaim - Win32 Debug"

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
# ADD CPP /nologo /MDd /W3 /Gm /GX /ZI /Od /I "C:\wxWindows-2.3.3\lib\mswd" /I "include" /I "../../../tools/expat" /I "../../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /D "_DEBUG" /D "WIN32" /D "_WINDOWS" /D "_MBCS" /D WINVER=0x400 /D wxUSE_GUI=1 /D "_AFXDLL" /YX /FD /GZ /c
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x419 /d "_DEBUG"
# ADD RSC /l 0x419 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:windows /debug /machine:I386 /pdbtype:sept
# ADD LINK32 C:\wxWindows-2.3.3\lib\wxmswd.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib wininet.lib ws2_32.lib rpcrt4.lib /nologo /subsystem:windows /debug /machine:I386 /out:"bin/RxClaim_d.exe" /pdbtype:sept

!ENDIF 

# Begin Target

# Name "RxClaim - Win32 Release"
# Name "RxClaim - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=.\src\ClipboardMatrix.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Composer.cpp
# End Source File
# Begin Source File

SOURCE=.\src\DateEditor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Detail.cpp
# End Source File
# Begin Source File

SOURCE=.\src\FileMatrix.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Header.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HeaderFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HeaderPanel.cpp
# End Source File
# Begin Source File

SOURCE=.\src\InternalComposer.cpp
# End Source File
# Begin Source File

SOURCE=.\src\LoginFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Main.cpp
# End Source File
# Begin Source File

SOURCE=.\src\MainFrame.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Matrix.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Messenger.cpp
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
# Begin Source File

SOURCE=.\include\rxclaim\ClipboardMatrix.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Composer.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\DateEditor.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Descriptor.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Detail.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Header.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\HeaderFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\HeaderPanel.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\MainFrame.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Matrix.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Messages.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Messenger.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\NetAutoHdr.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\Wizard.h
# End Source File
# Begin Source File

SOURCE=.\include\rxclaim\XmlDescriptor.h
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

SOURCE=.\rc\bitmaps\MONDRIAN.ICO
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
# ADD RSC /l 0x409 /i "rc" /i "C:\wxWindows-2.3.3\include"
# End Source File
# End Group
# End Target
# End Project
