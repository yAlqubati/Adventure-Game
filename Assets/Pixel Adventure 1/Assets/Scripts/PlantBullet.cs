using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float lifeTime;
    public Rigidbody2D rb;
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
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player hit the bullet");

            // if it hit a player check if he has a xBox controller or not
            if (other.gameObject.GetComponent<PlayerControllerXbox>())
            {
                Debug.Log("Player has a xBox controller");
                other.gameObject.GetComponent<PlayerControllerXbox>().FreezePlayer();
            }
            else
            {
                Debug.Log("Player has a keyboard controller");
                other.gameObject.GetComponent<PlayerController>().FreezePlayer();
            }

            Destroy(this.gameObject);
        }
    }
}
