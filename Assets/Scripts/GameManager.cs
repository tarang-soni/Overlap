
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    public int currentSceneNumber { get; set; }
    public List<MultiplayerPlayerController> multiplayerPlayerControllers = new List<MultiplayerPlayerController>();
    public MultiplayerPlayerController masterPlayer { get; set; }
    public int totalLevels { get; } = 15;
    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        currentSceneNumber = 0;
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(currentSceneNumber);
    }

    public class PlayerStats
    {
        public Character charType;
        public string name;
        public PlayerStats(Character _type,string _name)
        {
            charType = _type;
            name = _name;
        }
    }


}
