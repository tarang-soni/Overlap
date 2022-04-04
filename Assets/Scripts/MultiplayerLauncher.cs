using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MultiplayerLauncher : MonoBehaviourPunCallbacks
{
    public static MultiplayerLauncher Instance = null;
    MainMenuUIController controller;
    PanelManager panelManager;

    private List<TMP_Text> allPlayerNames = new List<TMP_Text>();
    private List<RoomButton> allRoomButtons = new List<RoomButton>();
    public bool allPlayersSelected = false;
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
    void Start()
    {
        controller = MainMenuUIController.Instance;
        panelManager = PanelManager.Instance;

        
    }
    public void MultiplayerButton()
    {
        panelManager.SwitchPanel(CanvasType.LoadingScreen);
        controller.SetLoadingScreen("Connecting To Network...");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        controller.SetLoadingScreen("Joining Lobby...");
    }
    public override void OnJoinedLobby()
    {
        panelManager.SwitchPanel(CanvasType.NameInput);
        if (!controller.HasSetNickname)
        {
            if (PlayerPrefs.HasKey("playerName"))
            {
                controller.nameInput.text = PlayerPrefs.GetString("playerName");
            }
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }
    }
    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(controller.roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;

            PhotonNetwork.CreateRoom(controller.roomNameInput.text, options);
            controller.SetLoadingScreen("Creating Room");
            panelManager.SwitchPanel(CanvasType.LoadingScreen);
        }
    }
    public override void OnJoinedRoom()
    {
        panelManager.SwitchPanel(CanvasType.RoomLobby);
        controller.roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        ListAllPlayers();
        
        if (PhotonNetwork.IsMasterClient)
        {
            controller.roomStartGameButton.SetActive(true);
        }
        else
        {
            controller.roomStartGameButton.SetActive(false);
        }
    }
    private void ListAllPlayers()
    {
        foreach (TMP_Text player in allPlayerNames)
        {
            Destroy(player.gameObject);
        }
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            TMP_Text newPlayerLabel = Instantiate(controller.playerNameLabel, controller.playerNameLabel.transform.parent);
            newPlayerLabel.text = players[i].NickName/* + " (" + GameManager.Instance.players[i].name + ")"*/;
            newPlayerLabel.gameObject.SetActive(true);

            allPlayerNames.Add(newPlayerLabel);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            controller.roomStartGameButton.SetActive(true);
        }
        else
        {
            controller.roomStartGameButton.SetActive(false);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        TMP_Text newPlayerLabel = Instantiate(controller.playerNameLabel, controller.playerNameLabel.transform.parent);
        newPlayerLabel.text = newPlayer.NickName;
        newPlayerLabel.gameObject.SetActive(true);
        allPlayerNames.Add(newPlayerLabel);
        ListAllPlayers();
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListAllPlayers();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        controller.errorTxt.text = "Failed To Create Room: " + returnCode;
        panelManager.SwitchPanel(CanvasType.ErrorScreen);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        controller.SetLoadingScreen("Leaving Room...");
        panelManager.SwitchPanel(CanvasType.LoadingScreen);
    }
    public override void OnLeftRoom()
    {
        panelManager.SwitchPanel(CanvasType.MultiplayerMainMenu);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach (RoomButton button in allRoomButtons)
        {
            Destroy(button.gameObject);
        }
        allRoomButtons.Clear();
        controller.theRoomButton.gameObject.SetActive(false);

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount!=roomList[i].MaxPlayers&&!roomList[i].RemovedFromList)
            {
                RoomButton newBtn = Instantiate(controller.theRoomButton, controller.theRoomButton.transform.parent);
                newBtn.SetButtonDetails(roomList[i]);
                newBtn.gameObject.SetActive(true);

                allRoomButtons.Add(newBtn);
            }
        }
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        controller.SetLoadingScreen("Joining Room...");
        panelManager.SwitchPanel(CanvasType.LoadingScreen);
    }
    //public void SwapCharacters()
    //{
    //    GameManager.Instance.SwapCharacterType();
    //    UpdatePlayerList();
    //    Debug.Log(GameManager.Instance.players[0].name);
    //}
    //void UpdatePlayerList()
    //{ 
    //    for (int i = 0; i < allPlayerNames.Count; i++)
    //    { 
    //        allPlayerNames[i].text = PhotonNetwork.PlayerList[i].NickName + " (" + GameManager.Instance.players[i].name + ")";
    //    }

    //}
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(controller.levelSelectDropDown.value+1);
    }
    public void SetNickname()
    {
        if (!string.IsNullOrEmpty(controller.nameInput.text))
        {
            PhotonNetwork.NickName = controller.nameInput.text;
            PlayerPrefs.SetString("playerName", controller.nameInput.text);

            panelManager.SwitchPanel(CanvasType.MultiplayerMainMenu);
            controller.HasSetNickname = true;

        }
    }
    
}
