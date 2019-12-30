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
            get => GetBool(true);
            set => SetBool(value);
        }
        public bool dumpAtlasAlways
        {
            get => GetBool(false);
            set => SetBool(value);
        }
        public bool dumpSpriteInfo
        {
            get => GetBool(true);
            set => SetBool(value);
        }
        public bool SpriteSizeFix
        {
            get => GetBool(true);
            set => SetBool(value);
        }
#if DEBUG
        public string mainGameObjectName
        {
            get => GetString("Knight");
            set => SetString(value);
        }
#endif
        public string AnimationsToDump
        {
            get => GetString("AnimationsToDump");
            set => SetString(value);
        }
    }

}
