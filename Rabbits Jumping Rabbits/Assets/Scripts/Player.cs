using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : MonoBehaviour
{
    public float drownSpeed;
    public float zipSpeed;
    public int teamNo;
    public bool mozeKlonirat;
    public float lifeTime;
    public bool jeKlon;
    public Rigidbody2D rb;
    public float jumpingPower;
    public LayerMask groundLayer;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public DirectionScript Circle;
    public int layerNumber;
    public KeyCode key;
    public float tolerance;
    public float collisionPower;
    public float dbjumpTime;
    public float highJumpTime;
    public float bigTime;
    public float smallTime;
    public float teleportTime;
    public float jumpHigherPower;
    public float enemyBouncePower;
    public float teammateBouncePower;
    public float highJumpPower;
    public float gravityUp;
    public float gravityDown;
    public float gravitySmashDown;
    public Vector2 velocityBefore;
    public bool smashingDown;
    public float rocketDistance;
    public float rocketAnlgeSpeed;
    public float rocketGravity;
    public float rocketPower;
    public float flappyPower;
    public float flappyTime;
    public float eggTime;
    public GameObject jaje;
    public float eggPower;
    public float gorillaTime;
    public GameObject gorillaSmash;
    public bool isGorilla;
    public float gorillaStunTime;
    public SpriteRenderer Stun;
    public Sprite zecSprite;
    public Sprite klonSprite;
    public SpriteRenderer sr;
    public Sprite gorillaSprite;
    public Sprite gorillaAngrySprite;
    public GameObject levoUvo;
    public GameObject desnoUvo;
    public GameObject dust;
    public GameObject dustGorilla;
    public ParticleSystem blood;
    public GameObject UFO;
    public GameObject Krila;

    private int canJump;
    private float canPort;
    private int canDoubleJump;
    private float dbjumpTimer;
    private float lifeTimer;
    private float groundedCD;
    private float jumpHigherCD;
    private float highJump;
    private float highJumpTimer;
    private float bigTimer;
    private float smallTimer;
    private float rocketTimer;
    private bool isRocketing;
    private bool isFlappy;
    private float flappyTimer;
    private bool isEgging;
    private float eggTimer;
    private bool canShootEgg;
    private float gorillaTimer;
    private float stunTimer;
    private bool isStunned;
    private float seVozi;
    private KriloScript KriloScript;
    private bool mrtav;
    void Start()
    {
        KriloScript=Krila.GetComponent<KriloScript>();
        Krila.SetActive(false);

        mozeKlonirat = true;

        if (transform.parent != null)
            jeKlon = true;

        rb = GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        sr.sprite = zecSprite;
        canDoubleJump = 0;
        canJump = 1;
        highJump = 1;
        seVozi = 1;
        canPort = 1;

        if (jeKlon)
        {
            gameObject.layer = transform.parent.gameObject.layer + 4;
            layerNumber = transform.parent.gameObject.GetComponent<Player>().layerNumber + 4;
            sr.sprite = klonSprite;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

    }

    void FixedUpdate()
    {
       // print(MainMenu.lastDied);
        if(IsGrounded() && smashingDown)
        {
            Instantiate(dust, transform.position+new Vector3(0,-1.5f,0), dust.transform.rotation);
        }
        if (isGorilla)
        {
            if (!IsGrounded())
            {
                sr.sprite = gorillaAngrySprite;
            }
            else
            {
                sr.sprite = gorillaSprite;
            }
        }
        mozeKlonirat = true;
        if (stunTimer > 0)
        {
            stunTimer -= Time.fixedDeltaTime;
            Stun.enabled = true;
        }
        else
        {
            isStunned = false;
            Stun.enabled = false;
        }
        if (IsGrounded())
        {
            smashingDown = false;
            canShootEgg = true;
        }
        if (isRocketing) rb.gravityScale = rocketGravity;
        else if (rb.velocity.y > 0) rb.gravityScale = gravityUp;
        else if (!smashingDown) rb.gravityScale = gravityDown;
        else rb.gravityScale = gravitySmashDown;

        jumpHigherCD += Time.fixedDeltaTime;

        if (!IsGrounded()) groundedCD += Time.fixedDeltaTime;
        else groundedCD = 0;

        if (groundedCD <= 0.1f) canJump = 1 + canDoubleJump;
        else if (canJump == 2) canJump = 1;
        else if (canDoubleJump == 0) canJump = 0;

        if (rb.velocity.y > 0 || isRocketing) Physics2D.IgnoreLayerCollision(layerNumber, 6, true);
        else Physics2D.IgnoreLayerCollision(layerNumber, 6, false);

        if (dbjumpTimer > 0) dbjumpTimer -= Time.fixedDeltaTime;
        else canDoubleJump = 0;

        if (highJumpTimer > 0) highJumpTimer -= Time.fixedDeltaTime;
        else highJump = 1;

        lifeTimer = lifeTime;

        if (rocketTimer > 0) rocketTimer -= Time.fixedDeltaTime;
        else if (rocketTimer < 0)
        {
            rocketTimer = 0;
            Circle.distance = Circle.startDistance;
            Circle.maxAngle = Circle.startMaxAngle;
            Circle.minAngle = Circle.startMinAngle;
            Circle.Angle = Circle.startAngle;
            Circle.AngleSpeed = Circle.startAngleSpeed;
            isRocketing = false;
        }

        if (flappyTimer > 0) flappyTimer -= Time.fixedDeltaTime;
        else
        {
            isFlappy = false;
            Krila.SetActive(false);
        }

        if (eggTimer > 0) eggTimer -= Time.fixedDeltaTime;
        else
        {
            isEgging = false;
        }

        if (gorillaTimer > 0) gorillaTimer -= Time.fixedDeltaTime;
        else
        {
            isGorilla = false;
            levoUvo.GetComponent<LineRenderer>().enabled = true;
            desnoUvo.GetComponent<LineRenderer>().enabled = true;
            if (jeKlon)
            {
                sr.sprite = klonSprite;
            }
            else
            {
                sr.sprite = zecSprite;
            }
        }

        if (bigTimer > 0 || smallTimer > 0)
        {
            bigTimer -= Time.fixedDeltaTime;
            smallTimer -= Time.fixedDeltaTime;
        }
        else
        {

            if (jeKlon)
            {
                transform.localScale = new Vector3(1 * seVozi, 1 / seVozi, 1);
            }
            else
            {
                transform.localScale = new Vector3(2 * seVozi, 2 / seVozi, 2);
                Circle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
        if (jeKlon) lifeTimer -= Time.fixedDeltaTime;
        if (lifeTimer < 0) Destroy(gameObject);

        if (canPort <= 1) canPort += Time.fixedDeltaTime;


        velocityBefore = rb.velocity;
    }

    private void Update()
    {
        if (!isStunned)
        {
            if (Input.GetKeyDown(key)) Jump();
            if (Input.GetKey(key)) JumpHigher();
        }
    }

    public void Jump()
    {
        if (isRocketing)
        {
            rb.velocity = (Vector2)(Circle.transform.position - transform.position).normalized * rocketPower;
        }
        else if (isFlappy && !IsGrounded())
        {
            rb.velocity = new Vector2(flappyPower * (Circle.transform.position - transform.position).normalized.x, flappyPower * (Circle.transform.position - transform.position).normalized.y);
            Krila.GetComponent<Animator>().Play("krila");
        }
        else if (isEgging && !IsGrounded() && canShootEgg)
        {
            canShootEgg = false;
            Instantiate(jaje, transform.position + new Vector3(0, -2, 0), transform.rotation);
            rb.velocity = new Vector2(rb.velocity.x, eggPower);
        }
        else if (canJump > 0)
        {
            rb.velocity = new Vector2(jumpingPower * highJump * (Circle.transform.position - transform.position).normalized.x, jumpingPower * (Circle.transform.position - transform.position).normalized.y);
            jumpHigherCD = 0;
            canJump--;
            
        }
        else if (!IsGrounded())
        {
            smashingDown = true;
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, 0);
        }
    }


    public void JumpHigher()
    {
        if (isRocketing)
        {
            rb.velocity = (Vector2)(Circle.transform.position - transform.position).normalized * rocketPower;
            //rb.velocity += (Vector2)(Circle.transform.position - transform.position).normalized * teleportPower*Time.deltaTime;
            //transform.position = Vector3.SmoothDamp(transform.position, transform.position+(Circle.transform.position - transform.position).normalized * 1.1f * Circle.distance,ref velocity, 0);
            //transform.position += (Circle.transform.position - transform.position).normalized*1.1f * Circle.distance;
        }
        else if (jumpHigherCD < 0.5 && !IsGrounded())
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
                rb.velocity += (Vector2)(transform.position - collision.gameObject.transform.position).normalized * enemyBouncePower;
            }
            else
            {
                rb.velocity += (Vector2)(transform.position - collision.gameObject.transform.position).normalized * teammateBouncePower;
            }
        }
        if (collision.gameObject.CompareTag("Zid") && !isRocketing)
        {
            rb.velocity = new Vector2(-velocityBefore.x, velocityBefore.y + collisionPower);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 19 && !jeKlon)
        {

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

            if (collision.gameObject.CompareTag("Doubler") && !jeKlon && mozeKlonirat)
            {
                mozeKlonirat = false;
                Instantiate(UFO, MainMenu.UFOstart.position, MainMenu.UFOstart.rotation, transform);

            }

            if (collision.gameObject.CompareTag("Big"))
            {
                transform.localScale = new Vector3(4, 4, 4);
                Circle.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                bigTimer = bigTime;
            }

            if (collision.gameObject.CompareTag("Small"))
            {
                transform.localScale = new Vector3(1, 1, 1);
                Circle.transform.localScale = new Vector3(1f, 1f, 1f);
                smallTimer = smallTime;
            }

            if (collision.gameObject.CompareTag("Rocket"))
            {
                isRocketing = true;
                rocketTimer = teleportTime;
                Circle.distance = rocketDistance;
                Circle.maxAngle = 1000000;
                Circle.minAngle = -40;
                Circle.AngleSpeed = rocketAnlgeSpeed;
            }

            if (collision.gameObject.CompareTag("Flappy"))
            {
                isFlappy = true;
                flappyTimer = flappyTime;
                Krila.SetActive(true);
            }

            if (collision.gameObject.CompareTag("Egg"))
            {
                isEgging = true;
                eggTimer = eggTime;
            }

            if (collision.gameObject.CompareTag("Gorilla"))
            {
                isGorilla = true;
                gorillaTimer = gorillaTime;
                sr.sprite = gorillaSprite;
                levoUvo.GetComponent<LineRenderer>().enabled = false;
                desnoUvo.GetComponent<LineRenderer>().enabled = false;
            }

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("GorillaSmash") && !isGorilla && IsGrounded())
        {
            Umri();
            stunTimer = gorillaStunTime;
        }
        if (collision.gameObject.CompareTag("Zip"))
        {
            seVozi = 0.6f;
        }
        if (collision.gameObject.CompareTag("kill") && !mrtav)
        {
            Umri();
        }
        if (collision.gameObject.CompareTag("Portal1") && canPort >= 0.5f)
        {
            canPort = 0;
            transform.position = new Vector3(22.5f, 9.5f, 0);
        }
        if (collision.gameObject.CompareTag("Portal2") && canPort >= 0.5f)
        {
            canPort = 0;
            transform.position = new Vector3(-22.5f, -9.5f, 0);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zip"))
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.85f, zipSpeed);
        }
        if (collision.gameObject.CompareTag("Lava"))
        {
            rb.velocity = new Vector2(0, -drownSpeed);
        }
        if (collision.gameObject.CompareTag("JumpT"))
        {
            smashingDown = false;
            rb.velocity = new Vector2(jumpingPower * highJump * (Circle.transform.position - transform.position).normalized.x, jumpingPower * (Circle.transform.position - transform.position).normalized.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zip"))
        {
            int randSmer = Random.Range(0, 2);
            randSmer = randSmer * 2 - 1;
            rb.velocity = new Vector2(collisionPower * randSmer, rb.velocity.y);
            seVozi = 1;
        }
    }

    public void Kloniraj(Vector3 pos)
    {
        Instantiate(gameObject, pos, transform.rotation, transform);
    }

    public void Umri()
    {
        mrtav = true;
        print("umro");
        Instantiate(blood, transform.position, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
        if (!jeKlon)
        {
            if (MainMenu.lastDied == teamNo)
            {
                MainMenu.numberOfPlayers = 1;
            }
            else
            {
                MainMenu.lastDied = teamNo;
                MainMenu.numberOfPlayers--;
            }
        }
        Destroy(gameObject);
        print(MainMenu.numberOfPlayers);
    }
}