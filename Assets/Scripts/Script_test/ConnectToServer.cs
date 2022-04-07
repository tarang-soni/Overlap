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

    private void Start()
    {
        panelManager = PanelManager.Instance;
    }

    public void OnClickConnect()
    {
        if (usernameInput.text.Length>=1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        buttonText.text = "Connecting to Lobby...";
        PhotonNetwork.JoinLobby();
            

    }
    public override void OnJoinedLobby()
    {
        panelManager.SwitchPanel(CanvasType.RoomLobby);
        buttonText.text = "Connect";
    }
}
