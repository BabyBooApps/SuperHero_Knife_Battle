using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayTest : MonoBehaviour {

	public bool gameover;

	// Use this for initialization
	void Start () 
	{
		gameover = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	
	}

	public void GameOver()
	{
		if (!gameover) {
			Debug.Log ("GameOver");
			gameover = true;
		}
	}

	public void SetGameover(bool over)
	{
		gameover = over;
	}

	public void GameSuccess()
	{
		Debug.Log ("Success");
	}

	public bool IsGameActive()
	{
		if (!gameover)
			return true;
		else
			return false;
		return false;
	}


}
