using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float BallSpeed = 5f;
    [HideInInspector]
    public bool StartMoving = false;
    //[HideInInspector]
    public bool canClone = false;
    public float DelayTime;
    float constantSpeed = 4f;
    private LevelManager levelManager;
    private void Start()
    {
        
        levelManager=FindObjectOfType<LevelManager>();
        Time.timeScale=0;
        Invoke("startCanCloning", DelayTime);
        move();
    }
    
    public void move()
    {
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = Random.Range(0, 2) == 0 ? -1 : 1;
        GetComponent<Rigidbody>().velocity = new Vector3(BallSpeed*sx, BallSpeed * 1 , 0f);
    } 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale=1;
        }
        ConstantVelocity();
        CastRay();
    }
    public void ConstantVelocity(){
    
        Vector3 cvel = GetComponent<Rigidbody>().velocity;
        Vector3  tvel = cvel.normalized * constantSpeed;
        GetComponent<Rigidbody>().velocity = tvel* constantSpeed;
    }
    public void startCanCloning() 
    {
        canClone = true;
    }
    public void CastRay(){
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
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("myBallz"))
        {
            //move();
        }
        if (other.gameObject.CompareTag("Player")){
            Vector2 vel;
            vel.x = GetComponent<Rigidbody>().velocity.x;
            vel.y = (GetComponent<Rigidbody>().velocity.y / 2.0f) + (other.collider.attachedRigidbody.velocity.y / 3.0f);
            GetComponent<Rigidbody>().velocity = vel;
        }
    }
}
