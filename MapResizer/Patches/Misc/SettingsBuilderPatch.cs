using Game;
using HarmonyLib;
using MapResizer.Util;
using System;
using System.Drawing;
using TMPro;
using UI.Builder;
using UI.CarInspector;
using UI.PreferencesWindow;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.Layouts.InputControlLayout;
using Logger = MapResizer.Util.Logger;

namespace MapResizer.Patches.Misc;

[HarmonyPatch(typeof(SettingsBuilder))]
public static class SettingsBuilderPatch
{
    
}
