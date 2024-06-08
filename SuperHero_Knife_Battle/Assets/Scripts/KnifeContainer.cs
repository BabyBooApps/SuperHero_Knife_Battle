using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeContainer : MonoBehaviour {
	public List<GameObject> KnifesUiElements;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetKnifesUI()
	{
		int count = this.transform.childCount;
		for (int i = 0; i < count; i++) 
		{
			KnifesUiElements.Add (this.transform.GetChild (i).gameObject);
			KnifesUiElements [i].SetActive (false);
		}
	}

	public void DeactivatePresentKnifeElement(int index)
	{
//		GameObject temp = KnifesUiElements [KnifesUiElements.Count - index];
//		Color ImgColor = temp.GetComponent<Image> ().color;
//		ImgColor.a = 0.5f;
//		temp.GetComponent<Image> ().color = ImgColor;
	}
}
