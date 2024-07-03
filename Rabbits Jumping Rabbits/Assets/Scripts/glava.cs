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
                if(MainMenu.lastDied == transform.parent.gameObject.GetComponent<Player>().teamNo)
                {
                    MainMenu.numberOfPlayers = 1;
                }
                else
                {
                    MainMenu.lastDied = transform.parent.gameObject.GetComponent<Player>().teamNo;
                    MainMenu.numberOfPlayers--;
                }
                Destroy(transform.parent.gameObject);
				collision.gameObject.transform.parent.gameObject.GetComponent<Player>().smashingDown = false;
            }

        }
        if (collision.gameObject.CompareTag("Jaje"))
        {
            Instantiate(blood, transform.position, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
            Destroy(transform.parent.gameObject);
            Destroy(collision.transform.gameObject);
            MainMenu.numberOfPlayers--;
        }
    }
}
