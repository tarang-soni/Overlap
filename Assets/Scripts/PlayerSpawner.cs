using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform p1Spawn, p2Spawn;
    private GameObject player;
    void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }
    void SpawnPlayer()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name, p1Spawn.position, Quaternion.identity);
        GameManager.Instance.playerSpawned.Add(player.GetComponent<MultiplayerPlayerController>());
    }
}
