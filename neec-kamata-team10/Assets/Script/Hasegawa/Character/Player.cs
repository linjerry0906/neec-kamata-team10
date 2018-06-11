using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField]
    //private EController eController;                            
    [SerializeField]
    private float jumpPower;                                      //ジャンプ力
    [SerializeField]
    private float climbSpeed;                                     //登るスピード
    [SerializeField]
    private float moveSpeed;                                      //移動スピード
    [SerializeField]
    private float moveForceMultiplier;                            //慣性の調整値

    private bool isJump = true;                                   //ジャンプフラグ
    private bool isClimb = false;                                  //ツタ登りフラグ
    private Vector3 direction = new Vector3(1, 0, 0);             //進行方向
    private ICharacterController controller;                      //コントローラー
    private EPlayerState state = EPlayerState.Jump;               //プレイヤーの状態
    private EPlayerState stateStorage = EPlayerState.Move;        //プレイヤーの状態(保存用)
    private RigidbodyConstraints freezeY;                         //Y座標の固定
    private RigidbodyConstraints normal;                          //座標の固定

    // Use this for initialization
    void Start()
    {
        Vector3 startPos = GameManager.Instance.GetStageManager().StartPos();
        if (startPos != Vector3.zero) transform.position = startPos;

        controller = GameManager.Instance.GetController();

        freezeY = RigidbodyConstraints.FreezePositionZ |
               RigidbodyConstraints.FreezePositionY |
               RigidbodyConstraints.FreezeRotationX |
               RigidbodyConstraints.FreezeRotationY |
               RigidbodyConstraints.FreezeRotationZ;

        normal = RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        Climb();
        Jump();
        Move();
        CheckStay();
        FreezePosition();
        CheckState();
    }

    void FixedUpdate()
    {
    }

    //移動
    void Move()
    {
        if (controller.IsFade()) return;

        Vector3 moveVector = Vector3.zero;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        ChangeDirection();
        moveVector = moveSpeed * controller.HorizontalMove();
        Vector3 velocity = new Vector3(rigidbody.velocity.x, 0, 0);
        rigidbody.AddForce(moveForceMultiplier * (moveVector - velocity));
    }

    //ジャンプ
    void Jump()
    {
        if (controller.Jump() && !isJump)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            state = EPlayerState.Jump;
        }
    }

    //登る(ツタブロック)
    void Climb()
    {
        if (isClimb)
        {
            state = EPlayerState.Clamb;
            GetComponent<Rigidbody>().AddForce(controller.VerticalMove() * climbSpeed, ForceMode.Force);
        }
    }

    //ポジションの固定
    void FreezePosition()
    {
        //ジャンプ中と登り中はZとRotationを固定
        if (isJump || isClimb)
        {
            GetComponent<Rigidbody>().constraints = normal;
        }
        //移動中はYとZとRotationを固定
        else //if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            GetComponent<Rigidbody>().constraints = freezeY;

        }

        //移動中はYとZとRotationを固定
        //else
        //{
        //    GetComponent<Rigidbody>().constraints = normal;
        //    //GetComponent<Rigidbody>().constraints = freezeY;
        //}
    }

    //ツタブロックとの判定
    void OnCollisionStay(Collision c)
    {
        if (!c.gameObject.tag.Contains("ivy")) return;

        if (c.gameObject.tag.Equals("ivy_upSideCollider"))
        {
            //Debug.Log("ツタの上");
            state = EPlayerState.Move;
            SetIsClimb(false);
        }
        else
        {
            //Debug.Log("ツタ登り中");
            SetIsClimb(true);
        }
    }

    //ツタブロックとの判定
    void OnCollisionExit(Collision c)
    {
        //if (c.gameObject.tag.Equals("stage_ivy"))
        //{
        //    state = EPlayerState.Jump;
        //    SetIsClimb(false);
        //}
        //state = EPlayerState.Jump;

        SetIsClimb(false);
    }

    //進行方向の保存と方向転換
    void ChangeDirection()
    {
        //進んでいた方向の保存
        if (controller.HorizontalMove() != Vector3.zero) direction = controller.HorizontalMove();

        //transform.xとdirection.xを比べて方向が同じだったらreturnする
        if ((transform.localScale.x > 0 && direction.x > 0) ||
            (transform.localScale.x < 0 && direction.x < 0)) return;

        //returnされなかったら方向転換する
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //stateが変更されたときにChangeStateを呼ぶ
    void CheckState()
    {
        if (state == stateStorage) return;
        GetComponent<PlayerAnime>().ChangeState(state);
        stateStorage = state;
        //Debug.Log(state);
    }

    //移動量が小さければ待機状態にする
    void CheckStay()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.x < 1 && rb.velocity.x > -1 && rb.velocity.y == 0)
            state = EPlayerState.Stay;
        //Debug.Log(state);
        //Debug.Log(rb.velocity.y);
        //Debug.Log(isJump);
    }

    //進行方向の取得
    public EDirection GetDirection()
    {
        if (direction.x < 0) return EDirection.LEFT;
        else return EDirection.RIGHT;
    }

    //状態の取得
    public EPlayerState GetPlayerState()
    {
        return state;
    }

    //Jumpフラグの設定
    public void SetIsJump(bool isJump)
    {
        this.isJump = isJump;
    }

    //Climbフラグの設定
    public void SetIsClimb(bool isClimb)
    {
        this.isClimb = isClimb;
    }

    //状態の設定
    public void SetPlayerState(EPlayerState state)
    {
        this.state = state;
    }
}