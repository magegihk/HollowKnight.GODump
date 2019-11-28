using Modding;

namespace GODump
{

    public class SaveSettings : ModSettings { }

    public class GlobalSettings : ModSettings
    {
        public bool dumpPosition
        {
            get => GetBool(false);
            set => SetBool(value);
        }
        public bool dumpAtlasOnce
        {
            get => GetBool(false);
            set => SetBool(value);
        }
        public bool dumpAtlasAlways
        {
            get => GetBool(false);
            set => SetBool(value);
        }
        public bool dumpSpriteInfo
        {
            get => GetBool(false);
            set => SetBool(value);
        }
        public string mainGameObjectName
        {
            get => GetString("Knight");
            set => SetString(value);
        }

        public string AnimationsToDump
        {
            get => GetString("AnimationsToDump");
            set => SetString(value);
        }
    }
    
}
