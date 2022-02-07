@echo off
set root=%cd%
cd ..\Bilibili.Manga.Avalonia
set Buildx64=%root%\Publish\osx-x64
set x64App=%root%\Bilibili.Manga.x64.app\
set BuildArm64=%root%\Publish\osx-arm64
set Arm64App=%root%\Bilibili.Manga.arm64.app\
::osx-x64
echo 正在编译x64版本...
dotnet publish Bilibili.Manga.Avalonia.csproj -o %Buildx64% --runtime osx-x64 --self-contained true /p:Configuration=Release
xcopy %root%\MacTemple %x64App% /s /e /y
xcopy %Buildx64%\ %x64App%\Contents\MacOS /s /e /y
rd /s/q %Buildx64%
::osx-arm64
echo 正在编译arm64版本...
dotnet publish Bilibili.Manga.Avalonia.csproj -o %BuildArm64% --runtime osx.11.0-arm64 --self-contained true /p:Configuration=Release
xcopy %root%\MacTemple %Arm64App% /s /e /y
xcopy %BuildArm64%\ %Arm64App%\Contents\MacOS /s /e /y
rd /s/q %BuildArm64%
::build
echo 正在打包x64版本...
cd %root%
7z a -ttar Bilibili.Manga.x64.tar %x64App%
7z a -tgzip Bilibili.Manga.x64.tar.gz Bilibili.Manga.x64.tar
rd /s/q %x64App%
del Bilibili.Manga.x64.tar
echo 正在打包arm64版本...
7z a -ttar Bilibili.Manga.arm64.tar %Arm64App%
7z a -tgzip Bilibili.Manga.arm64.tar.gz Bilibili.Manga.arm64.tar
rd /s/q %Arm64App%
del Bilibili.Manga.arm64.tar