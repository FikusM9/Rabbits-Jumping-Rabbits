using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glava : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Noge") && player.velocityBefore.y > collision.gameObject.transform.parent.gameObject.GetComponent<Player>().velocityBefore.y)
        {
            Destroy(transform.parent.gameObject);
            collision.gameObject.transform.parent.gameObject.GetComponent<Player>().smashingDown = false;

        }
        if (collision.gameObject.CompareTag("Jaje"))
        {
            Destroy(transform.parent.gameObject);
            Destroy(collision.transform.gameObject);
        }
    }
}
