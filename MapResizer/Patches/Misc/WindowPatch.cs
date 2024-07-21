using HarmonyLib;
using UnityEngine;
using UI.Common;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Logger = MapResizer.Util.Logger;
using UI.Map;

namespace MapResizer.Patches.Misc;

[HarmonyPatch(typeof(Window))]
public class WindowPatch
{
    private const int PTR_THRESH = 10;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Window), nameof(Window.OnPointerDown))]
    private static void OnPointerDown(Window __instance, PointerEventData eventData)
    {
        //force Input to be refreshed
        //Keyboard.current.ctrlKey.ReadValue();

        /*if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl))
        {
            
            
        }*/

        Window win = MapWindow.instance._window;

        if (__instance.Equals(win))
        {
            Logger.LogDebug("MapWindow Click");
            if(eventData.button == 0) {
                Vector2 ptrPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(win._rectTransform, eventData.position, eventData.pressEventCamera, out ptrPos);
                Logger.LogDebug($"eventPos: {eventData.position} ptrPos: {ptrPos}");

                if(ptrPos.x > (win._rectTransform.right.x - PTR_THRESH) && ptrPos.x < (win._rectTransform.right.x + PTR_THRESH)) 
                {
                    Logger.LogDebug($"Right Edge!");
                }
            }
        }

    }

}
