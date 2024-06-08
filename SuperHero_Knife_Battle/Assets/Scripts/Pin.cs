using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pin : MonoBehaviour {
	Rigidbody2D RgdBdy;
	public GameObject ChildPin;
	GamePlayManager GamePlayMgr;
	public bool CanActivate;
	public bool IsPlaced;

	void OnEnable()
	{
		if (this.tag != "Pin") {
			ChildPin = this.transform.GetChild (0).gameObject;
			ActivatePin (false);
			CanActivate = true;
		}
		 IsPlaced = false;
	}

	// Use this for initialization
	void Start () {
		//Debug.Log (this.gameObject.layer);
		GamePlayMgr = FindObjectOfType (typeof(GamePlayManager)) as GamePlayManager;
		if (this.tag != "Pin") {
			RgdBdy = this.GetComponent<Rigidbody2D> ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 dir = Vector3.zero - transform.position;
		dir.Normalize ();
		RaycastHit2D hit = Physics2D.Raycast(transform.position,dir,100);
		
			if (!IsPlaced && !GamePlayMgr.IsPaused) {
			if (this.gameObject == GamePlayMgr.PresentPin) {
				if (hit.collider != null && hit.collider.tag == this.tag && hit.collider.gameObject.layer != 8) {
					Debug.Log ("hit :" + hit.point);
					PlacePin (hit.collider.gameObject,hit.point);
					IsPlaced = true;
				}  else if(hit.collider.name != "PinHead" && hit.collider.gameObject.layer != 8) {
					Debug.Log ("nothit :" + hit.collider.name);
					RgdBdy.velocity = Vector2.zero;
					ActivatePin (true);
					GamePlayMgr.GameOver ();
				}
			}

		}
	}

	public void ThrowPin()
	{ 
		if (!IsPlaced) {
			RgdBdy.AddForce (Vector2.up * 500);
			Debug.Log ("Throwing :" + this.transform.name);
		}
	}

	void ActivatePin(bool status)
	{
		ChildPin.SetActive (status);
	}
		
	public void CollidedWithPin()
	{
		if(!IsPlaced)
		{
			CanActivate = false;
			if (RgdBdy) {
				RgdBdy.velocity = Vector2.zero;
			}
		     Debug.Log ("Pin Name :" + this.transform.name);
		}
	}

	public void PlacePin(GameObject Circle,Vector3 Pos)
	{
		if (CanActivate) {
			//Debug.Log("yes");
			this.transform.parent = Circle.transform;
			this.transform.position = new Vector3(Pos.x,Pos.y - 0.8f,0);
			RgdBdy.velocity = Vector2.zero;
			ActivatePin (true);
			RgdBdy.isKinematic = true;
			GamePlayMgr.ChangePin ();
		 }
	}

}
