using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glava : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Noge"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
