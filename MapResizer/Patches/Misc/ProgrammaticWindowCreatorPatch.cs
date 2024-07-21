using HarmonyLib;
using UI.Builder;
using UnityEngine;
using UI;
using UI.Common;
using System;

using Logger = MapResizer.Util.Logger;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using MapResizer.Util;



namespace MapResizer.harmonyPatches
{


    [HarmonyPatch(typeof(ProgrammaticWindowCreator))]
    public static class ProgrammaticWindowCreatorPatch
    {
        public static UIBuilderAssets builderAssets;
        static Window windowPrefab;
        static Transform transform;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ProgrammaticWindowCreator), "Start")]
        public static void Start(ProgrammaticWindowCreator __instance)
        {
            Logger.LogTrace($"ProgrammaticWindowCreator.Start()");
            builderAssets = __instance.builderAssets;
            windowPrefab = __instance.windowPrefab;
            transform = __instance.transform;

            //Create Our Fullscreen Button
            GameObject close = GameObject.Find("Map Window/Chrome/Title Bar/Close Button");
            GameObject tb = GameObject.Find("Map Window/Chrome/Title Bar");

            if (close != null && tb != null)
            {
                Logger.LogVerbose($"Close: {close.name}");

                GameObject go = new GameObject("Fullscreen");
                RectTransform rtFullScr = go.AddComponent<RectTransform>();

                go.transform.SetParent(tb.transform, false);
                go.transform.SetSiblingIndex(close.transform.GetSiblingIndex());

                UIPanel.Create(rtFullScr, ProgrammaticWindowCreatorPatch.builderAssets, delegate (UIPanelBuilder builder) {
                    builder.HStack(delegate (UIPanelBuilder builder)
                    {
                        builder.AddButtonCompact("R", delegate ()
                        {
                            Logger.LogVerbose("Resize Clicked");
                            MapResizer.ResetClick();
                            builder.Rebuild();
                            WindowUtils.PositionMapButtons();
                        });

                        builder.AddButtonCompact(MapResizer.mapMaximised ? "M" : "F", delegate ()
                        {
                            Logger.LogVerbose("Fullscreen Clicked");
                            MapResizer.FullscreenClick();
                            builder.Rebuild();
                            WindowUtils.PositionMapButtons();
                        });
                    });
                });

                WindowUtils.PositionMapButtons();
            }
            else
            {
                Logger.LogVerbose($"Unable to Locate Objects {close?.name} OR TB {tb?.name}");
            }
        }



        private static void CreateWindow<TWindow>(int width, int height, Window.Position position, Action<TWindow> configure = null) where TWindow : Component, IBuilderWindow
        {
            Window window = CreateWindow(width, height, position);
            window.name = typeof(TWindow).ToString();
            TWindow twindow = window.gameObject.AddComponent<TWindow>();
            twindow.BuilderAssets = builderAssets;
            window.CloseWindow();
            if (configure != null)
            {
                configure(twindow);
            }
        }

        // Token: 0x06000929 RID: 2345 RVA: 0x0004BFD1 File Offset: 0x0004A1D1
        private static Window CreateWindow(int width, int height, Window.Position position)
        {
            Window window = UnityEngine.Object.Instantiate<Window>(windowPrefab, transform, false);
            window.SetContentSize(new Vector2((float)width, (float)height));
            window.SetPosition(position);
            return window;
        }
    }
}