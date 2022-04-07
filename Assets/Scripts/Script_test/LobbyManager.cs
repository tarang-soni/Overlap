using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance = null;

    public TMP_Text roomName;

    PanelManager panelManager;

    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject startButton;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("destroyed");
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        panelManager = PanelManager.Instance;
    }
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers )
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }


    public void CreateRoom()
    {
        if (createInput.text.Length>=1)
        {
            PhotonNetwork.CreateRoom(createInput.text, new RoomOptions() { MaxPlayers = 2, BroadcastPropsChangeToAll = true })   ;

        }
    }
    public void JoinRoom()
    {
        if (joinInput.text.Length>=1)
        {
            PhotonNetwork.JoinRoom(joinInput.text);

        }

    }
    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom==null)
        {
            return;
        }
        foreach(KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }
            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room doesnt exists");
    }
    public override void OnJoinedRoom() // open loading screen, and show room screen
    {
        PanelManager.Instance.SwitchPanel(CanvasType.JoinRoom);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        createInput.text = "";
        joinInput.text = "";
        UpdatePlayerList();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void OnClickLeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
    public override void OnLeftRoom()
    {
        panelManager.SwitchPanel(CanvasType.RoomLobby);

    }
    public override void OnDisconnected(DisconnectCause cause)
    {

        panelManager.SwitchPanel(CanvasType.MainMenu);
    }
    public override void OnLeftLobby()
    {

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("disconnected");
        }

    }
    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel(1);
    }
    

}
