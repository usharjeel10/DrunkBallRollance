using UnityEngine;



public class InitializeAdsManager : MonoBehaviour
{
    public static InitializeAdsManager instance;

    public BannerPos myBannerPosition = BannerPos.Top;
    //public string myAdmobAppID = null;
    public string myAdmobBannerID1 = null;
    public string myAdmobBannerID2 = null;
    public string myAdmobIntersID = null;
    public string myAdmobRewardedID = null;
    //public string myUnityGameID = null;
    //public string myUnityinterID = null;
    //public string myUnityRewadedID = null;


    //string testAdmobAppID =      "ca-app-pub-5453965874578277~4128806100";
    string testAdmobBannerID2 ="ca-app-pub-3940256099942544/6300978111";
    string testAdmobIntersID  ="ca-app-pub-3940256099942544/1033173712";
    string testAdmobRewardedID = "ca-app-pub-3940256099942544/5224354917";

    #if UNITY_ANDROID
        string testAdmobBannerID1 = "ca-app-pub-3940256099942544/6300978111";
    #elif UNITY_IPHONE
        string testAdmobBannerID1 = "ca-app-pub-3940256099942544/2934735716";
    #else
        string testAdmobBannerID1 = "unexpected_platform";
    #endif

    public static bool check = false;
    public bool TestAds = false;

    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("RemoveAds"))
        {
            PlayerPrefs.SetInt("RemoveAds", 0);
        }

        AdsManager adsManager = AdsManager.Instance;
        adsManager.BannerPosition = myBannerPosition;

        if (!TestAds)
        {
            //adsManager.AdmobAppID = myAdmobAppID;
            adsManager.AdmobBannerID = myAdmobBannerID1;
            adsManager.RectBannerID = myAdmobBannerID2;
            adsManager.AdmobIntersID = myAdmobIntersID;
            adsManager.myAdmobRewardedID = myAdmobRewardedID;
            //adsManager.UnityinterID = myUnityinterID;
            //adsManager.UnityRewadedID = myUnityRewadedID;
            //adsManager.UnityGameID = myUnityGameID;
        }
        else
        {
            //adsManager.AdmobAppID = testAdmobAppID;
            adsManager.AdmobBannerID = testAdmobBannerID1;
            adsManager.RectBannerID = testAdmobBannerID2;
            adsManager.AdmobIntersID = testAdmobIntersID;
            adsManager.myAdmobRewardedID = testAdmobRewardedID;
            //adsManager.UnityinterID = myUnityinterID;
            //adsManager.UnityRewadedID = myUnityRewadedID;
            //adsManager.UnityGameID = myUnityGameID;
        }
        adsManager.InitializeAds();

    }
    void Start()
    {
        float repeatInterval = 30f;
        AdsManager.Instance.OnAdLoad += ShowBanner;
        InvokeRepeating("ShowAdmobInterstitial", repeatInterval, repeatInterval);
    }
    public void ShowBanner()
    {
        Debug.Log("ShowBanner");
        AdsManager.Instance.ShowBanner();
    }


    public void ShowBannerRectangle()
    {
        Debug.Log("ShowBannerRectangle");
        AdsManager.Instance.ShowBannerRectangle();
    }

    public void HideBanner()
    {
        Debug.Log("HideBanner");
        AdsManager.Instance.HideBanner();
    }

    public void HideBannerRectangle()
    {
        Debug.Log("HideBannerRectangle");
        AdsManager.Instance.HideAdmobBannerRectangle();
    }

    public void ShowAdmobInterstitial()
    {
        Debug.Log("ShowAdmobInterstitial");
        AdsManager.Instance.ShowAdmobInterstitial();
    }

    public void ShowAdmobRewarded()
    {
        if (AdsManager.Instance.RewardIsAvailable)
        {
            Debug.Log("ShowAdmobRewarded");
            AdsManager.Instance.ShowAdmobRewarded(0);
        }
    }

    //public void ShowUnityInters()
    //{

    //    Debug.Log("ShowUnityInters");
    //    AdsManager.Instance.ShowUnityIntersAd();
    //}

    //public void ShowUnityRewarded()
    //{
    //    if (AdsManager.Instance.unityRewardReady)
    //    {
    //        Debug.Log("ShowUnityRewarded");
    //        AdsManager.Instance.ShowUnityRewardedAd(0);
    //    }
    //}
   
    public void HideAllAds()
    {
        Debug.Log("HideAllAds");
        AdsManager.Instance.HideAllAds();
    }

    public void RemoveAds()
    {
        Debug.Log("Removeads");
        PlayerPrefs.SetInt("RemoveAds", 1);
        AdsManager.Instance.HideAllAds();
    }

}
