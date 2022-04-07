using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public Joystick leftJoystick;
    public Joystick rightJoystick;
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
