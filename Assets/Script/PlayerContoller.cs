using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 LastTapPos;
    public GameObject Player;
    public float Speed;
    public Rigidbody clonedAss;
    void Start()
    {
        Player = this.gameObject; 

        
    }

    // Update is called once per frame


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("myBallz"))
        {
           
            //clone.velocity = transform.TransformDirection(Vector3.forward * 10);
            if (collision.gameObject.GetComponent<Ball>().canClone == true)
            {

                Rigidbody clone;
                clone = Instantiate(clonedAss, collision.transform.position, collision.transform.rotation);
                clone.GetComponent<Ball>().moveAllAsses();
                clone.GetComponent<Ball>().StartMoving = true;
                //clone.GetComponent<Ball>().canClone = false;
                //collision.gameObject.GetComponent<Ball>().canClone=false;
            }
        }

    }



    void Update()
    {
        //Mouse Slider Controller :
        if (Input.GetMouseButton(0))
        {

            Vector2 curTapPos = Input.mousePosition;
            if (LastTapPos == Vector2.zero)
            {
                LastTapPos = curTapPos;
            }
            float delta = LastTapPos.x - curTapPos.x;
            LastTapPos = curTapPos;
            Player.transform.Translate(new Vector3((-delta * Speed),0 , 0));
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -8, 8);
            transform.position = pos;
        }
        
        
        
        
        
        if (Input.GetMouseButtonUp(0))
        {
            LastTapPos = Vector3.zero;

        }


        
    } 
}