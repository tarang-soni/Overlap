using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonObject : MonoBehaviour
{
    private Button btn;
    void OnEnable()
    {
        btn = GetComponent<Button>();
        AssignButtonSound();
        
    }
    private void Start()
    {
        AssignButtonSound();

    }

    void AssignButtonSound()
    {
        if (SoundManager.Instance != null)
        {
            btn.onClick.AddListener(SoundManager.Instance.OnButtonClick);
        }
    }
    void RemoveButtonSound()
    {
        if (SoundManager.Instance != null)
        {
            btn.onClick.RemoveListener(SoundManager.Instance.OnButtonClick);
        }
    }
    private void OnDisable()
    {
        RemoveButtonSound();
    }
}
