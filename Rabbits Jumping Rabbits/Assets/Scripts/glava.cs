using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glava : MonoBehaviour
{
    public Player player;
    public ParticleSystem blood;

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
            Instantiate(blood, transform.position, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
            Destroy(transform.parent.gameObject);
            collision.gameObject.transform.parent.gameObject.GetComponent<Player>().smashingDown = false;

        }
        if (collision.gameObject.CompareTag("Jaje"))
        {
            Instantiate(blood, transform.position, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
            Destroy(transform.parent.gameObject);
            Destroy(collision.transform.gameObject);
        }
    }
}
