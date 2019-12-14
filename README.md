# HollowKnight.GODump
English | [简体中文](./README_cn.md)

Dump the gameobject in **Hollow Knight** into sprites.Make it easier to customize any thing in hollow nest.

## Installation

1. Install the [Modding API]( https://github.com/seanpr96/HollowKnight.Modding ) of Hollow Knight 
2. Download the latest archive from Releases
3. Then unzip the archive into "Steam\SteamApps\common\Hollow Knight"


## Usage

1. Start the game and GODump.GlobalSettings.json file in "AppData\LocalLow\Team Cherry\Hollow Knight" will be generated
2. Change settings in GODump.GlobalSettings.json file and save it
3. Enter a scene where your **mainGameObject** (default by named "Knight") is loaded.
4. Press F3 and GODump.GlobalSettings.json will be added a new string of animations that your **mainGameObject** contains in itself and its childen
5. Delete the animations you don't want in that string and save it
6. Press F4 and all sprites in that **mainGameObject** you choose will be dumped into "AppData\LocalLow\Team Cherry\Hollow Knight\sprites" folder in .png formation
7. You may also get an atlas.png and a position.png of each sprite if it is configed in settings
8. Set dumpAtlasOnce and dumpSpriteInfo to true if you want to pack the sprites back.

## Update

* **v1.2** Change naming of sprites to "Animation Num - Frame Num - Collection Num" from "Collection Num" only.Slice sprites one pixel lower than before.Use ** SpritePacker** to pack sprites back into atlas.
* **v1.3** Add setting **SpriteSizeFix**.Cutted empty space of a sprite in an atlas by tk2d tool is now added back.**No More Worry About Where The Fuck is The Anchor!**

## Credits
* [KayDeeTee](https://github.com/KayDeeTee) - SpriteDump Mod save me a lot of time to figure out how to dump pngs.
* [Seanpr](https://github.com/seanpr96) - Modding API.
* [Team Cherry](https://teamcherry.com.au/) - Without which, we would not have Hollow Knight.

## License
[GPL-3.0](https://choosealicense.com/licenses/gpl-3.0/)