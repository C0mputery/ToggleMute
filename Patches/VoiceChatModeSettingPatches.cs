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

        // Push to Talk
        internal static int PushToTalkIndex;

        // Show the muted text
        internal static bool showMutedImage = false;

        // Add Push to Mute and Toggle Mute to the list of choices
        [HarmonyPostfix]
        [HarmonyPatch(nameof(VoiceChatModeSetting.GetChoices))]
        private static void GetChoicesPatch(ref List<string> __result)
        {
            PushToTalkIndex = __result.FindIndex(x => x == "Push to Talk");
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

            // Cannot be a switch statement because the values are not constant ):
            if (__instance.Value == PushToTalkIndex)
            {
                VoiceChatModeSettingPatches.showMutedImage = isPressingPushToTalkKey ? false : true;
            }
            else if (__instance.Value == PushToMuteIndex)
            {
                VoiceChatModeSettingPatches.showMutedImage = isPressingPushToTalkKey;
                __result = !isPressingPushToTalkKey;
            }
            else if (__instance.Value == ToggleMuteIndex)
            {
                bool keyWasJustPressed = !wasPressingButton && isPressingPushToTalkKey;
                VoiceChatModeSettingPatches.showMutedImage = keyWasJustPressed ? !VoiceChatModeSettingPatches.showMutedImage : VoiceChatModeSettingPatches.showMutedImage;
                __result = !VoiceChatModeSettingPatches.showMutedImage;
            }
            else
            {
                VoiceChatModeSettingPatches.showMutedImage = false;
            }
            wasPressingButton = isPressingPushToTalkKey;
        }
    }
}