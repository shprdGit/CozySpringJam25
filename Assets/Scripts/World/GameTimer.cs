using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    private float gameOverTimer;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI endText;
    public GameObject endObject;

    void Start()
    {
        gameOverTimer = 60f;
    }

    void Update()
    {
        gameOverTimer -= Time.deltaTime;
        textComponent.text = gameOverTimer.ToString();
        if (gameOverTimer <= 0f)
        {
            endText.text = "You delivered a total of " + PlayerInventory.Instance.totalDeliveries + " flowers!";
            endObject.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
