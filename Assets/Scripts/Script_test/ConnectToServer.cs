using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMP_Text buttonText;
    private PanelManager panelManager;

    public TMP_Text loadingText;
    public GameObject enterBtn;

    private void Start()
    {
        panelManager = PanelManager.Instance;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        enterBtn.SetActive(false);
        loadingText.gameObject.SetActive(true);
        loadingText.text = "Connecting to Server...";
    }

    public void OnClickConnect()
    {
        if (usernameInput.text.Length >= 1)
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.ConnectUsingSettings();
            }
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting to Lobby...";
            PhotonNetwork.JoinLobby();


        }
    }
    public override void OnConnectedToMaster()
    {

        enterBtn.SetActive(true);
        loadingText.gameObject.SetActive(false);
       


    }
    public override void OnJoinedLobby()
    {
        panelManager.SwitchPanel(CanvasType.RoomLobby);
        buttonText.text = "Connect";
    }
}
