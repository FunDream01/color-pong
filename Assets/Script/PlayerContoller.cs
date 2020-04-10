using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    // Start is called before the first frame update
    public float constantSpeed = 4f;
    public float SpeedIncrement = .1f;

    Vector2 LastTapPos;
    public float Speed;
    public GameObject CloneBall;
    public float MaxDistance;
    //Colors for the balls
    public Color[] Colors;
    GameObject MainBall;

    void Start()
    {
        MainBall = GameObject.FindGameObjectWithTag("myBallz");
        MainBall.GetComponent<Renderer>().material.SetColor("_Color", Colors[Random.Range(0, Colors.Length)]);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("myBallz"))
        {
            if (other.gameObject.GetComponent<Ball>().canClone == true)
            {
                GameObject clone;
                clone = Instantiate(CloneBall, other.transform.position, other.transform.rotation);
                other.gameObject.GetComponent<Ball>().canClone = false;
                clone.GetComponent<Renderer>().material.SetColor("_Color", Colors[Random.Range(0, Colors.Length)]);
                //clone.GetComponent<Ball>().move();
                other.gameObject.GetComponent<Ball>().Invoke("startCanCloning", 2);
            }
        }
    }
    private void FixedUpdate()
    {
        {
            constantSpeed += Time.deltaTime * SpeedIncrement;
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
            transform.Translate(new Vector3(-(delta * Speed), 0, 0));
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