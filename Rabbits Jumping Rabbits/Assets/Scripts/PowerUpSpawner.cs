using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerups;
    public Transform[] spawnPoints;
    public float spawnTime;

    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
    }


    void FixedUpdate()
    {
        if (spawnTimer > 0) spawnTimer -= Time.fixedDeltaTime;
        else
        {
            int spawnPoint = Random.Range(0, spawnPoints.Length);
            int powerup = Random.Range(0, powerups.Length);
            Instantiate(powerups[powerup], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            spawnTimer = spawnTime;

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
