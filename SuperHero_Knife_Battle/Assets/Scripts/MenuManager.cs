using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public GameObject ThemeSelectionScreen;
	public GameObject GameScreen;
	public GameObject GameOverScreen;
	public GameObject MenuScreen;

	public GameObject GameContinueScreen;

	public GameObject UnlockThemeScreen;
	public GameObject VictoryScreen;

	public GameObject RatingScreen;
    public GameObject PrivacyPolicyScreen;

    public GameObject StoreScreen;
    public GameObject CongratulationsScreen;

    public GameObject SplashScreen;



	// Use this for initialization
	void Start () {
       // PlayerPrefs.DeleteAll();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public void ActivateGameOverScreen()
	{
		GameScreen.SetActive (false);
		GameOverScreen.SetActive (true);
		GameContinueScreen.SetActive (false);
	}

	public void ActivateGameContinueScreen()
	{
		GameContinueScreen.SetActive (true);
	}

	public void ActivateGameScreen()
	{
		GameScreen.SetActive (true);
		ThemeSelectionScreen.SetActive (false);
	}

	public void ActivateMenuScreenFromStageFailed()
	{
		//MenuScreen.SetActive (true);
		ThemeSelectionScreen.SetActive(true);
		GameOverScreen.SetActive (false);
	}

	public void ActivateVictoryScreen()
	{
		VictoryScreen.SetActive (true);
		GameScreen.SetActive (false);
	}

	public void ContinueAfterGameOver()
	{
		GameContinueScreen.SetActive (false);
	}

	public void ActivateUnlockThemeScreen(bool status)
	{
		UnlockThemeScreen.SetActive (status);
	}


	public void ActivateRatingScreen()
	{
		RatingScreen.SetActive (true);
	}

    public void SetPrivacyPolicyScreen(bool status)
    {
        PrivacyPolicyScreen.SetActive(status);
    }

    public void ActivateStoreScreen(bool status)
    {
        StoreScreen.SetActive(status);
    }

    public void ActivateCongratulationsScreen(bool status)
    {
        CongratulationsScreen.SetActive(status);
    }

    public void DeactivateSplashScreen()
    {
        SplashScreen.SetActive(false);
    }

}
