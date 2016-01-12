@echo off
CLS
ECHO " _______    _   _    _   _    _____    _____      __       _      _ "
echo "|__   __|  | | | |  | \ | |  |  ___|  |_   _|    / _ \    | |    | |"
echo "   | |     | | | |  |  \| |  \_ \__     | |     / /_\ \   | |    | | "
echo "   | |     | |_| |  | |\  |   _\   )    | |    / /___\ \  | |__  | |__ "
echo "   |_|     |_____|  |_| |_|  |____/     |_|   /_/     \_\ |____| |____|"
echo.
echo  "Sybase libraries Installation Utility."
echo Copyright 2015 Tunstall Ibérica S.A.
echo.
echo Press any key to start.
pause>nul
set /p dirName=set the directory name where to find Sybase dlls. The installation will be done in the current directory:
echo %dirName%
if %PROCESSOR_ARCHITECTURE%==x86 (
	goto x86) else (
goto x64)
 
:x86
goto exitError
goto exit

:x64
echo Installing Sybase for x64 Processor architecture. Please wait.
FOR %%p IN (%dirName%\*.dll) DO (
echo Found %%p
echo registering...
%systemroot%\SysWoW64\regsvr32.exe "%%p"
IF not %ERRORLEVEL%==0 (goto exitError)
echo done!
)

goto exit

:exit
echo The installation has been finished successfully.
pause>nul
Exit /b

:exitError
echo The installation failed. Please check the previous message to solve and retry the installation.
echo Press any key to finish
pause>nul
Exit /b %ERRORLEVEL%