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
        }
    }
}