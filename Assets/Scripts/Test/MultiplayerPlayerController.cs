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

    public float moveSpeed, rotSpeed;
    public bool levelCompleted = false;
    Vector2 dir;

    public Character charType;
    Rigidbody2D rb;
    [SerializeField]
    PhotonView view;

    public PhotonView View { get { return view; } }
    public float angle;
    private void OnEnable()
    {
    }
    void Start()
    {


        if (view.IsMine)
        {
            rb = GetComponent<Rigidbody2D>();
            left_joystick = UiManager.Instance.leftJoystick;
            right_joystick = UiManager.Instance.rightJoystick;
        }
        GameManager.Instance.multiplayerPlayerControllers.Add(this);
        if (PhotonNetwork.IsMasterClient)
        {
            view.RPC("SetMasterClient", RpcTarget.AllBuffered);
        }

    }
    [PunRPC]
    void SetMasterClient()
    {
        GameManager.Instance.masterPlayer = this;
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
    void Move()     // Move and rotate the player according to the joystick value
    {
        dir.x = left_joystick.Horizontal;
        dir.y = left_joystick.Vertical ;

        Vector2 rightDir;
        rightDir.x = right_joystick.Horizontal;         // Assign joystick values to a temporary variable
        rightDir.y = right_joystick.Vertical;

        if (rightDir == Vector2.zero )
            return;
        
        angle = Mathf.Atan2(right_joystick.Vertical,right_joystick.Horizontal)*Mathf.Rad2Deg;  //get the look rotation angle
        Quaternion lookRot = Quaternion.Euler((angle-90f) * Vector3.forward);   //assign the angle in the quaternion variable
        transform.rotation = Quaternion.Lerp(transform.rotation,lookRot,Time.deltaTime*10f); // lerp is used for smooth rotation
    }
    [PunRPC]
    void LevelCompleteCallback()
    {
        UiManager.Instance.LevelComplete();
    }
    private void OnDisable()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.multiplayerPlayerControllers.Clear();
        }
    }
}
