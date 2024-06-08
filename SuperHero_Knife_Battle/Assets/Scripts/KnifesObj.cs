using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifesObj : MonoBehaviour {

	List<GameObject> KnifesList;
	public int MinCount;
	public int MaxCount;
	LevelTest Level;

	// Use this for initialization
	void Start () 
	{
		Level = GetComponentInParent<LevelTest> ();
		//GetMoneyBags ();
	}

	// Update is called once per frame
	void Update () 
	{

	}

	public void GetMoneyBags(int min,int max)
	{
		int Childs = transform.childCount;
		MinCount = min;
		MaxCount = max;

		Debug.Log ("Min : " + MinCount);
		KnifesList = new List<GameObject> ();
		for (int i = 0; i < Childs; i++) 
		{
			KnifesList.Add (this.transform.GetChild (i).gameObject);
			KnifesList [i].SetActive (false);
		}

		ShuffleList (KnifesList);

		int rand = Random.Range (MinCount, MaxCount);
		for (int j = 0; j < rand; j++) 
		{
			KnifesList [j].SetActive (true);
		}
	}

	void ShuffleList(List<GameObject> listName)
	{
		for (int i = 0; i < listName.Count; i++) {
			GameObject temp = listName[i];
			int randomIndex = Random.Range(i, listName.Count);
			listName[i] = listName[randomIndex];
			listName[randomIndex] = temp;
		}
	}

	public void DeactivateAllCoinBags()
	{
		for (int i = 0; i < KnifesList.Count; i++) 
		{
			KnifesList [i].SetActive (false);
		}
	}
}
