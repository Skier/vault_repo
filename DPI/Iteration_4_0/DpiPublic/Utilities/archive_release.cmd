@ECHO OFF

rem Set directory variables ***************************************************

SET ARCHIVER_DIR=%CD%\bin\
CD ..
SET RELEASE_DIR=%CD%\Release\
SET PUBLIC_ARC_NAME=public.zip
SET SECURE_ARC_NAME=secure.zip

IF EXIST "%RELEASE_DIR%" (
	
	rem Delete existing archives **********************************************
	
	IF EXIST "%RELEASE_DIR%\%PUBLIC_ARC_NAME%" DEL "%RELEASE_DIR%\%PUBLIC_ARC_NAME%"
	IF EXIST "%RELEASE_DIR%\%SECURE_ARC_NAME%" DEL "%RELEASE_DIR%\%SECURE_ARC_NAME%"
	
	rem Zip Public part *******************************************************
	
	"%ARCHIVER_DIR%\7za" a -tzip -r "%RELEASE_DIR%\%PUBLIC_ARC_NAME%" "%RELEASE_DIR%\Dpi.Central.Web\*" 
	
	rem Zip Account part ******************************************************

 	"%ARCHIVER_DIR%\7za" a -tzip -r "%RELEASE_DIR%\%SECURE_ARC_NAME%" "%RELEASE_DIR%\Dpi.Central.Web.Account\*"	

) ELSE (
	ECHO ERROR: Release directory not found.
	Exit 1
)