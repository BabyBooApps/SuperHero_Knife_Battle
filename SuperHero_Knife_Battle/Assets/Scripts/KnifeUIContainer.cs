using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeUIContainer : MonoBehaviour {

	public List<GameObject> Knifes;

	// Use this for initialization
	void Start () 
	{
		//SetKnifes ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetKnifes(int KnifesCount)
	{
		int count = transform.childCount;
		for (int i = 0; i < count; i++) 
		{
			GameObject temp = transform.GetChild (i).gameObject;
			Knifes.Add (temp);
			Image image = temp.GetComponent<Image> ();
			Color col = image.color;
			col.a = 1f;
			image.color = col;
			temp.SetActive (false);
		}

		for (int i = 0; i < KnifesCount; i++) 
		{
			Knifes [i].SetActive (true);
		}
	}

	public void FadeOutKnife(int count)
	{
		Image image = Knifes [count].GetComponent<Image> ();
		Color col = image.color;
		col.a = 0.3f;
		image.color = col;
	}

	public void ActivateKnife(int count)
	{
		Image image = Knifes [count].GetComponent<Image> ();
		Color col = image.color;
		col.a = 1.0f;
		image.color = col;
	}
}
