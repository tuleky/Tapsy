//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Ads : IUnityAdsListener
//{

//#if UNITY_IOS
//    private string gameId = "3257360";
//#elif UNITY_ANDROID
//    private string gameId = "3257361";
//#else
//    private string gameId = "";
//#endif
//    bool testMode = false;

//    public string myPlacementId = "rewardedVideo";

//    public ShowResult adWatchStatus = ShowResult.Failed;

//    public int lifeRewardAmount = 5;
   

//    // Start is called before the first frame update
//    public void Init()
//    {
//        Debug.Log("Ads are initializing");
//        Advertisement.AddListener(this);
//        Advertisement.Initialize(gameId, testMode);
//    }

//    public void LoadAd()
//    {
//        Debug.Log("Ad is loading");
//        Advertisement.Load(myPlacementId);
//    }

//    public void ShowAd()
//    {
        
//        if (Advertisement.IsReady())
//        {
//            Debug.Log("Ad is ready");
//            Advertisement.Show(myPlacementId);
//        }
//        Debug.Log("Ad is not ready");
//        LoadAd();
//    }

//    // Implement IUnityAdsListener interface methods:
//    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
//    {
//        // Define conditional logic for each ad completion status:
//        if (showResult == ShowResult.Finished)
//        {
//            // Reward the user for watching the ad to completion.
//            PlayerPrefs.SetInt("life", lifeRewardAmount);
//            Debug.Log("User rewarded with "+ lifeRewardAmount + " life!");
//            adWatchStatus = ShowResult.Finished;
//        }
//        else if (showResult == ShowResult.Skipped)
//        {
//            // Do not reward the user for skipping the ad.
//            adWatchStatus = ShowResult.Skipped;
//        }
//        else if (showResult == ShowResult.Failed)
//        {
//            Debug.Log("Ad failed : "+showResult);
//            adWatchStatus = ShowResult.Failed;
//        }
//    }

//    public void OnUnityAdsReady(string placementId)
//    {
//        // If the ready Placement is rewarded, show the ad:
//        if (placementId == myPlacementId)
//        {
//            Advertisement.Show(myPlacementId);
//        }
//        Debug.Log("Ad is not ready");
//    }

//    public void OnUnityAdsDidError(string message)
//    {
//        // Log the error.
//        Debug.Log("OnUnityAdsDidError :" + message);
//    }

//    public void OnUnityAdsDidStart(string placementId)
//    {
//        // Optional actions to take when the end-users triggers an ad.
//        Debug.Log("OnUnityAdsDidStart: " + placementId);
//    }

//    public int GetLifeRewardAmount()
//    {
//        return lifeRewardAmount;
//    }

//    public void SetLifeRewardAmount(int amount)
//    {
//        this.lifeRewardAmount = amount;
//    }
//}
