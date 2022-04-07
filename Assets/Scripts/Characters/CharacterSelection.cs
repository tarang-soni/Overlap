using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
public class CharacterSelection : MonoBehaviourPunCallbacks
{
    [SerializeField] private CharacterDefinition[] _characters;
    [SerializeField] private GameObject _characterSelectPanel;

    [SerializeField] private CharacterPanel _panelPrefab;
    private List<CharacterPanel> _panels = new List<CharacterPanel>();
    private CharacterPanel _currentCharacterPanel = default;
    private int _currentCharacterIndex = 0;
    public GameObject _parentPanelObject;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    Player player;
    private void Start()
    {
        CharacterSelectLoadPanels();
    }
    void CharacterSelectLoadPanels()
    {
        if (_parentPanelObject.transform.childCount ==0)
        {

            foreach (var character in _characters) {

                CharacterPanel newPanel = Instantiate(_panelPrefab, _panelPrefab.transform.parent);
                newPanel.CharacterName.text = character.name;
                newPanel.gameObject.SetActive(false);
                newPanel.name = "Panel_" + character.name;
                _panels.Add(newPanel);
            }
        _currentCharacterPanel = _panels[_currentCharacterIndex];
        _currentCharacterPanel.gameObject.SetActive(true);
        }
    }
    
    public void Select()
    {

    }
    public void LeftButton()
    {
        if ((int)playerProperties["character"] == 0)
        {
            playerProperties["character"] = _characters.Length - 1;
        }
        else
        {
            playerProperties["character"] = (int)playerProperties["character"]-1;
        }

        _panels[_currentCharacterIndex].gameObject.SetActive(false);

        _currentCharacterIndex--;
        if (_currentCharacterIndex<0)
        {
            _currentCharacterIndex += _panels.Count;
        }

        _panels[_currentCharacterIndex].gameObject.SetActive(true);
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    public void RightButton()
    {
        if ((int)playerProperties["character"] == _characters.Length - 1)
        {
            playerProperties["character"] = (int)playerProperties["character"]+1;
        }
        else
        {
            playerProperties["character"] = (int)playerProperties["character"] - 1;
        }

        _panels[_currentCharacterIndex].gameObject.SetActive(false);

        _currentCharacterIndex=(_currentCharacterIndex+1)%_panels.Count;

        _panels[_currentCharacterIndex].gameObject.SetActive(true);
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);

    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }
    public void UpdatePlayerList()
    {
        foreach(CharacterPanel item in _panels)
        {
            Destroy(item.gameObject);
        }
        _panels.Clear();

        if (PhotonNetwork.CurrentRoom==null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            CharacterPanel newPanel = Instantiate(_panelPrefab, _panelPrefab.transform.parent);
            _panels.Add(newPanel);
        }

    }
}
