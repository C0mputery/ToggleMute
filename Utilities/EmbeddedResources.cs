using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ToggleMute.Utilities
{

    //ToggleMute.Resources.MuteUI.AssetBundle
    internal static class EmbeddedResources
    {
        internal static AssetBundle? LoadAssetBundleFromResources(string assetBundlePath)
        {
            if (!ToggleMute.ToggleMuteAssembly.GetManifestResourceNames().Contains(assetBundlePath)) { return null; }

            using (Stream manifestResourceStream = ToggleMute.ToggleMuteAssembly.GetManifestResourceStream(assetBundlePath))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                manifestResourceStream.CopyTo(memoryStream);
                AssetBundle assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
                return assetBundle;
            }
        }
    }
}
