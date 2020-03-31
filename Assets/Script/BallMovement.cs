using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public float Speed = 30;


	// Use this for initialization
	void Start () {
        int Dicection = Random.Range(0, 2);//0-1
        if (Dicection == 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * Speed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * Speed;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
