using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{

    AudioManager Audiomgr;
    public AudioClip BubbleSound;
    public AudioClip RockingSound;

	// Use this for initialization
	void Start ()
    {
        Audiomgr = FindObjectOfType(typeof(AudioManager)) as AudioManager;
	}

    public void PlayBubbleSound()
    {
        Audiomgr.PlayEffectSound(BubbleSound);
    }

    public void PlayRockingSound()
    {
        Audiomgr.PlayEffectSound(RockingSound);
    }

    public void StopSound()
    {
        Audiomgr.EffectsAudio.Stop();
    }
	
	
}
