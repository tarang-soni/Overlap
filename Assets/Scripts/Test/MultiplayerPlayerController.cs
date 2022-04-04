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
public class MultiplayerPlayerController : MonoBehaviourPunCallbacks
{
    string HOR_STR;
    string VERT_STR;
    public float moveSpeed, rotSpeed;
    public bool levelCompleted = false;
    Vector2 dir;

    SpriteMask maskTorch;
    public Character charType;
    Rigidbody2D rb;
    [SerializeField]
    PhotonView view;
    public SpriteMask MaskTorch { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
        HOR_STR = "Horizontal";
        VERT_STR = "Vertical";

        if (view.IsMine)
        {
            maskTorch = GetComponentInChildren<SpriteMask>();
            rb = GetComponent<Rigidbody2D>();
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[i].NickName)
                {
                    //charType = GameManager.Instance.players[i].charType;
                    charType = GameManager.Instance.charType;
                    break;
                }
            }

            SelectCharType();
        }
    }
    void SelectCharType()
    {
        //if (view.AmOwner)
        //{
        //    for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //    {
        //        //GameManager.Instance.playerSpawned[i].charType = GameManager.Instance.players[i].charType;
        //        GameManager.Instance.playerSpawned[i].charType = GameManager.Instance.charType;
        //        if (charType == Character.Black)
        //        {
        //            transform.gameObject.layer = LayerMask.NameToLayer("Black");
        //            maskTorch.frontSortingLayerID = SortingLayer.NameToID("White");
        //        }
        //        else if (charType == Character.Twine)
        //        {
        //            transform.gameObject.layer = LayerMask.NameToLayer("White");
        //            maskTorch.frontSortingLayerID = SortingLayer.NameToID("Black");
        //        }
        //    }

        //}
        GameManager.Instance.SetupCharacterTypes(this);
     
    }
    // Update is called once per frame
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
            rb.velocity = transform.up * dir.y * moveSpeed * Time.fixedDeltaTime;

        }
    }
    void Move()
    {
        dir.x = Input.GetAxisRaw(HOR_STR);
        dir.y = Input.GetAxisRaw(VERT_STR);
        dir.Normalize();


        transform.Rotate(-Vector3.forward * dir.x, rotSpeed * Time.deltaTime);
    }
}
