using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
public class RoomButton : MonoBehaviour
{
    RoomInfo info;
    public TMP_Text buttonText;

    public void SetButtonDetails(RoomInfo inputInfo)
    {
        info = inputInfo;
        buttonText.text = info.Name + " " + info.PlayerCount + "/" + info.MaxPlayers;
    }
    public void OpenRoom()
    {
        MultiplayerLauncher.Instance.JoinRoom(info);
    }
}
