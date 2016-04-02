using UnityEngine;
using System.Collections;

public class VisitorHealth : MonoBehaviour {
	public int faintThreshold = 2;
	public bool hasFainted = false;
	public float sinkSpeed = 2.5f;

	CapsuleCollider capsuleCollider;
	VisitorMeta visitorMeta;
	int timesAttacked = 0;
	Animator anim;
	bool isSinking = false;



	public float VisitorAttacked() {
		if (hasFainted) {
			return 0f;
		}
		timesAttacked++;
		if (timesAttacked == faintThreshold) {
			hasFainted = true;

			Faint();
		}
		return visitorMeta.healthReturned;
	}
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		visitorMeta = GetComponent<VisitorMeta> ();
	}

	void Faint ()
	{
		hasFainted = true;

//		capsuleCollider.isTrigger = true;

		anim.SetTrigger ("Die");

//		enemyAudio.clip = deathClip;
//		enemyAudio.Play ();
	}

	// Update is called once per frame
	void Update () {
//		if(isSinking) {
//			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
//		}
	}

	public void StartSinking ()
	{
//		GetComponent <NavMeshAgent> ().enabled = false;
//		GetComponent <Rigidbody> ().isKinematic = true;
		isSinking = true;
//		Debug.Log (scoreValue);

//		ScoreManager.score += scoreValue;
//		Debug.Log (ScoreManager.score);
		Destroy (gameObject, 2f);
	}
}
