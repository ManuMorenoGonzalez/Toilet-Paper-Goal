using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdsBanner : MonoBehaviour {
    //Banner
    private BannerView bannerView;

    // Use this for initialization
    void Start () {
    #if UNITY_ANDROID
            string appId = "ca-app-pub-7498255284251761~5601353189";
    #elif UNITY_IPHONE
                    string appId = "ca-app-pub-3940256099942544~1458002511";
    #else
                        string appId = "unexpected_platform";
    #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        this.RequestBanner();

    }

    // Update is called once per frame
    void Update () {
		
	}

    //************** BANNER ********************************
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
        //adSize = new AdSize(320, 50);
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

    }
    //**********************************************************

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }
}
