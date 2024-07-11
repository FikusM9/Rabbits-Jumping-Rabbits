using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class UFOScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Player player;

    private bool klonirao;
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        transform.parent = null;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (-speed, 0);
    }

    void FixedUpdate()
    {
        if (transform.position.x < MainMenu.UFOstop.position.x && !klonirao)
        {
            klonirao = true;
            player.Kloniraj(transform.position);
        }
        if(transform.position.x < MainMenu.UFOend.position.x)
        {
            Destroy(gameObject);
        }
    }


}
