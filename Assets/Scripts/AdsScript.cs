using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdsScript : MonoBehaviour
{
    public StoreInformation storeInformation;
    //public InstantiatePoop instantiatePoop;

    //Video Recompensado
    private RewardBasedVideoAd rewardBasedVideo;

    //Intersticial
    private InterstitialAd interstitial;

    //Banner
    private BannerView bannerView;
    private AdSize adSize;

    //Coins100
    public bool coins100 = false;
    public bool coins100Pulsado = false;
    public Button bTcoins100;
    public float timeLeftCoins100 = 1.0f;

    //Coins150
    public bool coins150 = false;
    public bool coins150Pulsado = false;
    public Button bTcoins150;
    public float timeLeftCoins150 = 1.0f;

    //Coins200
    public bool coins250 = false;
    public bool coins250Pulsado = false;
    public Button bTcoins250;
    public float timeLeftCoins250 = 1.0f;

    //first Time Ad Banner
    private bool isFirstTime = true;


    // Use this for initialization
    void Start()
    { 
        storeInformation = GameObject.FindGameObjectWithTag("StoreInformation").GetComponent<StoreInformation>();
      //  instantiatePoop = GameObject.FindGameObjectWithTag("MainGameObject").GetComponent<InstantiatePoop>();
        // storeInformation.noAds = true; //TO TEST
        //*****************Cargar Anuncios********************
        //ID del telefono o cuenta de admob
        #if UNITY_ANDROID
                    string appId = "ca-app-pub-7498255284251761~5601353189";
        #elif UNITY_IPHONE
                    string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
                    string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
 


        //*****************************************************
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    //************** BANNER ********************************
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7498255284251761/4682737771";
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

        StartCoroutine(LoadBanner());
    }
    //**********************************************************

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    IEnumerator LoadBanner()
    {
        //Esta funcion se repetira siempre
        while (true)
        {
            if (isFirstTime)
            {
                Debug.Log("Primera vez ");
                bannerView.Hide();
                yield return new WaitForSeconds(3.0f);
            }

            isFirstTime = false;

            Debug.Log("SHOW BANNER ");
            // Show the banner with the request.
            bannerView.Show();

            yield return new WaitForSeconds(2.0f);

            Debug.Log("HIDE BANNER ");
            // Clean up banner ad before creating a new one.
            bannerView.Hide();

            yield return new WaitForSeconds(5.0f);

            yield return null;
        }
        yield return null;
    }

    //************ANUNCIO RECOMPENSADO FUNCTION*********************
    private void RequestRewardBasedVideo()
    {
        //Id del anuncio
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7498255284251761/2396605344";
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

    

    //************INTERSTICIAL FUNCTION*********************
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7498255284251761/5984690082";
#elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
                string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    //******************************************************

   

    public void startButton()
    {
      /*  instantiatePoop.done = false;

        instantiatePoop.isPressReset = false;
        instantiatePoop.timeReset = 11.0f;
        //storeInformation.Save();
        instantiatePoop.summary.SetActive(false);
        instantiatePoop.items.SetActive(true);
        instantiatePoop.buttons.SetActive(true);
        instantiatePoop.characters.SetActive(true);*/
        
    }
}
