using CurvedUI;
using PushToMute.Patches;
using UnityEngine;
using UnityEngine.UI;

namespace ToggleMute
{
    public class MuteImageHandler : MonoBehaviour
    {
        public Image image = null!;
        public Sprite mutedSprite = null!;
        public Sprite unmutedSprite = null!;

        public void LateUpdate()
        {
            image.sprite = VoiceChatModeSettingPatches.showMutedImage ? mutedSprite : unmutedSprite;
            float MicVol = Player.localPlayer.data.microphoneValue;
            MicVol *= 0.5f;
            MicVol = MicVol.Clamp(0.1f, 1f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, MicVol);
        }
    }
}