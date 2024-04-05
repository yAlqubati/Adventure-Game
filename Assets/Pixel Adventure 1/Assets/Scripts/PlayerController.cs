using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        isGrounded = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);

        anim.SetFloat("Running", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsJumping", !isGrounded);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isFrozen)
        {
            return;
        }

        float moveInput = context.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0)
        {
            sr.flipX = false;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isFrozen)
        {
            return;
        }
        
        if (context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
        else if (context.performed && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }
    }

    // collision with other player
    public void OnCollisionEnter2D(Collision2D other)   
    {
        if (other.gameObject.tag == "Player" && isFrozen)
        {
            UnFreezePlayer();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Strawberry")
        {
            other.gameObject.SetActive(false);
            GetComponent<PlayerHealth>().Heal(10);
        }

        else if(other.gameObject.tag == "Melon")
        {
            other.gameObject.SetActive(false);
            GetComponent<PlayerHealth>().Heal(20);
        }
    }

    public void FreezePlayer()
    {
        Debug.Log("Player is frozen");
        isFrozen = true;
        // make the color of the player blue
        sr.color = Color.blue;
        // stop the animation
        anim.enabled = false;
        // stop the movement of the player, don't let him move
        rb.velocity = Vector2.zero;
        
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
