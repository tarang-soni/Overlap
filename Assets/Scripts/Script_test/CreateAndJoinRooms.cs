using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);

    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room doesnt exists");
    }
    public override void OnJoinedRoom() // open loading screen, and show room screen
    {
        PanelManager.Instance.SwitchPanel(CanvasType.JoinRoom);
        createInput.text = "";
        joinInput.text = "";
    }
}
