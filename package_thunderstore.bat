@echo off
setlocal

:: Read version from manifest.json (simple parse)
for /f "tokens=2 delims=:, " %%v in ('findstr /i "version_number" thunderstore\manifest.json') do (
    set VERSION=%%~v
)

set ZIPNAME=GambleMaxPlayers-%VERSION%.zip

:: Build the project
echo Building...
dotnet build -c Debug
if errorlevel 1 (
    echo Build failed.
    exit /b 1
)

:: Stage DLL into thunderstore package layout
echo Staging DLL...
if not exist "thunderstore\BepInEx\plugins" mkdir "thunderstore\BepInEx\plugins"
copy /y "build\GambleMaxPlayers.dll" "thunderstore\BepInEx\plugins\GambleMaxPlayers.dll"

:: Copy README into staging folder
copy /y "README.md" "thunderstore\README.md"

:: Remove old ZIP if present
if exist "%ZIPNAME%" del "%ZIPNAME%"

:: Create ZIP from the contents of thunderstore\
echo Creating %ZIPNAME%...
powershell -NoProfile -Command "Compress-Archive -Path 'thunderstore\*' -DestinationPath '%ZIPNAME%'"

if errorlevel 1 (
    echo Packaging failed.
    exit /b 1
)

echo Done: %ZIPNAME%
endlocal
