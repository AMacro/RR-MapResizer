using UI.Builder;
using UI.Menu;
using UnityEngine;
using TMPro;
using Logger = MapResizer.Util.Logger;




namespace MapResizer.CustomMenu;

public class ModSettingsMenu : MonoBehaviour
{
    public GameObject contentPanel;
    public UIBuilderAssets assets;

    public int newGameDisplay;
    public int oldGameDisplay;
    public int newSecondDisplay;
    public int oldSecondDisplay;

    // Token: 0x06000AA5 RID: 2725 RVA: 0x00054B67 File Offset: 0x00052D67
    private void Awake()
    {
        TextMeshProUGUI title = GetComponentInChildren<TextMeshProUGUI>();
        title.text = "MapResizer";
  
        GameObject.DestroyImmediate(GameObject.Find("Settings Menu(Clone)/Content/Tab View(Clone)"));

        contentPanel = GameObject.Find("Settings Menu(Clone)/Content");

        assets = this.transform.GetComponent<SettingsMenu>().panelAssets;
    } 

    private void Start()
    {
        BuildPanelContent();
    }

    private void Update()
    {
        if(oldGameDisplay != newGameDisplay || oldSecondDisplay != newSecondDisplay)
        {
            Logger.LogDebug($"Update: {oldGameDisplay} {newGameDisplay} - {oldSecondDisplay} {newSecondDisplay}");

            oldGameDisplay = newGameDisplay;
            oldSecondDisplay = newSecondDisplay;

            Logger.LogDebug($"Post Update: {oldGameDisplay} {newGameDisplay} - {oldSecondDisplay} {newSecondDisplay}");

            GameObject.DestroyImmediate(GameObject.Find("Settings Menu(Clone)/Content/Tab View(Clone)"));
            BuildPanelContent();
        }
    }

    public void BuildPanelContent()
    {
        /*
        if (contentPanel != null)
        {
            UIPanel.Create(contentPanel.transform.GetComponent<RectTransform>(), assets, delegate (UIPanelBuilder builder)
            {
                Logger.LogTrace("PanelCreate");

                UIPanel.Create(contentPanel.transform.GetComponent<RectTransform>(), assets, delegate (UIPanelBuilder builder)
                {
                    builder.AddSection("Game Display");

                    List<DisplayInfo> displays = new();
                    Screen.GetDisplayLayout(displays);

                    List<int> values = displays.Select((DisplayInfo r, int i) => i).ToList();

                    builder.AddField("Display", builder.AddDropdownIntPicker(values, oldGameDisplay, (int i) => (i >= 0) ? $"Display: {i} ({displays[i].name})" : $"00", canWrite: true, delegate (int i)
                    {
                        Logger.LogDebug($"Selected Display: {i}");
                        if(i == oldSecondDisplay)
                        {
                            newSecondDisplay = oldGameDisplay;
                        }
                        newGameDisplay = i;

                    }));

                    builder.AddSection("Secondary Display");

                    builder.AddField("Display", builder.AddDropdownIntPicker(values, oldSecondDisplay, (int i) => (i >= 0) ? $"Display: {i} ({displays[i].name})" : $"00", canWrite: true, delegate (int i)
                    {
                        Logger.LogDebug($"Secondary Display: {i}");
                        if (i == 0)
                        {
                            oldSecondDisplay = -1;
                            return;
                        }
                            
                        if (i == oldGameDisplay)
                        {
                            newGameDisplay = oldSecondDisplay;
                        }
                        newSecondDisplay = i;
                        
                    }));
                    builder.AddLabel("Note: Display 0 can not be used for the Secondary Display due to a Unity Engine limitation.\r\nA work-around for this on Windows is to change your Primary display in the Windows Display settings.");

                    builder.Spacer().Height(20f);

                    builder.AddField("UI Scale", builder.AddSlider(() => MapResizer.settings.secondDisplayScale, () => string.Format("{0}%", Mathf.Round(MapResizer.settings.secondDisplayScale * 100f)),
                   delegate (float f)
                   {
                       MapResizer.settings.secondDisplayScale = f;
                       WindowUtils.UpdateScale();
                   },
                   0.2f, 2f, false));

                    builder.AddExpandingVerticalSpacer();

                });

                builder.Spacer(16f);
                builder.HStack(delegate (UIPanelBuilder builder)
                {
                    builder.AddButton("Back", delegate
                    {
                        MenuManagerPatch._MMinstance.navigationController.Pop();

                    });

                    builder.Spacer().FlexibleWidth(1f);

                    builder.AddButton("Apply", delegate
                    {
                        List<DisplayInfo> displays = new();
                        Screen.GetDisplayLayout(displays);

                        if (MapResizer.gameDisplay != newGameDisplay)
                        {
                            MapResizer.gameDisplay = newGameDisplay;
                            MapResizer.settings.gameDisplay = newGameDisplay;

                            Screen.MoveMainWindowTo(displays[MapResizer.gameDisplay], new Vector2Int(0, 0));
                        }

                        if (MapResizer.secondDisplay != newSecondDisplay)
                        {
                            MapResizer.secondDisplay = newSecondDisplay;
                            MapResizer.settings.secondDisplay = newSecondDisplay;

                            ShowRestart();
                        }

                    });
                });
            });
        */
        }
    }
