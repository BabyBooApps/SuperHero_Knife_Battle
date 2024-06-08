using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource[] GameAudios;

	AudioSource KnifeAudio;
	public AudioSource EffectsAudio;
	AudioSource UiSounds;
	AudioSource Uisounds1;
	AudioSource BgMusic;

	public AudioClip KnifeHitClip;
	public AudioClip KnifeClashClip;
	public AudioClip CoinClip;
	public AudioClip ButtonClick;
	public AudioClip ScreenTransition;
	public AudioClip SwordSwing;
	public AudioClip LevelStart;
	public AudioClip BgClip;
	public AudioClip CongratsClip;

	// Use this for initialization
	void Start () 
	{
		GetAudios ();
		//PlayBgMusic ();
		SetBgMusicVolume (0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GetAudios()
	{
		GameAudios = Camera.main.GetComponents<AudioSource> ();
		KnifeAudio = GameAudios [0];
		EffectsAudio = GameAudios [1];
		UiSounds = GameAudios [2];
		Uisounds1 = GameAudios [3];
		BgMusic = GameAudios [4];

		int music = PlayerPrefs.GetInt ("Music");
		int volume = PlayerPrefs.GetInt ("Volume");
		if (music == 0) {
			music = 2;
			PlayerPrefs.SetInt ("Music", 2);
		}
		if (volume == 0) {
			volume = 2;
			PlayerPrefs.SetInt ("Volume", 2);
		}

	}

	public void PlayBgMusic()
	{
			BgMusic.clip = BgClip;
			BgMusic.loop = true;
			BgMusic.Play ();
	}

	public void SetBgMusicVolume(float volume)
	{
		if (PlayerPrefs.GetInt ("Music") == 2) {
			BgMusic.volume = volume;
		} else {
			BgMusic.volume = 0;
		}
	}

    void PlayKnifeSound(AudioClip clip)
	{
		if (PlayerPrefs.GetInt ("Volume") ==2 ) {
			KnifeAudio.clip = clip;
			KnifeAudio.loop = false;
			KnifeAudio.volume = 1.0f;
			KnifeAudio.Play ();
		}
	}

	public void PlayKnifeHit()
	{
		PlayKnifeSound (KnifeHitClip);
	}

	public void PlayKnifeClash()
	{
		PlayKnifeSound (KnifeClashClip);
	}
	public void PlayCoinClip()
	{
		PlayKnifeSound (CoinClip);
	}

	public void PlaySwordSwing()
	{
		PlayKnifeSound (SwordSwing);
	}

	public void PlayCongratsClip()
	{
		//PlayKnifeSound (CongratsClip);
		PlayEffectSound (CongratsClip);
		EffectsAudio.volume = 1.0f;
	}

	public void PlayEffectSound(AudioClip clip)
	{
		if (PlayerPrefs.GetInt ("Volume") ==2) {
			EffectsAudio.clip = clip;
			EffectsAudio.loop = false;
			EffectsAudio.volume = 0.5f;
			EffectsAudio.Play ();
		}
	}


	public void PlayUiButtonClick()
	{
		if (PlayerPrefs.GetInt ("Volume") ==2) {
			UiSounds.clip = ButtonClick;
			UiSounds.loop = false;
			//UiSounds.volume = 0.5f;
			UiSounds.Play ();
		}
	}
	public void PlayUiTransition()
	{
		if (PlayerPrefs.GetInt ("Volume") ==2) {
			Uisounds1.clip = ScreenTransition;
			Uisounds1.loop = false;
			//UiSounds.volume = 0.5f;
			Uisounds1.Play ();
		}
	}

	public void PlayLevelStart()
	{
		if (PlayerPrefs.GetInt ("Volume") ==2) {
			EffectsAudio.clip = LevelStart;
			EffectsAudio.loop = false;
			EffectsAudio.volume = 1.0f;
			EffectsAudio.Play ();
		}
	}

	public void PlayBgClips()
	{
		if (PlayerPrefs.GetInt ("Volume") ==2) {
			UiSounds.clip = ButtonClick;
			UiSounds.loop = false;
			UiSounds.volume = 0.2f;
			UiSounds.Play ();
		}
	}


}
