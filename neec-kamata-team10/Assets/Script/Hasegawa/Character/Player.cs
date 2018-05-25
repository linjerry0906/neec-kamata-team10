using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private EController eController;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float climbSpeed;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float moveForceMultiplier;

    private bool isJump = true;
    private bool isClimb= false;

    private Vector3 direction = new Vector3(1, 0, 0);

    private ICharacterController controller;

    private EPlayerState state = EPlayerState.Jump;
    private EPlayerState stateStorage = EPlayerState.Move;

    private RigidbodyConstraints freezeY;
    private RigidbodyConstraints normal;

    // Use this for initialization
    void Start()
    {
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

    void Move()
    {
        if (controller.IsFade()) return;

        Vector3 moveVector = Vector3.zero;
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        ChangeDirection();
        //if (transform.localScale.x > 0 && direction.x < 0) transform.localScale.x *= direction.x;

        moveVector = moveSpeed * controller.HorizontalMove();

        Vector3 velocity = new Vector3(rigidbody.velocity.x, 0, 0);
        rigidbody.AddForce(moveForceMultiplier * (moveVector - velocity));
    }

    void Jump()
    {
        if (controller.Jump() && !isJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            state = EPlayerState.Jump;
        }
    }

    void Climb()
    {
        if (isClimb)
        {
            state = EPlayerState.Clamb;
            GetComponent<Rigidbody>().AddForce(controller.VerticalMove() * climbSpeed,ForceMode.Force);
        }
    }

    void FreezePosition()
    {
       if (GetComponent<Rigidbody>().velocity.y == 0)
       {
            //Debug.Log("Yを固定");
            //移動中はYとZとRotationを固定
            GetComponent<Rigidbody>().constraints = freezeY;
               
       }

        if (isJump || isClimb) 
        {
            //ジャンプ中はZとRotationを固定
            GetComponent<Rigidbody>().constraints = normal;
                
        }
    }

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

    void CheckState()
    {
        if (state == stateStorage) return;
        GetComponent<PlayerAnime>().ChangeState(state);
        stateStorage = state;
        //Debug.Log(state);
    }

    void CheckStay()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.x < 1 && rb.velocity.x > -1 && rb.velocity.y == 0) 
            state = EPlayerState.Stay;
    }

    public EDirection GetDirection()
    {
        if (direction.x < 0) return EDirection.LEFT;
        else return EDirection.RIGHT;
    }

    public EPlayerState GetPlayerState()
    {
        return state;
    }

    public void SetIsJump(bool isJump)
    {
        this.isJump = isJump;
    }

    public void SetIsClimb(bool isClimb)
    {
        this.isClimb = isClimb;
    }

    public void SetPlayerState(EPlayerState state)
    {
        this.state = state;
    }
}