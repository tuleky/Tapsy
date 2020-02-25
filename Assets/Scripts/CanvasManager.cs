using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    [SerializeField] private bool shakeUI;
    [SerializeField] private GameEvent continueToPlay;

    private void Start()
    {
        if (shakeUI)
        {
            GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        }
    }
    public void SceneChanger(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowRewardedAdOnAdManager()
    {
        if (GamePlayTime.instance.canShowAds)
        {
            AdManager.instance.ShowRewardedAd();
        }
    }

    public void SetDifficulty(int difficultyLevel)
    {
        DifficultyHolder.Instance.SetDifficulty(difficultyLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowAd()
    {
        AdManager.instance.ShowRewardedAd();
    }

    public void ContinueToPlay()
    {
        if (PlayerPrefs.GetInt("life") > 0)
        {
            GameManager.Instance.DecreaseLife();
            continueToPlay.Raise();
        }
    }

    public void NoAdClicked()
    {
        GameManager.Instance.NoAdClicked();
    }

    public void ExtendGameTime()
    {
        Timer.Instance.ExtendGameTimer(5f);
    }

    public void OpenWebSite(string siteDomain)
    {
        Application.OpenURL(siteDomain);
    }

    public void WriteHealth()
    {
        Debug.Log(PlayerPrefs.GetInt("life"));
    }
}
