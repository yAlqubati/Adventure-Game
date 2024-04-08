using System.Collections;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootInterval = 3f; // Shoot interval in seconds
    public Sprite deadSprite;
    public bool isDead = false;

    private SpriteRenderer spriteRenderer;
    public float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        timer += Time.deltaTime;

        // Shoot at regular intervals
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        // Spawn a bullet at the bullet spawn point
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        spriteRenderer.sprite = deadSprite;
        // Stop all coroutines
        StopAllCoroutines();
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
