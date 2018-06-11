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
    private float moveSpeed;                                      //移動スピード
    [SerializeField]
    private float moveForceMultiplier;                            //慣性の調整値

    private bool isJump = true;                                   //ジャンプフラグ
    private bool isMountSeesaw = false;
    private Vector3 direction = new Vector3(1, 0, 0);             //進行方向
    private ICharacterController controller;                      //コントローラー
    private EPlayerState state = EPlayerState.Jump;               //プレイヤーの状態
    private EPlayerState stateStorage = EPlayerState.Move;        //プレイヤーの状態(保存用)
    private RigidbodyConstraints freezeY;                         //Y座標の固定
    private RigidbodyConstraints normal;                          //座標の固定
    private AudioSource audio;

    // Use this for initialization
    void Start()
    {
        Vector3 startPos = GameManager.Instance.GetStageManager().StartPos();
        if (startPos != Vector3.zero) transform.position = startPos;

        controller = GameManager.Instance.GetController();
        audio = GetComponent<AudioSource>();

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
        if (GetComponent<AliveFlag>().IsDead()) return;
        Jump();
        Move();
        CheckStay();
        FreezePosition();
        CheckState();
        Debug.Log(isMountSeesaw);
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
            audio.clip = GetComponent<SEManager>().GetSE(0);
            audio.Play();

            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            state = EPlayerState.Jump;
        }
    }

    //ポジションの固定
    void FreezePosition()
    {
        //ジャンプ中はZとRotationを固定
        if (isJump || isMountSeesaw)
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

    //Jumpフラグの設定
    public void SetIsMountSeesaw(bool isMountSeesaw)
    {
        this.isMountSeesaw = isMountSeesaw;
    }

    //状態の設定
    public void SetPlayerState(EPlayerState state)
    {
        this.state = state;
    }
}