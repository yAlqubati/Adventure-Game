using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public float maxDistance = 10f; // Maximum distance the bullet can travel before destroying itself
    public Vector3 startPosition; // Starting position of the bullet

    void Start()
    {
        // Record the starting position of the bullet
        startPosition = transform.position;

        // No need to set velocity here, it will be set by EnemyPlant script
    }

    void Update()
    {
        // Check if the bullet has traveled beyond the max distance from the starting point
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            // Destroy the bullet if it's traveled too far
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the bullet hits a player, freeze the player
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().FreezePlayer();
            Destroy(gameObject);
        }

        else if(other.CompareTag("PlantEnemy"))
        {
            return;
        }

        // Destroy the bullet if it hits anything else
        else
        {
            Destroy(gameObject);
        }
    }
}
