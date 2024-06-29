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
    public float startDistance;
    public float maxAngle;
    public float minAngle;
    public float startMaxAngle;
    public float startMinAngle;
    public float Angle = 0;
    public float startAngleSpeed;

    private int Smer = 1;

    void Start()
    {
        startDistance = distance;
        startMaxAngle = maxAngle;
        startMinAngle = minAngle;
        startAngleSpeed = AngleSpeed;
        Angle = startAngle * Time.fixedDeltaTime * AngleSpeed;
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Angle > maxAngle) Smer = -1;
        if (Angle < minAngle) Smer = 1;
        Angle += Smer * Time.fixedDeltaTime * AngleSpeed;

        Transform.position = new Vector3(Mathf.Cos(Angle / 180 * 3.141f) * distance, Mathf.Sin(Angle / 180 * 3.141f) *distance, 0) + PlayerTransform.position;

        Transform.rotation = Quaternion.AngleAxis(Angle + 30, new Vector3(0, 0, 1));

    }



}
