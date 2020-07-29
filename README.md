# HollowKnight.GODump
English | [简体中文](./README_cn.md)

Dump the gameobject in **Hollow Knight** into sprites.Make it easier to customize any thing in hollow nest.

## Installation

1. Install the [Modding API]( https://github.com/seanpr96/HollowKnight.Modding ) of Hollow Knight 
2. Download the latest archive from Releases
3. Then unzip the dll into "Steam\SteamApps\common\Hollow Knight\hollow_knight_Data\Managed\Mods"


## Usage

1. Start the game and GODump.GlobalSettings.json file in "AppData\LocalLow\Team Cherry\Hollow Knight" will be generated(delete GODump.GlobalSettings.json before you start if you're updating from older version)
2. Enter a game scene
3. Press F2 and atlases will be generated in "AppData\LocalLow\Team Cherry\Hollow Knight\atlases".The naming of atlas is Animation1@Animation2@...@AnimationN#AtlasName.png
4. Press F3 and GODump.GlobalSettings.json will be updated a new string of animations in your scene
5. Delete the animations you don't want in that string and save it(Learn Animation name from 3.)
6. Press F4 and all sprites in animations you choose will be dumped into "AppData\LocalLow\Team Cherry\Hollow Knight\sprites" folder in .png formation

## Notice

* The red rectangular is the editable border of the sprite.
* **DumpPosition** whether dump position.png which shows the position of a sprite inside the atlas.Pack not needed.
* **DumpAtlasOnce** whether dump atlas into 0.Atlases folder.Pack needed.
* **DumpAtlasAlways** whether dump atlases into every clip folder.Pack not needed.
* **DumpSpriteInfo** whether dump SpriteInfo.json into 0.Atlases folder.Pack needed.
* **SpriteSizeFix** whether fix the area outside the red rectangular of a sprite.Pack with v1.2 selected if Not Fix.Pack with v1.3 selected if Fix.~~Who the Hell Won't Fix It?~~
* **RedRectangular** whether add red rectangular in each sprite.

## Update

* **v1.2** Change naming of sprites to "Animation Num - Frame Num - Collection Num" from "Collection Num" only.Slice sprites one pixel lower than before.Use ** SpritePacker** to pack sprites back into atlas.
* **v1.3** Add setting **SpriteSizeFix**.Cutted empty space of a sprite in an atlas by tk2d tool is now added back.**No More Worry About Where The Fuck is The Anchor!**
* **v1.4** Press F2 to check for atlases you need;Press F3 to print all the animations in the scene
* **v1.5** Simplify json file on API 57. Add **RedRectangular** setting.

## Credits
* [KayDeeTee](https://github.com/KayDeeTee) - SpriteDump Mod save me a lot of time to figure out how to dump pngs.
* [Seanpr](https://github.com/seanpr96) - Modding API.
* [Team Cherry](https://teamcherry.com.au/) - Without which, we would not have Hollow Knight.

## License
[GPL-3.0](https://choosealicense.com/licenses/gpl-3.0/)