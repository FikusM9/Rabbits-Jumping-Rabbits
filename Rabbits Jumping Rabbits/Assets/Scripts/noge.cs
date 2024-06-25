using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noge : MonoBehaviour
{
   public Player player;

    void Start()
    {

    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Glava"))
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.enemyBouncePower);
        }
    }
}
