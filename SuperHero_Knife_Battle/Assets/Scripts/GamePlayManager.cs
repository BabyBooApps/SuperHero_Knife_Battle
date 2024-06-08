using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class GamePlayManager : MonoBehaviour {
	UiManager UiMgr;

	public GameObject Container;
	public List<GameObject> Pins;
	public List<Vector2> PinsPos;
	public List<GameObject> LevelsList;

	public GameObject Pin;
	public GameObject PresentPin;
	GameObject PresentLevel;
	Circle circle;

	int PinCount = 0;
	int TargetPinCount;
	int LevelCount = 0;
	public bool IsPaused;

	public int TapCount = 0;

	void Awake()
	{
		UiMgr = FindObjectOfType (typeof(UiManager)) as UiManager;
	}

	// Use this for initialization
	void Start () 
	{
		IsPaused = false;
		UiMgr.SetScreen (UiMgr.MenuScreen);
		//SetLevel ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			TapCount++;
			ShootPin ();
		}
	}

	public void ShootPin()
	{
		if (!IsPaused) {
			if (Pins.Count > 0) {
				PresentPin = Pins [0];

				if (PresentPin) {
					PresentPin.GetComponent<Pin> ().ThrowPin ();
					//ChangePin ();
				}
			}
		}
	}

	public void ChangePin()
	{
		PinCount++;
		if (PinCount >= TargetPinCount) {
			Debug.Log ("Success Count :" + LevelCount);
			IsPaused = true;
			UiMgr.SetLevelNumText (LevelCount);
			LevelCount++;
			StartCoroutine (ActivateSuccessScreen ());

		} else {
			Pins.Remove (PresentPin);
			ExchangePos ();
		}


	}

	IEnumerator ActivateSuccessScreen()
	{
		yield return new WaitForSeconds (1.0f);
		SwitchToNextLevel ();
		UiMgr.SetScreen (UiMgr.LevelCompleteScreen);
	}

	public void SetLevel(int count)
	{
		LevelCount = count;
		Debug.Log ("Levelcount :" + LevelCount);
		IsPaused = false;
		UiMgr.SetScreen (UiMgr.IngameScreen);
		DestroyPresentLevel ();
		PresentLevel = Instantiate(LevelsList[LevelCount-1]) as GameObject;
		startLevel ();
	}

	public void DestroyPresentLevel()
	{
		if (PresentLevel) {
			Destroy (PresentLevel);
		}
	}

	public void RestartSameLevel()
	{
		Debug.Log ("Restart : " + LevelCount);
		SetLevel (LevelCount-1);
	}

	public void RestartFromPause()
	{
		SetLevel (LevelCount);
	}

	public void NextLevel()
	{
		SetLevel (LevelCount);
	}

	public void PauseGame(bool status)
	{
		IsPaused = status;
	}

	void startLevel()
	{
		TapCount = 0;
		UiMgr.SetLevelNumText (LevelCount);
		circle = FindObjectOfType (typeof(Circle)) as Circle;
		LevelManager Level = FindObjectOfType (typeof(LevelManager)) as LevelManager;
		List<GameObject> PinList = Level.Pins;
		Container Container = FindObjectOfType (typeof(Container)) as Container;
		PinCount = 0;
		Pins = PinList;
		int PinValue = Pins.Count;
		TargetPinCount = PinValue;
		GameObject temp = new GameObject ();
		PinsPos.Clear ();
		for (int i = 0; i < PinValue; i++) {
			//Debug.Log ("i :" + i);
			temp = Pins[i];
			//temp.GetComponentInChildren<TextMesh> ().text = (i + 1).ToString ();
			temp.transform.parent = Container.transform;
			temp.transform.localPosition = new Vector2 (0, i * (-0.75f));
			PinsPos.Add (temp.transform.localPosition);
		}

		Debug.Log ("pinPos :" + PinsPos.Count);
    }
		
	void SwitchToNextLevel()
	{
		if (PresentLevel) {
			Destroy (PresentLevel);
		}
		//LevelCount++;
		//SetLevel (LevelCount);
	}

	void ExchangePos()
	{
		for (int i = 0; i < Pins.Count; i++) {
			if (Pins [i]) {
				Pins [i].transform.localPosition = PinsPos [i];
			}
		}
	}

	IEnumerator ChangeScene()
	{
		circle.StopCircle ();
		IsPaused = true;
		yield return new WaitForSeconds (0.5f);
		//IsPaused = false;
		UiMgr.SetScreen (UiMgr.LevelFailedScreen);
		PresentPin.SetActive (false);
		//SetLevel (LevelCount);
	}

	public GameObject GetPresentpin()
	{
		Debug.Log ("PresentPin :" + PresentPin);
		return PresentPin;
	}

	public void GameOver()
	{ 
		if (!IsPaused) {
			IsPaused = true;
			Pin pinScript = PresentPin.GetComponent<Pin> ();
			pinScript.CollidedWithPin ();
			Debug.Log ("GameOver Count : " + LevelCount);
			UiMgr.SetLevelNumText (LevelCount);
			LevelCount = LevelCount + 1;
			StartCoroutine (ChangeScene ());
		}
	}
}
