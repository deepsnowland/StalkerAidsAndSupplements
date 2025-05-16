using UnityEngine;
using Il2Cpp;
using MelonLoader;
using UnityEngine.AddressableAssets;
using Unity.VisualScripting;


namespace StalkerAidsAndSupplementsMod
{
    internal static class StalkerAidsAndSupplementsUtils
    {
        public static bool IsScenePlayable(string scene)
        {
            return !(string.IsNullOrEmpty(scene) || scene.Contains("MainMenu") || scene == "Boot" || scene == "Empty");
        }
    }

}


