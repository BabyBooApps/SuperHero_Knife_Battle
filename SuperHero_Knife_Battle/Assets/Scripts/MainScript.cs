using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour 
{
	GamePlayTest GamePlayMgr;
	MenuManager MenuMgr;
	StatsManager StatsMgr;
	//AdsManager AdsMgr;
	//ServicesManager ServicesMgr;
	//RewardedVideoScript RewardVideo;
    AudioManager AudioMgr;
	public List<LevelTest> LevelsList;
	LevelTest PresentLevel;
	 
	public int PresentLevelCount;
	public Text LevelCount_Txt;
	public Text UnlockAmount;
	public Text KnifeHitsInCurrentLevel;
	public Text CoinsTextgameOver;
	public Text StageCount_Txt;
	public int AvangerId;

	public LevelTest Level;

	public bool GameOver;
	public bool OnHold;
	public string WatchVideoCause;

	public Text PinsNum;
	public Text LevelCompleteTxt;


	ThemeSelectionScript ThemeScript;

	int RatingScreenCount;

    public Image NoAdsButton;

	public static MainScript Instance;

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
	}

    // Use this for initialization
    void Start () 
	{
      
         PlayerPrefs.SetInt("Privacy", 1);
       // PlayerPrefs.SetInt("NoAds", 0);
        GamePlayMgr = FindObjectOfType (typeof(GamePlayTest)) as GamePlayTest;
		MenuMgr = GetComponent<MenuManager> ();
		StatsMgr = GetComponent<StatsManager> ();
		//AdsMgr = GetComponent<AdsManager> ();
		//ServicesMgr = GetComponent<ServicesManager> ();
		//RewardVideo = GetComponent<RewardedVideoScript> ();
        AudioMgr = GetComponent<AudioManager>();
		PresentLevelCount = 1;
		GameOver = false;
		OnHold = false;
		PlayerPrefs.SetInt ("Level0", 1);
		PlayerPrefs.SetInt ("Level1", 1);

		RatingScreenCount = PlayerPrefs.GetInt ("Rating");
        //SetLevel (PresentLevelCount);
        //AdsMgr.RequestBanner();
        //SetPrivacyPolicyScreen();
        ActivatePrivacyPolicy();
        //AdsMgr.RequestAds();
        SetNoAdsButton();
        Invoke("ActivateMainScreenFromSplashScreen", 5.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
       // ShootPin();
	}

    public void ActivateMainScreenFromSplashScreen()
    {
        AudioMgr.PlayBgMusic();
        MenuMgr.DeactivateSplashScreen();
    }

    public void ShootPin()
    {
        if(PresentLevel != null)
        {
            PresentLevel.ShootPin();
        }
    }

	void SetLevel(int levelNum)
	{
//		PresentLevel = Instantiate (LevelsList [levelNum-1]) as LevelTest;
//		PresentLevel.name = "LEVEL : " + PresentLevelCount;
//		SetLevelCount (PresentLevelCount);
		//RewardVideo.LoadAdMobRewardedVideo();
		PresentLevel = Instantiate (Level) as LevelTest;
		PresentLevel.name = "LEVEL : " + PresentLevelCount;
		SetLevelCount (PresentLevelCount);
		StatsMgr.SetCoins (StatsMgr.GetCoins ());
    }
		
	void IncrementPresentLevel()
	{
		
		if (PresentLevelCount % 3 == 0) {
			AdsManager.Instance.interstitial.ShowAd();
			//AdsMgr.ShowInterstitialAd ();
		}
		Destroy (PresentLevel.gameObject);
		PresentLevelCount++;
		CheckVictoryCondition ();
	}

	public void SetRatingScreen()
	{
		RatingScreenCount = PlayerPrefs.GetInt ("Rating");
		RatingScreenCount++;
		if (RatingScreenCount >= 5) {
			MenuMgr.ActivateRatingScreen ();
			RatingScreenCount = 0;
		}
		PlayerPrefs.SetInt ("Rating", RatingScreenCount);
	}

	public void LevelSuccess()
	{
		IncrementPresentLevel ();
	}

	public void CheckVictoryCondition()
	{
		if (PresentLevelCount >= Level.Levels.Count) {
			MenuMgr.ActivateVictoryScreen ();
			
		} else {
			SetLevel (PresentLevelCount);
		}
	}

	public void SetGameOver()
	{  
		SetRatingScreen ();
		int Defeat = PlayerPrefs.GetInt ("Theme" + AvangerId.ToString ());

		if (PresentLevelCount > Defeat) {
			PlayerPrefs.SetInt ("Theme" + AvangerId.ToString (), PresentLevelCount);
		}

		Debug.Log ("DefeatLevel : " + PlayerPrefs.GetInt ("Theme" + AvangerId.ToString ()) / 33.0f);
		GameOver = true;
		OnHold = false;
		StageCount_Txt.text = PresentLevelCount.ToString ();
		PresentLevel.GameOver ();
		GamePlayMgr.SetGameover (false);
		Destroy (PresentLevel.gameObject);
		MenuMgr.ActivateGameOverScreen ();
		KnifeHitsInCurrentLevel.text = StatsMgr.GetKnifeHits ().ToString ();
		CoinsTextgameOver.text = StatsMgr.GetCoins ().ToString ();
		//ServicesMgr.PostScoreToLeaderboard (GPGSIds.leaderboard_knifehitleader, StatsMgr.GetKnifeHits ());
		StatsMgr.SetKnifeHits (0);
	}

	public void GotoHomeFromGame()
	{
		PresentLevelCount = 1;
		StatsMgr.SetKnifeHits (0);
		StatsMgr.SetCoins (StatsMgr.GetCoins ());
		OnHold = false;
		PresentLevel.DestroyPresentPin ();
		Destroy (PresentLevel.gameObject);
	}

	public bool GetOnHold()
	{
		return OnHold;
	}

	public void ActivateGameContinueScreen()
	{
		OnHold = true;
		MenuMgr.ActivateGameContinueScreen ();
	}

	public void ContinueAfterGameOver()
	{
		OnHold = false;
		MenuMgr.ContinueAfterGameOver ();
		PresentLevel.ContinueAfterGameOver ();
    }

	public void RestartLevel()
	{
		PresentLevelCount = 1;
	    SetLevel (PresentLevelCount);
		StatsMgr.SetKnifeHits (0);
	}
	public void GotoMenuFromGameOver()
	{
		PresentLevelCount = 1;
		MenuMgr.ActivateMenuScreenFromStageFailed ();
	}

	void SetLevelCount(int count)
	{
		LevelCount_Txt.text = PresentLevelCount.ToString();
	}

	public void ActivateGame(int id)
	{
		MenuMgr.ActivateGameScreen ();
		AvangerId = id;
		SetLevel (PresentLevelCount);
	}

	public void ActivateThemeUnlockScreen(bool status)
	{
		MenuMgr.ActivateUnlockThemeScreen (status);
	}

	public void SetThemeSelectionScript(ThemeSelectionScript script)
	{
		ThemeScript = script;
		UnlockAmount.text = ThemeScript.CoinsCost.ToString ();
	}

	public void UnlockTheme()
	{
		if (StatsMgr.GetCoins() >= ThemeScript.CoinsCost) 
		{
			int count = StatsMgr.GetCoins ();
			count -= ThemeScript.CoinsCost;
			StatsMgr.SetCoins (count);
			PlayerPrefs.SetInt ("Level" + ThemeScript.LevelId.ToString (), 1);
			ThemeScript.SetActiveStatus ();
			ActivateThemeUnlockScreen (false);
		}
	}

	public void SetWatchvideoCause(string cause)
	{
		WatchVideoCause = cause;
	}

	public string GetWatchVideoCause()
	{
		return WatchVideoCause;
	}

	public void AddRewards()
	{
		if (GetWatchVideoCause () == "Coins") {
			int count = StatsMgr.GetCoins ();
			count += 200;
			StatsMgr.SetCoins (count);
		} else if (GetWatchVideoCause () == "Continue") {
			ContinueAfterGameOver ();
		}
	}

    public void AddCoins(int coins)
    {
        int count = StatsMgr.GetCoins();
        count += coins;
        StatsMgr.SetCoins(count);
        MenuMgr.ActivateStoreScreen(false);
        MenuMgr.ActivateCongratulationsScreen(true);
    }

	public void FailedToAddRewards()
	{
		if (GetWatchVideoCause () == "Coins") {
			
		} else if (GetWatchVideoCause () == "Continue") {
			SetGameOver ();
			return;
		}
	}

	public void Rategame()
	{
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.woodhorseappsSuperHerosKnifeBattle");
	}
	public void Moregames()
	{
		Application.OpenURL ("https://play.google.com/store/apps/developer?id=Superwiz+games");
	}

	public void QuitApp()
	{
		Application.Quit ();
	}

	public void SetPinText(int pin)
	{
		PinsNum.text = pin.ToString();
	}

	public void SetLevelCompleteText(string text)
	{
		LevelCompleteTxt.text = text;
	}
    public void SetPrivacyPolicyScreen()
    {
        Debug.Log("Privacy policy 1111");
        if(PlayerPrefs.GetInt("Privacy") != 1)
        {
            Debug.Log("Privacy policy 222");
            MenuMgr.SetPrivacyPolicyScreen(true);
            //AdsMgr.HideBanner();
        }else
        {
            Debug.Log("Privacy policy 333");
            MenuMgr.SetPrivacyPolicyScreen(false);
            //ServicesMgr.SetGoogleSignIn();
            
        }
    }

    public void ActivatePrivacyPolicy()
    {
       // PlayerPrefs.SetInt("Privacy", 1);
       // SetPrivacyPolicyScreen();
        //AdsMgr.RequestAds();
        //AdsMgr.RequestBanner();
        //AdsMgr.ShowBanner();
        //ServicesMgr.SetGoogleSignIn();

    }

    public void SetNoAdsButton()
    {
        Color col = NoAdsButton.color;
        if(PlayerPrefs.GetInt("NoAds") != 1)
        {
            col.a = 1;
        }else
        {
            col.a = 0.5f;
        }

        NoAdsButton.color = col;
    }

    public void ActivateNoadsFeature()
    {
        //AdsMgr.HideBanner();
        PlayerPrefs.SetInt("NoAds", 1);
        SetNoAdsButton();
       
    }
}
