using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateGO : MonoBehaviour
{
    public DoorType buttonType;
    public Character playerType;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<MultiplayerPlayerController>().charType==this.playerType)
        {
            DoorManager.Instance.Door_OpenMechanism(buttonType);
            gameObject.SetActive(false);
            DoorManager.Instance.audioSrc.PlayOneShot(DoorManager.Instance.clip);
        }
    }
}
