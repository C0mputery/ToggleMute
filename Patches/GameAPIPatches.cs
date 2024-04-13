using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace PushToMute.Patches
{
    [HarmonyPatch(typeof(GameAPI))]
    internal static class GameAPIPatches
    {
        internal static string MUTE_UI_ASSET_BUNDLE_PATH = "MuteUI";

        // Add Push to Mute and Toggle Mute to the hotbar
        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameAPI.Awake))]
        private static void AwakePatch(ref GameAPI __instance)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) { return; } // Don't load in the main menu
            if (ToggleMute.ToggleMute.MuteUIAssetBundle == null) { ToggleMute.ToggleMute.DebugLogger.LogError($"Asset bundle is null"); return; }
            GameObject.Instantiate(ToggleMute.ToggleMute.MuteUIAssetBundle.LoadAsset<GameObject>(MUTE_UI_ASSET_BUNDLE_PATH), __instance.transform);
        }
    }
}