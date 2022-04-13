using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomItem : MonoBehaviourPunCallbacks
{
    public TMP_Text roomName;
    LobbyManager lobbyManager;
    public RoomInfo roomInfo;
    public Player masterClient;
    public string password;
    public ExitGames.Client.Photon.Hashtable _CustomProperty { get; set; }

    private void Start()
    {
        lobbyManager = LobbyManager.Instance;
        

    }
    public void SetRoomInfo(Player _player)
    {
        masterClient = _player;
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }


}
