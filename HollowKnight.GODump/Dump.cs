using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using RectP = GODump.SpriteDump.RectP;

namespace GODump
{
    class Dump : MonoBehaviour
    {
        private static readonly string _spritePath = Application.persistentDataPath + "/sprites/";
        private static readonly string _atlasPath = Application.persistentDataPath + "/atlases/";

        private List<tk2dSpriteCollectionData> clns;
        private List<tk2dSpriteAnimation> anims;
        private string[] animNames;
        private int num;
#if DEBUG
        private string mainGameObjectName;
        private GameObject mainGameObject;
#endif


        public void Start()
        {
            clns = new List<tk2dSpriteCollectionData>();
            anims = new List<tk2dSpriteAnimation>();
            animNames = new string[] { };


        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                StartCoroutine(DumpAllAtlases());
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
#if DEBUG
                GODump.instance.LoadGlobalSettings();
                mainGameObjectName = GODump.instance.GlobalSettings.MainGameObjectName;
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

                    
                }
#endif
                clns = Resources.FindObjectsOfTypeAll<tk2dSpriteCollectionData>().ToList();
                anims = Resources.FindObjectsOfTypeAll<tk2dSpriteAnimation>().ToList();

                animNames = anims.Select(a => a.name).ToArray();

                GODump.instance.GlobalSettings.AnimationsToDump = String.Join("|", animNames);
                GODump.instance.SaveGlobalSettings();
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                GODump.instance.LoadGlobalSettings();
                animNames = GODump.instance.GlobalSettings.AnimationsToDump.Split('|');
                StartCoroutine(DumpAllSprites());

            }
        }

#if DEBUG

        private void DumpKnight(GameObject go, int depth)
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
            clns.Clear();
            foreach (tk2dSprite sprite in go.GetComponentsInChildren<tk2dSprite>(true))
            {
                if (!clns.Contains(sprite.Collection))
                {
                    clns.Add(sprite.Collection);

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
            anims.Clear();
            foreach (tk2dSpriteAnimator anim in go.GetComponentsInChildren<tk2dSpriteAnimator>(true))
            {
                if (!anims.Contains(anim.Library))
                {
                    anims.Add(anim.Library);

                    GODump.instance.Log("<AnimatorLibInfo:[GameObjectName]" + anim.gameObject.name + " /[AnimationName] " + anim.Library.name + ">");
                    foreach (tk2dSpriteAnimationClip clip in anim.Library.clips)
                    {
                        GODump.instance.Log("(" + anim.Library.name + ":" + clip.name + ")");
                    }

                }
            }
        }

#endif
        private IEnumerator DumpAllAtlases()
        {
            GODump.instance.Log("Begin Dumping Atlases.png");
            num = 0;
            clns = Resources.FindObjectsOfTypeAll<tk2dSpriteCollectionData>().ToList();
            anims = Resources.FindObjectsOfTypeAll<tk2dSpriteAnimation>().ToList();
            GODump.instance.Log("Find " + clns.Count + " Collections.");
            foreach (tk2dSpriteCollectionData cln in clns)
            {
                if (cln.allowMultipleAtlases && cln.textures.Length > 1)
                {
                    GODump.instance.LogWarn("Collection " + cln.name + "has multiple textures.");
                    yield return new WaitForSeconds(1.0f);
                }
                num++;
                Texture2D temp = SpriteDump.TextureReadHack(cln.textures[0] as Texture2D);
                SpriteDump.SaveTextureToFile(temp, _atlasPath + String.Join("@",GetUsedIns(cln,anims).Select(a => a.name).ToArray()) + "#" + cln.name + ".png");
                GameObject.DestroyImmediate(temp);
                yield return new WaitForSeconds(1.0f);
            }
            GODump.instance.Log("End Dumping Atlases.png:" + num);
            yield break;
        }
        private IEnumerator DumpAllSprites()
        {
            GODump.instance.Log("Begin Dumping Sprite.png");
            num = 0;
            foreach (var animL in anims)
            {
                if (animNames.Contains(animL.name))
                {
                    int i = 0;
                    SpriteInfo spriteInfo = new SpriteInfo();
                    GODump.instance.Log("Begin Dumping sprites in tk2dSpriteAnimator [" + animL.name + "].");
                    foreach (tk2dSpriteAnimationClip clip in animL.clips)
                    {
                        i++;
                        int j = -1;
                        float Xmax = -10000f;
                        float Ymax = -10000f;
                        float Xmin = 10000f;
                        float Ymin = 10000f;
                        foreach (tk2dSpriteAnimationFrame frame in clip.frames)
                        {
                            tk2dSpriteDefinition tk2DSpriteDefinition = frame.spriteCollection.spriteDefinitions[frame.spriteId];
                            Vector3[] pos = tk2DSpriteDefinition.positions;

                            float xmin = pos.Min(v => v.x);
                            float ymin = pos.Min(v => v.y);
                            float xmax = pos.Max(v => v.x);
                            float ymax = pos.Max(v => v.y);

                            Xmin = Xmin < xmin ? Xmin : xmin;
                            Ymin = Ymin < ymin ? Ymin : ymin;
                            Xmax = Xmax > xmax ? Xmax : xmax;
                            Ymax = Ymax > ymax ? Ymax : ymax;

                        }
                        foreach (tk2dSpriteAnimationFrame frame in clip.frames)
                        {
                            j++;

                            tk2dSpriteDefinition tk2DSpriteDefinition = frame.spriteCollection.spriteDefinitions[frame.spriteId];
                            Vector2[] uv = tk2DSpriteDefinition.uvs;
                            Vector3[] pos = tk2DSpriteDefinition.positions;
                            Texture texture = tk2DSpriteDefinition.material.mainTexture;
                            Texture2D texture2D = SpriteDump.TextureReadHack((Texture2D)texture);

                            string collectionname = frame.spriteCollection.spriteCollectionName;
                            string path = _spritePath + animL.name + "/0.Atlases/" + collectionname + ".png";
                            string path0 = _spritePath + animL.name + "/" + String.Format("{0:D3}", i) + "." + clip.name + "/" + collectionname + ".png";
                            string path1 = _spritePath + animL.name + "/" + String.Format("{0:D3}", i) + "." + clip.name + "/" + String.Format("{0:D3}", i) + "-" + String.Format("{0:D2}", j) + "-" + String.Format("{0:D3}", frame.spriteId) + "_position.png";
                            string path2r = animL.name + "/" + String.Format("{0:D3}", i) + "." + clip.name + "/" + String.Format("{0:D3}", i) + "-" + String.Format("{0:D2}", j) + "-" + String.Format("{0:D3}", frame.spriteId) + ".png";
                            string path2 = _spritePath + path2r;

                            bool flipped = tk2DSpriteDefinition.flipped == tk2dSpriteDefinition.FlipMode.Tk2d;

                            float xmin = pos.Min(v => v.x);
                            float ymin = pos.Min(v => v.y);
                            float xmax = pos.Max(v => v.x);
                            float ymax = pos.Max(v => v.y);



                            int x1 = (int)(uv.Min(v => v.x) * texture2D.width);
                            int y1 = (int)(uv.Min(v => v.y) * texture2D.height);
                            int x2 = (int)(uv.Max(v => v.x) * texture2D.width);
                            int y2 = (int)(uv.Max(v => v.y) * texture2D.height);

                            // symmetry transformation
                            int x11 = x1;
                            int y11 = y1;
                            int x22 = x2;
                            int y22 = y2;
                            if (flipped)
                            {
                                x22 = y2 + x1 - y1;
                                y22 = x2 - x1 + y1;
                            }

                            int x3 = (int)((Xmin - Xmin) / tk2DSpriteDefinition.texelSize.x);
                            int y3 = (int)((Ymin - Ymin) / tk2DSpriteDefinition.texelSize.y);
                            int x4 = (int)((Xmax - Xmin) / tk2DSpriteDefinition.texelSize.x);
                            int y4 = (int)((Ymax - Ymin) / tk2DSpriteDefinition.texelSize.y);

                            int x5 = (int)((xmin - Xmin) / tk2DSpriteDefinition.texelSize.x);
                            int y5 = (int)((ymin - Ymin) / tk2DSpriteDefinition.texelSize.y);
                            int x6 = (int)((xmax - Xmin) / tk2DSpriteDefinition.texelSize.x);
                            int y6 = (int)((ymax - Ymin) / tk2DSpriteDefinition.texelSize.y);

                            RectP uvpixel = new RectP(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
                            RectP posborder = new RectP(x11 - x5 + x3, y11 - y5 + y3, x4 - x3 + 1, y4 - y3 + 1);
                            RectP uvpixelr = new RectP(x5 - x3, y5 - y3, x22 - x11 + 1, y22 - y11 + 1);


                            if (!File.Exists(path) && GODump.instance.GlobalSettings.DumpAtlasOnce)
                            {
                                SpriteDump.SaveTextureToFile(texture2D, path);
                                num++;
                            }
                            if (!File.Exists(path0) && GODump.instance.GlobalSettings.DumpAtlasAlways)
                            {
                                SpriteDump.SaveTextureToFile(texture2D, path0);
                                num++;
                            }
                            if (!File.Exists(path1) && GODump.instance.GlobalSettings.DumpPosition)
                            {
                                Texture2D subposition2D = SpriteDump.SubTexturePosition(texture2D, uvpixel);
                                SpriteDump.SaveTextureToFile(subposition2D, path1);
                                num++;
                                UnityEngine.Object.DestroyImmediate(subposition2D);
                            }
                            if (GODump.instance.GlobalSettings.DumpSpriteInfo)
                            {
                                spriteInfo.Add(frame.spriteId, x1, y1, uvpixelr.x, uvpixelr.y, uvpixelr.width, uvpixelr.height, collectionname, path2r, flipped);
                            }
                            if (!File.Exists(path2))
                            {
                                Texture2D subtexture2D = SpriteDump.SubTexture(texture2D, uvpixel);
                                if (flipped)
                                {
                                    SpriteDump.Tk2dFlip(ref subtexture2D);
                                }
                                if (GODump.instance.GlobalSettings.SpriteSizeFix)
                                {
                                    Texture2D fixedtexture2D = SpriteDump.SpriteSizeFix(subtexture2D, uvpixelr, posborder);
                                    SpriteDump.SaveTextureToFile(fixedtexture2D, path2);
                                    UnityEngine.Object.DestroyImmediate(fixedtexture2D);
                                }
                                else
                                {
                                    SpriteDump.SaveTextureToFile(subtexture2D, path2);
                                }

                                UnityEngine.Object.DestroyImmediate(subtexture2D);
                                num++;
                            }
                            UnityEngine.Object.DestroyImmediate(texture2D);
                        }
                        yield return new WaitForSeconds(1.0f);
                    }

                    string spriteinfopath = _spritePath + animL.name + "/0.Atlases/SpriteInfo.json";
                    if (!File.Exists(spriteinfopath) && GODump.instance.GlobalSettings.DumpSpriteInfo)
                    {
                        using (FileStream fileStream = File.Create(spriteinfopath))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream))
                            {
                                string value = JsonUtility.ToJson(spriteInfo, true);
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
        private List<tk2dSpriteAnimation> GetUsedIns(tk2dSpriteCollectionData cln, List<tk2dSpriteAnimation> anims)
        {
            List<tk2dSpriteAnimation> used = new List<tk2dSpriteAnimation>();
            foreach (tk2dSpriteAnimation anim in anims)
            {
                foreach (tk2dSpriteAnimationClip clip in anim.clips)
                {
                    foreach (tk2dSpriteAnimationFrame frame in clip.frames)
                    {
                        if (frame.spriteCollection.name == cln.name && !used.Contains(anim))
                        {
                            used.Add(anim);
                        }
                    }
                }
            }
            return used;
        }
        

    }


}
