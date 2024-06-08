using UnityEngine;
using System.Collections;

public class TextOrder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<MeshRenderer>().sortingOrder = 10; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
