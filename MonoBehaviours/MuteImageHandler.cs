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
            var a = Player.localPlayer.data.microphoneValue;
            a *= 0.5f;
            a = a.Clamp(0.1f, 1f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        }
    }
}