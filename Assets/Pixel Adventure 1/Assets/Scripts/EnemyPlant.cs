using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    // if you changed the timer then you need to adjust the animation speed
    public SpriteRenderer sr;
    public Sprite deadSprite;
    public Animator anim;

    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float timer;
    public float shootInterval = 5;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        timer = 0;
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
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
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
