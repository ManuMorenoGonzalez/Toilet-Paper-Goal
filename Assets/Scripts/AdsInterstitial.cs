using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdsInterstitial : MonoBehaviour {

    //Intersticial
    private InterstitialAd interstitial;

    // Use this for initialization
    void Start()
    {
        #if UNITY_ANDROID
                string appId = "ca-app-pub-7498255284251761~5601353189";
        #elif UNITY_IPHONE
                            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
                                string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        this.RequestInterstitial();
    }


    //************INTERSTICIAL FUNCTION*********************
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
                string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    //******************************************************

    // Update is called once per frame
    void Update () {
		
	}


    public void startButton()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            SceneManager.LoadScene(2);
        } else SceneManager.LoadScene(2);
    }
}
