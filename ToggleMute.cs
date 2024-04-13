using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ToggleMute;

[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, true)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class ToggleMute : BaseUnityPlugin
{
    internal static Assembly ToggleMuteAssembly => Assembly.GetExecutingAssembly();
    internal static ManualLogSource DebugLogger { get; private set; } = null!;
    internal static Harmony Harmony { get; set; } = null!;

    internal static string MUTE_UI_ASSET_BUNDLE_PATH = "ToggleMute.Resources.MuteUI.AssetBundle";
    internal static AssetBundle MuteUIAssetBundle = null!;

    private void Awake()
    {
        DebugLogger = base.Logger;
        DebugLogger.LogInfo($"Logging set.");

        MuteUIAssetBundle = Utilities.EmbeddedResources.LoadAssetBundleFromResources(MUTE_UI_ASSET_BUNDLE_PATH)!;
        if (MuteUIAssetBundle == null) { DebugLogger.LogError($"Failed to load asset bundle from {MUTE_UI_ASSET_BUNDLE_PATH}"); }
        else { DebugLogger.LogInfo($"Loaded asset bundle from {MUTE_UI_ASSET_BUNDLE_PATH}"); }

        Harmony = Harmony.CreateAndPatchAll(ToggleMuteAssembly);
        DebugLogger.LogInfo($"Patched {Harmony.GetPatchedMethods().Count()} methods.");

        new VoiceChatModeSetting().GetChoices(); // Propigate the possible choices
        DebugLogger.LogInfo($"Propigated the possible choices for the VoiceChatModeSetting.");

        DebugLogger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded.");
    }
}
