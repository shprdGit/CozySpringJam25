using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player transform not assigned!");
            return;
        }
        
        float yDistance = Mathf.Abs(player.position.y - transform.position.y);

        if (yDistance <= 1f)
        {
            bool playerIsToTheRight = player.position.x > transform.position.x;
            Vector3 newScale = transform.localScale;
            newScale.x = playerIsToTheRight ? 1 : -1;
            transform.localScale = newScale;
        }
    }    
}
