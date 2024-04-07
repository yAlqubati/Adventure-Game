using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float shootInterval = 5f;
    public float detectionRange = 5f;
    public bool isShooting = false;
    public bool isDead = false;
    public Sprite deadSprite;
    public SpriteRenderer sr;
    public GameObject[] players;
    public float timer = 0f;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isDead)
        {
            foreach (GameObject player in players)
            {
                if (player != null)
                {
                    // Check if the player is within the detection range
                    if (Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
                    {
                        // Start shooting if not already shooting
                        if (!isShooting)
                        {
                            isShooting = true;
                            StartCoroutine(ShootPlayer());
                        }
                    }
                }
            }
        }
    }

    IEnumerator ShootPlayer()
    {
        while (isShooting && !isDead)
        {
            foreach (GameObject player in players)
            {
                if (player != null)
                {
                    // Get the direction to shoot based on the sprite's flipX property
                    Vector2 shootDirection = sr.flipX ? Vector2.right : Vector2.left;

                    // Shoot at the player
                    Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = shootDirection * 10f;
                }
            }

            // Wait for the specified interval before shooting again
            yield return new WaitForSeconds(shootInterval);
        }
    }


    public void Die()
    {
        Debug.Log("Plant died");
        isDead = true;
        sr.sprite = deadSprite;
        // Add any other death-related actions here
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        // wait for 1 second
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
