# 概要

このプロジェクトではDockerでAndroid用アセットバンドルの作成とAPKのビルドを行います。
自分用メモとして作成しているため、手順はざっくりと記述しています。ご了承くださいませ。

# 手順

## Docker Desktopのインストール

https://www.docker.com/products/docker-desktop

上記URLからDocker Desktopをインストール

## PCに本プロジェクトをcloneする

```sh
git clone https://github.com/kakureusagi/AndroidAssetBundleBuildOnUbuntu.git
```

## dockerでUbuntu&Androidの環境を作るためにイメージをpull

docker内のUbuntu上でAndroidのアセットバンドルとAPKのビルドを行うためにgame-ciが提供しているコンテナイメージを利用します。

```sh
docker pull ubuntu-2021.2.6f1-android-0.15.0
```

## Ubuntuを起動

リポジトリにdocker-compose.ymlが用意してあるので、下記コマンドを入力してUbuntuを起動します。

```sh
docker compose up -d
```

## Ubuntu上でUnityのライセンスをアクティベートする

まずはubuntuに入ります。

```sh
docker exec -it ubuntu-unity bash
```

docker内で以下のコマンドを実行します。

```sh
cd /var
git clone https://github.com/kakureusagi/AndroidAssetBundleBuildOnUbuntu.git

# ライセンス要求ファイルを作成し、共有フォルダへ移動
/opt/unity/Editor/Unity -batchmode -createManualActivationFile -logFile /var/Work/unity-log.log ; mv Unity_v2021.2.6f1.alf /var/Work
```

上記コマンドでWorkフォルダ内にライセンス要求ファイル（Unity_v2021.2.6f1.alf）が生成されるので、以下のリンクから手動でライセンスをアクティベートし、ライセンスファイル（Unity_v2021.x.ulf）をDLします。

https://license.unity3d.com/manual

DLしたファイルをWorkフォルダに入れ、ubuntu側でライセンスをオフラインでアクティベートします。

```sh
/opt/unity/Editor/Unity -batchmode -manualLicenseFile /var/Work/Unity_v2021.x.ulf -logFile /var/Work/unity-log.log
```

これでUnityのライセンス認証が通った状態になっているはずなので、アセットバンドルのビルドを行います。Ubuntu上で下記コマンドを実行してアセットバンドルを作成後、APKまで作成します。

```sh
# アセットバンドルのビルド
/opt/unity/Editor/Unity -batchmode -quit -nographics -logFile /var/Work/unity-log.log -buildTarget Android -executeMethod App.Builder.BuildAssetBundle -projectPath /var/AndroidAssetBundleBuildOnUbuntu/Project

# アプリのビルド
/opt/unity/Editor/Unity -batchmode -quit -nographics -logFile /var/Work/unity-log.log -buildTarget Android -executeMethod App.Builder.BuildApp -projectPath /var/AndroidAssetBundleBuildOnUbuntu/Project ; mv /var/AndroidAssetBundleBuildOnUbuntu/Project/App.apk /var/Work/App.apk
```

上記コマンドが成功すればWorkフォルダ内にApp.apkが出力されています。

# ハマったこと

私のdockerの実行環境はWindows10 + WSL2だったのですが、WSL2で使用できるメモリ使用量の上限を2GBに設定しているとAPKの生成時にエラーが発生してしましました。
上限を4GBにすることで回避することができました。

WSL2のメモリ使用量上限は以下の内容を `C:\Users\username\.wslconfig` に保存することで設定しました。
```config
[wsl2]
memory=4GB
```

参考：WSLの設定について
https://docs.microsoft.com/en-us/windows/wsl/wsl-config#configure-global-options-with-wslconfig
