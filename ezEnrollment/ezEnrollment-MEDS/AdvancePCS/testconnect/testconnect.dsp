# Microsoft Developer Studio Project File - Name="testconnect" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Console Application" 0x0103

CFG=testconnect - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "testconnect.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "testconnect.mak" CFG="testconnect - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "testconnect - Win32 Release" (based on "Win32 (x86) Console Application")
!MESSAGE "testconnect - Win32 Debug" (based on "Win32 (x86) Console Application")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
RSC=rc.exe

!IF  "$(CFG)" == "testconnect - Win32 Release"

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
# ADD BASE CPP /nologo /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /D "_MBCS" /YX /FD /c
# ADD CPP /nologo /MD /W3 /GX /O2 /I "include" /I "../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /I "../../tools/expat" /I "C:\wxWindows-2.3.3\contrib\include" /I "../advpcs/include" /I "../../tools/ZipArchive" /D "NDEBUG" /D "WIN32" /D "_CONSOLE" /D "_MBCS" /D wxUSE_GUI=0 /D "_AFXDLL" /YX /FD /c
# ADD BASE RSC /l 0x419 /d "NDEBUG"
# ADD RSC /l 0x419 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /machine:I386
# ADD LINK32 wxbase.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib wininet.lib ws2_32.lib /nologo /subsystem:console /machine:I386 /libpath:"C:\wxWindows-2.3.3\lib"

!ELSEIF  "$(CFG)" == "testconnect - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "bin"
# PROP Intermediate_Dir "obj/debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_CONSOLE" /D "_MBCS" /YX /FD /GZ /c
# ADD CPP /nologo /MDd /W3 /Gm /GX /ZI /Od /I "include" /I "../../tools/atflib/include" /I "C:\wxWindows-2.3.3\include" /I "../../tools/expat" /I "C:\wxWindows-2.3.3\contrib\include" /I "../advpcs/include" /I "../../tools/ZipArchive" /D "_DEBUG" /D "WIN32" /D "_CONSOLE" /D "_MBCS" /D wxUSE_GUI=0 /D "_AFXDLL" /D "__WXDEBUG__" /D WXDEBUG=1 /YX /FD /GZ /c
# ADD BASE RSC /l 0x419 /d "_DEBUG"
# ADD RSC /l 0x419 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /debug /machine:I386 /pdbtype:sept
# ADD LINK32 wxbased.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib wininet.lib ws2_32.lib /nologo /subsystem:console /debug /machine:I386 /out:"bin/testconnect_d.exe" /pdbtype:sept /libpath:"C:\wxWindows-2.3.3\lib"

!ENDIF 

# Begin Target

# Name "testconnect - Win32 Release"
# Name "testconnect - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=..\advpcs\src\Agent.cpp
# End Source File
# Begin Source File

SOURCE=..\advpcs\src\AgentException.cpp
# End Source File
# Begin Source File

SOURCE=..\advpcs\src\EdiResponse.cpp
# End Source File
# Begin Source File

SOURCE=..\advpcs\src\HttpAgent.cpp
# End Source File
# Begin Source File

SOURCE=.\src\main.cpp
# End Source File
# Begin Source File

SOURCE=..\advpcs\src\Resources.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=..\advpcs\include\advpcs\Agent.h
# End Source File
# Begin Source File

SOURCE=..\advpcs\include\advpcs\AgentException.h
# End Source File
# Begin Source File

SOURCE=..\advpcs\include\advpcs\EdiResponse.h
# End Source File
# Begin Source File

SOURCE=..\advpcs\include\advpcs\EdiStatus.h
# End Source File
# Begin Source File

SOURCE=..\advpcs\include\advpcs\EdiStatusStream.h
# End Source File
# Begin Source File

SOURCE=..\advpcs\include\advpcs\HttpAgent.h
# End Source File
# Begin Source File

SOURCE=..\advpcs\include\advpcs\NetAutoHdr.h
# End Source File
# Begin Source File

SOURCE=..\advpcs\include\advpcs\Resources.h
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# End Group
# End Target
# End Project
