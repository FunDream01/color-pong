using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBall : Ball
{
    void Start()
    {
        Invoke("startCanCloning", DelayTime);
        move();
    }
    void Update()
    {
        CastRay();
        ConstantVelocity();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("myBallz"))
        {
            move();
        }
    }
}
