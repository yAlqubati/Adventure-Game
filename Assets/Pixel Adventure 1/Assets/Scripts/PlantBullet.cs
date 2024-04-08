using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public float maxDistance = 10f; // Maximum distance the bullet can travel before destroying itself
    public Vector3 startPosition; // Starting position of the bullet

    void Start()
    {
        Debug.Log("PlantBullet Start");
        // Record the starting position of the bullet
        startPosition = transform.position;

        // Shoot the bullet in the direction it's facing
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
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
    }
}
