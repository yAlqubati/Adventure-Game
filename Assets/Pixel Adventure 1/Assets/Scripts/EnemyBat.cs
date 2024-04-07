using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite deadSprite;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
        }
    }

    public void Die()
    {
        Debug.Log("Rino died");

        sr.sprite = deadSprite;

        anim.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        // wait for 1 second
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
