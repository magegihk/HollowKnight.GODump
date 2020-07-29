using System.IO;
using UnityEngine;

namespace GODump
{
    //Reference to SpriteDump.dll by KayDeeTee
    //And have been modified a lot
    static class SpriteDump
    {
        private static readonly Color[] colors = new Color[4096 * 4096];
        public static void Tk2dFlip(ref Texture2D texture2D)
        {
            Texture2D flipped = new Texture2D(texture2D.height, texture2D.width);
            for (int x = 0; x < texture2D.width; x++)
            {
                for (int y = 0; y < texture2D.height; y++)
                {
                    flipped.SetPixel(y, x, texture2D.GetPixel(x, y));
                }
            }
            texture2D = flipped;
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
        public static Texture2D SubTexture(Texture2D in_tex, RectP rectP)
        {
            Texture2D texture2D = new Texture2D(rectP.width, rectP.height);
            texture2D.SetPixels(in_tex.GetPixels(rectP.x, rectP.y, rectP.width, rectP.height));
            texture2D.Apply();
            return texture2D;
        }
        public static Texture2D SubTexturePosition(Texture2D in_tex, RectP rectP)
        {
            Texture2D texture2D = new Texture2D(in_tex.width, in_tex.height);
            texture2D.SetPixels(colors);
            texture2D.SetPixels(rectP.x, rectP.y, rectP.width, rectP.height, in_tex.GetPixels(rectP.x, rectP.y, rectP.width, rectP.height));
            texture2D.Apply();
            return texture2D;
        }
        public static Texture2D SpriteSizeFix(Texture2D in_tex, RectP rectP, RectP border)
        {
            Texture2D texture2D = new Texture2D(border.width, border.height);
            texture2D.SetPixels(colors);
            texture2D.SetPixels(rectP.x, rectP.y, rectP.width, rectP.height, in_tex.GetPixels());
            if (GODump.instance.GlobalSettings.RedRectangular)
            {
                for (int i = 0; i < texture2D.width; i++)
                {
                    for (int j = 0; j < texture2D.height; j++)
                    {
                        if (((i == rectP.xmin - 1 || i == rectP.xmax + 1) && (rectP.ymin - 1 <= j && j <= rectP.ymax + 1)) || ((rectP.xmin - 1 <= i && i <= rectP.xmax + 1) && (j == rectP.ymin - 1 || j == rectP.ymax + 1)))
                        {
                            texture2D.SetPixel(i, j, Color.red);
                        }
                    }
                }
            }
            texture2D.Apply();
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
        public struct RectP
        {
            public int x { set; get; }
            public int y { set; get; }
            public int width { set; get; }
            public int height { set; get; }
            public int xmin { set; get; }
            public int ymin { set; get; }
            public int xmax { set; get; }
            public int ymax { set; get; }

            public RectP(int _x, int _y, int _width, int _height)
            {
                x = _x;
                y = _y;
                width = _width;
                height = _height;
                xmin = _x;
                ymin = _y;
                xmax = _x + _width - 1;
                ymax = _y + _height - 1;
            }
        }
    }
}
