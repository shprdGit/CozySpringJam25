using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private InputAction movement;
    private InputAction takeoff;

    private Rigidbody2D rb;
    [SerializeField]
    private PlayerAnimation animationScript;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float takeoffForce;
    [SerializeField]
    private float takeoffTimer;
    private float takeoffCountdown;
    public bool isGrounded;
    private bool canMove;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = false;
        movement = InputSystem.actions.FindAction("Move");
        takeoff = InputSystem.actions.FindAction("Jump");
        canMove = true;
        takeoffCountdown = takeoffTimer;
    }

    void Update()
    {
        if (isGrounded)
        {
            if (takeoff.IsPressed())
            {
                animationScript.ChargeControl(true);
                takeoffCountdown -= Time.deltaTime;
                canMove = false;
                if (takeoffCountdown <= 0f)
                {
                    StartCoroutine(Takeoff());
                }
            }else
            {
                canMove = true;
                takeoffCountdown = takeoffTimer;
                animationScript.ChargeControl(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if(movement.ReadValue<Vector2>() != Vector2.zero)
            {
                if (isGrounded)
                {
                    Vector2 direction = ForceHorizontal();
                    if (direction.x != 0f)
                    {
                        animationScript.IdleControl(false);
                        HandleFacing();
                    }
                    rb.linearVelocity = new Vector2(direction.x * movementSpeed, rb.linearVelocity.y);
                }else
                {
                    animationScript.AccelerationControl(true);
                    Vector2 direction = movement.ReadValue<Vector2>().normalized;
                    rb.linearVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
                    HandleFacing();
                }
            }else
            {
                if (isGrounded) 
                {
                    rb.linearVelocity = Vector2.zero;
                }
                animationScript.IdleControl(true);
                animationScript.AccelerationControl(false);
            }
        }else if (!canMove && isGrounded)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private Vector2 ForceHorizontal()
    {
        Vector2 forced = movement.ReadValue<Vector2>();
        forced = new Vector2(forced.x, 0f).normalized;

        return forced;
    }

    private void HandleFacing()
    {
        Vector2 direction = ForceHorizontal();
        if (direction.x > 0f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }else if (direction.x < 0f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    private IEnumerator Takeoff()
    {
        rb.AddForceY(takeoffForce, ForceMode2D.Impulse);
        takeoffCountdown = takeoffTimer;
        animationScript.ChargeControl(false);
        isGrounded = false;
        yield return new WaitForSeconds(takeoffTimer/2);
        canMove = true;
    }
}