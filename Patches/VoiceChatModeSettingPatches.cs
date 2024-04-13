using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PushToMute.Patches
{
    [HarmonyPatch(typeof(VoiceChatModeSetting))]
    internal static class VoiceChatModeSettingPatches
    {
        // Push to Mute
        internal static int PushToMuteIndex;

        // Toggle Mute
        internal static int ToggleMuteIndex;
        internal static bool canTalk = true;

        // Show the muted text
        internal static bool showMutedImage = false;

        // Add Push to Mute and Toggle Mute to the list of choices
        [HarmonyPostfix]
        [HarmonyPatch(nameof(VoiceChatModeSetting.GetChoices))]
        private static void GetChoicesPatch(ref List<string> __result)
        {
            __result.Add("Push to Mute");
            PushToMuteIndex = __result.Count - 1;
            __result.Add("Toggle Mute");
            ToggleMuteIndex = __result.Count - 1;
        }

        static bool wasPressingButton = false;

        // Handle Push to Mute and Toggle Mute
        [HarmonyPostfix()]
        [HarmonyPatch(nameof(VoiceChatModeSetting.CanTalk))]
        private static void CanTalkPatch(ref bool __result, ref VoiceChatModeSetting __instance)
        {
            bool isPressingPushToTalkKey = GlobalInputHandler.PushToTalkKey.GetKey();
            if (__instance.Value == PushToMuteIndex)
            {
                __result = !isPressingPushToTalkKey;
            }
            else if (__instance.Value == ToggleMuteIndex)
            {
                bool keyWasJustPressed = !wasPressingButton && isPressingPushToTalkKey;
                canTalk = keyWasJustPressed ? !canTalk : canTalk;
                __result = canTalk;
            }
            wasPressingButton = isPressingPushToTalkKey;

            showMutedImage = !__result;
        }
    }
}