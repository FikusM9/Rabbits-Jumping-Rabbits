using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DirectionScript : MonoBehaviour
{
    public Transform Transform;
    public Transform PlayerTransform;
    public float Angle = 0;
    public float AngleSpeed;
    private int Smer = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Angle += Smer * Time.fixedDeltaTime * AngleSpeed;
        if (Angle > 3.141 - 0.2) Smer = -1;
        if (Angle < 0.2 ) Smer = 1;

        Transform.position = new Vector3(Mathf.Cos(Angle) * 3, Mathf.Sin(Angle)*3, 0) + PlayerTransform.position;



    }



}
