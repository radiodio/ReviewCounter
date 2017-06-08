# .NET Core を CentOS7.2 と IIS10.0 のアプリケーションプール上で動かす

## 開発
* Windows
* Visual Studio 2017
### マイグレーション
PowerShell

```shell
cd {プロジェクトルート}  #ReviewCounter.csprojの階層
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef database drop
dotnet ef database remove
```

### 開発環境
1. git clone
1. パッケージマネージャコンソールで `Update-Database` コマンド実行

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
dotnet restore
dotnet publish
dotnet run
```

### Apache 経由でネットワークから接続

## IIS10.0 のアプリケーションプール上で動かす
