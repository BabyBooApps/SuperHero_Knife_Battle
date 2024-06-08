using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleKnife : MonoBehaviour {

	public List<Sprite> SpritesList;
	MainScript mainscript;
	SpriteRenderer spriteImg;


	// Use this for initialization
	void Start () 
	{
		mainscript = FindObjectOfType (typeof(MainScript)) as MainScript;
		spriteImg = GetComponent<SpriteRenderer> ();
		SetSprites ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetSprites()
	{
		int id = mainscript.AvangerId;
		spriteImg.sprite = SpritesList [id];

	}
}
