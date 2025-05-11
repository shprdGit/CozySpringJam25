using UnityEngine;
using UnityEngine.InputSystem;

public class PostOfficeInteraction : MonoBehaviour
{
    private bool canInteract;
    private InputAction interact;
    public GameObject interactable;

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
            interactable.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interaction")
        {
            canInteract = false;
            interactable.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (canInteract)
        {
            if (interact.IsPressed())
            {
                PlayerInventory.Instance.FillInventory();
            }
        }
    }
}