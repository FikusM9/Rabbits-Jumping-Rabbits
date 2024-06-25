using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public float bigTime;
    public float smallTime;
    public float jumpHigherPower;
    public float enemyBouncePower;
    public float teammateBouncePower;
    public float highJumpPower;
    public GameObject clone;
    public float gravityUp;
    public float gravityDown;
    public float gravitySmashDown;

    private int canJump;
    private int canDoubleJump;
    private float dbjumpTimer;
    private float groundedCD;
    private float jumpHigherCD;
    private float highJump;
    private float highJumpTimer;
    private float bigTimer;
    private float smallTimer;
    private bool smashingDown;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDoubleJump = 0;
        canJump = 1;
        highJump = 1;
    }

    void FixedUpdate()
    {
        if (IsGrounded()) smashingDown = false;
        if (rb.velocity.y > 0) rb.gravityScale = gravityUp;
        else if (!smashingDown) rb.gravityScale = gravityDown;
        else rb.gravityScale = gravitySmashDown;

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

        if (bigTimer > 0 || smallTimer > 0)
        {
            bigTimer -= Time.fixedDeltaTime;
            smallTimer -= Time.fixedDeltaTime;
        }
        else
        {
            transform.localScale = new Vector3(2, 2, 2);
            Circle.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
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
            rb.velocity = new Vector2(jumpingPower * highJump * (Circle.position - transform.position).normalized.x, jumpingPower * (Circle.position - transform.position).normalized.y);
            jumpHigherCD = 0;
            canJump--;
        }
        else if (!IsGrounded())
        {
            smashingDown = true;
            rb.velocity= new Vector2(rb.velocity.x*0.5f, rb.velocity.y);
        }
    }


    public void JumpHigher()
    {
        if (jumpHigherCD < 0.5 && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpingPower * highJump * Time.deltaTime * jumpHigherPower);
        }
    }


    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckLeft.position, 0.02f, groundLayer) || Physics2D.OverlapCircle(groundCheckRight.position, 0.02f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tim1") || collision.gameObject.CompareTag("Tim2") || collision.gameObject.CompareTag("Player"))
        {
            if ((collision.gameObject.CompareTag("Tim1") && gameObject.CompareTag("Tim2")) || collision.gameObject.CompareTag("Tim2") && gameObject.CompareTag("Tim1") || collision.gameObject.CompareTag("Player"))
            {
                print("gagafgafgag");
                rb.velocity += (Vector2)(transform.position - collision.gameObject.transform.position).normalized * enemyBouncePower;
            }
            else
            {
                rb.velocity += (Vector2)(transform.position - collision.gameObject.transform.position).normalized * teammateBouncePower;
            }
            /*/if (transform.position.y + tolerance < collision.gameObject.transform.position.y && ((collision.gameObject.CompareTag("Tim1") && gameObject.CompareTag("Tim2")) || (collision.gameObject.CompareTag("Tim2") && gameObject.CompareTag("Tim1")) || collision.gameObject.CompareTag("Player")))
            {
                Destroy(gameObject);
            }/*/

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
                highJump = highJumpPower;
                highJumpTimer = highJumpTime;
            }

            if (collision.gameObject.CompareTag("Doubler"))
            {
                Instantiate(clone, transform.position + new Vector3(5, 5, 0), transform.rotation);
            }

            if (collision.gameObject.CompareTag("Big"))
            {
                transform.localScale =new Vector3(4,4,4);
                Circle.localScale = new Vector3(0.25f,0.25f,0.25f);
                bigTimer = bigTime;
            }

            if (collision.gameObject.CompareTag("Small"))
            {
                transform.localScale = new Vector3(1, 1, 1);
                Circle.localScale = new Vector3(1f, 1f, 1f);
                smallTimer = smallTime;
            }

            Destroy(collision.gameObject);
        }
    }
}
