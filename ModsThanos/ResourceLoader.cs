using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Reactor.Extensions;
using UnityEngine;

namespace ModsThanos {
    public static class ResourceLoader {
        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        public static Dictionary<Sprite, string> allHats;
        public static AudioClip StonePickup;
        public static AudioClip SnapUsed;
        public static AudioClip RealityUsed;
        public static AudioClip MindUsed;
        public static AudioClip TimeUsed;
        public static AudioClip PowerUsed;
        public static AudioClip SpawnPortal;
        public static AudioClip PlayerRevive;

        public static void LoadAssets() {
            Stream resourceSteam = Assembly.GetManifestResourceStream("ModsThanos.Resources.xxx");
            AssetBundle assetBundle = AssetBundle.LoadFromMemory(resourceSteam.ReadFully());
            StonePickup = assetBundle.LoadAsset<AudioClip>("StonePickup").DontDestroy();
            SnapUsed = assetBundle.LoadAsset<AudioClip>("SnapUsed").DontDestroy();
            RealityUsed = assetBundle.LoadAsset<AudioClip>("RealityUsed").DontDestroy();
            MindUsed = assetBundle.LoadAsset<AudioClip>("MindUsed").DontDestroy();
            TimeUsed = assetBundle.LoadAsset<AudioClip>("TimeUsed").DontDestroy();
            PowerUsed = assetBundle.LoadAsset<AudioClip>("PowerUsed").DontDestroy();
            SpawnPortal = assetBundle.LoadAsset<AudioClip>("SpawnPortal").DontDestroy();
            PlayerRevive = assetBundle.LoadAsset<AudioClip>("PlayerRevive").DontDestroy();
        }
    }
}
