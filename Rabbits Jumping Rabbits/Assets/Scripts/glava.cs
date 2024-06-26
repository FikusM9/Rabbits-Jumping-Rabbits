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
        if (collision.gameObject.CompareTag("Noge") && player.velocityBefore > collision.gameObject.transform.parent.gameObject.GetComponent<Player>().velocityBefore)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
