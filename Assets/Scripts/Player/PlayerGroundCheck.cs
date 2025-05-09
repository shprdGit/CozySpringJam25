using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement movementScript;
    [SerializeField]
    private PlayerAnimation animationScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            movementScript.isGrounded = true;
            animationScript.GroundedControl(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            movementScript.isGrounded = false;
            animationScript.GroundedControl(false);
        }
    }
}
