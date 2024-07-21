using HarmonyLib;
using MapResizer.Util;
using UI.Builder;
using UI.Map;
using UnityEngine;
using Logger = MapResizer.Util.Logger;

namespace MapResizer.Patches.Windows;

[HarmonyPatch(typeof(MapWindow))]
public static class MapWindowPatch
{
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(MapWindow), nameof(MapWindow.Start))]
    private static void OnStart(MapWindow __instance)
    {
        Logger.LogVerbose($"MapWindow SizeDelta{__instance._window._rectTransform.sizeDelta}");

        if (MapResizer.mapBaseSize.Equals(Vector2.zero))
        {
            MapResizer.mapBaseSize = __instance._window._rectTransform.sizeDelta;
            Logger.LogVerbose($"MapWindow {__instance._window.transform.name}");
        }

    }
    
}