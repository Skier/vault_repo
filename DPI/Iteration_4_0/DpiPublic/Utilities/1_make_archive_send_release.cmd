@ECHO OFF

rem Set directory variables ***************************************************

SET COMMAND_DIR=%CD%

CD "%COMMAND_DIR%"
CALL "%COMMAND_DIR%\make_release.cmd"

CD "%COMMAND_DIR%"
CALL "%COMMAND_DIR%\archive_release.cmd"

CD "%COMMAND_DIR%"
CALL "%COMMAND_DIR%\send_release.cmd"