using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private EController eController;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float acceleration;

    private float speed = 0;
    private float storeDirectionX= 0;
    private Vector3 direction = new Vector3(1, 0, 0);

    private ICharacterController controller;

    // Use this for initialization
    void Start()
    {
        controller = gameManager.GetComponent<GameManager>().GetController(eController);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float directionX = controller.HorizontalMove().x;
        ChangeSpeed(directionX);
        ChangeDirection(directionX);
        transform.position += direction * speed * Time.deltaTime;
    }

    void ChangeDirection(float directionX)
    {
        if (directionX != 0) direction.x = directionX;
    }

    void ChangeSpeed(float directionX)
    {
        if (directionX != storeDirectionX && directionX != 0)  speed = 0;
        storeDirectionX = directionX;

        if (directionX != 0) speed += acceleration;
        else speed -= acceleration;

        speed = Mathf.Clamp(speed, 0, maxSpeed);
    }

    public EDirection GetDirection()
    {
        if (direction.x < 0) return EDirection.LEFT;
        else return EDirection.RIGHT;
    }
}
