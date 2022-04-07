using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public enum Character
{
    Twine,
    Black
};
public class MultiplayerPlayerController : MonoBehaviour
{
    Joystick left_joystick;
    Joystick right_joystick;
    string HOR_STR;
    string VERT_STR;
    public float moveSpeed, rotSpeed;
    public bool levelCompleted = false;
    Vector2 dir;

    public Character charType;
    Rigidbody2D rb;
    [SerializeField]
    PhotonView view;
    public float angle;
    public SpriteMask MaskTorch { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
        HOR_STR = "Horizontal";
        VERT_STR = "Vertical";

        if (view.IsMine)
        {
            rb = GetComponent<Rigidbody2D>();
            left_joystick = GameManager.Instance.leftJoystick;
            right_joystick = GameManager.Instance.rightJoystick;
        }

    }


    void Update()
    {
        if (view.IsMine)
        {
            Move();
        }
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            rb.velocity =  dir * moveSpeed * Time.fixedDeltaTime;

        }
    }
    void Move()
    {

        dir.x = left_joystick.Horizontal;
        dir.y = left_joystick.Vertical ;

        Vector2 rightDir;
        rightDir.x = right_joystick.Horizontal;
        rightDir.y = right_joystick.Vertical;
        if (rightDir == Vector2.zero )
        {
            return;
        }
        Debug.Log(right_joystick.Horizontal);
        angle = Mathf.Atan2(right_joystick.Vertical,right_joystick.Horizontal)*Mathf.Rad2Deg;
        Quaternion lookRot = Quaternion.Euler((angle-90f) * Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation,lookRot,Time.deltaTime*10f);
      
    }
}
