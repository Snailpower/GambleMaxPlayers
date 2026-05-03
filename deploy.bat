@echo off
setlocal

set GAME_DIR=C:\Program Files (x86)\Steam\steamapps\common\Gamble With Your Friends
set PLUGINS_DIR=%GAME_DIR%\BepInEx\plugins

echo Building...
dotnet build --verbosity minimal
if errorlevel 1 (
    echo Build failed.
    pause
    exit /b 1
)

echo Deploying to %PLUGINS_DIR%...
copy /y "build\GambleMaxPlayers.dll" "%PLUGINS_DIR%\GambleMaxPlayers.dll"
if errorlevel 1 (
    echo Deploy failed.
    pause
    exit /b 1
)

echo Done.
