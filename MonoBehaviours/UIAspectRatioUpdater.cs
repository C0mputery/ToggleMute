using UnityEngine;
using Zorro.Core;

namespace ToggleMute
{
    public class UIAspectRatioUpdater : MonoBehaviour
    {
        private (int, int) m_currentResolution;
        public AspectRatioUISetter[] AspectRatioUISetters = null!;

        // Taken from the UserInterface class
        private void LateUpdate()
        {
            (int, int) screenSize = (Screen.width, Screen.height);
            if (!(m_currentResolution == screenSize))
            {
                AspectRatio aspectRatio = AspectRatioUtility.GetAspectRatio((float)Screen.width / (float)Screen.height);
                LoadAspectRatio(aspectRatio);
                m_currentResolution = screenSize;
            }
        }

        // Taken from the UserInterface class
        public void LoadAspectRatio(AspectRatio aspectRatio)
        {
            if (aspectRatio == AspectRatio._16x10)
            {
                aspectRatio = AspectRatio._4x3;
            }
            foreach (AspectRatioUISetter aspectRatioUISetter in AspectRatioUISetters)
            {
                aspectRatioUISetter.LoadAspectPosition(aspectRatio);
            }
        }
    }
}