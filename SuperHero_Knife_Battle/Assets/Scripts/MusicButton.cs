using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour {
	public GameObject OnButton;
	public GameObject OffButton;

	public string ButtonType;
	AudioManager AudioMgr;

	void OnAwake()
	{
		
	}

	// Use this for initialization
	void Start () {
		AudioMgr = FindObjectOfType (typeof(AudioManager)) as AudioManager;
		SetButton ();
		
		}
	
	// Update is called once per frame
	void Update () 
	{
		
	}



	void SetButton()
	{
		int value = PlayerPrefs.GetInt (ButtonType);
		if (value == 0) {
			value = 2;
			PlayerPrefs.SetInt (ButtonType, value);
		}
		if (value == 1) {
			OnButton.SetActive (false);
			OffButton.SetActive (true);
		} else if (value == 2) {
			OnButton.SetActive (true);
			OffButton.SetActive (false);
		}

		AudioMgr.SetBgMusicVolume (0.5f);
	}

	public void ButtonClicked()
	{
		int value = PlayerPrefs.GetInt (ButtonType);

		if (value == 1) {
			PlayerPrefs.SetInt (ButtonType, 2);
			} else if (value == 2) {
			PlayerPrefs.SetInt (ButtonType, 1);
		}
		Debug.Log (ButtonType + " value : " + PlayerPrefs.GetInt (ButtonType));
		SetButton ();
	}
}
