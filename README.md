# .NET Core を マルチプラットフォームで動かす

## 開発環境
* Windows
* Visual Studio 2017

### セッティング
1. git clone https://github.com/radiodio/ReviewCounter.git
1. パッケージマネージャコンソールで `Update-Database` コマンド実行
1. StartUp.cs の connectionString を変更
```
var connection = @"Server=(localdb)\mssqllocaldb;Database=ReviewCounterDb;";
```

### マイグレーション
PowerShell

```shell
cd ReviewCounter/ReviewCounter/ReviewCounter/  #ReviewCounter.csprojの階層
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef database drop
dotnet ef database remove
```

### 注意点
Visual StudioでViewファイルに日本語を入力して保存するとエンコードがS-JISになってしまった。
正しい対処法が分からないので、テキストエディターでそのファイルを開いて、UTF-8で保存しなおした。

## CentOS7.2 で動かす
### 実行環境
* インターネット環境
* dotnet
* Git
* SQL Server
* Apache (+ mod_proxy)

### Dockerインストール
```
su
yum install -y yum-utils device-mapper-persistent-data lvm2
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
yum-config-manager --enable docker-ce-edge
yum makecache fast
yum install -y docker-ce
mkdir /etc/docker
cat <<_E > /etc/docker/daemon.json
{
  "storage-driver": "devicemapper"
}
_E
curl -L https://github.com/docker/compose/releases/download/1.14.0/docker-compose-`uname -s`-`uname -m` > /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose
```

### SQL Server起動 on Docker
```
sudo systemctl docker start
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Mickey_Mouse_123' -p 1433:1433 -d microsoft/mssql-server-linux
```

### dotnetインストール

```shell
sudo yum install -y libunwind libicu
curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?linkid=848821
sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin
```

### アプリをビルドして実行

```shell
git clone https://github.com/radiodio/ReviewCounter.git
cd ReviewCounter/ReviewCounter/ReviewCounter/
dotnet restore
# dotnet publish
dotnet run
```

### Apache 経由での接続
/etc/httpd/conf.d

```
LoadModule proxy_module modules/mod_proxy.so
LoadModule proxy_http_module modules/mod_proxy_http.so

<Location />
 ProxyPass http://localhost:5000/
</Location>
```

## IIS10.0 のアプリケーションプール上で動かす

* [公式サイト](https://docs.microsoft.com/en-us/aspnet/core/publishing/iis) 参照

### 実行環境
* IIS10.0
* .NET Core Windows Server Hosting
* Git

### デプロイ手順
1. 発行(publish)
1. IISマネージャでアプリケーションを追加
1. アクセス許可を付与
   * publishフォルダに読み取り実行権限を、コンピューター名\IIS_IUSRS に与える。
1. IISマネージャでサイトを再起動
