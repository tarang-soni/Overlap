using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    PlayerOne,
    PlayerTwo
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerType playerType;
    string HOR_STR;
    string VERT_STR;
    Vector2 dir;
    public float moveSpeed,rotSpeed;

    public bool levelCompleted = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        SelectMovementType(playerType);
        rb = GetComponent<Rigidbody2D>();
        switch (playerType)
        {
            case PlayerType.PlayerOne:
                LevelManager.Instance.playerOne = this.gameObject;
                break;
            case PlayerType.PlayerTwo:
                LevelManager.Instance.playerTwo = this.gameObject;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * dir.y * moveSpeed * Time.fixedDeltaTime;
    }
    void Move()
    {
        dir.x = Input.GetAxisRaw(HOR_STR);
        dir.y = Input.GetAxisRaw(VERT_STR);
        dir.Normalize();

        
        transform.Rotate(-Vector3.forward * dir.x, rotSpeed*Time.deltaTime);
    }
    void SelectMovementType(PlayerType player)
    {
        switch (player)
        {
            case PlayerType.PlayerOne:
                HOR_STR = "Horizontal";
                VERT_STR = "Vertical";
                break;
            case PlayerType.PlayerTwo:
                HOR_STR = "HorizontalOne";
                VERT_STR = "VerticalOne";
                break;
            default:
                break;
        }
    }
}
