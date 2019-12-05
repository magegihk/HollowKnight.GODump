using System;
using System.IO;
using Modding;
using UnityEngine;

namespace GODump
{
    public class GODump : Mod<SaveSettings, GlobalSettings>
    {
        internal static GODump instance;
        public override void Initialize()
        {
            if (instance != null) return;

            instance = this;

            instance.Log("Initializing");

            GameObject DumpObj = new GameObject();
            DumpObj.AddComponent<Dump>();
            GameObject.DontDestroyOnLoad(DumpObj);

            InitSettings();
        }

        public override string GetVersion()
        {
            return "v1.2";
        }

        public override bool IsCurrent()
        {
            return true;
        }

        private void InitSettings()
        {
            GlobalSettings.dumpPosition = GlobalSettings.dumpPosition;
            GlobalSettings.dumpAtlasOnce = GlobalSettings.dumpAtlasOnce;
            GlobalSettings.dumpAtlasAlways = GlobalSettings.dumpAtlasAlways;
            GlobalSettings.dumpSpriteInfo = GlobalSettings.dumpSpriteInfo;
            GlobalSettings.mainGameObjectName = GlobalSettings.mainGameObjectName;
            GlobalSettings.AnimationsToDump = GlobalSettings.AnimationsToDump;

            this.SaveGlobalSettings();
        }


    }
}
