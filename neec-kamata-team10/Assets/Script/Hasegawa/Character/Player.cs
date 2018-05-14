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
    private float moveSpeed;
    [SerializeField]
    private float moveForceMultiplier;

    private bool isJump = true;
    private bool isClimb= false;

    //private float speed = 0;
    //private float storeDirectionX= 0;
    private Vector3 direction = new Vector3(1, 0, 0);

    private ICharacterController controller;

    // Use this for initialization
    void Start()
    {
        controller = GameManager.Instance.GetController(eController);
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
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

        //direction = controller.HorizontalMove();
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

        if (isJump)
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

    void Climb()
    {
        if (isClimb)
        {
            //ツタを登っている時はZとRotationを固定
            GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;

            GetComponent<Rigidbody>().AddForce(controller.HorizontalMove() * jumpPower, ForceMode.Impulse);
        }
    }

    //void ChangeDirection(float directionX)
    //{
    //    if (directionX != 0) direction.x = directionX;
    //}
    //
    //void ChangeSpeed(float directionX)
    //{
    //    if (directionX != storeDirectionX && directionX != 0)  speed = 0;
    //    storeDirectionX = directionX;
    //
    //    if (directionX != 0) speed += acceleration;
    //    else speed -= acceleration;
    //
    //    speed = Mathf.Clamp(speed, 0, maxSpeed);
    //}

    public EDirection GetDirection()
    {
        if (direction.x < 0) return EDirection.LEFT;
        else return EDirection.RIGHT;
    }

    public void SetIsJump(bool isJump)
    {
        this.isJump = isJump;
    }

    public void SetIsHold(bool isClimb)
    {
        this.isClimb = isClimb;
    }
}