using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public PixelManager[] TotalPixels;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    private bool isFinished;
    private Transform camera;
    public Animator ImageAnimator;
    public int RemainPixels;
    private LevelGenerator Generator;
    private int PlayerLevel;
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
        
        RemainPixels= TotalPixels.Length;
    }
    public void CheckComplet(){
        isFinished=true;
        RemainPixels--;
        foreach (PixelManager pixel in TotalPixels)
        {
            if (pixel.isColored==false && RemainPixels>3){
                isFinished=false;
                return;
            }
        }
        if (RemainPixels<=3 ){
            foreach (PixelManager pixel in TotalPixels)
            {
                pixel.ColorThePixel();
            }
            StartCoroutine(Win());
        }
        if(isFinished){
            StartCoroutine(Win());
        }
    }
    IEnumerator Win(){
        camera.GetComponent<Animator>().SetInteger("State",1);
        ImageAnimator.SetInteger("State",1);
        yield return new WaitForSeconds(5);
        WinScreen.SetActive(true);
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball item in balls)
        {
            Destroy(item.gameObject);
        }
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");

        if (PlayerLevel>= Generator.maps.Length){

        }else{
            PlayerPrefs.SetInt("PlayerLevel",PlayerLevel+1);
        }
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
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }public void NextLevel(){
        if (PlayerLevel== 0){
            if (SceneManager.GetActiveScene().buildIndex+1>SceneManager.sceneCount){
                SceneManager.LoadScene(0);
            }else{
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }else{
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
