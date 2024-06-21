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
    public Transform transform;

    private bool canJump;
    private float groundedCD;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        canJump=true;
    }

    void FixedUpdate()
    {
        if (!IsGrounded()) groundedCD += Time.fixedDeltaTime;
        else groundedCD = 0;

        if (groundedCD <= 0.1f) canJump = true;
        else canJump = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKey(KeyCode.Space)) JumpHigher();
    }

    public void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(jumpingPower * (Circle.position.x - transform.position.x), jumpingPower * (Circle.position.y - transform.position.y));
        }
    }


    public void JumpHigher() 
    {
       if (groundedCD<0.5) 
       {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+jumpingPower*Time.deltaTime*3.5f);
       }
    }


    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckLeft.position, 0.02f, groundLayer) || Physics2D.OverlapCircle(groundCheckRight.position, 0.02f, groundLayer);
    }
}
