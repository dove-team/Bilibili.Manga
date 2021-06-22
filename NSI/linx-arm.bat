dotnet tool install --global dotnet-deb
dotnet tool install --global dotnet-zip

dotnet restore -r linux-arm
dotnet deb install
dotnet msbuild Bilibili.Manga.Avalonia.csproj /t:CreateDeb /p:TargetFramework=netcoreapp3.1 /p:RuntimeIdentifier=linux-arm /p:Configuration=Release