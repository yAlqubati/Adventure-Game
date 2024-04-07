using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite deadSprite;
    public Animator anim;

    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float timer;
    public float shootInterval = 5;
    public bool isDead = false;

    // Reference to the player GameObject
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        timer = 0;

        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval && !isDead)
        {
            timer = 0;
            Shoot();
        }

       // using the sprite renderer to flip the sprite based on the player's position
        if (player != null)
        {
            if (player.transform.position.x < transform.position.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }

    public void Shoot()
    {
        if (player != null)
        {
            if (player.transform.position.x < transform.position.x)
            {
                Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            }
        }
    }

    public void Die()
    {
        Debug.Log("Plant died");
        isDead = true;
        sr.sprite = deadSprite;
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        // wait for 1 second
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
