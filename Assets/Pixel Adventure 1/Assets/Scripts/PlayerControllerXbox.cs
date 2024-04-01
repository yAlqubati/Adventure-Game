using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerXbox : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public LayerMask groundLayer;
    public Transform feet;
    public bool isGrounded;
    public Animator anim;
    public SpriteRenderer sr;
    public Transform lastCheckpoint;
    public bool isFrozen = false;
    public bool isJumping = false;
    public bool isUsingController = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lastCheckpoint = new GameObject().transform;

        // check if the player is using 
        // a controller or a keyboard
        if (Gamepad.current != null)
        {
            isUsingController = true;
        }

        
    }

    

    private void Update()
    {
        if (isFrozen)
            return;

        isGrounded = Physics2D.OverlapCircle(feet.position, 0.2f, groundLayer);

        anim.SetFloat("Running", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsJumping", !isGrounded);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isFrozen)
            return;

        float moveInput = context.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0)
            sr.flipX = false;
        else if (moveInput < 0)
            sr.flipX = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isFrozen)
            return;

        if (isGrounded && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }

        else if (isJumping && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }
    }

    public void FreezePlayer()
    {
        Debug.Log("Player is frozen");
        isFrozen = true;
        rb.velocity = Vector2.zero;
        // make the color of the player blue
        sr.color = Color.blue;
        // stop the animation
        anim.enabled = false;
    }

    public void UnFreezePlayer()
    {
        Debug.Log("Player is unfrozen");
        isFrozen = false;
        // make the color of the player white
        sr.color = Color.white;
        // start the animation
        anim.enabled = true;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && isFrozen)
        {
            UnFreezePlayer();
        }
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawning player");
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = lastCheckpoint.position;
    }
}
