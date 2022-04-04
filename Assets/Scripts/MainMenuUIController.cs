using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    public static MainMenuUIController Instance = null;

    public TMP_Text loadingTxt;
    public TMP_Text roomNameText;
    public TMP_Text playerNameLabel;
    public TMP_Text errorTxt;

    public RoomButton theRoomButton;
    public TMP_Dropdown levelSelectDropDown;

    public GameObject roomStartGameButton;
    public TMP_InputField nameInput,roomNameInput;
    private bool hasSetNickname = false;
    public bool HasSetNickname
    {
        get
        {
            return hasSetNickname;
        }
        set
        {
            hasSetNickname = value;
        }
    }
    

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
    public void SetLoadingScreen(string value)
    {
        loadingTxt.text = value;
    }
}
