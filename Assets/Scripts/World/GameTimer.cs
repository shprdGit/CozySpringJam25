using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    private float gameOverTimer;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI endText;
    public GameObject endObject;

    public AudioSource audioSource;
    public AudioClip warningSound; // Sound for last 10 seconds (looping)
    public AudioClip endSound;     // Sound to play once when timer hits 0

    private bool warningSoundPlayed = false;
    private bool endSoundPlayed = false;

    void Start()
    {
        gameOverTimer = 60f;
        if (audioSource != null)
            audioSource.loop = false;
    }

    void Update()
    {
        gameOverTimer -= Time.deltaTime;
        textComponent.text = Mathf.Ceil(gameOverTimer).ToString();

        // Play warning sound in the last 10 seconds
        if (gameOverTimer <= 10f && !warningSoundPlayed)
        {
            if (audioSource != null && warningSound != null)
            {
                audioSource.clip = warningSound;
                audioSource.loop = true;
                audioSource.Play();
            }
            warningSoundPlayed = true;
        }

        // When time is up
        if (gameOverTimer <= 0f && !endSoundPlayed)
        {
            if (audioSource != null && endSound != null)
            {
                audioSource.Stop(); // Stop warning loop
                audioSource.loop = false;
                audioSource.clip = endSound;
                audioSource.Play(); // Play end sound once
            }

            endText.text = "You delivered a total of " + PlayerInventory.Instance.totalDeliveries + " flowers!";
            endObject.SetActive(true);
            Time.timeScale = 0;

            endSoundPlayed = true;
        }
    }
}
