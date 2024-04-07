using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public float distance;
    public float currentDistance;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentDistance = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        // move the enemy
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // check if the enemy has reached the distance
        currentDistance += Mathf.Abs(rb.velocity.x) * Time.deltaTime;
        if (currentDistance >= distance)
        {
            speed = -speed;
            sr.flipX = !sr.flipX;
            currentDistance = 0;
        }
        
    }

    
}
