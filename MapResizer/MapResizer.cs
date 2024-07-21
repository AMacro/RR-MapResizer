using System;
using System.IO;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine;
using UnityModManagerNet;
using System.Collections.Generic;
using Analytics;
using Helpers;
using TMPro;
using Logger = MapResizer.Util.Logger;
using UI.Map;
using MapResizer.Util;


namespace MapResizer;

public static class MapResizer
{
    public static UnityModManager.ModEntry ModEntry;
    public static Settings settings;
    public static bool mapMaximised = false;
    private static Vector2 _mapBaseSize = new Vector2();
    private static Vector2 _mapSize = new Vector2();

    public static Vector2 mapBaseSize
    {
        get => _mapBaseSize;
        set {
            _mapBaseSize = value;
            _mapSize = value; 
        }
    }



    [UsedImplicitly]
    private static bool Load(UnityModManager.ModEntry modEntry)
    {
        ModEntry = modEntry;
        settings = Settings.Load<Settings>(modEntry);
        ModEntry.OnGUI = settings.Draw;
        ModEntry.OnSaveGUI = settings.Save;
        ModEntry.OnLateUpdate += MapResizer.LateUpdate;

        Harmony harmony = null;

        try
        {
            //Apply patches
            Logger.LogInfo("Patching...");
            harmony = new Harmony(ModEntry.Info.Id);
            harmony.PatchAll();
            Logger.LogInfo("Patched");
 
        }
        catch (Exception ex)
        {
            Logger.LogInfo("Failed to load: {ex}");
            harmony?.UnpatchAll();
            return false;
        }

        return true;
    }

    private static void LateUpdate(UnityModManager.ModEntry modEntry, float deltaTime)
    {
        if(ModEntry.NewestVersion != null && ModEntry.NewestVersion.ToString() != "")
        {
            Logger.LogInfo($"MapResizer Latest Version: {ModEntry.NewestVersion}");

            ModEntry.OnLateUpdate -= MapResizer.LateUpdate;

            if (ModEntry.NewestVersion > ModEntry.Version)
            {
                ShowUpdate();
            }
            
        }
        
    }
    private static void ShowUpdate()
    {
        EarlyAccessSplash earlyAccessSplash = UnityEngine.Object.FindObjectOfType<EarlyAccessSplash>();

        if (earlyAccessSplash == null)
            return;

        earlyAccessSplash = UnityEngine.Object.Instantiate<EarlyAccessSplash>(earlyAccessSplash, earlyAccessSplash.transform.parent);

        TextMeshProUGUI text = GameObject.Find("Canvas/EA(Clone)/EA Panel/Scroll View/Viewport/Text").GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"\r\n<style=h3>MapResizer Update</style>\r\n\r\nA new version of MapResizer Mod is available.\r\n\r\nCurrent version: {ModEntry.Version}\r\nNew version: {ModEntry.NewestVersion}\r\n\r\nRun Unity Mod Manager Installer to apply the update.";

        RectTransform rt = GameObject.Find("Canvas/EA(Clone)/EA Panel").transform.GetComponent<RectTransform>();


        UnityEngine.Object.DestroyImmediate(GameObject.Find("Canvas/EA(Clone)/EA Panel/Label Regular"));
        UnityEngine.Object.DestroyImmediate(GameObject.Find("Canvas/EA(Clone)/EA Panel/Buttons/Opt Out"));

        UnityEngine.UI.Button button = GameObject.Find("Canvas/EA(Clone)/EA Panel/Buttons/Opt In").GetComponentInChildren<UnityEngine.UI.Button>();
        button.TMPText().text = "OK";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate {
            earlyAccessSplash.Dismiss();
            UnityEngine.Object.Destroy(earlyAccessSplash);
        });

        earlyAccessSplash.Show();
    }

    public static void FullscreenClick()
    {
        if (!mapMaximised)
        {
            Logger.LogVerbose($"Parent Name: {MapWindow.instance.transform.parent.name}");
            Vector2 canvasSize = MapWindow.instance.transform.parent.GetComponent<RectTransform>().sizeDelta;

            MapWindow.instance._window._rectTransform.localScale = new Vector3(1f,1f,1f);
            MapWindow.instance._window._rectTransform.sizeDelta = canvasSize;
            MapWindow.instance._window.SetPosition(UI.Common.Window.Position.UpperLeft);

        }
        else
        {
            MapWindow.instance._window._rectTransform.sizeDelta = _mapSize;
        }
        MapWindow.instance.mapBuilder.Rebuild();
        
        mapMaximised = !mapMaximised;
    }
    public static void ResetClick()
    {
        
        MapWindow.instance._window._rectTransform.sizeDelta = _mapBaseSize;
        MapWindow.instance.mapBuilder.Rebuild();

        WindowUtils.PositionMapButtons();

        mapMaximised = false;
    }
}
