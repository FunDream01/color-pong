using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using TMPro;

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
    public int PlayerLevel;
    public TextMeshProUGUI LevelIndicator;
    public GameObject analyticsPrefab;
    public GameObject FireflyFX;

    Analytics analytics;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        FireflyFX.SetActive(false);
        if (analytics == null)
        { Instantiate(analyticsPrefab); analytics = analyticsPrefab.GetComponent<Analytics>(); }
        StartCoroutine(analytics.waitToCall(analytics.LogLevelStarted, SceneManager.GetActiveScene().buildIndex));
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        camera = Camera.main.transform;
        TotalPixels = FindObjectsOfType<PixelManager>();
        Generator = FindObjectOfType<LevelGenerator>();
        RemainPixels = TotalPixels.Length;
        LevelIndicator.text = "LEVEL " + (PlayerLevel + 1);
    }
    public void CheckComplet()
    {
        isFinished = true;
        RemainPixels--;
        foreach (PixelManager pixel in TotalPixels)
        {
            if (pixel.isColored == false && RemainPixels > 3)
            {
                isFinished = false;
                return;
            }
        }
        if (RemainPixels <= 3)
        {
            foreach (PixelManager pixel in TotalPixels)
            {
                pixel.ColorThePixel();
            }
            StartCoroutine(Win());
        }
    }
    IEnumerator Win()
    {
        StartCoroutine(analytics.waitToCall(analytics.LogLevelSucceeded, SceneManager.GetActiveScene().buildIndex));
        camera.GetComponent<Animator>().SetInteger("State", 1);
        ImageAnimator.SetInteger("State", 1);
        FireflyFX.SetActive(true);
        yield return new WaitForSeconds(4);
        WinScreen.SetActive(true);
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball item in balls)
        {
            Destroy(item.gameObject);
        }
        PlayerLevel++;
        if (PlayerLevel >= Generator.maps.Length)
        {
            PlayerPrefs.SetInt("PlayerLevel", 0);
        }
        else
        {
            PlayerPrefs.SetInt("PlayerLevel", PlayerLevel);
        }
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
    }
    public void CkeckLose()
    {
        Ball[] ball = FindObjectsOfType<Ball>();
        if (ball.Length <= 1)
        {
            StartCoroutine(analytics.waitToCall(analytics.LogLevelFailed, SceneManager.GetActiveScene().buildIndex));
            Debug.Log("lose");
            LoseScreen.SetActive(true);
        }
    }
    public void RestartLevel()
    {

        Debug.Log("RestartLevel");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        Debug.Log("NextLevel");
        if (PlayerLevel == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 > 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
