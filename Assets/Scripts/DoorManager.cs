using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance=null;
    public event Action<DoorType> DoorOpen;
    public AudioSource audioSrc;
    public AudioClip clip;
    private void Awake()
    {
        if (Instance !=null &&Instance !=this)
        {
            Destroy(gameObject);
        }
        else if (Instance==null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        audioSrc.volume = 0.5f;
    }
    public void Door_OpenMechanism(DoorType _type)
    {
        DoorOpen?.Invoke(_type);
    }
    
}
