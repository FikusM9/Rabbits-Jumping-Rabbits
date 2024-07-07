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
            if (transform.parent.gameObject.GetComponent<Player>().teamNo == collision.gameObject.transform.parent.gameObject.GetComponent<Player>().teamNo
				&& transform.parent.gameObject.GetComponent<Player>().teamNo != 0)
            {
				print("mosa");
			}
            else
            {
                transform.parent.gameObject.GetComponent<Player>().Umri();
				collision.gameObject.transform.parent.gameObject.GetComponent<Player>().smashingDown = false;
            }

        }
        if (collision.gameObject.CompareTag("Jaje"))
        {
            transform.parent.gameObject.GetComponent<Player>().Umri();
            Destroy(collision.transform.gameObject);
        }
    }
}
