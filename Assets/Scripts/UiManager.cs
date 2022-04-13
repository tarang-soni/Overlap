using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class UiManager : MonoBehaviourPunCallbacks
{
    public static UiManager Instance = null;

    public GameObject levelCompleteObj;
    public GameObject lostConnectionObj;
    public GameObject pauseBtn;
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public GameObject nextLevelBtn;
    public Action LevelCompleteScreenEvent;
    public GameObject clientNextLevelText;
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
        if (PhotonNetwork.IsMasterClient)
        {
            nextLevelBtn.SetActive(true);
            clientNextLevelText.SetActive(false);
        }
        else
        {
            nextLevelBtn.SetActive(false);
            clientNextLevelText.SetActive(true);
        }
    }
    public void MainMenuBtn()
    {
        PhotonNetwork.LeaveRoom();
        GameManager.Instance.currentSceneNumber = 0;

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
    }
    public void LevelComplete()
    {
        levelCompleteObj.SetActive(true);
    }
    public void NextLevel()
    {
        if (GameManager.Instance.currentSceneNumber <= GameManager.Instance.totalLevels)
        {
            
            PhotonNetwork.LoadLevel(++GameManager.Instance.currentSceneNumber);
        }
        else
        {
            MainMenuBtn();
        }
    }

}
