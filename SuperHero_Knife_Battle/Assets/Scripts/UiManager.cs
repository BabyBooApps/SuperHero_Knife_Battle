using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

	public GameObject MenuScreen;
	public GameObject LevelSelectionScreen;
	public GameObject IngameScreen;
	public GameObject PauseScreen;
	public GameObject SettingsScreen;
	public GameObject LevelCompleteScreen;
	public GameObject LevelFailedScreen;
	public GameObject QuitScreen;
	public Text FrameRateText;

	float deltaTime = 0.0f;

	public Text LevelNum_Ingame;
	public Text LevelNum_GameOver;
	public Text LevelNum_Success;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		float fps = 1.0f / deltaTime;

		FrameRateText.text = "FrameRate :" + fps.ToString ();
	
	}

	public void SetScreen(GameObject Screen)
	{
		MenuScreen.SetActive (false);
		IngameScreen.SetActive (false);
		PauseScreen.SetActive (false);
		SettingsScreen.SetActive (false);
		LevelCompleteScreen.SetActive (false);
		LevelFailedScreen.SetActive (false);
		QuitScreen.SetActive (false);

		Screen.SetActive (true);
	}

	public void ActivateInGamescreen()
	{
		IngameScreen.SetActive (true);
	}

	public void SetLevelNumText(int levelNum)
	{
		LevelNum_Ingame.text = levelNum.ToString ();
		LevelNum_GameOver.text = (levelNum).ToString ();
		LevelNum_Success.text = (levelNum).ToString ();
	}
}
