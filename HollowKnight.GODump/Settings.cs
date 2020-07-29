using Modding;
using System;
using System.Runtime.Remoting.Messaging;

namespace GODump
{

    public class SaveSettings : ModSettings { }

    [Serializable]
    public class GlobalSettings : ModSettings
    {
        //default values
        
        [NonSerialized]
        private bool dumpPosition = false;
        [NonSerialized]
        private bool dumpAtlasOnce = true;
        [NonSerialized]
        private bool dumpAtlasAlways = false;
        [NonSerialized]
        private bool dumpSpriteInfo = true;
        [NonSerialized]
        private bool spriteSizeFix = true;
        [NonSerialized]
        private bool redRectangular = true;
#if DEBUG
        [NonSerialized]
        private string mainGameObjectName = "Knight";
#endif
        [NonSerialized]
        private string animationsToDump = "AnimationsToDump";

        public bool DumpPosition
        {
            get { return dumpPosition; }
            set { dumpPosition = value; }
        }
        public bool DumpAtlasOnce
        {
            get { return dumpAtlasOnce; }
            set { dumpAtlasOnce = value; }
        }
        public bool DumpAtlasAlways
        {
            get { return dumpAtlasAlways; }
            set { dumpAtlasAlways = value; }
        }
        public bool DumpSpriteInfo
        {
            get { return dumpSpriteInfo; }
            set { dumpSpriteInfo = value; }
        }
        public bool SpriteSizeFix
        {
            get { return spriteSizeFix; }
            set { spriteSizeFix = value; }
        }

        public bool RedRectangular
        {
            get { return redRectangular; }
            set { redRectangular = value; }
        }
#if DEBUG
        public string MainGameObjectName
        {
            get { return mainGameObjectName; }
            set { mainGameObjectName = value; }
        }
#endif
        public string AnimationsToDump
        {
            get { return animationsToDump; }
            set { animationsToDump = value; }
        }
    }

}
