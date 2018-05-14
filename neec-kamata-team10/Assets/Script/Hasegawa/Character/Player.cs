using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private EController eController;
    //[SerializeField]
    //private float maxSpeed;
    //[SerializeField]
    //private float acceleration;
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

    //private float speed = 0;
    //private float storeDirectionX= 0;
    private Vector3 direction = new Vector3(1, 0, 0);

    private ICharacterController controller;
    private EPlayerState state = EPlayerState.Jump;

    // Use this for initialization
    void Start()
    {
        controller = GameManager.Instance.GetController();
    }

    // Update is called once per frame
    void Update()
    {
        //FreezePosition();
        Climb();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
        Debug.Log(state);
    }

    //void Move()
    //{
    //    float directionX = controller.HorizontalMove().x;
    //    ChangeSpeed(directionX);
    //    ChangeDirection(directionX);
    //
    //    //Rigidbodyのvelocity.xだけを変更する
    //    Vector3 velocity= direction * speed;
    //    Vector3 temp = GetComponent<Rigidbody>().velocity;
    //    temp.x = velocity.x;
    //    GetComponent<Rigidbody>().velocity = temp;
    //    //GetComponent<Rigidbody>().AddForce(100 * (directionX - temp.x), 0, 0);
    //    //transform.position += direction * speed * Time.deltaTime;
    //}

    void Move()
    {
        Vector3 moveVector = Vector3.zero;
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (controller.HorizontalMove() != Vector3.zero) direction = controller.HorizontalMove();
        moveVector = moveSpeed * controller.HorizontalMove();

        Vector3 velocity = new Vector3(rigidbody.velocity.x, 0, 0);
        rigidbody.AddForce(moveForceMultiplier * (moveVector - velocity));
    }

    void Jump()
    {
        if (controller.Jump() && !isJump)
        {
            //Debug.Log("ジャンプ");
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
        FreezePosition();
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
        if (isJump || isClimb) 
        {
            //ジャンプ中はZとRotationを固定
            GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            //Debug.Log("Yを固定");
            //移動中はYとZとRotationを固定
            GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
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
        state = EPlayerState.Jump;
        SetIsClimb(false);
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