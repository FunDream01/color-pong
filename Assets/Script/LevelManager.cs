using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public Transform imagepos;
    public Transform image;
    public static LevelManager instance;
    public PixelManager[] TotalPixels;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    private bool isFinished;
    private Transform camera;
    private Tweener CameraTween;
    private Tweener ImageTween;
    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        camera=Camera.main.transform;
        TotalPixels=FindObjectsOfType<PixelManager>();
    }
    public void CheckComplet(){
        isFinished=true;
        foreach (PixelManager pixel in TotalPixels)
        {
            if (pixel.isColored==false){
                isFinished=false;
                return;
            }
        }
        if(isFinished){
            Win();
        }
    }
    Vector3 Rot=new Vector3(0, 0, -10);
    void Win(){
        /*
        CameraTween=camera.DORotate(new Vector3(-95,0,0),2f).OnComplete(delegate{
            
            GameObject ImageColne =Instantiate(image.gameObject,imagepos.position,Quaternion.identity);
            
            GameObject parent =new GameObject();
            parent.transform.position=ImageColne.transform.position;
            ImageColne.transform.parent=parent.transform;
            ImageColne.transform.rotation=camera.rotation;
            ImageTween=parent.transform.DORotate(-Rot,1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
            WinScreen.SetActive(true);
        });*/
        WinScreen.SetActive(true);
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball item in balls)
        {
            Destroy(item.gameObject);
        }
        int PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        PlayerPrefs.SetInt("PlayerLevel",PlayerLevel+1);
    }
    public void CkeckLose()
    {
        Ball[] ball = FindObjectsOfType<Ball>();
        if (ball.Length<=1){
            Debug.Log("lose");
            LoseScreen.SetActive(true);
        }
    }
    public void RestartLevel(){
        
        Debug.Log("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
