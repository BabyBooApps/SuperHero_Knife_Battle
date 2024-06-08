using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBag : MonoBehaviour {

	SpriteRenderer ObjectSprite;
	ParticleSystem Particles;
	CircleCollider2D col;
	StatsManager StatsMgr;

	// Use this for initialization
	void Start () 
	{
		ObjectSprite = GetComponent<SpriteRenderer> ();
		Particles = GetComponentInChildren<ParticleSystem> ();
		col = GetComponent<CircleCollider2D> ();
		StatsMgr = FindObjectOfType (typeof(StatsManager)) as StatsManager;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void CoinBagShooted()
	{
		Debug.Log ("You Have Hitted The CoinBag");
		col.enabled = false;
		ObjectSprite.enabled = false;
		PlayCoinParticles ();

		int coins = StatsMgr.GetCoins ();
		coins += 10;
		StatsMgr.SetCoins (coins);
	}



	void PlayCoinParticles()
	{
		Particles.Play ();
	}
}
