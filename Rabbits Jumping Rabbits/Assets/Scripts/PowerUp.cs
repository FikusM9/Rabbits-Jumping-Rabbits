using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private float life;
    public GameObject spawner;

    void Start()
    {
        life = spawner.GetComponent<PowerUpSpawner>().spawnTime;
    }

    void FixedUpdate()
    {
        if (life > 0) life -= Time.fixedDeltaTime;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
