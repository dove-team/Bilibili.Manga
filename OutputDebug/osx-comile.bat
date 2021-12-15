@echo off
set root=%cd%
cd ..\Bilibili.Manga.Avalonia
set Buildx64=%root%\Publish\osx-x64
set BuildArm=%root%\Publish\osx-arm64
dotnet publish Bilibili.Manga.Avalonia.csproj -o %Buildx64% --runtime osx.11.0-x64 --self-contained true /p:Configuration=Release
dotnet publish Bilibili.Manga.Avalonia.csproj -o %BuildArm% --runtime osx.11.0-arm64 --self-contained true /p:Configuration=Release
pause