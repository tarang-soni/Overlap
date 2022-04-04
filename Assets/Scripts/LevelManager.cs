using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;
    public GameObject playerOneSpawn, playerTwoSpawn;
    public GameObject playerOne, playerTwo;
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
    private void Start()
    {
        SpawnPlayer(playerOne, playerOneSpawn.transform);
        SpawnPlayer(playerTwo, playerTwoSpawn.transform);
    }
    private void Update()
    {
        if (playerOne.GetComponent<PlayerMovement>().levelCompleted && playerTwo.GetComponent<PlayerMovement>().levelCompleted)
        {
            UiManager.Instance.LevelComplete();
        }
       
    }
    void SpawnPlayer(GameObject obj,Transform pos)
    {
        Debug.Log("Spawned");
        GameObject tempPlayer = Instantiate(obj, pos.position, pos.rotation);
        Debug.Log(tempPlayer);
    }
}
