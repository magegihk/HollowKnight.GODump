using System.IO;
using UnityEngine;

namespace GODump
{
    //Reference to SpriteDump.dll by KayDeeTee
    static class SpriteDump
    {
        private static readonly Color[] colors = new Color[4096 * 4096];
        public static void Tk2dFlip(ref Texture2D texture2D)
        {
            Texture2D fliped = new Texture2D(texture2D.height, texture2D.width);
            for (int x = 0; x < texture2D.width; x++)
            {
                for (int y = 0; y < texture2D.height; y++)
                {
                    fliped.SetPixel(y, x, texture2D.GetPixel(x, y));
                }
            }
            texture2D = fliped;
        }

        public static Texture2D SubTexturePosition(Texture2D in_tex, int x, int y, int w, int h)
        {
            RenderTexture temporary = RenderTexture.GetTemporary(in_tex.width, in_tex.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(in_tex, temporary);
            RenderTexture active = RenderTexture.active;
            RenderTexture.active = temporary;
            Texture2D texture2D = new Texture2D(in_tex.width, in_tex.height);
            texture2D.SetPixels(0, 0, in_tex.width, in_tex.height, colors);
            texture2D.ReadPixels(new Rect((float)x, (float)y, (float)w, (float)h), x, y);
            texture2D.Apply();
            RenderTexture.active = active;
            RenderTexture.ReleaseTemporary(temporary);
            return texture2D;
        }

        
        public static Texture2D TextureReadHack(Texture2D in_tex)
        {
            RenderTexture temporary = RenderTexture.GetTemporary(in_tex.width, in_tex.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(in_tex, temporary);
            RenderTexture active = RenderTexture.active;
            RenderTexture.active = temporary;
            Texture2D texture2D = new Texture2D(in_tex.width, in_tex.height);
            texture2D.ReadPixels(new Rect(0f, 0f, (float)temporary.width, (float)temporary.height), 0, 0);
            texture2D.Apply();
            RenderTexture.active = active;
            RenderTexture.ReleaseTemporary(temporary);
            return texture2D;
        }

        public static Texture2D SubTexture(Texture2D in_tex, int x, int y, int w, int h)
        {
            RenderTexture temporary = RenderTexture.GetTemporary(in_tex.width, in_tex.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(in_tex, temporary);
            RenderTexture active = RenderTexture.active;
            RenderTexture.active = temporary;
            Texture2D texture2D = new Texture2D(w, h);
            texture2D.ReadPixels(new Rect((float)x, (float)y, (float)w, (float)h), 0, 0);
            texture2D.Apply();
            RenderTexture.active = active;
            RenderTexture.ReleaseTemporary(temporary);
            return texture2D;
        }
        public static void SaveTextureToFile(Texture2D tex, string fileName)
        {
            byte[] buffer = tex.EncodeToPNG();
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            FileStream output = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binaryWriter = new BinaryWriter(output);
            binaryWriter.Write(buffer);
            binaryWriter.Close();
        }

        
    }
}
