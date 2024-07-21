using UI.Common;
using UnityEngine;

namespace MapResizer.Util
{
    public static class WindowUtils
    {
        public static void PositionMapButtons()
        {
            GameObject close = GameObject.Find("Map Window/Chrome/Title Bar/Close Button");
            GameObject go = GameObject.Find("Fullscreen");

            RectTransform rtFullScr = go.GetComponent<RectTransform>();

            RectTransform rtClose = close.GetComponentInChildren<RectTransform>();
            float xPos = rtClose.localPosition.x + rtFullScr.rect.x;

            rtFullScr.SetLocalPositionAndRotation(new Vector3(xPos, -1.5f), new Quaternion());
            rtFullScr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0.1f);
        }

    }
}
