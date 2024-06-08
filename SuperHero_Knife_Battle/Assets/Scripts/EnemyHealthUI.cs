using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour {

	public Image EnemyHealth;
	public Image AvengerHealth;
	public GameObject EnemySpriteObj;
	public GameObject AvengerSpriteObj;
	public GameObject EnemyNameObj;
	public GameObject AvengerNameObj;

	MainScript Mainscript;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SetUiSprites(int id)
	{
		int count = EnemySpriteObj.transform.childCount;
		for (int i = 0; i < count; i++) 
		{
			EnemySpriteObj.transform.GetChild (i).gameObject.SetActive (false);
			AvengerSpriteObj.transform.GetChild (i).gameObject.SetActive (false);
			EnemyNameObj.transform.GetChild (i).gameObject.SetActive (false);
			AvengerNameObj.transform.GetChild (i).gameObject.SetActive (false);
		}
		EnemySpriteObj.transform.GetChild (id).gameObject.SetActive (true);
		AvengerSpriteObj.transform.GetChild (id).gameObject.SetActive (true);
		EnemyNameObj.transform.GetChild (id).gameObject.SetActive (true);
		AvengerNameObj.transform.GetChild (id).gameObject.SetActive (true);
	}

	public void SetEnemyHealth(float Health)
	{
		EnemyHealth.fillAmount = Health;
		AvengerHealth.fillAmount = 1 - Health;
		//Debug.Log ("Health Value :" + Health);
	}

}
