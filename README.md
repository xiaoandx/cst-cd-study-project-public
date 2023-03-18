<h1 align="center">Welcome to cst-cd-study-project-public 👋</h1>
<p>
  <img alt="Version" src="https://img.shields.io/badge/version-0.0.1-blue.svg?cacheSeconds=2592000" />

  <img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-yellow.svg" />
</p>

> 学习C#编程小项目

目前编程完成小项目，参考如下表格

| 序号 | 名称                    | 描述                                             |
| ---- | ----------------------- | ------------------------------------------------ |
| 1    | xmlOperationDome        | XML解析操作                                      |
| 2    | ExcelINTOCreateTableSQL | Excel数据转SQL创建表脚本                         |
| 3    | NPOIOperationDemo       | NPOI操作，导入导出Excel，CSV文件                 |
| 4    | SocketDemo              | Socket通信（实现SC间的消息发送，聊天文件导出等） |
| 5    | an-chaos-socketssl      | SocketSSL                                        |
| 6    | FurionDemo              | Furion WebAPI 搭建脚手架                         |

## Author

👤 **WEI.ZHOU**

* Website: http://blog.xiaoandx.club
* Github: [@xiaoandx](https://github.com/xiaoandx)
* Gitee: [@xiaoandx](https://gitee.com/xiaoandx)

## Show your support

Give a ⭐️ if this project helped you!


## commitizen 使用

[commitizen](https://github.com/commitizen/cz-cli) 是一个 cli 工具，用于规范化 git commit 信息，可以代替 git commit

```bash
npm install -g commitizen cz-conventional-changelog  # 安装规范化提交插件

echo '{"path": "cz-conventional-changelog"}' > ~/.czrc # 配置
git cz  
```


#### 接下来就在项目目录下使用即可[使用参考](./commitizen-practice.md)

```bash
git init
git add .
git cz  
```

## conventional-changelog生成CHANGELOG.md

#### 安装conventional-changelog
```bash
npm install -g conventional-changelog
```

#### 去项目目录下
```bash
cd cst-cd-study-project-public
```

#### 生成CHANGELOG.md
```bash
conventional-changelog -i CHANGELOG.md -s -r 0
```
