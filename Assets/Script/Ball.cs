using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float BallSpeed = 5f;
    [HideInInspector]
    public bool moveAss = false;
    [HideInInspector]
    public bool canClone = false;
    public float DelayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("startCanCloning", DelayTime);
    
    }

    public void moveAllAsses()
    {
        
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = Random.Range(0, 2) == 0 ? -1 : 1;

        GetComponent<Rigidbody>().velocity = new Vector3(BallSpeed*sx, BallSpeed * sy , 0f);

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& moveAss == false)
        {
            moveAllAsses();
            moveAss = true;
        }

        
    }

    void startCanCloning() 
    {
        canClone = true;
    }
}
