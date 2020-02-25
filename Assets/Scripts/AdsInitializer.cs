using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsInitializer : MonoBehaviour
{
    void Awake()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-6089465811738219~9072670587";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif

        //Initialize the Google Mobile Ads SDK
        MobileAds.Initialize(appId);
    }


}
