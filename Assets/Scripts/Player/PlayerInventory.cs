using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    public bool peony;
    public bool columbine;
    public bool bluebell;

    public Image peonyImage;
    public Image columbineImage;
    public Image bluebellImage;

    public AudioClip addItemSound;  // Sound to play when an item is added (inventory slot set to true)
    public AudioClip removeItemSound;  // Sound to play when an item is removed (inventory slot set to false)
    private AudioSource audioSource;

    private bool canPlayAddSound = true; // To prevent the "add item" sound from playing repeatedly
    private bool canPlayRemoveSound = true; // To prevent the "remove item" sound from playing repeatedly

    private float addSoundCooldown = 1f; // Cooldown time in seconds for the add sound
    private float removeSoundCooldown = 1f; // Cooldown time in seconds for the remove sound

    public static PlayerInventory Instance;

    public int totalDeliveries;
    public TextMeshProUGUI textComponent;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        EmptyInventory();
        totalDeliveries = 0;
        UpdateUI();
    }

    void OnEnable()
    {
        EmptyInventory();
        totalDeliveries = 0;
        UpdateUI();
    }

    public void FillInventory()
    {
        ChangePeony(true);
        ChangeColumbine(true);
        ChangeBluebell(true);
    }

    public void EmptyInventory()
    {
        ChangePeony(false);
        ChangeColumbine(false);
        ChangeBluebell(false);
    }

    public void ChangePeony(bool value)
    {
        if (peony != value)  // Only change and play sound if the value is actually different
        {
            peony = value;
            UpdateUI();
            if (peony && canPlayAddSound)
            {
                PlaySound(addItemSound);  // Play the add sound when setting to true
                StartCoroutine(PlayAddSoundCooldown());  // Start cooldown for add sound
            }
            else if (!peony && canPlayRemoveSound)
            {
                PlaySound(removeItemSound);  // Play the remove sound when setting to false
                StartCoroutine(PlayRemoveSoundCooldown());  // Start cooldown for remove sound
            }
        }
    }

    public void ChangeColumbine(bool value)
    {
        if (columbine != value)  // Only change and play sound if the value is actually different
        {
            columbine = value;
            UpdateUI();
            if (columbine && canPlayAddSound)
            {
                PlaySound(addItemSound);  // Play the add sound when setting to true
                StartCoroutine(PlayAddSoundCooldown());  // Start cooldown for add sound
            }
            else if (!columbine && canPlayRemoveSound)
            {
                PlaySound(removeItemSound);  // Play the remove sound when setting to false
                StartCoroutine(PlayRemoveSoundCooldown());  // Start cooldown for remove sound
            }
        }
    }

    public void ChangeBluebell(bool value)
    {
        if (bluebell != value)  // Only change and play sound if the value is actually different
        {
            bluebell = value;
            UpdateUI();
            if (bluebell && canPlayAddSound)
            {
                PlaySound(addItemSound);  // Play the add sound when setting to true
                StartCoroutine(PlayAddSoundCooldown());  // Start cooldown for add sound
            }
            else if (!bluebell && canPlayRemoveSound)
            {
                PlaySound(removeItemSound);  // Play the remove sound when setting to false
                StartCoroutine(PlayRemoveSoundCooldown());  // Start cooldown for remove sound
            }
        }
    }

    public void UpdateUI()
    {
        textComponent.text = totalDeliveries.ToString();
        peonyImage.gameObject.SetActive(peony);
        columbineImage.gameObject.SetActive(columbine);
        bluebellImage.gameObject.SetActive(bluebell);
    }

    // Play a sound using AudioSource
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);  // Play the sound once
        }
    }

    // Coroutine to manage the cooldown for the add sound
    private IEnumerator PlayAddSoundCooldown()
    {
        canPlayAddSound = false;  // Disable playing the add sound
        yield return new WaitForSeconds(addSoundCooldown);  // Wait for cooldown time
        canPlayAddSound = true;  // Re-enable playing the add sound
    }

    // Coroutine to manage the cooldown for the remove sound
    private IEnumerator PlayRemoveSoundCooldown()
    {
        canPlayRemoveSound = false;  // Disable playing the remove sound
        yield return new WaitForSeconds(removeSoundCooldown);  // Wait for cooldown time
        canPlayRemoveSound = true;  // Re-enable playing the remove sound
    }
}