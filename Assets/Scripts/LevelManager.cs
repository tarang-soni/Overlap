using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LevelManager : MonoBehaviourPunCallbacks
{
    public static LevelManager Instance = null;
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

    public void CheckLevelStatus()
    {
        foreach (MultiplayerPlayerController player in GameManager.Instance.multiplayerPlayerControllers)
        {
            if (player.levelCompleted == false)
            {
                return;
            }
        }
        GameManager.Instance.masterPlayer.View.RPC("LevelCompleteCallback", RpcTarget.AllBuffered);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UiManager.Instance.lostConnectionObj.SetActive(true);
        UiManager.Instance.levelCompleteObj.SetActive(false);
        UiManager.Instance.pauseBtn.SetActive(false);
        UiManager.Instance.leftJoystick.gameObject.SetActive(false);
        UiManager.Instance.rightJoystick.gameObject.SetActive(false);
    }
}
