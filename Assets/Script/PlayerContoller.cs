using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 LastTapPos;
    public float Speed;
    public GameObject Clones;
    public float MaxDistance;
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("myBallz"))
        {
            //clone.velocity = transform.TransformDirection(Vector3.forward * 10);
            if (other.gameObject.GetComponent<Ball>().canClone == true)
            {

                GameObject clone;
                clone = Instantiate(Clones, other.transform.position, other.transform.rotation);
                clone.GetComponent<Ball>().move();
                clone.GetComponent<Ball>().StartMoving = true;
                other.gameObject.GetComponent<Ball>().Invoke("startCanCloning", 2);
                //clone.GetComponent<Ball>().canClone = false;
                //collision.gameObject.GetComponent<Ball>().canClone=false;
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Vector2 curTapPos = Input.mousePosition;
            if (LastTapPos == Vector2.zero)
            {
                LastTapPos = curTapPos;
            }
            float delta = LastTapPos.x - curTapPos.x;
            LastTapPos = curTapPos;
            transform.Translate(new Vector3((delta * Speed),0 , 0));
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -MaxDistance, MaxDistance);
            transform.position = pos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            LastTapPos = Vector3.zero;

        }
    } 
}