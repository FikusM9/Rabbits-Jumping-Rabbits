using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaSmash : MonoBehaviour
{
    public float lifeTime;

    private float lifeTimer;
    void Start()
    {
        lifeTimer = lifeTime;
    }

    void FixedUpdate()
    {
        lifeTimer -= Time.fixedDeltaTime;
        if (lifeTimer < 0) Destroy(gameObject);
    }
}
