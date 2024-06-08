using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelectionScript : MonoBehaviour {

	MainScript mainscript;
	StatsManager StatsMgr;
	public int LevelId;
	public bool CanActivateLevel;
	public int CoinsCost;

	public GameObject UnlockObj;
	public GameObject SelectObj;

	public Text DefeatLevel_Txt;


	void OnEnable()
	{
		float DefeatLevel = (PlayerPrefs.GetInt ("Theme" + LevelId.ToString ()) / 33.0f) * 100;
		double d = System.Math.Round (DefeatLevel,3);

		if (d >= 100.0f) {
			DefeatLevel_Txt.text = "DEFEATED";
			
		} else {
			DefeatLevel_Txt.text = d.ToString () + "%";
		}
		//Debug.Log ("DefeatLevel of  " + LevelId.ToString () + " is :" + d);
	}

	// Use this for initialization
	void Start () 
	{
		mainscript = FindObjectOfType (typeof(MainScript)) as MainScript;
		StatsMgr = FindObjectOfType (typeof(StatsManager)) as StatsManager;

		SetActiveStatus ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}



	public void ActivateLevel()
	{
		if (CanActivateLevel) {
			mainscript.ActivateGame (LevelId);
		} else {
			mainscript.ActivateThemeUnlockScreen (true);
			SetThemeInMainScript ();
		}
	}

	public void SetActiveStatus()
	{
		int statusvalue = PlayerPrefs.GetInt ("Level" + LevelId.ToString ());
		if (statusvalue == 0) {
			CanActivateLevel = false;
		} else {
			CanActivateLevel = true;
		}

		UnlockObj.SetActive (!CanActivateLevel);
		SelectObj.SetActive (CanActivateLevel);
			
	}

	public void SetThemeInMainScript()
	{
		mainscript.SetThemeSelectionScript (this);
	}


}
