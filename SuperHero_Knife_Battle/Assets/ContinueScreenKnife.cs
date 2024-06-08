using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScreenKnife : MonoBehaviour {

	MainScript mainscript;

	void OnEnable()
	{
		mainscript = FindObjectOfType (typeof(MainScript)) as MainScript;
		SetKnife ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetKnife()
	{
		int count = this.transform.childCount;
		for (int i = 0; i < count; i++) {
			this.transform.GetChild (i).gameObject.SetActive (false);
		}
		this.transform.GetChild (mainscript.AvangerId).gameObject.SetActive (true);

	}
}
