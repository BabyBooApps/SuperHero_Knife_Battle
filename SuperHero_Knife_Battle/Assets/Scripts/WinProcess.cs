using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinProcess : MonoBehaviour {
	public List<GameObject> EffectsList;
	MainScript mainscript;

	// Use this for initialization
	void Start () 
	{
		mainscript = FindObjectOfType (typeof(MainScript)) as MainScript;
		SetEffect ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetEffect()
	{
		int id = mainscript.AvangerId;
		for (int i = 0; i < EffectsList.Count; i++) {
			EffectsList [i].SetActive (false);
		}

		EffectsList [id].SetActive (true);
	}


}
