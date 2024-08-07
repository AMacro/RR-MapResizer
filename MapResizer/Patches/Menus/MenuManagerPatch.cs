﻿using HarmonyLib;
using UnityEngine.UI;
using UI.Menu;
using TMPro;
using MapResizer.CustomMenu;
using MapResizer.Util;

namespace MapResizer.Patches.Menus;

[HarmonyPatch(typeof(MainMenu))]
public static class MenuManagerPatch
{
    public static MainMenu _instance;
    public static MenuManager _MMinstance;
    public static SettingsMenu ModMenu;
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Awake))]
    private static void Awake(MainMenu __instance)
    {
        Logger.LogVerbose("MainMenu.Start()");

        _instance = __instance;

        _instance.AddButton("MapResizer Mod",onClick);

        Button[] buttons = _instance.GetComponentsInChildren<Button>();

        int insertindex = 0;
        Button modSettings = null;
        foreach (Button button in buttons)
        {
            if(button.GetComponentInChildren<TMP_Text>()?.text == "Settings" )
                insertindex= button.transform.GetSiblingIndex() + 1;

            if (button.GetComponentInChildren<TMP_Text>()?.text == "MapResizer Mod")
                modSettings = button;
        }

        if (modSettings != null)
        {
            modSettings.transform.SetSiblingIndex(insertindex);
        }
  

        //MapResizer.Log($"My Index: {modSettings.transform.GetSiblingIndex()}");
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(MenuManager), nameof(MenuManager.Start))]
    private static void Start(MenuManager __instance)
    {
        Logger.LogTrace("MenuManager.Start()");

        _MMinstance = __instance;

    }

    private static void onClick()
    {
        if (_MMinstance != null)
        {
            ModMenu = UnityEngine.Object.Instantiate<SettingsMenu>(_MMinstance.settingsMenu);
            ModMenu.transform.gameObject.AddComponent<ModSettingsMenu>();
            _MMinstance.navigationController.Push(ModMenu);
           
        }
    }
}

