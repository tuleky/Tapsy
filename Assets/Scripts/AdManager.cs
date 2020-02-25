using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    [SerializeField] private GameEvent rewardedVideoWatched;
    public static AdManager instance = null;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RequestInterstitial();
        RequestRewardedAd();
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }


    public void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial ad before creating a new one.
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;

        // Load the interstitial with the request.
        interstitial.LoadAd(CreateAdRequest());
    }

    public void RequestRewardedAd()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Create new rewarded ad instance.
        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        //Load the rewarded ad with the request.
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

     #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion


    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
        RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region RewardedAd callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        RequestRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        rewardedVideoWatched.Raise();
        GamePlayTime.instance.ResetShowAds();
    }

    #endregion

    //public void AdWatchClicked()
    //{
    //    ads.ShowAd();
    //    if (ads.adWatchStatus.Equals(UnityEngine.Advertisements.ShowResult.Failed) || ads.adWatchStatus.Equals(UnityEngine.Advertisements.ShowResult.Finished))
    //    {
    //        ads.SetLifeRewardAmount(1);
    //        Timer.Instance.isTimeStopped = false;
    //        adMenu.SetActive(false);
    //        Timer.Instance.remainingGameTime = 5; // süreyi 5 saniye yap
    //    }
    //    else
    //    {
    //        NoAdClicked();
    //    }
    //}


    //public void NoAdClicked()
    //{
    //    adMenu.SetActive(false);
    //    //ads.SetLifeRewardAmount(5);
    //    if (!lifeDecreased)
    //    {
    //        life -= 1;

    //        if (life == 0)
    //        {
    //            //ads.ShowAd();
    //        }

    //        // life alanını 1 eksilt
    //        PlayerPrefs.SetInt("life", life);
    //        lifeDecreased = true;
    //        Debug.Log("PlayerPrefs life left: " + life);
    //    }
    //    if (GameManager.Instance.difficulty == 1) // hard mode
    //    {
    //        NewHardHighScore();
    //        highScore.text = PlayerPrefs.GetInt("hardHighScore").ToString();
    //    }
    //    else
    //    {
    //        NewEasyHighScore();
    //        highScore.text = PlayerPrefs.GetInt("easyHighScore").ToString();
    //    }
    //    Timer.Instance.isTimeStopped = true;
    //    menu.SetActive(true);
    //}
}
