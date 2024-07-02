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
        if (collision.gameObject.layer == 6 && player.isGorilla && player.velocityBefore.y<=0.05f && player.smashingDown)
        {
            Instantiate(player.gorillaSmash, transform.position + new Vector3(0, 0, 0), transform.rotation);
            print("akdpmf");
        }
    }

}
