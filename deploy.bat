@echo off
setlocal

set PLUGINS_DIR=C:\Users\tomho\AppData\Roaming\Thunderstore Mod Manager\DataFolder\GambleWithYourFriends\profiles\Default\BepInEx\plugins\Snailpower-GambleMaxPlayers

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
