using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTime : MonoBehaviour
{
    public static GamePlayTime instance = null;
    private float gamePlayTime;
    public int gamePlayAmount;
    public bool canShowAds = true;

    private void Awake(){
        gamePlayAmount = 0;
        gamePlayTime = 0;

        if(instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update(){
        gamePlayTime += Time.deltaTime;

        if(gamePlayTime > 60){
            canShowAds = true;
            gamePlayTime = 0;
        }
    }

    public void UpdateShowAds()
    {
        //if (PlayerPrefs.GetInt("life") > 0)
        //{
        //    canShowAds = false;
        //}
        //else
        //{
        //    canShowAds = true;
        //}
    }

    public void ResetShowAds(){
        canShowAds = false;
        gamePlayTime = 0;
    }
}
