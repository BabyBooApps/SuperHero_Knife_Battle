using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public Unity_Ads_Manager Unity_Ads;
    public  BannerViewController banner;
    public  InterstitialAdController interstitial;
    public RewardedAdController RewardAd;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

       

    }
    // Start is called before the first frame update
    void Start()
    {
        if(interstitial != null)
        {
            interstitial.LoadAd();
        }
       
        //banner.LoadAd();
        if(RewardAd != null)
        {
            RewardAd.LoadAd();
        }
       
        //banner.ShowAd();

        /* MobileAds.Initialize((InitializationStatus initStatus) =>
         {
             if (initStatus.ToString() == "Initialized")
             {
                 Debug.Log("Ads Initialized Successfully!!!");
                  // Initialization was successful, you can proceed with loading ads.
                  interstitial.LoadAd();
                 banner.LoadAd();
                 RewardAd.LoadAd();
             }
             else
             {
                  // Initialization failed, handle the error or inform the user.
                  Debug.LogError("Initialization failed: " + initStatus.ToString());
             }
         });*/



        // banner.ShowAd();
    }

    public void On_Admob_Banner_View_Failed_To_Show()
    {
        Unity_Ads.banner_Unity_Ads.ShowBannerAd();
    }

    public void On_Admob_Interstitials_Failed_To_Show()
    {
        Unity_Ads.interstitials_Unity_Ads.ShowAd();
    }

    public void On_Admob_Reward_Video_Failed_To_Show()
    {
        Unity_Ads.reward_Video_Unity_Ads.ShowAd();
    }
}
