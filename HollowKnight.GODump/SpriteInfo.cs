using System;
using System.Collections.Generic;

namespace GODump
{
    [Serializable]
    public class SpriteInfo
    {
        public List<int> sid;
        public List<int> sx;
        public List<int> sy;
        public List<int> sxr;
        public List<int> syr;
        public List<int> swidth;
        public List<int> sheight;


        public List<string> scollectionname;
        public List<string> spath;

        public List<bool> sfilpped;

        public SpriteInfo()
        {
            sid = new List<int>();
            sx = new List<int>();
            sy = new List<int>();
            sxr = new List<int>();
            syr = new List<int>();
            swidth = new List<int>();
            sheight = new List<int>();

            scollectionname = new List<string>();
            spath = new List<string>();

            sfilpped = new List<bool>();
        }

        public void Add(int _sid, int _sx, int _sy, int _sxr, int _syr, int _swidth, int _sheight, string _scollectionname, string _spath, bool _sfilpped)
        {
            sid.Add(_sid);
            sx.Add(_sx);
            sy.Add(_sy);
            sxr.Add(_sxr);
            syr.Add(_syr);
            swidth.Add(_swidth);
            sheight.Add(_sheight);
            scollectionname.Add(_scollectionname);
            spath.Add(_spath);
            sfilpped.Add(_sfilpped);
        }


    }
}
