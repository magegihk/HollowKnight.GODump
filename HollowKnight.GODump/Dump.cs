using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using JetBrains.Annotations;

namespace GODump
{
    class Dump : MonoBehaviour
    {
        private static readonly string _spritePath = Application.persistentDataPath + "/sprites/";

        private List<tk2dSpriteCollectionData> tk2dSpriteCollectionDatas;
        private List<tk2dSpriteAnimation> tk2dSpriteAnimations;
        private string[] AnimLibNames;
        private int num;
        private string mainGameObjectName;
        private GameObject mainGameObject;
        private SpriteInfo spriteInfo;


        public void Start()
        {
            tk2dSpriteCollectionDatas = new List<tk2dSpriteCollectionData>();
            tk2dSpriteAnimations = new List<tk2dSpriteAnimation>();
            spriteInfo = new SpriteInfo();
            AnimLibNames = new string[] { };


        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                GODump.instance.LoadGlobalSettings();
                mainGameObjectName = GODump.instance.GlobalSettings.mainGameObjectName;
                mainGameObject = GameObject.Find(mainGameObjectName);
                if (mainGameObject)
                {
                    GODump.instance.Log("Main GameObject [" + mainGameObjectName + "] Found.");
                    
                    GODump.instance.Log("Begin Dumping Main GameObject.");
                    DumpKnight(mainGameObject, 0);
                    GODump.instance.Log("End Dumping Main GameObject.");
                    

                    GODump.instance.Log("Begin Dumping SpriteCollection.");
                    DumpSpriteCollection(mainGameObject);
                    GODump.instance.Log("End Dumping SpriteCollection.");

                    GODump.instance.Log("Begin Dumping SpriteAnimator.");
                    DumpSpriteAnimator(mainGameObject);
                    GODump.instance.Log("End Dumping SpriteAnimator.");

                    AnimLibNames = tk2dSpriteAnimations.Select(a => a.name).ToArray();

                    GODump.instance.GlobalSettings.AnimationsToDump = String.Join("|", AnimLibNames);
                    GODump.instance.SaveGlobalSettings();
                }
                
            }

            if (Input.GetKeyDown(KeyCode.F4) && mainGameObject)
            {
                GODump.instance.LoadGlobalSettings();
                AnimLibNames = GODump.instance.GlobalSettings.AnimationsToDump.Split('|');
                StartCoroutine(DumpAllSprites());
                 
            }
        }



        private void DumpKnight(GameObject go,int depth)
        {
            GODump.instance.Log(new String('-', depth) + go.name);
            foreach (Component comp in go.GetComponents<Component>())
            {
                switch (comp.GetType().ToString())
                {
                    case "PlayMakerFSM":
                        GODump.instance.Log(new String('-', depth) + "<" + comp.GetType().ToString() + " : " + (comp as PlayMakerFSM).FsmName + ">");
                        break;
                    case "tk2dSprite":
                        GODump.instance.Log(new String('-', depth) + "<" + comp.GetType().ToString() + " : " + (comp as tk2dSprite).Collection.spriteCollectionName + "/" + (comp as tk2dSprite).CurrentSprite.name + ">");
                        break;
                    case "tk2dSpriteAnimator":
                        GODump.instance.Log(new String('-', depth) + "<" + comp.GetType().ToString() + " : " + (comp as tk2dSpriteAnimator).Library.name + ">");
                        break;
                    default:
                        GODump.instance.Log(new String('-', depth) + "<" + comp.GetType().ToString() + ">");
                        break;
                }
                


            }
            foreach (Transform child in go.transform)
            {
                DumpKnight(child.gameObject, depth + 1);
            }
        }

        private void DumpSpriteCollection(GameObject go)
        {
            foreach (tk2dSprite sprite in go.GetComponentsInChildren<tk2dSprite>(true))
            {
                if (!tk2dSpriteCollectionDatas.Contains(sprite.Collection))
                {
                    tk2dSpriteCollectionDatas.Add(sprite.Collection);
                    
                    GODump.instance.Log("<SpriteCollectionInfo:[GameObjectName]" + sprite.gameObject.name + "/[CollectionName]" + sprite.Collection.spriteCollectionName + "/[assetName]" + sprite.Collection.assetName + ">");
                    foreach (tk2dSpriteDefinition def in sprite.Collection.spriteDefinitions)
                    {
                        GODump.instance.Log("(" + sprite.Collection.spriteCollectionName + ":" + def.name + ")");
                    }
                    
                }
            }
        }

        private void DumpSpriteAnimator(GameObject go)
        {
            foreach (tk2dSpriteAnimator anim in go.GetComponentsInChildren<tk2dSpriteAnimator>(true))
            {
                if (!tk2dSpriteAnimations.Contains(anim.Library))
                {
                    tk2dSpriteAnimations.Add(anim.Library);
                    
                    GODump.instance.Log("<AnimatorLibInfo:[GameObjectName]" + anim.gameObject.name + " /[AnimationName] " + anim.Library.name + ">");
                    foreach (tk2dSpriteAnimationClip clip in anim.Library.clips)
                    {
                        GODump.instance.Log("(" + anim.Library.name + ":" + clip.name + ")");
                    }
                    
                }
            }
        }

        private IEnumerator DumpAllSprites()
        {
            GODump.instance.Log("Begin Dumping Sprite.png");
            num = 0;
            foreach (var animL in tk2dSpriteAnimations)
            {
                if (AnimLibNames.Contains(animL.name))
                {
                    int i = 0;
                    GODump.instance.Log("Begin Dumping sprites in tk2dSpriteAnimator [" + animL.name + "].");
                    foreach (tk2dSpriteAnimationClip clip in animL.clips)
                    {
                        i++;
                        foreach (tk2dSpriteAnimationFrame frame in clip.frames)
                        {
                            Vector2[] uv = frame.spriteCollection.spriteDefinitions[frame.spriteId].uvs;

                            Texture texture = frame.spriteCollection.spriteDefinitions[frame.spriteId].material.mainTexture;
                            Texture2D texture2D = SpriteDump.TextureReadHack((Texture2D)texture);

                            string collectionname = frame.spriteCollection.spriteCollectionName;
                            string path = _spritePath + animL.name + "/0.Atlases/" + collectionname + ".png";
                            string path0 = _spritePath + animL.name + "/" + i + "." + clip.name + "/atlas.png";
                            string path1 = _spritePath + animL.name + "/" + i + "." + clip.name + "/" + frame.spriteId + "_position.png";
                            string path2 = _spritePath + animL.name + "/" + i + "." + clip.name + "/" + frame.spriteId + ".png";

                            int x0 = (int)(uv.Min(v => v.x) * texture2D.width);
                            int y0 = (int)(uv.Min(v => v.y) * texture2D.height);
                            int x1 = (int)(uv.Max(v => v.x) * texture2D.width);
                            int y1 = (int)(uv.Max(v => v.y) * texture2D.height);
                            int width = x1 - x0;
                            int height = y1 - y0;

                            //the origin in GUI is left top other than left bottom,learn more in UnityEngine.Rect
                            int y2 = texture2D.height - y1;

                            bool flipped = frame.spriteCollection.spriteDefinitions[frame.spriteId].flipped == tk2dSpriteDefinition.FlipMode.Tk2d;

                            if (!File.Exists(path) && GODump.instance.GlobalSettings.dumpAtlasOnce)
                            {
                                SpriteDump.SaveTextureToFile(texture2D, path);
                                num++;
                            }
                            if (!File.Exists(path0) && GODump.instance.GlobalSettings.dumpAtlasAlways)
                            {
                                SpriteDump.SaveTextureToFile(texture2D, path0);
                                num++;
                            }
                            if (!File.Exists(path1) && GODump.instance.GlobalSettings.dumpPosition)
                            {
                                Texture2D subposition2D = SpriteDump.SubTexturePosition(texture2D, x0, y2, width, height);
                                SpriteDump.SaveTextureToFile(subposition2D, path1);
                                num++;
                                UnityEngine.Object.DestroyImmediate(subposition2D);
                            }
                            if (GODump.instance.GlobalSettings.dumpSpriteInfo)
                            {
                                spriteInfo.Add(frame.spriteId, x0, y0, width, height, clip.name, collectionname, path2,  flipped);
                            }
                            if (!File.Exists(path2))
                            {
                                Texture2D subtexture2D = SpriteDump.SubTexture(texture2D, x0, y2, width, height);
                                if (flipped)
                                {
                                    SpriteDump.Tk2dFlip(ref subtexture2D);
                                }
                                SpriteDump.SaveTextureToFile(subtexture2D, path2);
                                num++;
                                UnityEngine.Object.DestroyImmediate(subtexture2D);
                            }
                            UnityEngine.Object.DestroyImmediate(texture2D);
                        }
                        yield return new WaitForSeconds(1.0f);
                    }

                    string spriteinfopath = _spritePath + animL.name + "/0.Atlases/SpriteInfo.json";
                    if (!File.Exists(spriteinfopath) && GODump.instance.GlobalSettings.dumpSpriteInfo)
                    {
                        using (FileStream fileStream = File.Create(spriteinfopath))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream))
                            {
                                string value = JsonUtility.ToJson(spriteInfo,true);
                                streamWriter.Write(value);
                            }
                        }
                    }
                    

                    GODump.instance.Log("End Dumping sprites in tk2dSpriteAnimator [" + animL.name + "].");

                }

            }
            

            GODump.instance.Log("End Dumping Sprite.png " + num + " sprites dumped.");
            yield break;
        }

        


    }


}
