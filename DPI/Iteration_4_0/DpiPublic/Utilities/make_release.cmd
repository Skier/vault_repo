@ECHO OFF

rem IF "%~1"=="" (
rem	ECHO ERROR: Directory of DpiPublic solution must be passed as the first parameter.
rem	GOTO END
rem )

CD ..

SET RELEASE_DIR=Release
SET SOLUTION_DIR=%CD%

IF DEFINED IIS_ROOT_DIR (	
 	IF EXIST "%SOLUTION_DIR%\%RELEASE_DIR%" RD "%SOLUTION_DIR%\%RELEASE_DIR%" /S /Q
	CD "%SOLUTION_DIR%"	
 	MD "%RELEASE_DIR%"
	
	rem Public part ***********************************************************
 	
 	MD "%RELEASE_DIR%\Dpi.Central.Web"
 	XCOPY "%IIS_ROOT_DIR%\Dpi.Central.Web" "%RELEASE_DIR%\Dpi.Central.Web" /S /Y
 	
	CD "%RELEASE_DIR%\Dpi.Central.Web\bin"
 	DEL "*.pdb"
 	
	CD ..
 	DEL *.cs
 	DEL *.resx
 	DEL *.csproj
 	DEL *.webinfo

 	CD flash
 	IF EXIST _vti_cnf RD _vti_cnf /S /Q
 	IF EXIST _sgbak RD _sgbak /S /Q
	
	CD ../images
 	IF EXIST _vti_cnf RD _vti_cnf /S /Q

 	CD "%SOLUTION_DIR%"
	
	rem Account part **********************************************************

 	MD "%RELEASE_DIR%\Dpi.Central.Web.Account" 
 	XCOPY "%IIS_ROOT_DIR%\Dpi.Central.Web.Account" "%RELEASE_DIR%\Dpi.Central.Web.Account" /S /Y
 	
	CD "%RELEASE_DIR%\Dpi.Central.Web.Account\bin"
 	DEL *.pdb
	
 	CD ..
 	DEL *.cs
 	DEL *.resx
 	DEL *.csproj
 	DEL *.webinfo	
	rem IF EXIST flash RD flash /S /Q

 	CD account
 	DEL *.cs
 	DEL *.resx
 	IF EXIST _vti_cnf RD _vti_cnf /S /Q
 	IF EXIST _sgbak RD _sgbak /S /Q

 	CD payment
 	DEL *.cs
 	DEl *.resx
 	IF EXIST _vti_cnf RD _vti_cnf /S /Q
 	IF EXIST _sgbak RD _sgbak /S /Q

 	CD ../../account_setup
 	DEL *.cs
 	DEL *.resx
 	IF EXIST _vti_cnf RD _vti_cnf /S /Q
 	IF EXIST _sgbak RD _sgbak /S /Q
) ELSE (
	ECHO ERROR: Environment variable IIS_ROOT_DIR must be defined.
	Exit 1
)