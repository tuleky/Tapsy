using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using TMPro;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject adMenuWithAds;
    [SerializeField] private GameObject continueMenu;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScore;

    private int life = 5;
    private int easyHighScore = 0;
    private int hardHighScore = 0;
    bool lifeDecreased = false;
    private int score = 0;
    private int watchLeft = 3;  //How many times player can watch ads
    public bool isGameStopped;


    public int difficulty = 0;
    public float penaltyTime = 3f;

    public bool isPressed = false;
    public bool adWatchPressed = false;
   

    private void Start()
    {
        isGameStopped = false;
        if (DifficultyHolder.Instance != null)
        {
            difficulty = DifficultyHolder.Instance.difficulty;
        }
        else
        {
            //Debug.Log("Difficulty Holder couldn't found");
        }
        if (GestureGiver.Instance != null)
        {
            HighScore();
            GestureGiver.Instance.GenerateGesture();
        }
        LifeInit();
    }


    private void Update()
    {
        if (Timer.Instance != null)
        {
            Debug.Log("timer instance found");
            if (Timer.Instance.remainingGameTime <= 0 && !isGameStopped)
            {
                isGameStopped = true;
                if (GamePlayTime.instance.gamePlayAmount++ > 3)
                {
                    AdManager.instance.ShowInterstitial();
                }

                if (PlayerPrefs.GetInt("life") > 0)
                {
                    continueMenu.SetActive(true);
                }
                else if (GamePlayTime.instance.canShowAds)
                {
                    adMenuWithAds.SetActive(true);
                }
                else
                {
                    menu.SetActive(true);
                }
                //Timer.Instance.isTimeStopped = true;
                //life = PlayerPrefs.GetInt("life");
                // kullanıcı yandığı halde kaldığı yerden devam etmek isterse reklam göster devam ettir
            }
        }

        if (Input.touchCount == 0)
        {
            isPressed = false;
        }
    }

    private void HighScore()
    {
        if (PlayerPrefs.HasKey("easyHighScore"))
        {
            easyHighScore = PlayerPrefs.GetInt("easyHighScore");
        }
        else
        {
            PlayerPrefs.SetInt("easyHighScore", easyHighScore);
        }

        if (PlayerPrefs.HasKey("hardHighScore"))
        {
            hardHighScore = PlayerPrefs.GetInt("hardHighScore");
        }
        else
        {
            PlayerPrefs.SetInt("hardHighScore", hardHighScore);
        }
    }

    private void NewEasyHighScore()
    {
        easyHighScore = PlayerPrefs.GetInt("easyHighScore");
        if (score > easyHighScore)
        {
            PlayerPrefs.SetInt("easyHighScore", score);
        }
       
    }

    private void NewHardHighScore()
    {
        hardHighScore = PlayerPrefs.GetInt("hardHighScore");
        if (score > hardHighScore)
        {
            PlayerPrefs.SetInt("hardHighScore", score);
        }
        
    }

    private void LifeInit()
    {
        if (PlayerPrefs.HasKey("life"))  //life anahtarıyla kaydedilmiş bir veri var mı ?
        {
            life = PlayerPrefs.GetInt("life"); // life anahtarıyla kaydedilmiş veriyi getir
            Debug.Log("PlayerPrefs has life: " + life);
            if (life < 0)
            {
                Debug.Log("life is negative!? Adding life " + life);
                PlayerPrefs.SetInt("life", 1);
            }
        }
        else
        {
            Debug.Log("There is no life, adding life");
            PlayerPrefs.SetInt("life", life); // life anahtarıyla totalScore verisini sakla
        }
    }

    public void IncreaseLife()
    {
        int life = PlayerPrefs.GetInt("life");
        life++;
        PlayerPrefs.SetInt("life", life);
    }

    public void DecreaseLife()
    {
        int lifeNumber = PlayerPrefs.GetInt("life");
        lifeNumber--;
        PlayerPrefs.SetInt("life", lifeNumber);
    }

    public void CorrectMove()
    {
        if (Timer.Instance.remainingGameTime > 0)
        {
            IncreaseScore();
            CameraShaker.Instance.ShakeOnce(1f, 0.6f, 0.1f, 0.4f);
            Timer.Instance.ExtendGameTimer(2f);
            GestureGiver.Instance.GenerateGesture();
        }
    }

    public void WrongMove()
    {
        if (Timer.Instance.remainingGameTime > 0)
        {
            Timer.Instance.remainingGameTime -= GameManager.Instance.penaltyTime;
            CameraShaker.Instance.ShakeOnce(3f, 2f, 0.1f, 0.3f);
        }
    }

    private void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void NoAdClicked()
    {
        //ads.SetLifeRewardAmount(5);
        if (!lifeDecreased)
        {
            life -= 1;

            if (life == 0)
            {
                //ads.ShowAd();  yerine AdManager.instance.ShowRewardedAd();
            }

            // life alanını 1 eksilt
            PlayerPrefs.SetInt("life", life);
            lifeDecreased = true;
            Debug.Log("PlayerPrefs life left: " + life);
        }
        if (GameManager.Instance.difficulty == 1) // hard mode
        {
            NewHardHighScore();
            highScore.text = PlayerPrefs.GetInt("hardHighScore").ToString();
        }
        else
        {
            NewEasyHighScore();
            highScore.text = PlayerPrefs.GetInt("easyHighScore").ToString();
        }
        Timer.Instance.isTimeStopped = true;
        menu.SetActive(true);
    }
}
