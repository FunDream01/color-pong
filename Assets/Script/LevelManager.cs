using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public PixelManager[] TotalPixels;
    public GameObject WinScreen;
    private bool isFinished;
    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        WinScreen.SetActive(false);
        TotalPixels=FindObjectsOfType<PixelManager>();
    }

    void Update()
    {
        
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
            WinScreen.SetActive(true);
        }
    }
}
