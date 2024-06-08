using UnityEngine;
using System.Collections;

public class CircleRotation: MonoBehaviour
{
	public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
	public Vector3 initialPosition;
	public Vector3 destinationPosition;
	public float time = 1.0f;
	int[] factorArray;
	int factor;

	bool Flag = false;
	float RotSpeed;
	public float Speed = 10;
	public bool CanRotate;
	LevelTest Level;

	void Start()
	{
		Level = GetComponentInParent<LevelTest> ();
		SetPerameters ();
		CanRotate = true;
		SetRotateFactor ();
	}

	void SetPerameters()
	{
		curve = Level.curve;
		Speed = Level.Circle_Speed;
		time = Level.Circle_Time;
	}

	void Update()
	{
		if (!Level.GetPause ()) {
			if (CanRotate) {
				RotateCircle ();
			}
			if (!Flag) {
				Flag = true;
				StartCoroutine (UsingAnimationCurve (initialPosition, destinationPosition, time));

			}
		}
	}
	IEnumerator UsingAnimationCurve(Vector3 startPosition, Vector3 endPosition, float time)
	{
		float i = 0.0f;
		float rate = 1 / time;
		bool Rotate = true;
		while (Rotate)
		{
			i += Time.deltaTime * rate;
			endPosition.x += 0.1f;
			//transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(i));
			RotSpeed = curve.Evaluate (i);
			//Debug.Log (RotSpeed);
			yield return 0;
		}
		yield return 0;
	}

	void SetRotateFactor()
	{
		factorArray = new int[2];
		factorArray [0] = -1;
		factorArray [1] = 1;

		factor = factorArray [Random.Range (0, factorArray.Length)];
	}

	void RotateCircle()
	{
		this.transform.Rotate (Vector3.back * RotSpeed * Speed * factor);
	}
}
