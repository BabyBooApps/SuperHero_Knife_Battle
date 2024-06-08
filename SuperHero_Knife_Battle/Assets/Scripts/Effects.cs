using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

	ParticleSystem Particles;
	AudioManager AudioMgr;
	public AudioClip WebClip;
	public AudioClip BlastClip;

	void OnEnable()
	{
		Particles = GetComponentInChildren<ParticleSystem> ();
		AudioMgr = FindObjectOfType (typeof(AudioManager)) as AudioManager;
	}

	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void BlastParticles()
	{
		Particles.Play ();
		iTween.ShakePosition (Camera.main.gameObject, new Vector3 (0.5f, 0, 0), 0.3f);
	}

	public void PlayWebClip()
	{
		if (WebClip) {
			AudioMgr.PlayEffectSound (WebClip);
		}
	}

	public void PlayBlastClip()
	{
		AudioMgr.PlayEffectSound (BlastClip);
	}
}
