using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SoundManager : MonoBehaviour
{
    private AudioSource bgMusic;
    public AudioSource buttonClick;
    public AudioClip buttonSound;

    public static SoundManager Instance = null;
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
        bgMusic = GetComponent<AudioSource>();
        bgMusic.Play();
        bgMusic.loop = true;
    }
    public void OnButtonClick()
    {
        buttonClick.PlayOneShot(buttonSound);
    }
}
