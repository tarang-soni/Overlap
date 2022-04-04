using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    //public PlayerStats[] players;
    public List<MultiplayerPlayerController> playerSpawned = new List<MultiplayerPlayerController>();
    public Character charType;

    //PlayerStats black, twine; //game character names
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
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //black = new PlayerStats(Character.Black, "Black");
        //twine = new PlayerStats(Character.Twine, "Twine");

        //SetupCharacterTypes();
    }
    public void SetupCharacterTypes(MultiplayerPlayerController player)
    {
        //players = new PlayerStats[2] {black,twine};
        //players.Add(black);
        //players.Add(twine);
        switch (charType)
        {
            case Character.Twine:
                player.transform.gameObject.layer = LayerMask.NameToLayer("Black");
                player.MaskTorch.frontSortingLayerID = SortingLayer.NameToID("White");
                break;
            case Character.Black:
                player.transform.gameObject.layer = LayerMask.NameToLayer("White");
                player.MaskTorch.frontSortingLayerID = SortingLayer.NameToID("Black");
                break;
            default:
                break;
        }
    }
    //public void SwapCharacterType()
    //{
    //    PlayerStats temp = players[0];
    //    players[0] = players[1];
    //    players[1] = temp;
    //}
    [Serializable]
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
