@echo off
set root=%cd%
cd ..\Bilibili.Manga.Avalonia
set Buildx64=%root%\Publish\win-x64
set Buildx86=%root%\Publish\win-x86
set BuildArm=%root%\Publish\win-arm64
if exist %Buildx64%\ (del /q/a/f/s %Buildx64%\*.*) 
if exist %Buildx86%\ (del /q/a/f/s %Buildx86%\*.*) 
if exist %BuildArm%\ (del /q/a/f/s %BuildArm%\*.*) 
dotnet publish -c Release -r win-x64 -o %Buildx64% --self-contained true
dotnet publish -c Release -r win-x86 -o %Buildx86% --self-contained true
dotnet publish -c Release -r win-arm64 -o %BuildArm% --self-contained true
pause