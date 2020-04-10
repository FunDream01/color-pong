using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float BallSpeed = 5f;
    public float sx = 1;
    [HideInInspector]
    public bool StartMoving = false;
    //[HideInInspector]
    public bool canClone = false;
    public float DelayTime;
    public float constantSpeed = 4f;
    public PlayerContoller playerMngr;

    private void Start()
    {
        Time.timeScale=1;
        Invoke("startCanCloning", DelayTime);
        move();
    }
    
    public void move()
    {
        sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = Random.Range(0, 2) == 0 ? -1 : 1;
        GetComponent<Rigidbody>().velocity = new Vector3(BallSpeed*sx, BallSpeed , 0f);
    } 
    private void Update()
    {
        ConstantVelocity();
        CastRay();
        // GetComponent<Rigidbody>().velocity *= BallSpeed*100;
        //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
        /*if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale=1;
        }*/
    }
    public void ConstantVelocity(){
        if (playerMngr == null)
        {
            playerMngr = FindObjectOfType<PlayerContoller>();
        }
        if (playerMngr != null)
        {
            constantSpeed = playerMngr.constantSpeed;
            Vector3 cvel = GetComponent<Rigidbody>().velocity;
            Vector3 tvel = cvel.normalized * constantSpeed;
            GetComponent<Rigidbody>().velocity = tvel * constantSpeed;
        }
    }
    public void startCanCloning() 
    {
        canClone = true;
    }
    public void CastRay(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.back), out hit, 500)){
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.back) * hit.distance, Color.red);
            if (hit.transform.tag == "Pixel"){
                if (hit.transform.GetComponent<PixelManager>() == null){
                    return;
                }
                if (hit.transform.GetComponent<PixelManager>().isColored){
                    return;
                }
                else{
                    hit.transform.GetComponent<PixelManager>().ColorThePixel();
                }
            }
        }
    }
    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag(Tag.CubeTag)){
            //move();
        }
        else if (other.gameObject.CompareTag("Player")){
            Vector2 vel;
            vel.x = GetComponent<Rigidbody>().velocity.x;
            vel.y = (GetComponent<Rigidbody>().velocity.y / 2.0f) + (other.collider.attachedRigidbody.velocity.y / 3.0f);
            GetComponent<Rigidbody>().velocity = vel;
        }
        else if (other.gameObject.CompareTag(Tag.Destroy)){
            LevelManager.instance.CkeckLose();
            Destroy(this.gameObject);
        }
    }
}
