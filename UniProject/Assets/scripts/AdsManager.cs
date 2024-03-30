using UnityEngine;
using System;
using GoogleMobileAds.Api;
using System.Collections;
using UnityEngine.Advertisements;
using GoogleMobileAds.Common;

public enum BannerPos
{
    Top = 0,
    Bottom = 1
};

public enum RewardType
{
    AddCoins,
    DoubleReward,
    LevelSkip
}

public class AdsManager : MonoBehaviour
{
    #region Instance

    // Static singleton instance
    private static AdsManager instance;
    public static AdsManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("AdsManager");
                instance = obj.AddComponent<AdsManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    #endregion

    public BannerPos BannerPosition = BannerPos.Bottom;
    public string AdmobBannerID = null;
    public string RectBannerID = null;
    public string AdmobIntersID = null;
    public string myAdmobRewardedID = null;
    //  public string UnityGameID = null;
    // public string UnityinterID = null;
    //  public string UnityRewadedID = null;
    //  public bool unityRewardReady = false;

    private BannerView bannerView1 = null;
    private BannerView rectBannerView = null;
    private InterstitialAd interstitial = null;
    private RewardedAd rewardedAd = null;


    static bool isInitialized = false;



    public static bool DoubleRewards;

    RewardType rewardType;

    public event Action OnAdLoad;


    public void InitializeAds()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            try
            {
                MobileAds.Initialize(initStatus => { });
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }

            // Listen to application foreground and background events.
            AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
            Invoke("ShowAppOpen", 3);

            StartCoroutine(RequestBanner(1));
            StartCoroutine(RequestRectBanner(2));
            StartCoroutine(RequestInterstitial(3));
            StartCoroutine(RequestRewardBasedVideo(5));

            // Load an app open ad when the scene starts

            // StartCoroutine(InitializeUnityAds(8));
            //  StartCoroutine(LoadUnityIntersAd(10));
            //  StartCoroutine(LoadUnityRewardedAd(12));
        }
    }

    
    /// <summary>
    /// Use this Method GiveRewards() to give rewards to user 
    /// 0 is for double reward on level complete
    /// 1 is for skip level
    /// 2 is for free cash e.g. 2000 cash
    /// </summary>
    private void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);
        if (state == AppState.Foreground)
        {
        }
    }
    void GiveRewards()
    {
        Debug.Log("GiveRewards");
        switch (rewardType)
        {
            case RewardType.AddCoins:
               

                break;
            case RewardType.DoubleReward:
                
                break;
            case RewardType.LevelSkip:


                break;
        }
    }



    public bool Restart, Home, Next;






    private void ShowAppOpen( )
    {


        Debug.Log("hey");

        OnAppStateChanged(AppState.Foreground);


    }



    public void HideAllAds()
    {
        HideBanner();
        HideAdmobBannerRectangle();
    }

    #region Admob Banner Ads
    private IEnumerator RequestBanner(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("RequestBanner");
        try
        {
            string adUnitId1 = AdmobBannerID;

            OnAdLoad();
            if (BannerPosition == BannerPos.Top)
            {
                bannerView1 = new BannerView(adUnitId1, AdSize.Banner, AdPosition.Top);
            }
            else
            {
                bannerView1 = new BannerView(adUnitId1, AdSize.Banner, AdPosition.Bottom);
            }

            // Create an e1mpty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the banner with the request.
            bannerView1.LoadAd(request);
            bannerView1.Hide();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }

    }
    private IEnumerator RequestRectBanner(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("RequestRectBanner");
        try
        {
            string adUnitId2 = RectBannerID;
            rectBannerView = new BannerView(adUnitId2, AdSize.MediumRectangle, AdPosition.Bottom);

            // Create an e1mpty ad request.
            AdRequest request2 = new AdRequest.Builder().Build();
            rectBannerView.LoadAd(request2);
            rectBannerView.Hide();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            HideBanner();
            HideAdmobBannerRectangle();
            try
            {
                if (bannerView1 != null)
                    bannerView1.Show();
                else
                    StartCoroutine(RequestBanner(1));
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }
    public void ShowBannerRectangle()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            HideBanner();
            HideAdmobBannerRectangle();
            try
            {
                if (rectBannerView != null)
                    rectBannerView.Show();
                else
                    StartCoroutine(RequestRectBanner(1));
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }

    public void HideBanner()
    {
        if (bannerView1 != null)
        {
            try
            {
                bannerView1.Hide();
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }
    public void HideAdmobBannerRectangle()
    {
        if (rectBannerView != null)
        {
            try
            {
                rectBannerView.Hide();
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }

    public void DestroyAdmobBannerTop()
    {
        try
        {
            if (bannerView1 != null)
                bannerView1.Destroy();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
    public void DestroyAdmobRectangle()
    {
        try
        {
            if (rectBannerView != null)
                rectBannerView.Destroy();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }


    #endregion

    #region Admob Interstitial Ads

    private IEnumerator RequestInterstitial(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("RequestInterstitial");
        try
        {
            string adUnitId = AdmobIntersID;

            // Clean up interstitial before using it
            if (interstitial != null)
            {
                interstitial.Destroy();
            }

            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            //this.interstitial.OnAdLoaded += HandleOnAdLoaded;
            //// Called when an ad request failed to load.
            //this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            //// Called when an ad is shown.
            //this.interstitial.OnAdOpening += HandleOnAdOpened;
            //// Called when the ad is closed.
            this.interstitial.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }


    }

    public void ShowAdmobInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            try
            {
                if (interstitial != null && interstitial.IsLoaded())
                {
                    HideBanner();
                    HideAdmobBannerRectangle();
                    interstitial.Show();
                    StartCoroutine(RequestInterstitial(3));
                }
                else
                {
                   
                    HideBanner();
                    HideAdmobBannerRectangle();
                    ShowBanner();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }


        }
        else
        {
           
        }
    }

    public void HideInterstitial()
    {
        Debug.Log("Hide interstitial");
    }

    public void DestroyInterstitial()
    {
        try
        {
            if (interstitial != null)
                interstitial.Destroy();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    #region Interstitial CallBacks
    //public void HandleOnAdLoaded(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLoaded event received");
    //}

    //public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
    //    //                    + args.Message);
    //}

    //public void HandleOnAdOpened(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdOpened event received");
    //}

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
      
        ShowBanner();

        MonoBehaviour.print("HandleAdClosed event received");




    }

    //public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLeavingApplication event received");
    //}
    #endregion

    #endregion

    #region Admob Rewarded Ads

    private IEnumerator RequestRewardBasedVideo(float time)
    {

        yield return new WaitForSeconds(time);
        Debug.Log("RequestRewardBasedVideo");
        try
        {
            string adUnitId = myAdmobRewardedID;


            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
            }

            this.rewardedAd = new RewardedAd(myAdmobRewardedID);

            // Called when an ad request has successfully loaded.
            this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(request);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
    public bool RewardIsAvailable
    {

        get
        {
            try
            {
                if (rewardedAd != null)
                    return rewardedAd.IsLoaded();
                //else
                //    StartCoroutine(RequestRewardBasedVideo(2));
                return false;
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
                return false;
            }
        }
    }

    // Always Check if rewarded is available before calling this method
    public void ShowAdmobRewarded(RewardType rewardType)
    {
        try
        {

            this.rewardType = rewardType;
           // createline.instance.LoadingAd.SetActive(true);
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
                StartCoroutine(RequestRewardBasedVideo(5));
            }
            else
            {
               // createline.instance.LoadingAd.SetActive(false);
                ////show unity ad on admob fail
                //if (unityRewardReady)
                //{
                //    unityRewardReady = false;
                //    // Then show the ad:
                //    Advertisement.Show(UnityRewadedID, this);
                //}

                StartCoroutine(RequestRewardBasedVideo(5));
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }

    }

    #region RewardedVideo CallBacks NEW API

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    //public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    //{
    //    MonoBehaviour.print(
    //        "HandleRewardedAdFailedToLoad event received with message: "
    //                         + args.Message);
    //}

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        try
        {
           // createline.instance.LoadingAd.SetActive(false);
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print(
                "HandleRewardedAdRewarded event received for "
                            + amount.ToString() + " " + type);


            GiveRewards();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }

    }
    #endregion

    #endregion


    #region UnityAds


    // /// <summary>
    // /// Unity initialization
    // /// </summary>

    // private IEnumerator InitializeUnityAds(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     Debug.Log("InitializeUnityAds");

    //     try
    //     {
    //         bool _testMode = false;
    //         Advertisement.Initialize(UnityGameID, _testMode, this);
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }
    // }


    // public void OnInitializationComplete()
    // {
    //     Debug.Log("Unity Ads initialization complete.");
    // }

    // public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    // {
    //     Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    // }

    // /// <summary>
    // /// Unity interstitial
    // /// </summary>



    // // Load content to the Ad Unit:
    // private IEnumerator LoadUnityIntersAd(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     Debug.Log("LoadUnityIntersAd");

    //     try
    //     {
    //         // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
    //         Debug.Log("Loading Ad: " + UnityinterID);
    //         Advertisement.Load(UnityinterID, this);
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }
    // }

    // // Show the loaded content in the Ad Unit:
    // public void ShowUnityIntersAd()
    // {
    //     if (PlayerPrefs.GetInt("RemoveAds") == 0)
    //     {
    //         try
    //         {
    //             // Note that if the ad content wasn't previously loaded, this method will fail
    //             Debug.Log("Showing Ad: " + UnityinterID);
    //             Advertisement.Show(UnityinterID, this);
    //         }
    //         catch (Exception e)
    //         {
    //             Debug.LogException(e, this);
    //         }
    //     }
    // }



    // /// <summary>
    // /// Unity rewarded
    // /// </summary>

    // // Load content to the Ad Unit:
    //private IEnumerator LoadUnityRewardedAd(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     Debug.Log("LoadUnityRewardedAd");

    //     try
    //     {
    //         // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
    //         Debug.Log("Loading Ad: " + UnityRewadedID);
    //         Advertisement.Load(UnityRewadedID, this);
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }
    // }

    // // Implement Load Listener and Show Listener interface methods: 
    // public void OnUnityAdsAdLoaded(string adUnitId)
    // {
    //     try
    //     {
    //         // Optionally execute code if the Ad Unit successfully loads content.

    //         Debug.Log("Ad Loaded: " + adUnitId);

    //         if (adUnitId.Equals(UnityRewadedID))
    //         {
    //             unityRewardReady = true;
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }
    // }

    // // Implement a method to execute when the user clicks the button:
    // public void ShowUnityRewardedAd(RewardType rewardType)
    // {
    //     try
    //     {
    //         this.rewardType = rewardType;
    //         if (unityRewardReady)
    //         {
    //             unityRewardReady = false;
    //             // Then show the ad:
    //             Advertisement.Show(UnityRewadedID, this);

    //         }
    //         else 
    //         {
    //             // show admob rewarded if unity fail
    //             if (rewardedAd.IsLoaded())
    //             {
    //                 rewardedAd.Show();
    //                 StartCoroutine(RequestRewardBasedVideo(10));
    //             }
    //         }

    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }

    // }

    // // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    // public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    // {
    //     try
    //     {
    //         if (adUnitId.Equals(UnityRewadedID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
    //         {
    //             Debug.Log("Unity Ads Rewarded Ad Completed");
    //             // Grant a reward.

    //             GiveRewards();

    //             // Load another ad:]
    //             StartCoroutine(LoadUnityRewardedAd(2));
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }
    // }

    // // Implement Load and Show Listener error callbacks:
    // public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    // {
    //     Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    //     // Use the error details to determine whether to try to load another ad.
    // }

    // public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    // {

    //     // show admob interstitial on unity fail
    //     try
    //     {
    //         Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    //         // Use the error details to determine whether to try to load another ad.

    //         if (interstitial != null && interstitial.IsLoaded())
    //         {
    //             interstitial.Show();
    //             StartCoroutine(RequestInterstitial(5));
    //         }

    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }

    // }

    // public void OnUnityAdsShowStart(string adUnitId) { }
    // public void OnUnityAdsShowClick(string adUnitId) { }

    #endregion
}
