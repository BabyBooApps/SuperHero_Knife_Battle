using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Circle : MonoBehaviour {
	public bool CanRotate;
	public GameObject WhiteOverlay;
	public GameObject EnemyCircle;
	float RotateSpeed;
	CoinBagObj CoinBagsScript;
	CircleRotation CircleRotateScript;
	public KnifesObj KnifeObj;
	public List<GameObject> ObstaclesList;
	public int[] Speeds;
	MainScript mainscript;
	public GameObject SpriteContainer;


	// Use this for initialization
	void Start () {

		CanRotate = true;
		WhiteOverlay.SetActive (false);
		CoinBagsScript = GetComponentInChildren<CoinBagObj> ();
		CircleRotateScript = GetComponentInChildren<CircleRotation> ();
		KnifeObj = GetComponentInChildren<KnifesObj> ();
		mainscript = FindObjectOfType (typeof(MainScript)) as MainScript;
		SetCircleSprite ();
		setActiveObstaclesList ();
		this.transform.localScale = Vector2.zero;
		iTween.ScaleTo (this.gameObject, Vector2.one, 0.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
	}

	void SetCircleSprite()
	{
		int id = mainscript.AvangerId;
		int count = SpriteContainer.transform.childCount;
		for (int i = 0; i < count; i++) {
			SpriteContainer.transform.GetChild (0).gameObject.SetActive (false);
		}
		SpriteContainer.transform.GetChild (id).gameObject.SetActive (true);
	}

	public void StopCircle()
	{
		CanRotate = false;
		CircleRotateScript.CanRotate = CanRotate;
	}

	public void DetachAllKnifes()
	{
		SpriteContainer.transform.GetChild (mainscript.AvangerId).gameObject.SetActive (false);
		transform.GetChild(0).DetachChildren ();
		CoinBagsScript.DeactivateAllCoinBags ();
		DestroyAllObstacles ();
	}

	IEnumerator AnimteEffect()
	{
		WhiteOverlay.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		WhiteOverlay.SetActive (false);
	}


	void setActiveObstaclesList()
	{
		int count = KnifeObj.transform.childCount;
		for (int i = 0; i < count; i++) 
		{
			if(KnifeObj.transform.GetChild (i).gameObject.activeInHierarchy)
			{
				ObstaclesList.Add (KnifeObj.transform.GetChild (i).gameObject);
			}
		}
	}

	void DestroyAllObstacles()
	{
		for (int i = 0; i < ObstaclesList.Count; i++) {
			Destroy (ObstaclesList [i]);
		}
	}
		

}
