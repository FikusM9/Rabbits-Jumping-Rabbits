using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class KriloScript : MonoBehaviour
{

    public Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Zamahni()
    {
        animator.Play("krila");
    }
}
