using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float BallSpeed = 5f;
    [HideInInspector]
    public bool StartMoving = false;
    [HideInInspector]
    public bool canClone = false;
    public float DelayTime;
    public LayerMask PlayerLayerMask;
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& StartMoving == false)
        {
            moveAllAsses();
            StartMoving = true;
        }
        if (StartMoving){CastRay();}
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("myBallz"))
        {
            moveAllAsses();
        }
        /*if (other.transform.CompareTag(Tag.CubeTag))
        {
            other.transform.GetComponent<PixelManager>().ColorThePixel();
        }*/
    }

    void startCanCloning() 
    {
        canClone = true;
    }
    void CastRay(){
        RaycastHit hit;
        Debug.Log("CastRay");
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 500))
            Debug.Log("CastRay");
        {
        Debug.Log("CastRay");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
            if (hit.transform.tag == "Pixel")
            {
                hit.transform.GetComponent<PixelManager>().ColorThePixel();
            }
        }
    }
}
