using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private InputAction movement;
    private InputAction takeoff;
    private InputAction lookAround;

    private Rigidbody2D rb;
    [SerializeField]
    private PlayerAnimation animationScript;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float flightMultiplier;
    [SerializeField]
    private float takeoffForce;
    [SerializeField]
    private float takeoffTimer;
    private float takeoffCountdown;
    public bool isGrounded;
    private bool canMove;

    [SerializeField]
    private Slider takeoffSlider;
    [SerializeField]
    private GameObject bigCamera;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = false;
        movement = InputSystem.actions.FindAction("Move");
        takeoff = InputSystem.actions.FindAction("Jump");
        lookAround = InputSystem.actions.FindAction("LookAround");
        canMove = true;
        takeoffCountdown = 0f;
    }

    void Update()
    {
        if (isGrounded)
        {
            if (takeoff.IsPressed())
            {
                animationScript.ChargeControl(true);
                takeoffCountdown += Time.deltaTime;
                takeoffSlider.gameObject.SetActive(true);
                takeoffSlider.value = takeoffCountdown / takeoffTimer;
                canMove = false;
                if (takeoffCountdown > takeoffTimer)
                {
                    StartCoroutine(Takeoff());
                }
            }else
            {
                takeoffSlider.gameObject.SetActive(false);
                canMove = true;
                takeoffCountdown = 0f;
                animationScript.ChargeControl(false);
            }
        }

        if (lookAround.IsPressed() && isGrounded)
        {
            bigCamera.gameObject.SetActive(true);
        }else
        {
            bigCamera.gameObject.SetActive(false);
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
                    rb.linearVelocity = new Vector2(direction.x * movementSpeed * flightMultiplier, direction.y * movementSpeed * flightMultiplier);
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
        takeoffSlider.gameObject.SetActive(false);
        takeoffCountdown = 0f;
        animationScript.ChargeControl(false);
        isGrounded = false;
        yield return new WaitForSeconds(takeoffTimer/2);
        canMove = true;
    }
}
