using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelectionScreenScript : MonoBehaviour {
	AudioManager AudioMgr;

	void OnEnable()
	{
		StopAllCoroutines ();
		StartCoroutine (PlayBgClicks ());
	}

	// Use this for initialization
	void Start () 
	{
		AudioMgr = FindObjectOfType (typeof(AudioManager)) as AudioManager;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator PlayBgClicks()
	{
		yield return new WaitForEndOfFrame ();
		AudioMgr.PlayBgClips ();
		yield return new WaitForSeconds (2.0f);
		StartCoroutine (PlayBgClicks ());
	}
}
