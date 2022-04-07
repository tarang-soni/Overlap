using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomInfoPanel : MonoBehaviour
{
    public TMP_Text roomInfoPanelName;
    public TMP_Text capacity;
    public TMP_InputField password;
    public string roomName;
    public Button joinRoomBtn;
    private void OnEnable()
    {
        
        joinRoomBtn.onClick.AddListener(JoinRoom);
    }
    void JoinRoom()
    {
        Photon.Pun.PhotonNetwork.JoinRoom(roomName);
    }
    private void OnDisable()
    {
        joinRoomBtn.onClick.RemoveListener(JoinRoom);
    }
}
