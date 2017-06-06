# .NET Core を CentOS7.2 と IIS10.0 のアプリケーションプール上で動かす

## 開発
* Windows
* Visual Studio 2017
### EntityFrameworkCoreでマイグレーションする準備

```xml:ReviewCounter.csproj
<ItemGroup>
  <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="1.0.0" />
  <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.0" />
</ItemGroup>
```

### マイグレーション

```shell
cd {プロジェクトルート}  #ReviewCounter.csprojの階層
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef database drop
dotnet ef database remove
```

## CentOS7.2 で動かす
### dotnetインストール

```shell
sudo yum install libunwind libicu
curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?linkid=848821
sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin
```

### ビルドして実行
* インターネット環境

```shell
$ dotnet restore
$ dotnet publish
$ dotnet run
```
