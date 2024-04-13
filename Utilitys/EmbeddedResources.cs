using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ToggleMute.Utilitys
{

    //ToggleMute.Resources.MuteUI.AssetBundle
    internal static class EmbeddedResources
    {
        internal static bool LoadAssetBundleFromResources(string assetBundlePath, [NotNullWhen(true)] out AssetBundle? assetBundle)
        {
            if (!ToggleMute.ToggleMuteAssembly.GetManifestResourceNames().Contains(assetBundlePath))
            {
                assetBundle = null;
                return false;
            }

            using (Stream manifestResourceStream = ToggleMute.ToggleMuteAssembly.GetManifestResourceStream(assetBundlePath))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                manifestResourceStream.CopyTo(memoryStream);
                assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
            }
            return true;
        }
    }
}
