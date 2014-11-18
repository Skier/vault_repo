@ECHO OFF

rem Set directory variables ***************************************************

SET COMMAND_DIR=%CD%

CD "%COMMAND_DIR%"
CALL "%COMMAND_DIR%\receive_release.cmd"

CD "%COMMAND_DIR%"
CALL "%COMMAND_DIR%\extract_release.cmd"