using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }


    [Tooltip("Game starts with that amount of time")]
    [SerializeField] private float gameStartingTime;
    public float remainingGameTime;

    [SerializeField] private Transform camTransform;

    [HideInInspector] float startingTimeExtender;
    [SerializeField] float currentTimeExtender;

    public bool isTimeStopped = false;
    private float gameTime = 0;
    private int gameLevel = 0;
    private enum GameLevel
    {
        Easy = 1, Medium, Hard, Insane,
    }

    [SerializeField] TextMeshProUGUI timerText;

    private void Awake()
    {
        remainingGameTime = gameStartingTime;

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private bool x;
    private void Update()
    {
        if (remainingGameTime > 0)
        {
            isTimeStopped = false;
            x = false;
        }
        else
        {
            isTimeStopped = true;
        }

        if (isTimeStopped && !x)
        {
            x = true;
            Debug.LogError("Disabling all gestures");
            //isTimeStopped = true;
            timerText.text = "Remaining Time: " + 0;
            //Time.timeScale = 0;
            GestureGiver.Instance.DisableAllGestures();
            camTransform.position = new Vector3(0f, 0f, 0f);
            camTransform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        
        if (!isTimeStopped)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            gameTime += Time.deltaTime;

            if (gameTime < 10)
            {
                gameLevel = (int)GameLevel.Easy;
            }
            else if (gameTime < 20)
            {
                gameLevel = (int)GameLevel.Medium;
            }
            else if (gameTime < 30)
            {
                gameLevel = (int)GameLevel.Hard;
            }
            else
            {
                gameLevel = (int)GameLevel.Insane;
            }

            remainingGameTime -= Time.deltaTime;

            float time = Mathf.Round(remainingGameTime * 100) / 100;

            timerText.text = "Remaining Time: " + time.ToString();
        }
    }


    public void ExtendGameTimer(float amount)
    {
        remainingGameTime += amount;
        isTimeStopped = false;
        GameManager.Instance.isGameStopped = false;
    }
}
