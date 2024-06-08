using UnityEngine;
using System.Collections;

public class PinController : MonoBehaviour {
	public Transform StartPos;
	public Transform EndPos;

	public GameObject PinTail;

	public bool IsValidPin;
	public bool HittedWithKnife;
	bool CanShoot;
	RaycastHit2D[] hit;

	LevelTest Level; 

	ParticleSystem[] Particles;
	MainScript Mainscript;
	GameObject SpriteRendrerContainer;
	AudioManager AudioMgr;

	void Awake()
	{
		//GetComponent<Rigidbody2D> ().AddRelativeForce (Vector3.up * 1000);
		CanShoot = false;
		Level = FindObjectOfType (typeof(LevelTest)) as LevelTest;
		AudioMgr = FindObjectOfType (typeof(AudioManager)) as AudioManager;
		GetComponent<CircleCollider2D> ().enabled = false;
		Particles = GetComponentsInChildren<ParticleSystem> ();
		Mainscript = FindObjectOfType (typeof(MainScript)) as MainScript;
		SpriteRendrerContainer = this.transform.GetChild (0).gameObject;
		//this.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		
		//SetValidPin (false);
		HittedWithKnife = false;
		SetSprites ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (IsValidPin) {
			//Debug.Log (this.name);
			if (CanShoot) {
				ShootPin ();
			}

			Debug.DrawLine (StartPos.position, EndPos.position, Color.red);

			//hit = Physics2D.Linecast (StartPos.position, EndPos.position);
			hit = Physics2D.RaycastAll (StartPos.position, new Vector2 (0, 1),0.2f);

			foreach (RaycastHit2D r in hit) {
				if (r) {
					if (r.collider.gameObject.layer == 8) {
						//Debug.Log ("casting with :" + hit.collider.name);
						CanShoot = false;

						if (r.collider.tag == "Wood") {
							Debug.Log ("Hitted Tag :" + r.collider.tag);
							MakeThisAsChild (r.collider.transform);
							//Debug.Log ("Cool");
						} 

					}
				}
			}
		}

		if (HittedWithKnife) 
		{
			RotateKnife ();
		}
	}

	void SetSprites()
	{
		int id = Mainscript.AvangerId;
		int count = SpriteRendrerContainer.transform.childCount;
		for (int i = 0; i < count; i++) 
		{
			SpriteRendrerContainer.transform.GetChild (i).gameObject.SetActive (false);
		}

		SpriteRendrerContainer.transform.GetChild (id).gameObject.SetActive (true);
	}

	public void SetShoot()
	{
		if (IsValidPin) {
			
				CanShoot = true;
			    AudioMgr.PlaySwordSwing ();
			}
			
	}

	public void ShootPin()
	{
		float y = this.transform.position.y;
		y += 0.5f;
		this.transform.position = new Vector3 (this.transform.position.x, y, 0);
	}

	public void MakeThisAsChild(Transform target)
	{
		iTween.ShakePosition (target.gameObject, new Vector2 (0.1f, 0.1f), 0.1f);
		this.transform.parent = target.transform.GetChild(0).transform;
		//target.GetComponent<Circle> ().AnimateCircleHitEffect ();
		this.GetComponent<CircleCollider2D> ().enabled = true;
		Level.SetPreviousPin (this.gameObject);
		Level.IncrementPinCount ();
		SetValidPin (false);
		PlayParticles (0);
	}

	public void SetValidPin(bool status)
	{
		IsValidPin = status;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.tag != "Wood" && other.tag == "Pin") {
			if (this.gameObject == Level.GetPreviousPin () && other.gameObject != Level.PresentPin) {
				Debug.Log ("Other Name :" + other.name);
				HitwithKnife ();
				Level.LevelFailed ();
			}
		}

		if (other.tag == "CoinBag") 
		{
			CoinBag coinbag = other.GetComponent<CoinBag> ();
			AudioMgr.PlayCoinClip ();
			coinbag.CoinBagShooted ();
		}
	}

	public void PlayParticles(int index)
	{
		Particles[index].Play ();
	}

	public void HitwithKnife()
	{
		Debug.Log ("Knife");
		if (this.gameObject == Level.GetPreviousPin()) {
			this.transform.parent = null;
			this.GetComponent<Rigidbody2D> ().gravityScale = 5;
			HittedWithKnife = true;
			PlayParticles(1);
		}

	}

	void RotateKnife()
	{
		this.transform.Rotate (Vector3.back * 20);
	}


}
