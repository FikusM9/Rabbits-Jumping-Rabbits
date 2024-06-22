using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float dbjumpTime;
    public float highJumpTime;

    private int canJump;
    private int canDoubleJump;
    private float dbjumpTimer;
    private float groundedCD;
    private float jumpHigherCD;
    private float highJump;
    private float highJumpTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDoubleJump = 0;
        canJump = 1;
        highJump = 1;
    }

    void FixedUpdate()
    {
        jumpHigherCD += Time.fixedDeltaTime;

        if (!IsGrounded()) groundedCD += Time.fixedDeltaTime;
        else groundedCD = 0;

        if (groundedCD <= 0.1f) canJump = 1 + canDoubleJump;
        else if (canJump == 2) canJump = 1;
        else if (canDoubleJump == 0) canJump = 0;

        if (rb.velocity.y > 0) Physics2D.IgnoreLayerCollision(layerNumber, 6, true);
        else Physics2D.IgnoreLayerCollision(layerNumber, 6, false);

        if (dbjumpTimer > 0) dbjumpTimer -= Time.fixedDeltaTime;
        else canDoubleJump = 0;

        if (highJumpTimer > 0) highJumpTimer -= Time.fixedDeltaTime;
        else highJump = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key)) Jump();
        if (Input.GetKey(key)) JumpHigher();
    }

    public void Jump()
    {
        if (canJump > 0)
        {
            rb.velocity = new Vector2(jumpingPower * highJump * (Circle.position.x - transform.position.x), jumpingPower * (Circle.position.y - transform.position.y));
            jumpHigherCD = 0;
            canJump--;
        }
    }


    public void JumpHigher()
    {
        if (jumpHigherCD < 0.5 && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpingPower * highJump * Time.deltaTime * 5.5f);
        }
    }


    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckLeft.position, 0.02f, groundLayer) || Physics2D.OverlapCircle(groundCheckRight.position, 0.02f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tim1") || collision.gameObject.CompareTag("Tim2"))
        {
            print("sdfnjo");
            rb.velocity += (Vector2)(transform.position - collision.gameObject.transform.position).normalized * collisionPower;
            if (transform.position.y + tolerance < collision.gameObject.transform.position.y && ((collision.gameObject.CompareTag("Tim1") && gameObject.CompareTag("Tim2")) || collision.gameObject.CompareTag("Tim2") && gameObject.CompareTag("Tim1")))
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
        if (collision.gameObject.layer == 19)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y + collisionPower);

            if (collision.gameObject.CompareTag("DBJump"))
            {
                canDoubleJump = 1;
                dbjumpTimer = dbjumpTime;
            }

            if (collision.gameObject.CompareTag("HighJump"))
            {
                highJump = 1.5f;
                highJumpTimer = highJumpTime;
            }

            if (collision.gameObject.CompareTag("Doubler"))
            {
                Instantiate(gameObject, transform.position + new Vector3(5, 5, 0), transform.rotation);
            }

            Destroy(collision.gameObject);
        }
    }
}
