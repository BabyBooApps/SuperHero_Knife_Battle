using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	MainScript mainscript;
	AudioManager Audiomgr;
	public GameObject Knifes;
	public GameObject Knifes1;

	public GameObject Enemies;
	ParticleSystem Particles;



	void OnEnable()
	{
		mainscript = FindObjectOfType (typeof(MainScript)) as MainScript;
		Audiomgr = mainscript.GetComponent<AudioManager> ();
		Particles = GetComponentInChildren<ParticleSystem> ();
		SetImages ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetImages()
	{
		int a = Knifes.transform.childCount;
		for (int i = 0; i < a; i++) {
			Knifes.transform.GetChild (i).gameObject.SetActive (false);
			Knifes1.transform.GetChild (i).gameObject.SetActive (false);
		}
		int b = Enemies.transform.childCount;
		for (int j = 0; j < a; j++) {
			Enemies.transform.GetChild (j).gameObject.SetActive (false);
		}

		int c = mainscript.AvangerId;

		Knifes.transform.GetChild (c).gameObject.SetActive (true);
		Knifes1.transform.GetChild (c).gameObject.SetActive (true);
		Enemies.transform.GetChild (c).gameObject.SetActive (true);
	}

	public void PlayKnifeSwing()
	{
		Audiomgr.PlaySwordSwing ();
	}

	public void PlayKnifeClash()
	{
		Audiomgr.PlayKnifeClash ();
	}

	public void PlayCongratsClip()
	{
		Audiomgr.PlayCongratsClip ();
	}

	public void PlayKnifeParticles()
	{
		Particles.Play ();
	}
	public void ShakeCamera()
	{
		iTween.ShakePosition (this.gameObject, new Vector3 (0.1f, 0.1f,0), 0.2f);
	}
}
