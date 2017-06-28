# 使い方

## 開発環境
[環境(Wikki)](https://github.com/radiodio/ReviewCounter/wiki#%E9%96%8B%E7%99%BA%E7%92%B0%E5%A2%83)
1. `git clone https://github.com/radiodio/ReviewCounter.git`
1. StartUp.cs :37 の connectionString を変更  
`var connection = @"Server=(localdb)\mssqllocaldb;Database=ReviewCounterDb;";`
1. Visual Studio パッケージマネージャコンソールで `Update-Database` コマンド実行

### マイグレーション
PowerShellかコマンドプロンプト
```shell
cd ReviewCounter/ReviewCounter/ReviewCounter/    # ReviewCounter.csprojの階層
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef database drop
dotnet ef database remove
```

### 注意点
Visual StudioでViewファイルに日本語を入力して保存するとエンコードがS-JISになってしまうことも。
正しい対処法が分からないので、テキストエディターでそのファイルを開いてUTF-8で保存しなおした。

## Linux 実行環境
[環境(Wikki)](https://github.com/radiodio/ReviewCounter/wiki#linux)
* インターネット環境必須
* MSがSqlServerのDockerイメージを公開しているのでこれを使う。

### Docker インストール
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

### SQL Server を起動 on Docker
```
sudo systemctl docker start
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Mickey_Mouse_123' -p 1433:1433 -d microsoft/mssql-server-linux
```

### dotnet インストール

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

## Windows 実行環境
[環境(Wikki)](https://github.com/radiodio/ReviewCounter/wiki#windows)

### IIS 10.0 インストール
[公式](https://docs.microsoft.com/en-us/aspnet/core/publishing/iis) を参照

### SQL Server 2016 Express インストール


### デプロイ手順
1. `git clone https://github.com/radiodio/ReviewCounter.git`
1. StartUp.cs :37 の connectionString を変更  
`var connection = @"";`
1. 発行(publish)    
   > Visual Studio からでOK
1. IISマネージャでアプリケーションを追加
1. アクセス許可を付与
   * publishフォルダに読み取り実行権限を、コンピューター名\IIS_IUSRS に与える。
1. IISマネージャでサイトを再起動
