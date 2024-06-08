using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife_Smaple : MonoBehaviour {
	Rigidbody2D RgdBdy;

	// Use this for initialization
	void Start () {
		RgdBdy = GetComponent<Rigidbody2D> ();
		RgdBdy.AddForce (Vector3.up * 100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
