using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBall : MonoBehaviour
{
    public float BallSpeed = 5f;
    [HideInInspector]
    public bool StartMoving = false;
    //[HideInInspector]
    public bool canClone = false;
    public float DelayTime;
    void Start()
    {
        Invoke("startCanCloning", DelayTime);
        move();
    }
    public void move()
    {
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = Random.Range(0, 2) == 0 ? -1 : 1;
        GetComponent<Rigidbody>().velocity = new Vector3(BallSpeed*sx, BallSpeed * sy , 0f);
    }
    void Update()
    {
        CastRay();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("myBallz"))
        {
            move();
        }
    }
    void startCanCloning() 
    {
        canClone = true;
    }
    void CastRay(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 500))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
            if (hit.transform.tag == "Pixel")
            {
                hit.transform.GetComponent<PixelManager>().ColorThePixel();
            }
        }
    }
}
