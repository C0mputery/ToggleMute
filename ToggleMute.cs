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
    internal static ToggleMute Instance { get; private set; } = null!;
    internal static ManualLogSource DebugLogger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; } = null!;

    private void Awake()
    {
        Instance = this;
        DebugLogger = base.Logger;
        Harmony = Harmony.CreateAndPatchAll(ToggleMuteAssembly);

        DebugLogger.LogInfo("Loading asset bundles from resources...");

        DebugLogger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }
}
