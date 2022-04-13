using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    public GameObject leftArrowBtn;
    public GameObject rightArrowBtn;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;
    Player player;
    public Image selectedImg;

    public override void OnEnable()
    {
        base.OnEnable();
        leftArrowBtn.GetComponent<Button>().onClick.AddListener(OnClickLeftArrow);
        rightArrowBtn.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);
        
    }
    private void Start()
    {
        playerProperties["playerAvatar"] = 0;
        playerProperties["canSelect"] = true;
        selectedImg.enabled = false;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    public void SetPlayerInfo(Player _player)
    {
        if (_player == PhotonNetwork.MasterClient)
        {
            playerName.text = _player.NickName + " (Host)";
        }
        else
        {
            playerName.text = _player.NickName;

        }
        player = _player;
        UpdatePlayerItem(player);
    }
    //public void SelectButton()
    //{
    //    playerProperties["canSelect"] =!(bool)playerProperties["canSelect"];
    //    rightArrowBtn.SetActive(!(bool)player.CustomProperties["canSelect"]);
    //    leftArrowBtn.SetActive(!(bool)player.CustomProperties["canSelect"]);
    //    PhotonNetwork.SetPlayerCustomProperties(playerProperties);

    //}
    public void ApplyLocalChanges()
    {
        leftArrowBtn.SetActive(true);
        rightArrowBtn.SetActive(true);
    }
    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
            OnPlayerListUpdate(targetPlayer);
        }
    }
    private void UpdatePlayerItem(Player targetPlayer)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];

            //selectBtn.gameObject.SetActive((bool)player.CustomProperties["canSelect"]);
            //leftArrowBtn.SetActive((bool)player.CustomProperties["canSelect"]);
            //rightArrowBtn.SetActive((bool)player.CustomProperties["canSelect"]);
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }
        if (player.CustomProperties.ContainsKey("canSelect"))
        {
            selectedImg.enabled = !(bool)player.CustomProperties["canSelect"];
        }
        else
        {
            playerProperties["canSelect"] = true;
        }
    }
    void OnPlayerListUpdate(Player thisPlayer)
    {
        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            if (thisPlayer !=_player.Value)
            {
                if (thisPlayer.CustomProperties.ContainsKey("playerAvatar") && _player.Value.CustomProperties.ContainsKey("playerAvatar"))
                {

                    if ((int)thisPlayer.CustomProperties["playerAvatar"] == (int)_player.Value.CustomProperties["playerAvatar"])
                    {
                        LobbyManager.Instance.CanStartGame = false;
                        //LobbyManager.Instance.startButton.gameObject.SetActive(false);

                    }
                    else
                    {
                        LobbyManager.Instance.CanStartGame = true;
                        //LobbyManager.Instance.startButton.gameObject.SetActive(true);

                    }
                }
            }
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        leftArrowBtn.GetComponent<Button>().onClick.RemoveListener(OnClickLeftArrow);
        rightArrowBtn.GetComponent<Button>().onClick.RemoveListener(OnClickRightArrow);
    }
}
