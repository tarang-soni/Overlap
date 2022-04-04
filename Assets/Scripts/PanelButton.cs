using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    public CanvasType canvasType;
    private PanelManager panelManager;
    Button btn;
    private void Awake()
    {

        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }
    private void Start()
    {
        panelManager = PanelManager.Instance;
    }
    void OnButtonClick()
    {
        panelManager.SwitchPanel(canvasType);
    }
}
