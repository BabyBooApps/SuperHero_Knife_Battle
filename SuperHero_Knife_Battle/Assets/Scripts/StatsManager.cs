using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
	public int Coins;
	public int KnifeHits;

	public Text Coins_InGameText;
	public Text KnifeHit_IngameText;
	public Text Coins_Unlock;
	public Text Coins_MainScreen;

	// Use this for initialization
	void Start () {
		SetParameters ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetParameters()
	{
		Coins = PlayerPrefs.GetInt ("Coins");
		SetCoins (Coins);
		KnifeHits = 0;
		SetKnifeHits (KnifeHits);
	}

	public int GetCoins()
	{
		return Coins;
	}

	public void SetCoins(int count)
	{
		Coins = count;
		Coins_InGameText.text = Coins.ToString ();
		//Coins_MainScreen.text = Coins.ToString ();
		Coins_Unlock.text = Coins.ToString ();
		PlayerPrefs.SetInt ("Coins", Coins);
	}

	public int GetKnifeHits()
	{
		return KnifeHits;
	}
	public void SetKnifeHits(int count)
	{
		KnifeHits = count;
		KnifeHit_IngameText.text = KnifeHits.ToString ();
	}

	public void IncrementKnifeHits(int count)
	{
		KnifeHits+= count;
		KnifeHit_IngameText.text = KnifeHits.ToString ();
	}
}
