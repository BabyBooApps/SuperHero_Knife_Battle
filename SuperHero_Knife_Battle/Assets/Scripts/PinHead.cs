using UnityEngine;
using System.Collections;

public class PinHead : MonoBehaviour {
	Pin pin;
	GamePlayManager GamePlayMgr;

	// Use this for initialization
	void Start () {
		pin = this.GetComponentInParent<Pin> ();
		GamePlayMgr = FindObjectOfType (typeof(GamePlayManager)) as GamePlayManager;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D Other)
	{
		if (Other.name == this.name) {
			//pin.CollidedWithPin ();
			if (pin.gameObject == GamePlayMgr.GetPresentpin ()) {
				
				Debug.Log ("Name :" + this.name);
				GamePlayMgr.GameOver ();
			}
		}
	}
}
