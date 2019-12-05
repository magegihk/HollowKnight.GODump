# HollowKnight.GODump
[English](./README.md) | 简体中文

导出《空洞骑士》中游戏对象的精灵图（sprite）,让制作自定义圣巢更简单。

## 安装

1. 安装《空洞骑士》的 [Modding API]( https://github.com/seanpr96/HollowKnight.Modding ) 
2. 下载最新的Release包
3. 解压到目录 "Steam\SteamApps\common\Hollow Knight"

## 使用

1. 打开游戏，然后文件 GODump.GlobalSettings.json 会在 "AppData\LocalLow\Team Cherry\Hollow Knight" 生成
2. 修改文件 GODump.GlobalSettings.json 中的设置并保存
3. 进入加载了你的 **mainGameObject** (默认名 "Knight") 的场景
4. 按F3，然后 GODump.GlobalSettings.json 底下会新增一个包含了你的 **mainGameObject** 本身以及子对象的动画集合的字符串变量
5. 删除该字符串中你不想要的动画集合然后保存
6. 按F4，然后你选择的**mainGameObject**中的所有精灵图都会以png的格式被导出到 "AppData\LocalLow\Team Cherry\Hollow Knight\sprites" 文件夹中
7. 如果你修改了设置，你还能获得每个精灵图的 atlas.png 和 position.png
8. 要打包回去请将设置dumpAtlasOnce和dumpSpriteInfo改为true。

## 更新

* **v1.2** 导出精灵图的编号方式从“图集编号”改为“动画编号3位-帧序编号2位-图集编号3位”；精灵图裁剪向下一个像素；现在可以用**精灵图包装鸡**把导出修改的精灵图打包回去了。

## 感谢
* [KayDeeTee](https://github.com/KayDeeTee) - SpriteDump Mod 解决了导出png的问题，帮我节省了不少时间。
* [Seanpr](https://github.com/seanpr96) - Modding API.
* [Team Cherry](https://teamcherry.com.au/) - 好！

## License
[GPL-3.0](https://choosealicense.com/licenses/gpl-3.0/)