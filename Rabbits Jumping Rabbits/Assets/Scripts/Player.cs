using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpingPower;
    public LayerMask groundLayer;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Transform Circle;
    public int layerNumber;
    public KeyCode key;
    public float tolerance;
    public float collisionPower;

    private bool canJump;
    private float groundedCD;
    private float jumpHigherCD;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        canJump=true;
    }

    void FixedUpdate()
    {
        jumpHigherCD += Time.fixedDeltaTime;

        if (!IsGrounded()) groundedCD += Time.fixedDeltaTime;
        else groundedCD = 0;

        if (groundedCD <= 0.1f) canJump = true;
        else canJump = false;

        if (rb.velocity.y > 0) Physics2D.IgnoreLayerCollision(layerNumber, 6, true);
        else Physics2D.IgnoreLayerCollision(layerNumber, 6, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(key)) Jump();
        if (Input.GetKey(key)) JumpHigher();
    }

    public void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(jumpingPower * (Circle.position.x - transform.position.x), jumpingPower * (Circle.position.y - transform.position.y));
            jumpHigherCD = 0;
        }
    }


    public void JumpHigher() 
    {
       if (jumpHigherCD<0.5 && !IsGrounded()) 
       {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+jumpingPower*Time.deltaTime*5.5f);
       }
    }


    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckLeft.position, 0.02f, groundLayer) || Physics2D.OverlapCircle(groundCheckRight.position, 0.02f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("sdfnjo");
            rb.velocity += (Vector2)(transform.position - collision.gameObject.transform.position).normalized * collisionPower;
            if (transform.position.y+tolerance < collision.gameObject.transform.position.y)
            {
                Destroy(gameObject);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zid"))
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y + collisionPower);

        }
    }
}
