using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FlowerDesire : MonoBehaviour
{
    private InputAction interact;

    private enum FlowersList { peony, columbine, bluebell };
    [SerializeField]
    private FlowersList requestedFlower;
    [Tooltip("Amount of time until NPC asks for a flower again")]
    [SerializeField]
    private float happyFor;
    private float happyTimer;
    private bool wantsFlower;
    private bool canInteract;
    public Sprite peonySprite;
    public Sprite columbineSprite;
    public Sprite bluebellSprite;
    public SpriteRenderer FlowerRender;

    void Start()
    {
        happyTimer = happyFor;
        wantsFlower = false;
        canInteract = false;
        interact = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        if (wantsFlower == false)
        {
            happyTimer -= Time.deltaTime;
            if (happyTimer <= 0f)
            {
                FlowerRender.gameObject.SetActive(true);
                switch (requestedFlower)
                {
                    case FlowersList.peony:
                        FlowerRender.sprite = peonySprite;
                        break;

                    case FlowersList.columbine:
                        FlowerRender.sprite = columbineSprite;
                        break;

                    case FlowersList.bluebell:
                        FlowerRender.sprite = bluebellSprite;
                        break;

                    default:
                        break;
                }
                wantsFlower = true;
                Debug.Log(gameObject.name + " wants a " + requestedFlower);
            }
        }

        if (canInteract && wantsFlower)
        {
            if (interact.IsPressed())
            {
                TryReceiveFlower();
            }
        }
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

    void TryReceiveFlower()
    {
        switch (requestedFlower)
        {
            case FlowersList.peony:
                if (PlayerInventory.Instance.peony == true)
                {
                    PlayerInventory.Instance.ChangePeony(false);
                    Debug.Log("Delivered a Peony!");
                    happyTimer = happyFor;
                    wantsFlower = false;
                    FlowerRender.gameObject.SetActive(false);
                }else
                {
                    Debug.Log("You don't have a Peony!");
                }
                break;

            case FlowersList.columbine:
                if (PlayerInventory.Instance.columbine == true)
                {
                    PlayerInventory.Instance.ChangeColumbine(false);
                    Debug.Log("Delivered a Columbine!");
                    happyTimer = happyFor;
                    wantsFlower = false;
                    FlowerRender.gameObject.SetActive(false);
                }else
                {
                    Debug.Log("You don't have a Columbine!");
                }
                break;

            case FlowersList.bluebell:
                if (PlayerInventory.Instance.bluebell == true)
                {
                    PlayerInventory.Instance.ChangeBluebell(false);
                    Debug.Log("Delivered a Bluebell!");
                    happyTimer = happyFor;
                    wantsFlower = false;
                    FlowerRender.gameObject.SetActive(false);
                }else
                {
                    Debug.Log("You don't have a Bluebell!");
                }
                break;

            default:
                break;

        }
    }
}
