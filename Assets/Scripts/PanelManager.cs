using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum CanvasType
{
    MainMenu,
    LevelSelect,
    Credits,
    Story,

    NameInput,
    MultiplayerMainMenu,
    JoinRoom,
    CreateRoom,
    RoomLobby,
    LoadingScreen,
    ErrorScreen
}
public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance = null;
    public List<PanelController> panels;
    public PanelController lastActivePanel;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        panels = GetComponentsInChildren<PanelController>().ToList();
        panels.ForEach(x => x.gameObject.SetActive(false));
        SwitchPanel(CanvasType.MainMenu);

    }
    public void SwitchPanel(CanvasType _type)
    {
        if (lastActivePanel != null)
        {

            lastActivePanel.gameObject.SetActive(false);
        }
        PanelController desiredCanvas = panels.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            foreach (PanelController item in panels)
            {
                if (item.canvasType == _type)
                {
                    desiredCanvas = item;
                }
            }

            desiredCanvas.gameObject.SetActive(true);

            lastActivePanel = desiredCanvas;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
