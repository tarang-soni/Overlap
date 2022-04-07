using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character",menuName ="Character")]
public class CharacterDefinition : ScriptableObject
{
    public string CharacterName;
    public GameObject PlayerPrefab;

}
