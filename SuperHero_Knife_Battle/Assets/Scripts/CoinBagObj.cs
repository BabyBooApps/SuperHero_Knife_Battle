using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinBagObj : MonoBehaviour {

	List<GameObject> MoneyBagsList;
	public int MinCount;
	public int MaxCount;
	LevelTest level;

	// Use this for initialization
	void Start () 
	{
		level = GetComponentInParent<LevelTest> ();
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
		MoneyBagsList = new List<GameObject> ();
		for (int i = 0; i < Childs; i++) 
		{
			MoneyBagsList.Add (this.transform.GetChild (i).gameObject);
			MoneyBagsList [i].SetActive (false);
		}

		ShuffleList (MoneyBagsList);

		int rand = Random.Range (MinCount, MaxCount);
		for (int j = 0; j < rand; j++) 
		{
			MoneyBagsList [j].SetActive (true);
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
		for (int i = 0; i < MoneyBagsList.Count; i++) 
		{
			MoneyBagsList [i].SetActive (false);
		}
	}
}
