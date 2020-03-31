using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdsReward : MonoBehaviour {

    public StoreInformation storeInformation;
    public InstantiatePoop instantiatePoop;

    //Video Recompensado
    private RewardBasedVideoAd rewardBasedVideo;

    //Coins100
    public bool coins100 = false;
    public bool coins100Pulsado = false;
    public Button bTcoins100;
    public float timeLeftCoins100 = 10.0f;

    //Coins150
    public bool coins150 = false;
    public bool coins150Pulsado = false;
    public Button bTcoins150;
    public float timeLeftCoins150 = 1.0f;
    public bool done150 = false;

    //Coins200
    public bool coins250 = false;
    public bool coins250Pulsado = false;
    public Button bTcoins250;
    public float timeLeftCoins250 = 1.0f;
    public bool done250 = false;


    // Use this for initialization
    void Start()
    {
        storeInformation = GameObject.FindGameObjectWithTag("StoreInformation").GetComponent<StoreInformation>();
        if(SceneManager.GetActiveScene().name == "Scene1")
            instantiatePoop = GameObject.FindGameObjectWithTag("MainScript").GetComponent<InstantiatePoop>();


#if UNITY_ANDROID
        string appId = "ca-app-pub-7498255284251761~5601353189";
#elif UNITY_IPHONE
                    string appId = "ca-app-pub-3940256099942544~1458002511";
#else
                        string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

        this.RequestRewardBasedVideo();

    }

    //************ANUNCIO RECOMPENSADO FUNCTION*********************
    private void RequestRewardBasedVideo()
    {
        //Id del anuncio
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
               string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
               string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        /*   AdRequest request = new AdRequest.Builder()
             .AddTestDevice("F0FD2CC1C43C2DB9FDC6090CEB4AF21B")
             .Build(); */

        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    // Update is called once per frame
    void Update () {
        if (coins100)
        {
            timeLeftCoins100 -= Time.deltaTime;
            if (timeLeftCoins100 < 0)
            {
                bTcoins100.interactable = true;
                coins100 = false;
                timeLeftCoins100 = 10.0f;
            }
        }
        if (coins150)
        {
            timeLeftCoins150 -= Time.deltaTime;
            if (timeLeftCoins150 < 0)
            {
                bTcoins150.interactable = true;
                coins150 = false;
                timeLeftCoins150 = 10.0f;
            }
        }
        if (coins250)
        {
            timeLeftCoins250 -= Time.deltaTime;
            if (timeLeftCoins250 < 0)
            {
                bTcoins250.interactable = true;
                coins250 = false;
                timeLeftCoins100 = 10.0f;
            }
        }
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log("ENTRA 3");

        string type = args.Type;
        double amount = args.Amount;
        // Debug.Log("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);

        //Aqui cada vey que pulsamos el boton correspondiente (obtener 30 segundos o 100 coins, cuando el anuncio termine, se le dara la recompensa correspondiente)
        if (coins100Pulsado)
        {
            Debug.Log("ENTRA en +100");
            coins100Pulsado = false;
            coins100 = true;
            storeInformation.coins += 100;
            storeInformation.Save();
        }
        if (coins150Pulsado)
        {
            Debug.Log("ENTRA en +150");
            coins150Pulsado = false;
            coins150 = true;
            storeInformation.coins += 150;
            instantiatePoop.currentCoins += 150;
            storeInformation.Save();
        }
        if (coins250Pulsado)
        {
            Debug.Log("ENTRA en +250");
            coins250Pulsado = false;
            coins250 = true;
            storeInformation.coins += 250;
            instantiatePoop.currentCoins += 250;
            storeInformation.Save();
        }
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }


    private void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            Debug.Log("ENTRA mostrar anuncio");
            this.rewardBasedVideo.Show();
        }
        else
            MonoBehaviour.print("Reward based video ad is not ready yet");
    }

    public void shopButtonCoins()
    {
        if (!coins100)
        {
            coins100Pulsado = true;
            //esto se encarga de mostrar el anuncio 
            ShowRewardBasedVideo();
        }
    }

    public void shopButtonCoins150()
    {
        if (!coins150 && !done150)
        {
            done150 = true;
            coins150Pulsado = true;
            bTcoins150.interactable = false;
            bTcoins150.GetComponent<Image>().color = Color.black;
            //esto se encarga de mostrar el anuncio 
            ShowRewardBasedVideo();
        }
    }

    public void shopButtonCoins250()
    {
        if (!coins250 && !done250)
        {
            done250 = true;
            coins250Pulsado = true;
            bTcoins250.interactable = false;
            bTcoins250.GetComponent<Image>().color = Color.black;
            //esto se encarga de mostrar el anuncio 
            ShowRewardBasedVideo();
        }
    }
    //*************************************************************
}
