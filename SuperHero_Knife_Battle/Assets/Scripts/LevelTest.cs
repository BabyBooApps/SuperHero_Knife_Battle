using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelTest : MonoBehaviour {
	public List<Level> Levels = new List<Level>();
	public AnimationCurve curve;
	public float Circle_Speed;
	public float Circle_Time;
	public int LevelNum;
	public int MinMoneyBags;
	public int MaxMoneyBags;
	public int MinKnifes;
	public int MaxKnifes;
	public List<GameObject> Knifes;
	public GameObject PinPrefab;
	public GameObject PresentPin;
	public GameObject PreviousPin;
	public GameObject WhiteOverlay;
	public GameObject WinParticles;

	int PinCount;
	int score;


	PinController pinCon;
	GamePlayTest GamePlyTest;
	MainScript mainscript;
	StatsManager StatsMgr;
	Circle Circle_Script;
	EnemyHealthUI HealthScript;
	AudioManager AudioMgr;

	public Transform PresentPinPos;
	public int LevelPinCount;

	public KnifeContainer KnifesUIContainer;
	public KnifeUIContainer UiKnifes;
	public GameObject BgContainer;
	CoinBagObj CoinsBag;
	KnifesObj Knifeobj;

	public bool IsPaused;

	void Awake()
	{
		
	}


	// Use this for initialization
	void Start () 
	{
		GamePlyTest = FindObjectOfType (typeof(GamePlayTest)) as GamePlayTest;
		mainscript = GamePlyTest.gameObject.GetComponent<MainScript> ();
		StatsMgr = GamePlyTest.GetComponent<StatsManager> ();
		AudioMgr = GamePlyTest.GetComponent<AudioManager> ();
		Circle_Script = FindObjectOfType (typeof(Circle)) as Circle;
		KnifesUIContainer = GetComponentInChildren<KnifeContainer> ();
		HealthScript = FindObjectOfType (typeof(EnemyHealthUI)) as EnemyHealthUI;
		UiKnifes = FindObjectOfType (typeof(KnifeUIContainer)) as KnifeUIContainer;
		CoinsBag = GetComponentInChildren<CoinBagObj> ();
		Knifeobj = GetComponentInChildren<KnifesObj> ();
		IsPaused = false;

		GetLevelParameters ();
		KnifesUIContainer.SetKnifesUI ();
		IntiatePresentPin ();
		HealthScript.SetEnemyHealth (1);
		SetLevelBg ();
		UiKnifes.SetKnifes (LevelPinCount);
		AudioMgr.PlayLevelStart ();
		AudioMgr.SetBgMusicVolume (0.2f);

	}
	
	// Update is called once per frame
	void Update () 
	{
        //ShootPin();
	}

    public void ShootPin()
    {
        if (!IsPaused && GamePlyTest != null)
        {
            if (GamePlyTest.IsGameActive())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pinCon.SetShoot();
                }
            }
        }
    }

	public bool GetPause()
	{
		return IsPaused;
	}

	void GetLevelParameters()
	{
		int count = mainscript.PresentLevelCount-1;
		LevelPinCount = Levels [count].LevelPinCount;
		curve = Levels [count].curve;
		Circle_Speed = Levels [count].Speed;
		Circle_Time = Levels [count].time;
		MinMoneyBags = Levels [count].MinMoneyBagCount;
		MaxMoneyBags = Levels [count].MaxMoneyBagCount;
		MinKnifes = Levels [count].MinKnifeCount;
		MaxKnifes = Levels [count].MaxKnifeCount;

		CoinsBag.GetMoneyBags (MinMoneyBags,MaxMoneyBags);
		Knifeobj.GetMoneyBags (MinKnifes,MaxKnifes);
	}

	void SetLevelBg ()
	{
		int id = mainscript.AvangerId;
		int count = BgContainer.transform.childCount;
		for (int i = 0; i < count; i++) 
		{
			BgContainer.transform.GetChild (i).gameObject.SetActive (false);
		}
		BgContainer.transform.GetChild (id).gameObject.SetActive (true);
		HealthScript.SetUiSprites (id);
	}
		
	void IntiatePresentPin()
	{
		PinCount = 0;
		ActivatePresentPin ();

	}

	public void IncrementPinCount()
	{
		AudioMgr.PlayKnifeHit ();
		score++;
		StatsMgr.IncrementKnifeHits (1);
		HealthScript.SetEnemyHealth (1 - ((float)score / LevelPinCount));
		KnifesUIContainer.DeactivatePresentKnifeElement(score);
		UiKnifes.FadeOutKnife (score-1);
		if (score < LevelPinCount) {
			//mainscript.SetPinText (score);
			ActivatePresentPin ();
			} else {
			LevelSuccess ();
		}
	}

	public void DecrementPinCountAfterContinue()
	{
		score--;
		StatsMgr.IncrementKnifeHits (-1);
		HealthScript.SetEnemyHealth (1 - ((float)score / LevelPinCount));
		UiKnifes.ActivateKnife (score);
		ActivatePresentPin ();
	}
		
	void ActivatePresentPin()
	{
		PresentPin = Instantiate(PinPrefab) as GameObject;
		Knifes.Add (PresentPin);
	    pinCon = PresentPin.GetComponent<PinController> ();
		pinCon.SetValidPin (true);
		PresentPin.transform.position = PresentPinPos.position;
		PresentPin.GetComponent<CircleCollider2D> ().enabled = true;
	}
		
	public void LevelFailed()
	{
		StartCoroutine (LevelFailedProcess ());
	}

	IEnumerator LevelFailedProcess()
	{
		AudioMgr.PlayKnifeClash ();
		//Circle_Script.StopCircle ();
		AnimateGameOver ();
		IsPaused = true;
		mainscript.OnHold = true;
		yield return new WaitForSeconds (1.0f);
		//GameOver ();
		Destroy(PresentPin);
		Destroy (PreviousPin);
		AudioMgr.SetBgMusicVolume (0.5f);
		mainscript.ActivateGameContinueScreen();
	}

	public void DestroyPresentPin()
	{
		Destroy (PresentPin);
	}

	public void GameOver()
	{
		DestroyAllKnifes ();
		AudioMgr.SetBgMusicVolume (0.5f);
		//GamePlyTest.GameOver ();
		//mainscript.SetGameOver ();
	}

	public void ContinueAfterGameOver()
	{
		IsPaused = false;
		DecrementPinCountAfterContinue ();
		Debug.Log ("continue :" + this.name);
		AudioMgr.SetBgMusicVolume (0.2f);
	}

	public void LevelSuccess()
	{
		//mainscript.SetLevelCompleteText ("Completed");
		StartCoroutine (SuccessProcess ());
	}

	IEnumerator SuccessProcess()
	{
		yield return new WaitForSeconds (0.1f);
		//mainscript.SetLevelCompleteText ("Entered");
			if (GamePlyTest.gameover) {
				LevelFailed ();
			} else {
			//mainscript.SetLevelCompleteText ("To be Activated"  + mainscript.OnHold);
			Debug.Log ("OnHold :" + mainscript.OnHold);
			if (!mainscript.OnHold) {
				mainscript.SetLevelCompleteText (" Activated");
				WinParticles.SetActive (true);
				yield return new WaitForSeconds (0.6f);
				AnimateAllKnifesAfterWin ();
				yield return new WaitForSeconds (1.4f);
				//mainscript.SetLevelCompleteText ("Yessssssssss"  + mainscript.OnHold);
				DestroyAllKnifes ();
				GamePlyTest.GameSuccess ();
				mainscript.LevelSuccess ();
			}
		}
	}

	public void SetPreviousPin(GameObject Pin)
	{
		PreviousPin = Pin;
	}

	public GameObject GetPreviousPin()
	{
		return PreviousPin;
	}

	IEnumerator AnimateGameOverWhiteoverlay()
	{
		WhiteOverlay.SetActive (true);
		yield return new WaitForSeconds (0.05f);
		WhiteOverlay.SetActive (false);
	}

	public void AnimateGameOver()
	{
		StartCoroutine (AnimateGameOverWhiteoverlay ());
	}

	public void AnimateAllKnifesAfterWin()
	{
		Circle_Script.DetachAllKnifes ();
		for (int i = 0; i < Knifes.Count; i++) 
		{
			if (Knifes [i]) 
			{
				PinController knife = Knifes [i].GetComponent<PinController> ();
				knife.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 2;
				//knife.gameObject.GetComponent<Rigidbody2D> ().AddRelativeForce(Vector3.forward * 100) ;
				//knife.gameObject.GetComponent<Rigidbody2D> ().AddRelativeForce (Vector3.up * 1000);
				knife.gameObject.GetComponent<Rigidbody2D> ().AddRelativeForce (-Vector3.up * Random.Range(200,500));
				knife.HittedWithKnife = true;
			}
		}
	}

	void DestroyAllKnifes()
	{
		for (int i = 0; i < Knifes.Count; i++) 
		{
			Destroy (Knifes [i]);
		}
		Knifes.Clear ();
	}

}

[System.Serializable]
public class Level{

	public AnimationCurve curve;
	public int LevelPinCount;
	public int time ;
	public int Speed;
	public int MinKnifeCount;
	public int MaxKnifeCount;
	public int MinMoneyBagCount;
	public int MaxMoneyBagCount;
	
}
