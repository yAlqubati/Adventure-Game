using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float lifeTime;
    public Rigidbody2D rb;
    public float distance = 10f;
    public LayerMask whatIsSolid;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Vector2 moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

    }

    // Update is called once per frame
    void Update()
    {
        // if the bullet is too far from the player destroy it
        if (Vector2.Distance(transform.position, player.transform.position) > distance)
        {
            Destroy(gameObject);
        }

        
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player hit the bullet");

            // if it hit a player check if he has a xBox controller or not
            
            other.gameObject.GetComponent<PlayerController>().FreezePlayer();
            

            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "PlantEnemy")
        {
            return;
        }

        if (other.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }

    
}
