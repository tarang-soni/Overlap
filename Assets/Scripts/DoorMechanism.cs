using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum DoorType
{
    Star1,
    Plus1,
    Hex1,
    Triangle1,
    Star2,
    Plus2,
    Hex2,
    Triangle2,

}
public class DoorMechanism : MonoBehaviour
{
    public DoorType doorType;
    public bool isExitDoor;
    private void Start()
    {
        Debug.Log(DoorManager.Instance);
        DoorManager.Instance.DoorOpen += OpenDoor;
    }


    void OpenDoor(DoorType id)
    {
        if (id == doorType)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnDisable()
    {
        DoorManager.Instance.DoorOpen -= OpenDoor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExitDoor)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("triggered");
                collision.GetComponent<PlayerMovement>().levelCompleted = true;
                Debug.Log("triggered"+ collision.GetComponent<PlayerMovement>().levelCompleted);
            }

        }
    }
}
