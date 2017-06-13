# .NET Core を マルチプラットフォームで動かす

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
1. git clone https://github.com/radiodio/ReviewCounter.git
1. パッケージマネージャコンソールで `Update-Database` コマンド実行

### 注意点
Visual StudioでViewファイルに日本語を入力して保存するとエンコードがS-JISになってしまった。
正しい対処法が分からないので、テキストエディターでそのファイルを開いて、UTF-8で保存しなおした。

## CentOS7.2 で動かす
### 前提
* インターネット環境（firewallも確認）
* Git
* Apache (+ mod_proxy)

### dotnetインストール

```shell
sudo yum install libunwind libicu
curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?linkid=848821
sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin
```

### ビルドして実行

```shell
git clone https://github.com/radiodio/ReviewCounter.git
cd ReviewCounter
dotnet restore
# dotnet publish
dotnet run
```

### Apache 経由でネットワークから接続
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

### デプロイ手順
1. 発行(publish)
1. IISマネージャでアプリケーションを追加
1. アクセス許可を付与
    * publishフォルダに読み取り実行権限を、コンピューター名\IIS_IUSRS に与える。
1. IISマネージャでサイトを再起動
