@ECHO OFF

IF "%~1"=="" (
	ECHO ERROR: Directory of Dpi.Central.Web.Common project must be passed as the first parameter.
	GOTO ERROR
)

IF DEFINED IIS_ROOT_DIR (
	IF NOT EXIST "%IIS_ROOT_DIR%\Dpi.Central.Web\images" MD "%IIS_ROOT_DIR%\Dpi.Central.Web\images"
	XCOPY "%~1\images" "%IIS_ROOT_DIR%\Dpi.Central.Web\images" /S /Y /R /Q /D
	IF NOT EXIST "%IIS_ROOT_DIR%\Dpi.Central.Web\script" MD "%IIS_ROOT_DIR%\Dpi.Central.Web\script"
	XCOPY "%~1\script" "%IIS_ROOT_DIR%\Dpi.Central.Web\script" /S /Y /R /Q /D
	XCOPY "%~1\styles\DPI.css" "%IIS_ROOT_DIR%\Dpi.Central.Web\" /S /Y /R /Q /D
	IF NOT EXIST "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\images" MD "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\images"
	XCOPY "%~1\images" "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\images" /S /Y /R /Q /D
	IF NOT EXIST "%IIS_ROOT_DIR%\Dpi.Central.Web\flash" MD "%IIS_ROOT_DIR%\Dpi.Central.Web\flash"
	XCOPY "%~1\flash" "%IIS_ROOT_DIR%\Dpi.Central.Web\flash" /S /Y /R /Q /D
	IF NOT EXIST "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\flash" MD "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\flash"
	XCOPY "%~1\flash" "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\flash" /S /Y /R /Q /D
	IF NOT EXIST "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\script" MD "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\script"
	XCOPY "%~1\script" "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\script" /S /Y /R /Q /D
	XCOPY "%~1\styles\DPI.css" "%IIS_ROOT_DIR%\Dpi.Central.Web.Account\" /S /Y /R /Q /D
	GOTO NO_ERROR
) ELSE (
	ECHO ERROR: Environment variable IIS_ROOT_DIR must be defined.
	GOTO ERROR
)

:ERROR
Exit 1

:NO_ERROR
Exit 0