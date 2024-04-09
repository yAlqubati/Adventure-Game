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
        // Determine the direction to shoot based on plant's orientation
        Vector2 shootDirection = !spriteRenderer.flipX ? Vector2.left : Vector2.right;

        // Spawn a bullet at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        
        // Set the bullet's velocity based on the shoot direction and bullet speed
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bullet.GetComponent<PlantBullet>().speed;
    }

    public void Die()
    {
        AudioManager.instance.PlayAudio(3);

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
