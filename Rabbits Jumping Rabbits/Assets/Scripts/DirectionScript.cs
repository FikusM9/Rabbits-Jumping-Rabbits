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
    public float Angle = 0;
    public float AngleSpeed;
    private int Smer = 1;
    public float distance;
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
        if (Angle > 160) Smer = -1;
        if (Angle < 20) Smer = 1;

        Transform.position = new Vector3(Mathf.Cos(Angle / 180 * 3.141f) * distance, Mathf.Sin(Angle / 180 * 3.141f) *distance, 0) + PlayerTransform.position;

        Transform.rotation = Quaternion.AngleAxis(Angle + 30, new Vector3(0, 0, 1));

    }



}
