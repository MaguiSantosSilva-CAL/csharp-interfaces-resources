@echo off
cls
SET JOB =%~n0
SET TILE=%~n0 Create List of Folder Contents as Text

::where job lives
set startDP=%~dp0
set basedir=%~dp0



@echo off
@REM ==============================================
@REM author: maguiss
@REM date: 05/05/2021 [@CAL 07/10/2021]
@REM
@REM purpose: Creates txt file with list of folder contents folder contents and sends to clipboard
@REM ==============================================
cls

SET SRCPATH=P:\GitHub\csharp-interfaces-resources\net50\module2\before\Polygons\Polygons\Resources
REM SET DRIVE=

:metadata
SET JOB=%~n0
SET TITLE=%~n0 Create List of Folder Contents as Txt
SET "PROJECT=Polygons and Logins"
SET PROJECTFILE=%PROJECT%.ssmssln
SET PROJECTPATH=%SRCPATH%\%PROJECTFILE%
SET "FILENAME=Sql Resources for %project%"

:: Where the job lives
SET startDP=%~dp0
SET basedir=%~dp0

:: Resources
SET BASE=P:\GitHub\csharp-interfaces-resources\net50\module2\before\Polygons\Polygons\Resources
SET TARGET=%~1\
SET TARGETHOME=%BASE%%TARGET%
SET RESOURCES=%TARGETHOME%SQL

SET LISTING=%TARGETHOME%%FILENAME%.txt

:: Log
set logdir=%basedir%Logs\
SET LOG=%logdir%%FILENAME%_log.txt

SET "FORSOLUTION=%logdir%%FILENAME% for Solution_log.txt"

:start
echo cd:	%CD%
echo list from:	%RESOURCES%
echo into:	%LISTING%
echo ---------------------------------------------------------------------------
echo.

rem dir /s/b /A:-D "%RESOURCES%"
echo.
echo Output to: %LISTING%

IF EXIST "%LISTING%" DEL "%LISTING%"
dir /s/b /A:-D "%RESOURCES%" > "%LISTING%"

IF EXIST "%FORSOLUTION%" DEL "%FORSOLUTION%"
(echo. ) >> "%FORSOLUTION%"
echo point 0
pause

for /F "tokens=*" %%A in ("%LISTING%") do (echo %%A = %%A & echo.) >> "%FORSOLUTION%"

echo point 1
pause

type "%FORSOLUTION%" | clip

(echo.
echo %date% ; %TIME% ; %USERNAME% ; Extracted contents of: %RESOURCES% into %LISTING%) >> "%LOG%"

:choice
cls
echo.
echo FILE PATHS
echo ------------------------------------------------------------
echo.
type "%LISTING%"
echo.
echo.
echo COPIED FROM %RESOURCES%
echo COPIED TO CLIPBOARD on %date%
echo.
echo ------------------------------------------------------------
echo.
echo.

echo Select or Default in 5 seconds.
echo ---------------------
echo default 1. [E]xit
echo 2. [C]urrent Directory...
echo 3. [O]pen Listing File:
echo (%PROJECTPATH%)
echo  P:\GitHub\csharp-interfaces-resources\net50\module2\before\Polygons\Polygons\Resources\Polygons and Logins.ssmssln
echo 4. [W]ait
echo Ctrl-C will prompt for job termination.
SET KEY=error
choice /c ecow1234 /m ">" /n /t 5 /d e

IF %ERRORLEVEL% EQU 8 goto :wait
IF %ERRORLEVEL% EQU 7 goto :file
IF %ERRORLEVEL% EQU 6 goto :folder
IF %ERRORLEVEL% EQU 5 goto :EOF
IF %ERRORLEVEL% EQU 4 goto :wait
IF %ERRORLEVEL% EQU 3 goto :file
IF %ERRORLEVEL% EQU 2 goto :folder
IF %ERRORLEVEL% EQU 1 goto :EOF

goto :EOF

:wait
timeout /t 30
goto :choice

:file
START "" CMD /c START "" notepad.exe %PROJECTPATH%
GOTO :EOF

:folder
%SystemRoot%\explorer.exe %startDP%
GOTO :EOF

:EOF
IF EXIST "%LISTING%" DEL "%LISTING%"
exit

:endnotes
echo @TODO: Make this fle exclude itself from the list?

echo %~1
echo f %~f1
echo d %~d1
echo p %~p1
echo n %~n1
echo x %~x1
echo s %~s1
echo a %~a1
echo t %~t1
echo z %~z1
echo path %~$PATH:1