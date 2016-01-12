@echo off
CLS
ECHO " _______    _   _    _   _    _____    _____      __       _      _ "
echo "|__   __|  | | | |  | \ | |  |    _|  |_   _|    / _ \    | |    | |"
echo "   | |     | | | |  |  \| |  \_  \_     | |     / /_\ \   | |    | | "
echo "   | |     | |_| |  | |\  |    _\  )    | |    / /___\ \  | |__  | |__ "
echo "   |_|     |_____|  |_| |_|  |____/     |_|   /_/     \_\ |____| |____|"
echo.
for %%f in (*.exe) do (
set file=%%f)
echo  "%file%-- Service Installation Utility."
echo Copyright 2013 Tunstall Iberica S.A.
echo.
echo Press any key to start.
pause>nul
set /p svcName=set the service name to start the service when installed or press enter to do it manually:

if %PROCESSOR_ARCHITECTURE%==x86 (
	goto x86) else (
goto x64)
 
:x86
echo Installing Service %file% for x86 Processor architecture. Please wait.
%windir%\Microsoft.NET\Framework\v4.0.30319\installutil.exe %file%
IF not %ERRORLEVEL%==0 (goto exitError)

goto exit

:x64
echo Installing Service %file% for x86 Processor architecture. Please wait.
%windir%\Microsoft.NET\Framework64\v4.0.30319\installutil.exe %file%
goto exit

:exit
echo.
echo The installation has been finished successfully.
echo Initiating service in local machine. WAIT!
if defined svcName (
SC start %svcName%
) else (
echo "There's no argument defined to start the service. Must be done manually!")
echo Press any key to finish
pause>nul
Exit

:exitError
echo The installation failed. Please check the previous message to solve and retry the installation.
echo Press any key to finish
pause>nul
Exit