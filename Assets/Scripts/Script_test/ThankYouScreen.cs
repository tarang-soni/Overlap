using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
public class ThankYouScreen : MonoBehaviourPunCallbacks
{

    public void MainMenuBtn()
    {
        PhotonNetwork.LeaveRoom();
        GameManager.Instance.currentSceneNumber = 0;
        
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(GameManager.Instance.currentSceneNumber);
    }
}
