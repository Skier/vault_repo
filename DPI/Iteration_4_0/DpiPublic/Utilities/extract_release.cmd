@ECHO OFF

rem OVERWRITE_WEB_CONFIG values are: yes/no
SET OVERWRITE_WEB_CONFIG=no

rem Set directory variables ***************************************************

SET ARCHIVER_DIR=%CD%\bin\
SET RELEASE_DIR=%CD%
SET PUBLIC_SITE_DIR=D:\My Documents\Projects\Visual Studio 2003\DPI\Iteration_4_0\DpiPublic\remove_server\pbl\
SET SECURE_SITE_DIR=D:\My Documents\Projects\Visual Studio 2003\DPI\Iteration_4_0\DpiPublic\remove_server\sec\
SET PUBLIC_ARC_NAME=public.zip
SET SECURE_ARC_NAME=secure.zip

IF EXIST "%RELEASE_DIR%" (
	
	rem Unzip Public part *****************************************************
	
	IF "%OVERWRITE_WEB_CONFIG%"=="no" ( 
		IF EXIST "%PUBLIC_SITE_DIR%\Web.config" RENAME "%PUBLIC_SITE_DIR%\Web.config" Web.config_bk 
	)	
	
	"%ARCHIVER_DIR%\7za" x -o"%PUBLIC_SITE_DIR%" -r -aoa "%RELEASE_DIR%\%PUBLIC_ARC_NAME%" *
	
	IF "%OVERWRITE_WEB_CONFIG%"=="no" ( 
		IF EXIST "%PUBLIC_SITE_DIR%\Web.config_bk" (
			DEL "%PUBLIC_SITE_DIR%\Web.config"
			RENAME "%PUBLIC_SITE_DIR%\Web.config_bk" Web.config 
		)
	)
	
	rem Unzip Account part ****************************************************
	
	IF "%OVERWRITE_WEB_CONFIG%"=="no" ( 
		IF EXIST "%SECURE_SITE_DIR%\Web.config" RENAME "%SECURE_SITE_DIR%\Web.config" Web.config_bk 
	)	
	
	"%ARCHIVER_DIR%\7za" x -o"%SECURE_SITE_DIR%" -r -aoa "%RELEASE_DIR%\%SECURE_ARC_NAME%" *
	
	IF "%OVERWRITE_WEB_CONFIG%"=="no" ( 
		IF EXIST "%SECURE_SITE_DIR%\Web.config_bk" (
			DEL "%SECURE_SITE_DIR%\Web.config"
			RENAME "%SECURE_SITE_DIR%\Web.config_bk" Web.config 
		)
	)
) ELSE (
	ECHO ERROR: Release directory not found.
	Exit 1
)