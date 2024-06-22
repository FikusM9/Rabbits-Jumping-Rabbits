using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class uvo : MonoBehaviour
{
    public int len;
    public LineRenderer lr;
    public Vector3[] segpos;
    public Transform targetdir;
    public float targetdist;
    public float smoothspeed;
    public float wigglespeed;
    public float wigglemagnitude;
    public float trailspeed;
    public Transform wiggledir;
    public float angle;
    public Player player;


    private Vector3[] segspeed;

    private void Start()
    {
        lr.positionCount = len;
        segpos = new Vector3[len];
        segspeed = new Vector3[len];
        ResetPos();

    }



    private void FixedUpdate()
    {
        wiggledir.localRotation = Quaternion.Euler(0, angle + Mathf.Sin(Time.time * wigglespeed) * wigglemagnitude, 0);
        segpos[0] = targetdir.position+targetdir.right;
        segpos[0].z = 2;
        if (!player.IsGrounded())
        {
            for (int i = 1; i < segpos.Length; i++)
            {
                segpos[i] = Vector3.SmoothDamp(segpos[i], segpos[i - 1] + (segpos[i] - segpos[i - 1]).normalized * targetdist , ref segspeed[i], smoothspeed);
                segpos[i].z = 2;
            }
        }
        lr.SetPositions(segpos);
        if (player.IsGrounded())
        {
            for (int i = 1; i < segpos.Length; i++)
            {
                 segpos[i] = Vector3.SmoothDamp(segpos[i], segpos[i - 1] + (segpos[i] - segpos[i - 1] + new Vector3(0, 0.1f, 0)).normalized * targetdist , ref segspeed[i], smoothspeed);
            }
        }
    }

    public void ResetPos()
    {
        segpos[0] = targetdir.position;
        segpos[0].z = 2;
        for (int i = 1; i < len; i++)
        {
            segpos[i] = segpos[i - 1] + new Vector3(0,0.1f,0);
        }
        lr.SetPositions(segpos);
    }
}
