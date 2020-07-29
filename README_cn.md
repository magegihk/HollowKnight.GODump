# HollowKnight.GODump
[English](./README.md) | 简体中文

导出《空洞骑士》中游戏对象的精灵图（sprite）,让制作自定义圣巢更简单。

## 安装

1. 安装《空洞骑士》的 [Modding API]( https://github.com/seanpr96/HollowKnight.Modding ) 
2. 下载最新的Release包
3. 解压dll文件到目录 "Steam\SteamApps\common\Hollow Knight\hollow_knight_Data\Managed\Mods"

## 使用

1. 打开游戏，然后文件 GODump.GlobalSettings.json 会在 "AppData\LocalLow\Team Cherry\Hollow Knight" 生成（如更新版本请删除旧GODump.GlobalSettings.json文件再打开游戏）
2. 进入游戏场景
3. 按F2，然后在"AppData\LocalLow\Team Cherry\Hollow Knight\atlases"中找到你想修改的图集，图集文件名为"动画1@动画2@...@动画n#图集名.png"，动画1到n是使用了该图集的动画
4. 按F3，然后 GODump.GlobalSettings.json 底下会新增一个包含了该游戏场景所有的动画集合的字符串变量："动画1|动画2|...|动画n"
5. 删除该字符串中你不想要的动画集合然后保存
6. 按F4，然后你选择的动画集合中的所有精灵图都会以png的格式被导出到 "AppData\LocalLow\Team Cherry\Hollow Knight\sprites" 文件夹中

## 名词

* **精灵图** 英文名为sprite，指小图，可以是固定的（比如每个护符），也可以连成动画片段
* **图集** 英文名atlas，指精灵图组成的大图，是收纳精灵图的集合（比如自定义骑士文件夹里的每一张）
* **动画片段** 英文名clip，指数张精灵图连成的动画动作（比如小骑士走路）
* **动画** 英文名animation，指动画片段的集合（比如小骑士动画，包含了小骑士闲置、走路、冲刺、跳跃等等）


## 说明

* 精灵图中的红框是编辑的边界，编辑红框之外（包括红框）不会生效
* **DumpPosition** 是否导出位置图，该图和图集一样大，但是只包含一个精灵图，用于确认该精灵图在整个图集中的位置，非打包必要项
* **DumpAtlasOnce** 是否导出图集到0.Atlases文件夹，打包必要项
* **DumpAtlasAlways** 是否导出图集到每一个动画片段文件夹，非打包必要项
* **DumpSpriteInfo** 是否导出SpriteInfo.json文件到0.Atlases文件夹，打包必要项
* **SpriteSizeFix** 是否补全精灵图被削去的部分（红框外），不补全动作对不齐。否的话按v1.2打包，是的话按v1.3打包。~~傻子才选否~~。
* **RedRectangular** 是否显示红框

## 更新

* **v1.2** 导出精灵图的编号方式从“图集编号”改为“动画编号3位-帧序编号2位-图集编号3位”；精灵图裁剪向下一个像素；现在可以用**精灵图包装鸡**把导出修改的精灵图打包回去了。
* **v1.3** 添加设置**SpriteSizeFix**。精灵图被tk2d裁剪掉的空白被补回来了。再也不用担心对不齐的问题！
* **v1.4** 增加F2提前看有哪些图集；F3现在不再显示**MainGameObject**下的动画，转而显示当前场景的所有动画！
* **v1.5** 在57的API下，json文件不再显示双重设置；添加设置RedRectangular，可选红框

## 感谢
* [KayDeeTee](https://github.com/KayDeeTee) - SpriteDump Mod 解决了导出png的问题，帮我节省了不少时间。
* [Seanpr](https://github.com/seanpr96) - Modding API.
* [Team Cherry](https://teamcherry.com.au/) - 好！

## License
[GPL-3.0](https://choosealicense.com/licenses/gpl-3.0/)