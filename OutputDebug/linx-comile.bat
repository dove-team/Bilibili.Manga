@echo off
set root=%cd%
dotnet tool install --global dotnet-deb
dotnet tool install --global dotnet-zip
cd ..\Bilibili.Manga.Avalonia
set Buildx64=%root%\Publish\linux-x64
set BuildArm=%root%\Publish\linux-arm64
::linux-x64
dotnet restore -r linux-x64
dotnet deb install
dotnet msbuild Bilibili.Manga.Avalonia.csproj -property:OutDir=%Buildx64% /t:CreateDeb /p:TargetFramework=net6.0 /p:RuntimeIdentifier=linux-x64 /p:Configuration=Release
::linux-arm
dotnet restore -r linux-arm
dotnet deb install
dotnet msbuild Bilibili.Manga.Avalonia.csproj -property:OutDir=%BuildArm% /t:CreateDeb /p:TargetFramework=net6.0 /p:RuntimeIdentifier=linux-arm /p:Configuration=Release
pause