using UnityEngine;
using UnityEngine.InputSystem;

public class PostOfficeInteraction : MonoBehaviour
{
    private bool canInteract;
    private InputAction interact;

    void Start()
    {
        canInteract = false;
        interact = InputSystem.actions.FindAction("Interact");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interaction")
        {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interaction")
        {
            canInteract = false;
        }
    }

    void Update()
    {
        if (canInteract)
        {
            if (interact.IsPressed())
            {
                PlayerInventory.Instance.EmptyInventory();
                PlayerInventory.Instance.FillInventory();
            }
        }
    }
}
