using UnityEngine;
using UnityEngine.Advertisements;

public class Unity_Ads_Manager : MonoBehaviour, IUnityAdsInitializationListener
{
    public Banner_Unity_Ads banner_Unity_Ads;
    public Interstitials_Unity_Ads interstitials_Unity_Ads;
    public Reward_Video_Unity_Ads reward_Video_Unity_Ads;


    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;



    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        banner_Unity_Ads.LoadBanner();
        interstitials_Unity_Ads.LoadAd();
        reward_Video_Unity_Ads.LoadAd();

        banner_Unity_Ads.ShowBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    
}
