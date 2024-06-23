using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class DirectionScript : MonoBehaviour
{
    public Transform Transform;
    public Transform PlayerTransform;
    public float AngleSpeed;
    public float distance;
    public int startAngle;

    private float Angle = 0;
    private int Smer = 1;

    void Start()
    {
        Angle = startAngle * Time.fixedDeltaTime * AngleSpeed;
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Angle > 170) Smer = -1;
        if (Angle < 10) Smer = 1;
        Angle += Smer * Time.fixedDeltaTime * AngleSpeed;

        Transform.position = new Vector3(Mathf.Cos(Angle / 180 * 3.141f) * distance, Mathf.Sin(Angle / 180 * 3.141f) *distance, 0) + PlayerTransform.position;

        Transform.rotation = Quaternion.AngleAxis(Angle + 30, new Vector3(0, 0, 1));

    }



}
