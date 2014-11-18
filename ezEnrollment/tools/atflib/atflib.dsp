# Microsoft Developer Studio Project File - Name="atflib" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Static Library" 0x0104

CFG=atflib - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "atflib.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "atflib.mak" CFG="atflib - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "atflib - Win32 Release" (based on "Win32 (x86) Static Library")
!MESSAGE "atflib - Win32 Debug" (based on "Win32 (x86) Static Library")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
RSC=rc.exe

!IF  "$(CFG)" == "atflib - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "obj/Release"
# PROP Intermediate_Dir "obj/Release"
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_MBCS" /D "_LIB" /YX /FD /c
# ADD CPP /nologo /MD /W3 /GX /O2 /I "include" /I "../expat" /D "NDEBUG" /D "WIN32" /D "_MBCS" /D "_LIB" /D "_AFXDLL" /D "EXPAT_STATIC" /YX /FD /c
# ADD BASE RSC /l 0x419 /d "NDEBUG"
# ADD RSC /l 0x419 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LIB32=link.exe -lib
# ADD BASE LIB32 /nologo
# ADD LIB32 /nologo /out:"lib/atflib.lib"

!ELSEIF  "$(CFG)" == "atflib - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "obj/Debug"
# PROP Intermediate_Dir "obj/Debug"
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_MBCS" /D "_LIB" /YX /FD /GZ /c
# ADD CPP /nologo /MDd /W3 /Gm /GX /ZI /Od /I "include" /I "../expat" /D "_DEBUG" /D "WIN32" /D "_MBCS" /D "_LIB" /D "_AFXDLL" /D "EXPAT_STATIC" /FD /GZ /c
# SUBTRACT CPP /YX
# ADD BASE RSC /l 0x419 /d "_DEBUG"
# ADD RSC /l 0x419 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LIB32=link.exe -lib
# ADD BASE LIB32 /nologo
# ADD LIB32 /nologo /out:"lib/atflib_d.lib"

!ENDIF 

# Begin Target

# Name "atflib - Win32 Release"
# Name "atflib - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=.\src\AbstractAppFactory.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Asn1Node.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ByteIterator.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ByteStream.cpp
# End Source File
# Begin Source File

SOURCE=.\src\CfgCmdLine.cpp
# End Source File
# Begin Source File

SOURCE=.\src\CfgXml.cpp
# End Source File
# Begin Source File

SOURCE=.\src\CmdLineApp.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Const.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Environment.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ErrorConst.cpp
# End Source File
# Begin Source File

SOURCE=.\src\EventLogger.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Exception.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ExceptionLogger.cpp
# End Source File
# Begin Source File

SOURCE=.\src\FileExecutor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\FileExecutorFactory.cpp
# End Source File
# Begin Source File

SOURCE=.\src\FileListener.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HTTPExecutor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\HTTPExecutorFactory.cpp
# End Source File
# Begin Source File

SOURCE=.\src\IBaseService.cpp
# End Source File
# Begin Source File

SOURCE=.\src\IDoneExecutor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Logger.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Message.cpp
# End Source File
# Begin Source File

SOURCE=.\src\MqException.cpp
# End Source File
# Begin Source File

SOURCE=.\src\MqExecutor.cpp
# End Source File
# Begin Source File

SOURCE=.\src\MqExecutorFactory.cpp
# End Source File
# Begin Source File

SOURCE=.\src\MqListener.cpp
# End Source File
# Begin Source File

SOURCE=.\src\NoMoreDataException.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ObjectPool.cpp
# End Source File
# Begin Source File

SOURCE=.\src\PoolableThread.cpp
# End Source File
# Begin Source File

SOURCE=.\src\PoolManager.cpp
# End Source File
# Begin Source File

SOURCE=.\src\PoolMap.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ServiceApp.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ServiceInstallApp.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ServiceUninstallApp.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StreamIOHandler.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StreamLogger.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StringIOHandler.cpp
# End Source File
# Begin Source File

SOURCE=.\src\StringTokenizer.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Thread.cpp
# End Source File
# Begin Source File

SOURCE=.\src\ThreadBodyRunnbale.cpp
# End Source File
# Begin Source File

SOURCE=.\src\Util.cpp
# End Source File
# Begin Source File

SOURCE=.\src\XmlDocument.cpp
# End Source File
# Begin Source File

SOURCE=.\src\XmlLoader.cpp
# End Source File
# Begin Source File

SOURCE=.\src\XmlNode.cpp
# End Source File
# Begin Source File

SOURCE=.\src\XmlStringWriter.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=.\include\atf\AbstractAppFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\AbstractPoolableThreadFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Asn1Node.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\AssertException.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ByteIterator.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ByteStream.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Cfg.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\CfgCmdLine.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\CfgException.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\CfgXml.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\CmdLineApp.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Const.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Environment.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ErrorConst.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\EventLogger.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Exception.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ExceptionLogger.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ExecutorFactoryFasade.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ExecutorPool.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\FileExecutor.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\FileExecutorFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\FileListener.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\FileLogger.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\HTTPExecutor.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\HTTPExecutorFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IApp.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IAppFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IBaseService.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IByteIterator.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IDoneExecutor.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IExecutor.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IExecutorFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IListener.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IManageable.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IMessage.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Interrupted.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IObjectFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IPool.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IRunnable.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IService.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\IThreadFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Logger.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Message.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\MqException.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\MqExecutor.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\MqExecutorFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\MqListener.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Mutex.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\NoMoreDataException.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ObjectPool.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\PoolableThread.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\PoolManager.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\PoolMap.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\resource.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ServiceApp.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ServiceInstallApp.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ServiceUninstallApp.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\SocketException.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\StreamIOHandler.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\StreamLogger.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\StringIOHandler.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\StringTokenizer.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\SystemException.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Thread.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ThreadBodyRunnable.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ThreadFactory.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ThreadFactoryFasade.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\ThreadPool.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Types.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\Util.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\XmlDocument.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\XmlIOHandler.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\XmlLoader.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\XmlLoadException.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\XmlNode.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\XmlProperty.h
# End Source File
# Begin Source File

SOURCE=.\include\atf\XmlStringWriter.h
# End Source File
# End Group
# End Target
# End Project
