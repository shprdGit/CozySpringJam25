using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void IdleControl(bool value)
    {
        animator.SetBool("isIdle", value);
    }

    public void GroundedControl (bool value)
    {
        animator.SetBool("isGrounded", value);
    }

    public void AccelerationControl (bool value)
    {
        animator.SetBool("isAccelerating", value);
    }

    public void ChargeControl(bool value)
    {
        animator.SetBool("isCharging", value);
    }
}
