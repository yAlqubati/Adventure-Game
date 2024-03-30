using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public LayerMask groundLayer;
    public Transform feet;
    public bool isGrounded;
    public bool isJumping;
    public Animator anim;
    public SpriteRenderer sr;
    public Transform lastCheckpoint;
    public bool isFrozen = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            return;
        }

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }

        else if(Input.GetKeyDown(KeyCode.Space) && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }

        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }

        anim.SetFloat("Running", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsJumping", !isGrounded);

        
    }

    // other player hit this player and this player is frozen then unfreeze the player
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && isFrozen)
        {
            UnFreezePlayer();
        }
    }

    public void FreezePlayer()
    {
        Debug.Log("Player is frozen");
        isFrozen = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.isKinematic = false;
        // make the color of the player blue
        sr.color = Color.blue;
        // stop the animation
        anim.enabled = false;
        
    }

    
    
    // unfreeze the player
    public void UnFreezePlayer()
    {
        Debug.Log("Player is unfrozen");
        isFrozen = false;
        // make the color of the player white
        sr.color = Color.white;
        // start the animation
        anim.enabled = true;
    }

}
